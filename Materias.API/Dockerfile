FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Materias.API/Materias.API.csproj", "Materias.API/"]
COPY ["Materias.Application/Materias.Application.csproj", "Materias.Application/"]
COPY ["Materias.Domain/Materias.Domain.csproj", "Materias.Domain/"]
COPY ["Materias.Infrastructure/Materias.Infrastructure.csproj", "Materias.Infrastructure/"]
RUN dotnet restore "Materias.API/Materias.API.csproj"

COPY . .
WORKDIR "Materias.API"
RUN dotnet build "Materias.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Materias.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Materias.API.dll"]
