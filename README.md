# tag-news

Sistema ASP.NET MVC com Entity Framework (Model First) e Migrations para realização de CRUD de Noticias e Tags.

### ⚙️ Pré-requisitos

- .NET SDK instalado (versão 8.0)
- PostgreSQL 17.5
- String de conexão ajustada no `appsettings.json`

---

### 🚀 Passos para Inicializar o Banco de Dados

1. **Abra o terminal no diretório do projeto**

2. **Instale a ferramenta do Entity Framework:**

```bash
dotnet tool install --global dotnet-ef
```

3. **Crie o banco de dados no Postgresql:**

```bash
dotnet ef database update
```

---

-> Acesse o [Repositório GitHub](https://github.com/otaviofriedein/tag-news).