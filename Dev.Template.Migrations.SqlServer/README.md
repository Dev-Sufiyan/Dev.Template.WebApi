# Entity Framework Migration and Database Update 🛠️

This document provides instructions for generating EF migrations and updating the database.

## Prerequisites ⚙️

Ensure that you are in the `Dev.Template.WebApi` directory before running the commands.

### Connection String Configuration 🔗

Before running the commands, make sure to set up the connection string in your `appsettings.{environment}.json` based on your environment. Here is an example connection string configuration:

```json
{
  "ConnectionStrings": {
    "SqlServer": "Data Source={Server Name};Initial Catalog={DBName};User ID={UserId};Password={Pwd};Persist Security Info=True;TrustServerCertificate=True;"
  }
} 
```

Replace `{Server Name}`, `{DBName}`, `{UserId}`, and `{Pwd}` with your actual SQL Server details.

## Migration Commands 🚀

### Generate Migration 📝

To create a new EF migration, use the following command:

```bash
dotnet ef migrations add V1.0x0 --project ..\Dev.Template.Migrations.SqlServer
```
V1.0x0 is the name of the migration. Replace it with an appropriate name for your migration.

### Update Database 🗃️

To apply the migration to the database, use the following command:

```bash
dotnet ef database update --project ..\Dev.Template.Migrations.SqlServer
```
