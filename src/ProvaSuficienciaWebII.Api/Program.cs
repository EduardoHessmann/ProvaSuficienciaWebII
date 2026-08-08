using Microsoft.EntityFrameworkCore;
using ProvaSuficienciaWebII.Application;
using ProvaSuficienciaWebII.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços ao container.

builder.Services.AddControllers();

// Configuração do Swagger (documentação e interface de testes da API).
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Contexto de banco de dados (PostgreSQL via EF Core).
// UseSnakeCaseNamingConvention: tabelas, colunas e constraints geradas no padrão snake_case do PostgreSQL.
builder.Services.AddDbContext<ContextoBancoDados>(opcoes =>
    opcoes.UseNpgsql(builder.Configuration.GetConnectionString("ConexaoPadrao"))
          .UseSnakeCaseNamingConvention());

// MediatR: registra todos os handlers do assembly Application.
builder.Services.AddMediatR(configuracao =>
    configuracao.RegisterServicesFromAssembly(typeof(MarcadorApplication).Assembly));

var app = builder.Build();

// Aplica automaticamente as migrations pendentes ao iniciar a aplicação,
// garantindo que o banco de dados esteja sempre atualizado com o código.
using (var escopo = app.Services.CreateScope())
{
    var contexto = escopo.ServiceProvider.GetRequiredService<ContextoBancoDados>();
    contexto.Database.Migrate();
}

// Configura o pipeline de requisições HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
