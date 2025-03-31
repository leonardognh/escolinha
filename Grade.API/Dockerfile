FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Grade.API/Grade.API.csproj", "Grade.API/"]
COPY ["Grade.Application/Grade.Application.csproj", "Grade.Application/"]
COPY ["Grade.Domain/Grade.Domain.csproj", "Grade.Domain/"]
COPY ["Grade.Infrastructure/Grade.Infrastructure.csproj", "Grade.Infrastructure/"]
RUN dotnet restore "Grade.API/Grade.API.csproj"

COPY . .
WORKDIR "Grade.API"
RUN dotnet build "Grade.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Grade.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Grade.API.dll"]
