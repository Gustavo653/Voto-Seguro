FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ItsCheck.API/ItsCheck.API.csproj", "ItsCheck.API/"]
COPY ["ItsCheck.DTO/ItsCheck.DTO.csproj", "ItsCheck.DTO/"]
COPY ["ItsCheck.Domain/ItsCheck.Domain.csproj", "ItsCheck.Domain/"]
COPY ["ItsCheck.Persistence/ItsCheck.Persistence.csproj", "ItsCheck.Persistence/"]
COPY ["ItsCheck.Service/ItsCheck.Service.csproj", "ItsCheck.Service/"]
COPY ["ItsCheck.DataAccess/ItsCheck.DataAccess.csproj", "ItsCheck.DataAccess/"]
COPY ["ItsCheck.Infrastructure/ItsCheck.Infrastructure.csproj", "ItsCheck.Infrastructure/"]
COPY ["ItsCheck.Utils/ItsCheck.Utils.csproj", "ItsCheck.Utils/"]
RUN dotnet restore "ItsCheck.API/ItsCheck.API.csproj"
COPY . .
WORKDIR "/src/ItsCheck.API"
RUN dotnet build "ItsCheck.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ItsCheck.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ItsCheck.API.dll"]