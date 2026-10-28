# Portfolio backend

Backend voor mijn portfoliosite: een ASP.NET Core Web API met volledige CRUD voor projecten en blogposts, opgeslagen in SQL Server via EF Core.

## Overzicht

- **.NET-versie:** 10.0.112 (.NET 10)
- **API-stijl:** Minimal API
- **Codestructuur:** gelaagd, binnen één Web API-project
- **Database:** SQL Server 2022 (in Docker), benaderd via EF Core
- **Database aanmaken:** met EF Core-migraties (`dotnet ef database update`)
- **Documentatie/testen:** Swagger UI (Swashbuckle.AspNetCore)

## Codestructuur

```
PortfolioApi/
├── Program.cs         DI-registratie, Swagger en het mappen van de endpoints
├── appsettings.json   Connectionstring
├── Endpoints/         HTTP-laag: routes en resultaattypen (Minimal API)
├── Services/          Applicatielogica en mapping tussen entiteit en DTO
├── Data/              AppDbContext en Migrations (EF Core)
├── Models/            EF Core-entiteiten (Project, Blogpost)
└── Dtos/              Invoer- en uitvoer-DTO's per feature
```

Een request loopt als volgt: **Swagger UI → Endpoints → Services → AppDbContext → SQL Server**, en komt als DTO in JSON terug. Endpoints gebruiken de DbContext nooit rechtstreeks en geen enkel endpoint ontvangt of verzendt een entiteit.

## Endpoints

Voor zowel projecten (`/api/projects`) als blogposts (`/api/blogposts`):

| Methode | Route | Beschrijving |
|---|---|---|
| GET | `/api/<resource>` | Alle items |
| GET | `/api/<resource>/{id}` | Eén item (404 als het niet bestaat) |
| POST | `/api/<resource>` | Item aanmaken |
| PUT | `/api/<resource>/{id}` | Item wijzigen (404 als het niet bestaat) |
| DELETE | `/api/<resource>/{id}` | Item verwijderen (404 als het niet bestaat) |

## De applicatie starten

### Vereisten

- [.NET 10 SDK](https://dotnet.microsoft.com/download) (controleer met `dotnet --version`)
- Docker (of een andere SQL Server, zie de opmerking onder stap 2)
- De EF Core CLI-tool:

```bash
dotnet tool install --global dotnet-ef
```

Zorg dat `~/.dotnet/tools` in je `PATH` staat (Linux/macOS).

### 1. Docker installeren (Arch Linux)

Ik gebruik Arch Linux, waar SQL Server niet native draait. Daarom gebruik ik Docker.

```bash
sudo pacman -S docker
sudo systemctl enable --now docker
sudo usermod -aG docker