# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia a solução e arquivos de projeto
COPY ApiPay/ApiPay.sln ApiPay/
COPY ApiPay/ApiPay.csproj ApiPay/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure/Infrastructure.csproj Infrastructure/
COPY Shared/Shared.csproj Shared/

# Restaura dependências
RUN dotnet restore ApiPay/ApiPay.sln

# Copia todo o conteúdo restante
COPY . ./

# Publica o projeto principal
WORKDIR /src/ApiPay
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expõe porta padrão do ASP.NET
EXPOSE 7001
ENV ASPNETCORE_URLS=http://+:7001

ENTRYPOINT ["dotnet", "ApiPay.dll"]