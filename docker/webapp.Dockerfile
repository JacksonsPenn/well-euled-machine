# Blazor WebAssembly Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Below automates the installation of the StrawberryShake CLI tool

# Install StrawberryShake CLI
RUN dotnet tool install --global StrawberryShake.Tools

# Add StrawberryShake CLI to PATH
ENV PATH="${PATH}:/root/.dotnet/tools"

COPY src/WebApp/ ./

# Generate StrawberryShake client (replace URL if needed)
RUN dotnet strawberry shake init WellEuledClient --namespace WebApp.GraphQL --url http://api:5000/graphql --project .

RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "WebApp.dll"]