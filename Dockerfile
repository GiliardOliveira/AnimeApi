# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar apenas csproj e restaurar dependências (caching eficiente)
COPY ["AnimesApi/AnimesApi.csproj", "AnimesApi/"]
RUN dotnet restore "AnimesApi/AnimesApi.csproj"

# Copiar todo o código e publicar
COPY . .
WORKDIR "/src/AnimesApi"
RUN dotnet publish "AnimesApi.csproj" -c Release -o /app/publish /p:UseAppHost=false /p:TrimUnusedDependencies=true

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar arquivos publicados
COPY --from=build /app/publish ./

# Definir usuário (opcional, mas mais seguro que root)
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_USE_POLLING_FILE_WATCHER=1
EXPOSE 80

# Comando de inicialização
ENTRYPOINT ["dotnet", "AnimesApi.dll"]
