# Use ASP.NET runtime for the final container
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Use SDK for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy client project into container
COPY src/BlazorApp/BlazorApp.Client/ ./BlazorApp.Client/

# Set working directory to the client project folder
WORKDIR /src/BlazorApp.Client

# Publish the client
RUN dotnet publish BlazorApp.Client.csproj -c Release -o /app

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "BlazorApp.Client.dll"]
