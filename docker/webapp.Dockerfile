FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the entire BlazorApp folder (server + client)
COPY src/BlazorApp/ ./BlazorApp

# Set WORKDIR to server project folder
WORKDIR /src/BlazorApp/BlazorApp

# Restore and publish server project (automatically builds client)
RUN dotnet restore BlazorApp.csproj
RUN dotnet publish BlazorApp.csproj -c Release -o /app

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "BlazorApp.dll"]
