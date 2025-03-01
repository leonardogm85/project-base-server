FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS base
WORKDIR /project/
COPY ../ ./
ENV PATH "$PATH:/root/.dotnet/tools"
RUN find . -iname "bin" -o -iname "obj" | xargs rm -rf
RUN dotnet restore "./src/ProjetoBase.Service/ProjetoBase.Service.csproj"
RUN dotnet tool install --version 3.0.3 --global dotnet-ef
ENTRYPOINT dotnet ef database update \
  -p "./src/ProjetoBase.Infrastructure.CrossCutting.Identity/" \
  -s "./src/ProjetoBase.Service/" \
  -c ApplicationContext \
  && \
  dotnet ef database update \
  -p "./src/ProjetoBase.Infrastructure.Data/" \
  -s "./src/ProjetoBase.Service/" \
  -c ProjetoBaseContext
