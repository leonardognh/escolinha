FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Turmas.API/Turmas.API.csproj", "Turmas.API/"]
COPY ["Turmas.Application/Turmas.Application.csproj", "Turmas.Application/"]
COPY ["Turmas.Domain/Turmas.Domain.csproj", "Turmas.Domain/"]
COPY ["Turmas.Infrastructure/Turmas.Infrastructure.csproj", "Turmas.Infrastructure/"]
RUN dotnet restore "Turmas.API/Turmas.API.csproj"

COPY . .
WORKDIR "Turmas.API"
RUN dotnet build "Turmas.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Turmas.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Turmas.API.dll"]
