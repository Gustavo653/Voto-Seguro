FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VotoSeguro.API/VotoSeguro.API.csproj", "VotoSeguro.API/"]
COPY ["VotoSeguro.DTO/VotoSeguro.DTO.csproj", "VotoSeguro.DTO/"]
COPY ["VotoSeguro.Domain/VotoSeguro.Domain.csproj", "VotoSeguro.Domain/"]
COPY ["VotoSeguro.Persistence/VotoSeguro.Persistence.csproj", "VotoSeguro.Persistence/"]
COPY ["VotoSeguro.Service/VotoSeguro.Service.csproj", "VotoSeguro.Service/"]
COPY ["VotoSeguro.DataAccess/VotoSeguro.DataAccess.csproj", "VotoSeguro.DataAccess/"]
COPY ["VotoSeguro.Infrastructure/VotoSeguro.Infrastructure.csproj", "VotoSeguro.Infrastructure/"]
COPY ["VotoSeguro.Utils/VotoSeguro.Utils.csproj", "VotoSeguro.Utils/"]
RUN dotnet restore "VotoSeguro.API/VotoSeguro.API.csproj"
COPY . .
WORKDIR "/src/VotoSeguro.API"
RUN dotnet build "VotoSeguro.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "VotoSeguro.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VotoSeguro.API.dll"]