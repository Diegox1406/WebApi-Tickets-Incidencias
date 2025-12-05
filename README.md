# 🎫 API REST de Gestión de Tickets

Sistema de Gestión de incidentes y tickets de soporte técnico desarrollado con ASP.NET Core Web API, Entity Framework Core y SQL Server.

## 🚀 Características

- **Autenticación JWT** - Seguridad con tokens
- **Sistema de estados** - Abierto → En Progreso → Cerrado
- **Asignación de responsables** - Gestión de equipo
- **Historial de cambios** - Auditoría completa
- **Prioridades y tipos** - Clasificación de tickets

## 🛠️ Tecnologías

- **ASP.NET Core** - Framework Web API
- **Entity Framework Core** - ORM
- **SQL Server** - Base de datos
- **JWT (JSON Web Tokens)** - Autenticación
- **BCrypt.Net** - Encriptación de contraseñas
- **Swagger** - Documentación API
- **Docker** - Dockerización

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/sql-server) (Local o Docker)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (Opcional)

## Inicio

### Opción 1: Ejecución Local

```bash
# Clonar el repositorio
git clone https://github.com/tu-usuario/webApiTickets.git
cd webApiTickets

# Restaurar dependencias
dotnet restore

# Configurar cadena de conexión en appsettings.json
# "DefaultConnection": "Server=localhost;Database=TicketsDb;Integrated Security=True;TrustServerCertificate=True"

# Crear y aplicar migraciones
dotnet ef migrations add InitialCreate
dotnet ef database update

# Ejecutar
dotnet run
```

Acceder a Swagger: https://localhost:7000/swagger

### Opción 2: Docker Compose

```bash
# Levantar todos los servicios
docker-compose up -d --build

# Aplicar migraciones
docker-compose exec api dotnet ef database update
```

Acceder a Swagger: http://localhost:5000/swagger

## Estructura del Proyecto

