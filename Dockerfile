# Fase de construção (build)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copia todos os arquivos e restaura as dependências
COPY . ./
RUN dotnet restore

# Publica a aplicação na pasta 'out'
RUN dotnet publish -c Release -o out

# Fase de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /runtime-app

# Copia os arquivos publicados da fase de build
COPY --from=build-env /app/out .

# Expor as portas que serão usadas pela aplicação
EXPOSE 8080

# Define as variáveis de ambiente para a string de conexão (pode ser configurada externamente)
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ConnectionStrings__DefaultConnection="Server=db;Database=ProjectAuth;User Id=sa;Password=Euamo1cachorro;TrustServerCertificate=True"

ENTRYPOINT ["dotnet", "AuthApplication.dll"]
