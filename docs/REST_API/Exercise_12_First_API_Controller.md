# Exercise 12 — First API Controller (PersonController)

## 🎯 Goal
Implement the first backend controller of the application in the `Nihdi.DevoLearning.Host.Bff` project.
You will expose a **read-only** Person Details endpoint using DTOs defined in the `Nihdi.DevoLearning.Contracts` project.
This marks the beginning of your API layer; the list (Overview) and create/update endpoints will be introduced in later exercises.
You will also launch the backend and explore its contract through NSwag before integrating it with the Blazor frontend.

## 🧠 Context
Up to now, person-related data has lived as mocked, in-memory objects inside the Blazor Presentation layer.

In a real application, the UI does not own or mock domain data; it must retrieve it from a backend API. This exercise marks the beginning of the backend phase by moving the existing mocked **details** data into the Host BFF, exposing a read-only endpoint, and preparing the system so that future exercises can expand the API with list, create, and update operations. Later exercises will also replace the mock with Application-layer logic and eventually a real database.

You will also run the backend and explore its contract through NSwag, which has already been configured in the project.

## 📚 Learn / Review Before Starting
[ASP.NET Core Controllers – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/actions)
[REST API Design Tutorial – restfulapi.net](https://restfulapi.net/rest-api-design-tutorial-with-example/)
[NSwag Documentation – NSwag GitHub Wiki](https://github.com/RicoSuter/NSwag/wiki)

------------------------------------------------

## 🧱 Exercise Steps

### ⚙️ Section 1 — Create the Person Details Contract

Before implementing the API endpoint, we first define the DTO that will be returned by the backend.
All API contracts live in the `Nihdi.DevoLearning.Contracts` project and form the shared schema between backend and frontend.

#### Step 1 — Create the structure for this exercise
The contracts for this feature will be organized under the `Persons/Details` folder in the `Nihdi.DevoLearning.Contracts` project.

```
Nihdi.DevoLearning.Contracts/
└── Persons/
    └── Details/
```

#### Step 2 — Create the PersonDetailsDto contract
In the `Persons/Details` folder, create `PersonDetailsDto.cs`.

This DTO contains the fields used for displaying person details.

| Property     | Type      |
|--------------|-----------|
| Id           | int       |
| LastName     | string    |
| FirstName    | string    |
| GenderId     | int       |
| CivilStateId | int       |
| BirthDate    | DateTime? |
| DeceasedDate | DateTime? |
| Email        | string    |
| Mobile       | string    |
| CreatedOn    | DateTime  |
| CreatedBy    | string    |
| ModifiedOn   | DateTime  |
| ModifiedBy   | string    |

------------------------------------------------

### ⚙️ Section 2 — Implement the Person Details Controller

You will now create the first API controller in the `Nihdi.DevoLearning.Host.Bff` project.
Follow the controller structure shown here:
https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio#examine-the-get-methods

#### Step 1 — Create the PersonController
In the `Controllers` folder, create `PersonController.cs`.
Make the controller inherit from `ControllerBase`.

#### Step 2 — Add controller attributes
Add the following attributes on the controller:

```
[ApiController]
[Route("api/[controller]")]
```

#### Step 3 — Define the Details endpoint
Create the GET endpoint with attribute and method signature:

```
[HttpGet("{id}")]
public IActionResult GetPersonDetailsById(int id)
```

#### Step 4 — Return mocked details data
Implement `GetPersonDetailsById` that implements it similar to this pseudocode:

```
GetPersonByIdAsync(id):
    result = new()
    {
        Id = id,
        LastName = "Vermeer",
        FirstName = "Alice",
        BirthDate = new DateTime(1992, 4, 10),
        DeceasedDate = null,
        GenderId = 2,
        CivilStateId = 1,
        Email = "alice.vermeer@example.com",
        Mobile = "+32 475 11 22 33",
        CreatedOn = new DateTime(2020, 1, 15, 10, 30, 0),
        CreatedBy = "Migration",
        ModifiedOn = new DateTime(2020, 1, 15, 10, 30, 0),
        ModifiedBy = "Migration",
    };
    return Ok(result);
```

------------------------------------------------

### ⚙️ Section 3 — Run the Backend and Explore the API

#### Step 1 — Start the Host application
Run the `Nihdi.DevoLearning.Host.Bff` project.
Ensure the API starts without errors.

#### Step 2 — Open the Swagger / NSwag UI
The NSwag UI opens automatically when the application starts.

#### Step 3 — Authorize the API
Click the **Authorize** button in NSwag.
Confirm authorization by clicking **Authorize** again.

#### Step 4 — Test the Details endpoint
Execute the endpoint with sample values such as `1`, `5`, or `10`.
Verify the response structure matches the `PersonDetailsDto`.

------------------------------------------------

## 🧩 Focus Points
- The first backend controller is now implemented.
- The API exposes a real Person Details endpoint with consistent DTOs.
- Mocked data has been moved from the Presentation layer to the Host BFF.
- The backend is now accessible and testable via Swagger / NSwag.

------------------------------------------------

## 🧠 Next Steps
In the next exercise, you will continue building the PersonController by adding the **Person Overview** endpoint and preparing additional data for integration.
👉 Continue with *Overview endpoint and lookup services.*
