# Well-Euled Machine MVP Plan

## 🎯 Goals
- Showcase fullstack and production-grade engineering skills
- Provide a useful and extensible platform for mathematical tools
- Leverage .NET, Blazor, PostgreSQL, MartenDB, Wolverine, HotChocolate GraphQL

---

## 🧩 Core Features

### 1. **User Authentication**
- Basic registration & login
- Secure API endpoints

### 2. **Calculation Engine**
- Support for inputting math expressions (start with algebra/calculus)
- Use Math.NET Numerics and/or AngouriMath for evaluation
- Store calculation requests/results in PostgreSQL via MartenDB

### 3. **History & Persistence**
- View list of previous calculations (per user)
- Store calculation metadata, input, output, and timestamps

### 4. **GraphQL API**
- Query for calculations
- Mutation to submit new calculation
- Use HotChocolate for schema and resolver setup

### 5. **Event-Driven Workflow**
- On calculation request, publish CalculationRequested event (Wolverine)
- Handler evaluates calculation and publishes CalculationCompleted event

### 6. **Blazor Frontend**
- Modern UI to submit calculations and view results/history
- Responsive design

### 7. **Dockerized Infrastructure**
- Dockerfiles for frontend, API, and database
- Docker Compose for local development

### 8. **CI/CD Pipeline**
- GitHub Actions for build, test, and deploy

### 9. **Documentation**
- Comprehensive README
- Architecture overview
- API documentation

---

## 📦 MVP Tech Stack

- **Frontend:** Blazor WebAssembly (.NET)
- **Backend:** ASP.NET Core, HotChocolate GraphQL
- **Database:** PostgreSQL (MartenDB for persistence)
- **Eventing:** Wolverine (event-driven messaging)
- **Math Libraries:** Math.NET Numerics, AngouriMath
- **Infrastructure:** Docker, GitHub Actions

---

## 🚀 MVP Milestones

1. **Project Setup**
   - Scaffold repo structure
   - Init .NET solution & projects
   - Set up Docker & Compose

2. **Backend Implementation**
   - Set up MartenDB for calculation persistence
   - Implement Calculation model, events, handlers
   - Integrate Math.NET/AngouriMath
   - Create GraphQL schema: queries & mutations

3. **Frontend Implementation**
   - Basic Blazor app layout
   - Calculation submission form
   - Calculation history display

4. **Authentication**
   - Add user registration/login (ASP.NET Identity or similar)

5. **Event Workflow**
   - Wolverine event publishing/handling for calculations

6. **Infrastructure**
   - Dockerize services
   - Set up CI/CD

7. **Documentation**
   - Write README, architecture, API docs

---

## 📝 MVP Feature List

- [ ] User registration & login
- [ ] Submit and evaluate math calculations (algebra/calculus)
- [ ] Store and display calculation history
- [ ] Query/mutate calculations via GraphQL
- [ ] Event-driven calculation workflow
- [ ] Responsive Blazor frontend
- [ ] Dockerized backend/frontend/db
- [ ] CI pipeline for build/test
- [ ] Documentation

---

## 🌱 Next Steps

1. Scaffold .NET solution and projects
2. Set up PostgreSQL & MartenDB
3. Implement basic calculation model and event flow
4. Build minimal GraphQL API
5. Create frontend for calculation submission/history
6. Add authentication
7. Write documentation
