# Prova de Suficiência — Programação Web II

API REST de controle de equipamentos, desenvolvida em .NET 9 com PostgreSQL.

**Eduardo Hessmann** · FURB · 2026/2

---

## O que a API faz

Cadastro de equipamentos e seus tipos, com autenticação por token JWT. Todos os endpoints exigem autenticação, exceto o cadastro de usuário e o login.

| Recurso | Endpoints |
|---|---|
| **Autenticação** | `POST /login` |
| **Usuários** | `POST` · `GET` · `GET /{id}` · `PUT /{id}` · `DELETE /{id}` |
| **Equipamentos** | `GET` · `GET /{id}` · `POST` · `PUT /{id}` · `DELETE /{id}` |
| **Tipos de equipamento** | `GET` · `GET /{id}` · `POST` · `PUT /{id}` · `DELETE /{id}` |

URL base: `http://localhost:8080/RestAPIFurb/`

O `PUT` é parcial: apenas os campos enviados são alterados, os demais permanecem como estão.

---

## Tecnologias

- **.NET 9** com ASP.NET Core
- **PostgreSQL 18** via **Entity Framework Core** (tabelas geradas por migrations)
- **JWT** (HMAC-SHA256) para autenticação
- **Swagger** para documentação
- **MediatR** para separar os serviços dos controllers

---

## Arquitetura

Solução dividida em cinco projetos, com os modelos separados dos serviços em assemblies distintos:

```
src/
├── ProvaSuficienciaWebII.Domain/          Entidades
├── ProvaSuficienciaWebII.Contracts/       DTOs de entrada e saída
├── ProvaSuficienciaWebII.Application/     Serviços (comandos e consultas)
├── ProvaSuficienciaWebII.Infrastructure/  Banco de dados e geração de token
└── ProvaSuficienciaWebII.Api/             Controllers e configuração
```

As dependências apontam sempre para dentro: o Domain não referencia nenhum outro projeto da solução — sua única dependência externa é o pacote de anotações do Entity Framework, usado para o mapeamento das entidades.

O mapeamento fica nas próprias classes de domínio, por anotações (`[Table]`, `[Required]`, `[MaxLength]`, `[ForeignKey]`, `[Index]`), seguindo a nomenclatura padrão de banco: classe no singular, tabela no plural.

---

## Como rodar

### 1. Banco de dados

Instale o **PostgreSQL** (marcando o componente "PostgreSQL Server" no instalador) e crie o banco:

```powershell
createdb -h localhost -p 5433 -U postgres -E UTF8 prova_suficiencia
```

> A porta usada aqui é a **5433**. Se o seu PostgreSQL estiver na porta padrão (5432), ajuste tanto o comando acima quanto a connection string do passo seguinte.

Não é necessário criar tabelas: elas são geradas pelas migrations do Entity Framework.

### 2. Connection string

Crie o arquivo `src/ProvaSuficienciaWebII.Api/appsettings.Development.json` (ele não é versionado) usando o `appsettings.json` como modelo:

```json
{
  "ConnectionStrings": {
    "ConexaoPadrao": "Host=localhost;Port=5433;Database=prova_suficiencia;Username=postgres;Password=SUA_SENHA"
  }
}
```

### 3. Executar

```powershell
dotnet run --project src\ProvaSuficienciaWebII.Api --launch-profile http
```

As tabelas são criadas automaticamente na primeira execução, a partir das migrations.

Acesse: **http://localhost:8080/RestAPIFurb/swagger**

---

## Como testar no Swagger

Como todos os endpoints de equipamentos e tipos exigem autenticação, siga esta ordem:

1. **`POST /usuarios`** — crie um usuário (endpoint público)
2. **`POST /login`** — envie e-mail e senha, copie o `token` da resposta
3. Clique em **Authorize** no topo da página e cole o token
4. Agora todos os demais endpoints estão liberados
