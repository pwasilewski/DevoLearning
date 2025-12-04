# Exercise 14 — Referential Data API (Lookup Services Backend)

## 🎯 Goal
Create a backend controller that exposes referential (lookup) data such as genders and civil states.
You will introduce shared lookup DTOs in the `Nihdi.DevoLearning.Contracts` project and expose them through a new `ReferentialController` in the Host BFF.
This removes mocked lookup data from the Presentation layer and establishes the backend as the single source of truth for reference values.

## 🧠 Context
Until now, gender and civil-state values were mocked inside the Presentation LookupServices.
To ensure consistency across layers, referential data must be centrally managed and served by the backend.
This exercise introduces the DTOs and endpoints required to deliver lookup data properly.
The Presentation layer will switch to these backend endpoints in a later exercise.

## 📚 Learn / Review Before Starting
- [ASP.NET Core Web API Controllers – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/web-api)  
- [REST: Resource Modeling Best Practices – restfulapi.net](https://restfulapi.net/resource-naming/)  
- [Model Binding in ASP.NET Core – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/mvc/models/model-binding)

------------------------------------------------

## 🧱 Exercise Steps

### ⚙️ Section 1 — Add Referential Contracts

All lookup DTOs belong in the `Nihdi.DevoLearning.Contracts` project to ensure consistent shared data structures between backend and frontend.

#### Step 1 — Create the folder structure
Create the following structure to organize all referential DTOs:

```
Nihdi.DevoLearning.Contracts/
└── Referential/
```

(The Shared folder already exists and will be reused.)

#### Step 2 — Create the LocalizedStringDto contract
In the existing Shared folder, create `LocalizedStringDto.cs`.
This DTO contains French and Dutch label values.

| Property           | Type   | Notes |
|-------------------|--------|-------|
| ValueFr           | string |  |
| ValueNl           | string |  |
| ~~LocalizedValue~~ | ~~string~~ | (Presentation-only computed property; excluded from Contracts) |

#### Step 3 — Create GenderDto
In the `Referential` folder, create `GenderDto.cs`.
This DTO represents one gender entry with identifier and localized labels.

#### Step 4 — Create CivilStateDto
In the same folder, create `CivilStateDto.cs`.
This DTO represents one civil-state entry with identifier and localized labels.

------------------------------------------------

### ⚙️ Section 2 — Implement the ReferentialController

This controller will expose gender and civil-state reference data used throughout the application.

#### Step 1 — Create the controller structure
In the `Controllers` folder, create `ReferentialController.cs`.
The controller should inherit from `ControllerBase` and use standard API attributes.

#### Step 2 — Add the genders endpoint
Add a GET endpoint returning all genders based on mocked in-memory values.

The method should:
- be annotated with `[HttpGet("genders")]`
- return a list of `GenderDto`
- use mocked values previously defined in the Presentation layer

#### Step 3 — Add the civil states endpoint
Add a GET endpoint returning all civil states using mocked in-memory values.

The method should:
- be annotated with `[HttpGet("civilstates")]`
- return a list of `CivilStateDto`
- use mocked values previously defined in the Presentation layer

------------------------------------------------

### ⚙️ Section 3 — Run and Test via NSwag

#### Step 1 — Launch the Host application
Run the `Nihdi.DevoLearning.Host.Bff` project and ensure it starts without errors.

#### Step 2 — View the endpoints in NSwag
NSwag will open automatically.
Confirm the following endpoints are visible:

- `GET /api/referential/genders`
- `GET /api/referential/civilstates`

#### Step 3 — Execute the endpoints
Run both endpoints and verify the returned data matches the structure of the DTOs created earlier.

------------------------------------------------

## 🧩 Focus Points
- Lookup DTOs now live in the Contracts project and provide a shared structure across layers.
- The backend now exposes all referential values instead of the Presentation layer.
- The `ReferentialController` follows clean and simple REST patterns.
- These endpoints prepare the frontend to stop mocking referential data entirely.

------------------------------------------------

## 🧠 Next Steps
In the next exercise, you will update the Presentation LookupServices to consume these new backend endpoints using generated API clients.

👉 Continue with *Integrating LookupServices with backend referential endpoints.*
