FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Alunos.API/Alunos.API.csproj", "Alunos.API/"]
COPY ["Alunos.Application/Alunos.Application.csproj", "Alunos.Application/"]
COPY ["Alunos.Domain/Alunos.Domain.csproj", "Alunos.Domain/"]
COPY ["Alunos.Infrastructure/Alunos.Infrastructure.csproj", "Alunos.Infrastructure/"]
RUN dotnet restore "Alunos.API/Alunos.API.csproj"

COPY . .
WORKDIR "Alunos.API"
RUN dotnet build "Alunos.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Alunos.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Alunos.API.dll"]
