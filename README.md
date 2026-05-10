# intens-hr-platform
HR platform for adding and monitoring job candidates and their skills developed as a part of intens internship task

## Start Project

## Server Setup

In folder `Server/Server/` run this commands:

### Add packages
```Bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

### Instal EF CLI
```Bash 
dotnet tool install --global dotnet-ef
```

## Database setup

In root folder run this commands:

### Create docker contrainer

```Bash
docker compose up -d
```

In folder `Server/Server/` run this commands:
### Create database

```Bash
dotnet ef database update
```
