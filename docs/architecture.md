# Architecture Overview

## Structure
- **Blazor WebApp** (`src/WebApp`): Frontend client
- **ASP.NET Core API** (`src/Api`): Backend server
- **Shared Library** (`src/Shared`): Models and logic shared between frontend and backend

## Containers
- **webapp**: Blazor frontend served from ASP.NET Core
- **api**: ASP.NET Core backend API
- **db**: PostgreSQL database

## Local Development
- Use Docker Compose (`docker/docker-compose.yml`) to run webapp, api, and database together.

## Deployment
- CI/CD via GitHub Actions (`.github/workflows/ci-cd.yml`)
- Multi-environment support (dev/stage/prod)

## Next Steps
- Clone the repo
- Run `docker compose up --build` from the root directory
- Access the app at `http://localhost:8080`