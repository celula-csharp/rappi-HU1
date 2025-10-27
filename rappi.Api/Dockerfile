# Etapa baserappi
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Etapa build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar proyectos
COPY ["rappi.Api/rappi.Api.csproj", "rappi.Api/"]
COPY ["rappi.Application/rappi.Application.csproj", "rappi.Application/"]
COPY ["rappi.Domain/rappi.Domain.csproj", "rappi.Domain/"]
COPY ["rappi.Infrastructure/rappi.Infrastructure.csproj", "rappi.Infrastructure/"]

# Restaurar dependencias
RUN dotnet restore "rappi.Api/rappi.Api.csproj"

# Copiar todo el código
COPY . .

# Compilar
WORKDIR "/src/rappi.Api"
RUN dotnet build "rappi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicar
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "rappi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final
FROM base AS final
WORKDIR /app
RUN apt-get update && apt-get install -y curl
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "rappi.Api.dll"]
