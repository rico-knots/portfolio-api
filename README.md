# Portfolio backend

- .NET 10
- Minimal API

## Run the program

1. Install and start docker
```bash
1. sudo pacman -S docker
sudo systemctl enable --now docker
sudo usermod -aG docker $USER
```

2. Start SQL server
```aiignore
docker run -d --name sql \
  -e "ACCEPT_EULA=Y" \
  -e "MSSQL_SA_PASSWORD={PASSWORD}" \
  -p 1433:1433 \
  -v sqldata:/var/opt/mssql \
  --restart unless-stopped \
  mcr.microsoft.com/mssql/server:2022-latest
```

3. Put this in `appsettings.json`
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=PortfolioDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True"
}
```