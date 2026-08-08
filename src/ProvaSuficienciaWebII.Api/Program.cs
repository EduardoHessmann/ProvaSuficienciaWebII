var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços ao container.

builder.Services.AddControllers();

// Configuração do Swagger (documentação e interface de testes da API).
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
