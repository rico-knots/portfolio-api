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
sudo usermod -aG docker $USER
```

Log daarna opnieuw in, zodat de groepswijziging actief wordt.

### 2. SQL Server starten

```bash
docker run -d --name sql \
  -e "ACCEPT_EULA=Y" \
  -e "MSSQL_SA_PASSWORD={PASSWORD}" \
  -p 1433:1433 \
  -v sqldata:/var/opt/mssql \
  --restart unless-stopped \
  mcr.microsoft.com/mssql/server:2022-latest
```

Het volume `sqldata` zorgt dat de data blijft bestaan na een herstart van de container. Na een herstart van de computer start je de container eventueel opnieuw met `docker start sql`.

> Op Windows kan ook SQL Server Express of LocalDB gebruikt worden. Pas dan de connectionstring aan, bijvoorbeeld:
> `Server=(localdb)\MSSQLLocalDB;Database=PortfolioDb;Trusted_Connection=True;TrustServerCertificate=True`

### 3. Connectionstring configureren

In `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=PortfolioDb;User Id=sa;Password={PASSWORD};TrustServerCertificate=True"
}
```

Het wachtwoord moet overeenkomen met `MSSQL_SA_PASSWORD` uit stap 2. Dit is een lokaal ontwikkelwachtwoord voor een wegwerpcontainer.

### 4. Database aanmaken

De migraties staan in `Data/Migrations`. Voer in de projectmap uit:

```bash
dotnet ef database update
```

Dit maakt de database `PortfolioDb` aan met de tabellen `Projects` en `Blogposts`.

### 5. Applicatie starten

```bash
dotnet run
```

Open daarna Swagger UI op de URL die in de console staat, aangevuld met `/swagger`, bijvoorbeeld `http://localhost:5000/swagger`. De exacte poort staat in de console-output (`Now listening on: ...`).

## Eigen wijzigingen aan het datamodel

Na het aanpassen van een entiteit maak je een nieuwe migratie en pas je die toe:

```bash
dotnet ef migrations add <Naam> -o Data/Migrations
dotnet ef database update
```

## Gebruikte packages

- `Swashbuckle.AspNetCore`
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Design`