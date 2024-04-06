**Solution Structure**

```
PixelStoreSolution
│
├── PixelStore.Domain
│   ├── Entities
│   ├── ValueObjects
│   ├── Interfaces
│   └── Exceptions
│
├── PixelStore.Application
│   ├── Commands
│   ├── Queries
│   ├── DTOs
│   ├── Interfaces
│   ├── Services
│   └── Mappings (e.g., AutoMapper profiles)
│
├── PixelStore.Infrastructure
│   ├── Data
│   │   ├── Contexts (e.g., ApplicationDbContext using EF Core)
│   │   └── Repositories
│   ├── Identity
│   ├── Services (implementations of application interfaces, e.g., email service)
│   └── Migrations
│
├── PixelStore.WebApi
│   ├── Controllers
│   ├── Filters
│   ├── Models (for API-specific models, if needed)
│   └── Program.cs
│
├── PixelStore.WebApp (Blazor WebAssembly)
│   ├── Pages
│   ├── Components
│   ├── Services (for calling WebAPI)
│   └── wwwroot (static files)
│
└── PixelStore.Tests
    ├── UnitTests
    │   ├── DomainTests
    │   └── ApplicationTests
    └── IntegrationTests
        └── InfrastructureTests
```

**Key Components**
- `PixelStore.Domain`: Contains the core business logic and data models. This project should have no dependencies on 
                       other projects to keep it isolated and focused on the business rules.
- `PixelStore.Application`: This layer coordinates the application logic and is dependent on the Domain layer but not on any other project. 
                            It defines interfaces that the outer layers will implement.
- `PixelStore.Infrastructure`: Implements the interfaces defined in the Application layer, such as data persistence (repositories),
                               file storage, and third-party services. It's dependent on the Application layer but should not depend directly on the presentation layer.
- `PixelStore.WebApi`: Serves as the entry point for the frontend to communicate with the backend. It translates HTTP requests into actions performed by the Application layer.
- `PixelStore.WebApp (Blazor WebAssembly)`: The client-side web application that interacts with the user. It makes HTTP requests to the WebAPI project and presents data to the user.
- `PixelStore.Tests`: Contains all tests for the application, divided into unit tests for the Domain and Application layers and integration tests for testing the interaction between Application and Infrastructure layers.
