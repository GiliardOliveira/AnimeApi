# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar csproj (cache do restore)
COPY AnimesApi/AnimesApi.csproj ./AnimesApi/
COPY Animes.Application/Animes.Application.csproj ./Animes.Application/
COPY Animes.Domain/Animes.Domain.csproj ./Animes.Domain/
COPY Animes.Infra/Animes.Infra.csproj ./Animes.Infra/
COPY Animes.Infrastructure/Animes.Infrastructure.csproj ./Animes.Infrastructure/

RUN dotnet restore AnimesApi/AnimesApi.csproj

# Copiar o restante do código
COPY . .

# Publicar
WORKDIR /src/AnimesApi
RUN dotnet publish AnimesApi.csproj -c Release -o /app/publish /p:UseAppHost=false /p:TrimUnusedDependencies=true

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar artefatos publicados
COPY --from=build /app/publish ./

# Variáveis
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_USE_POLLING_FILE_WATCHER=1
ENV ASPNETCORE_URLS=http://0.0.0.0:80

EXPOSE 80

ENTRYPOINT ["dotnet", "AnimesApi.dll"]
