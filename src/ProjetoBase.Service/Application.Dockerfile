FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS publish
WORKDIR /src/
COPY ../ ./
RUN find . -iname "bin" -o -iname "obj" | xargs rm -rf
RUN dotnet restore "./src/ProjetoBase.Service/ProjetoBase.Service.csproj"
RUN dotnet build "./src/ProjetoBase.Service/ProjetoBase.Service.csproj" \
  -c Release \
  -o /app/build
RUN dotnet publish "./src/ProjetoBase.Service/ProjetoBase.Service.csproj" \
  -c Release \
  -o /app/publish \
  /p:UseAppHost=false
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app/
COPY --from=publish /app/publish ./
EXPOSE 8080
ENTRYPOINT dotnet "./ProjetoBase.Service.dll"
