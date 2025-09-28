# ASP.NET Core API Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/Api/ ./Api
WORKDIR /src/Api

RUN dotnet restore Api.csproj
RUN dotnet publish Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Api.dll"]