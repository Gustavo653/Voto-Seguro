# Introdução 
Its Check API

# Como executar o projeto?
docker run -d --name postgresql --restart always -e POSTGRES_PASSWORD=sua_senha -v /var/lib/postgresql/data:/var/lib/postgresql/data -p 5432:5432 postgres:latest
docker pull gustavo1rx7/its-check
docker run -e DatabaseConnection="Host=localhost;Port=5432;Username=postgres;Password=sua_senha;Database=db-itscheck-prod;Pooling=true;" --restart always -d --name its-check-api -p 3000:8080 gustavo1rx7/its-check:latest

# Como criar uma migration?
dotnet ef migrations add Initial -p ItsCheck.Persistence -s ItsCheck.API -c ItsCheckContext --verbose
