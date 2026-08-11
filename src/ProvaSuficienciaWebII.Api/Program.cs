using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using ProvaSuficienciaWebII.Application;
using ProvaSuficienciaWebII.Domain.Entidades;
using ProvaSuficienciaWebII.Infrastructure;
using ProvaSuficienciaWebII.Infrastructure.Autenticacao;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços ao container.

builder.Services.AddControllers();

// Configuração do Swagger (documentação e interface de testes da API),
// com suporte a autenticação via token JWT pelo botão "Authorize".
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opcoes =>
{
    opcoes.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Informe somente o token JWT obtido no login (o prefixo 'Bearer' é adicionado automaticamente)."
    });

    opcoes.AddSecurityRequirement(documento => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", documento)] = new List<string>()
    });
});

// Autenticação via token JWT (Bearer).
var secaoJwt = builder.Configuration.GetSection(ConfiguracoesJwt.Secao);
builder.Services.Configure<ConfiguracoesJwt>(secaoJwt);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opcoes =>
    {
        opcoes.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = secaoJwt["Emissor"],
            ValidAudience = secaoJwt["Publico"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secaoJwt["Chave"]!))
        };
    });

builder.Services.AddAuthorization();

// Serviços de autenticação: hash de senha e geração de token JWT.
builder.Services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
builder.Services.AddScoped<IGeradorTokenJwt, GeradorTokenJwt>();

// Contexto de banco de dados (PostgreSQL via EF Core).
// UseSnakeCaseNamingConvention: tabelas, colunas e constraints geradas no padrão snake_case do PostgreSQL.
builder.Services.AddDbContext<ContextoBancoDados>(opcoes =>
    opcoes.UseNpgsql(builder.Configuration.GetConnectionString("ConexaoPadrao"))
          .UseSnakeCaseNamingConvention());

// MediatR: registra todos os handlers do assembly Application.
builder.Services.AddMediatR(configuracao =>
    configuracao.RegisterServicesFromAssembly(typeof(MarcadorApplication).Assembly));

// CORS: libera o front-end Angular (executado em outra porta) a consumir esta API.
const string politicaAngular = "PoliticaAngular";
builder.Services.AddCors(opcoes =>
{
    opcoes.AddPolicy(politicaAngular, politica =>
        politica.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod());
});

var app = builder.Build();

// Aplica automaticamente as migrations pendentes ao iniciar a aplicação,
// garantindo que o banco de dados esteja sempre atualizado com o código.
using (var escopo = app.Services.CreateScope())
{
    var contexto = escopo.ServiceProvider.GetRequiredService<ContextoBancoDados>();
    contexto.Database.Migrate();
}

app.UsePathBase("/RestAPIFurb");

// O CORS precisa vir antes do redirecionamento HTTPS e da autenticação: a requisição
// de verificação (preflight OPTIONS) não segue redirecionamentos nem envia credenciais.
app.UseCors(politicaAngular);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Em desenvolvimento o redirecionamento para HTTPS é dispensável
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
