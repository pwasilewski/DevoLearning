# Exercise 13 — Person Overview & Create API

## 🎯 Goal
Extend the backend API by completing the `PersonController` in the `Nihdi.DevoLearning.Host.Bff` project.
You will add a **Person Overview** endpoint that returns a paginated list of persons and a **Person Create** endpoint that accepts a create payload and returns the created identifier.
Both endpoints use DTOs defined in the `Nihdi.DevoLearning.Contracts` project and prepare the API for integration with the Blazor Presentation layer.

## 🧠 Context
In the previous exercise, you introduced the first API endpoint for Person Details.
The next step is to expose the **Overview** and **Create** operations so that the frontend can gradually stop using mocked data and start calling the backend for real workflows.

In this exercise, you define shared paging and overview DTOs in the Contracts project and extend the `PersonController` with:

- a `GET /api/person` endpoint that accepts a query DTO and returns a paginated result of persons  
- a `POST /api/person` endpoint that accepts a create DTO and returns the newly created person identifier  

The implementation still uses mocked data for now. In later exercises, the controller will be wired to the Application layer and, eventually, a database.

## 📚 Learn / Review Before Starting
- [Model binding in ASP.NET Core – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/mvc/models/model-binding)  
- [ASP.NET Core Web API: Binding from body, route, query – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-8.0#binding-source-parameter-inference)  
- [HTTP Methods: GET vs POST – restfulapi.net](https://restfulapi.net/http-methods/)

------------------------------------------------

## 🧱 Exercise Steps

### ⚙️ Section 1 — Add Contracts for Overview and Create

All API contracts live in the `Nihdi.DevoLearning.Contracts` project and are shared between backend and frontend.

#### Step 1 — Extend the contracts structure
Create the missing folders to support shared pagination and person overview/create contracts.

```
Nihdi.DevoLearning.Contracts/
└── Shared/
    └── Pagination/
└── Persons/
    ├── Overview/
    └── Create/
```

#### Step 2 — Create PaginatedResultDto
In the `Shared/Pagination` folder, create `PaginatedResultDto.cs`.   

This DTO represents a generic paginated result and should align with your existing pagination concept in the Presentation layer.   
Use the same properties you already use for pagination (for example: items, total count, page number, page size).

#### Step 3 — Create PersonOverviewQueryDto
In the `Persons/Overview` folder, create `PersonOverviewQueryDto.cs`.   

This DTO contains the query parameters needed to retrieve the overview list (for example: page number, page size, optional filters).   
Use the same properties as the query model used by the Presentation layer when requesting the overview.

#### Step 4 — Create PersonOverviewDto
In the `Persons/Overview` folder, create `PersonOverviewDto.cs`.   

This DTO represents a single row in the overview list.   
Reuse the same fields as the existing Person overview model in the Presentation layer (for example: Id, FirstName, LastName, BirthDate and Email).

#### Step 5 — Create PersonCreateDto
In the `Persons/Create` folder, create `PersonCreateDto.cs`.   

This DTO contains the data required to create a new person.   
Reuse the fields you already use in the Person Create form (for example: FirstName, LastName, GenderId, CivilStateId, BirthDate, DeceasedDate, Email, Mobile).

------------------------------------------------

### ⚙️ Section 2 — Implement the Person Overview endpoint

In this section, you extend the existing `PersonController` with a read-only overview endpoint.

#### Step 1 — Add the Overview action signature
Add a GET method without route parameters in `PersonController`.  
The method should:

- use the `[HttpGet]` attribute  
- accept a `PersonOverviewQueryDto` using `[FromQuery]`  
- return a `PaginatedResultDto<PersonOverviewDto>`

💡 **Hint:** REST conventions recommend passing pagination and filtering via **query parameters**, which is why the DTO is decorated with `[FromQuery]`.

#### Step 2 — Reuse the mocked overview data
Reuse the same mocked overview list you previously defined in your Presentation `PersonOverviewServiceClient`.

Create the list similar to this pseudocode structure:

```csharp
private readonly List<PersonOverviewDto> _mockData =
[
    new() { Id = 1, FirstName = "Alice", LastName = "Vermeer", BirthDate = new DateTime(1992, 4, 10), Email = "alice.vermeer@example.com" },
    new() { Id = 2, FirstName = "Benoît", LastName = "Durand", BirthDate = new DateTime(1988, 6, 21), Email = "benoit.durand@example.com" },
    new() { Id = 3, FirstName = "Clara", LastName = "Peeters", BirthDate = new DateTime(1995, 12, 5), Email = "clara.peeters@example.com" },
    new() { Id = 4, FirstName = "David", LastName = "De Smet", BirthDate = new DateTime(1990, 1, 15), Email = "david.desmet@example.com" },
    new() { Id = 5, FirstName = "Emma", LastName = "Lefèvre", BirthDate = new DateTime(1993, 8, 30), Email = "emma.lefevre@example.com" },
    new() { Id = 6, FirstName = "Felix", LastName = "Dupont", BirthDate = new DateTime(1985, 2, 13), Email = "felix.dupont@example.com" },
    new() { Id = 7, FirstName = "Greta", LastName = "Van Acker", BirthDate = new DateTime(1991, 3, 25), Email = "greta.vanacker@example.com" },
    new() { Id = 8, FirstName = "Hugo", LastName = "Lemaire", BirthDate = new DateTime(1994, 9, 1), Email = "hugo.lemaire@example.com" },
    new() { Id = 9, FirstName = "Inès", LastName = "Declercq", BirthDate = new DateTime(1997, 7, 12), Email = "ines.declercq@example.com" },
    new() { Id = 10, FirstName = "Jules", LastName = "Martens", BirthDate = new DateTime(1989, 11, 9), Email = "jules.martens@example.com" },
    new() { Id = 11, FirstName = "Karen", LastName = "De Wilde", BirthDate = new DateTime(1986, 5, 17), Email = "karen.dewilde@example.com" },
    new() { Id = 12, FirstName = "Liam", LastName = "Janssens", BirthDate = new DateTime(1998, 10, 3), Email = "liam.janssens@example.com" },
    new() { Id = 13, FirstName = "Maya", LastName = "Moreau", BirthDate = new DateTime(1992, 2, 28), Email = "maya.moreau@example.com" },
    new() { Id = 14, FirstName = "Noah", LastName = "Verhoeven", BirthDate = new DateTime(1996, 12, 19), Email = "noah.verhoeven@example.com" },
    new() { Id = 15, FirstName = "Olivia", LastName = "Lambert", BirthDate = new DateTime(1993, 4, 22), Email = "olivia.lambert@example.com" },
    new() { Id = 16, FirstName = "Pablo", LastName = "Garcia", BirthDate = new DateTime(1987, 7, 7), Email = "pablo.garcia@example.com" },
    new() { Id = 17, FirstName = "Quinn", LastName = "Dubois", BirthDate = new DateTime(1999, 1, 31), Email = "quinn.dubois@example.com" },
    new() { Id = 18, FirstName = "Rafael", LastName = "Costa", BirthDate = new DateTime(1991, 6, 11), Email = "rafael.costa@example.com" },
    new() { Id = 19, FirstName = "Sofia", LastName = "Ricci", BirthDate = new DateTime(1995, 3, 9), Email = "sofia.ricci@example.com" },
    new() { Id = 20, FirstName = "Thomas", LastName = "Nguyen", BirthDate = new DateTime(1988, 9, 27), Email = "thomas.nguyen@example.com" },
    new() { Id = 21, FirstName = "Uma", LastName = "Kovacs", BirthDate = new DateTime(1990, 12, 14), Email = "uma.kovacs@example.com" },
    new() { Id = 22, FirstName = "Victor", LastName = "Mueller", BirthDate = new DateTime(1984, 8, 5), Email = "victor.mueller@example.com" },
    new() { Id = 23, FirstName = "Wiktor", LastName = "Nowak", BirthDate = new DateTime(1996, 1, 20), Email = "wiktor.nowak@example.com" },
    new() { Id = 24, FirstName = "Xenia", LastName = "Petrova", BirthDate = new DateTime(1993, 11, 16), Email = "xenia.petrova@example.com" },
    new() { Id = 25, FirstName = "Yara", LastName = "Haddad", BirthDate = new DateTime(1997, 5, 2), Email = "yara.haddad@example.com" },
];

GetPersons(query):
    - Apply filtering (none for now)
    - Set total counts (filtered/unfiltered)
    - Slice with Skip(PageIndex * PageSize).Take(PageSize)
    - Return PaginatedResultDto<T>
```

Wrap the list into a `PaginatedResultDto<PersonOverviewDto>` and return it.

------------------------------------------------

### ⚙️ Section 3 — Implement the Person Create endpoint

This section adds a mocked create endpoint that returns the created identifier.

#### Step 1 — Add the Create action signature
Add a POST method in `PersonController`.  
The method should:

- use the `[HttpPost]` attribute  
- accept `PersonCreateDto` using `[FromBody]`  
- return an integer Id inside an `IActionResult`

💡 **Hint:** POST requests send their data inside the **request body**, which is why `[FromBody]` is used.

#### Step 2 — Mock the creation behavior
Since this is a creation operation, the client does not send an Id.  
Assign one yourself — either a fixed value or a randomly generated integer — and return it in the HTTP response.

------------------------------------------------

### ⚙️ Section 4 — Run and test the new endpoints in NSwag

#### Step 1 — Start the Host application
Run the `Nihdi.DevoLearning.Host.Bff` project and ensure the API starts without errors.

#### Step 2 — Open the Swagger / NSwag UI
Use the Swagger / NSwag UI exposed by the Host project.  
Confirm that both endpoints are available.

#### Step 3 — Test the Overview endpoint
Call `GET /api/person` with different query parameters.  
Verify that the response is a paginated overview list.

#### Step 4 — Test the Create endpoint
Call `POST /api/person` with a JSON payload matching `PersonCreateDto`.  
Verify that the response returns an integer Id.

------------------------------------------------

## 🧩 Focus Points
- Overview and Create contracts now live in the shared `Nihdi.DevoLearning.Contracts` project.  
- The `PersonController` exposes both a paginated overview endpoint and a create endpoint.  
- `[FromQuery]` and `[FromBody]` are correctly used to map inputs.  
- NSwag can be used to test and validate both endpoints.

------------------------------------------------

## 🧠 Next Steps
In the next exercise, you will introduce a dedicated `ReferentialController` that provides lookup data (gender, civil state, etc.) for the LookupServices used by the Presentation layer.  

👉 Continue with *ReferentialController and lookup endpoints.*
