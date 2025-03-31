FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Professores.API/Professores.API.csproj", "Professores.API/"]
COPY ["Professores.Application/Professores.Application.csproj", "Professores.Application/"]
COPY ["Professores.Domain/Professores.Domain.csproj", "Professores.Domain/"]
COPY ["Professores.Infrastructure/Professores.Infrastructure.csproj", "Professores.Infrastructure/"]
RUN dotnet restore "Professores.API/Professores.API.csproj"

COPY . .
WORKDIR "Professores.API"
RUN dotnet build "Professores.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Professores.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Professores.API.dll"]
