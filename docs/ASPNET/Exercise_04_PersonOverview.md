# Exercise 04 — Person Overview (Feature Structure + Pagination)

## 🎯 Goal
In this exercise, you will build the **Person Overview** feature, including its folder structure, models, ViewModel, ServiceClient, and Razor page. This establishes the end-to-end workflow you’ll reuse across all future Person features (Details, Create, Edit, Lookups) and later across other modules.

## 🧠 Context
Until now, your application contained placeholder pages without real feature structure. The Person Overview is the first dynamic feature: it retrieves data (mocked for now), loads it through a ViewModel, and displays it using pagination.  
This exercise introduces your long‑term feature pattern:  
**Models → Query → ServiceClient → ViewModel → Razor Page**

All future exercises will follow the same structure.

## 📚 Learn / Review Before Starting
- [Blazor Component Architecture – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/)
- [Dependency Injection in Blazor – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/dependency-injection)
- [Interfaces in C# – Microsoft Docs](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/interfaces/)

---

## 🧱 Exercise Steps

### ⚙️ Section 1 — Prepare the Feature Folder Structure

#### Step 1 — Understand the feature structure
All features in this training follow this pattern:

```
Features/
└── [FeatureName]/
    └── [SubFeature]/
        ├── Components/      → Smaller pieces of UI used to split pages
        ├── Models/          → Contains models and query/filter classes
        ├── Pages/           → Razor pages with @attribute [Route]
        ├── ServiceClients/  → Responsible for communicating with backend APIs
        ├── Validators/      → FluentValidation rules (if needed)
        └── ViewModels/      → Manages UI state & logic between UI and services
```

💡 This architecture ensures separation of concerns and keeps features maintainable over time.

#### Step 2 — Create the structure for this exercise
```
Features/
└── Persons/
    └── Overview/
        ├── Models/
        ├── Pages/
        ├── ServiceClients/
        └── ViewModels/
```

No `Components/` or `Validators/` yet — those will appear in future exercises.

### ⚙️ Section 2 — Create the Shared PaginatedResult Model

#### Step 1 — Add the model file  
In the `Shared/Models` folder, create `PaginatedResult.cs`.

#### Step 2 — Define the structure

| Property                | Type     | Description |
|------------------------|----------|-------------|
| Items                 | List<T>  | Current page of data |
| PageIndex             | int      | Current page index |
| PageSize              | int      | Number of items per page |
| FilteredResultCount   | int      | Total count after filters |
| UnfilteredResultCount | int      | Total unfiltered count |

💡 Useful documentation: [Generics in C# – Microsoft Docs](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics)

### ⚙️ Section 3 — Create the Overview Models

#### Step 1 — Add PersonOverviewModel  
In the `Features/Persons/Overview/Models` folder, create `PersonOverviewModel.cs`.

| Property  | Type     | Description |
|-----------|----------|-------------|
| Id        | int      | Unique identifier |
| FirstName | string   | First name |
| LastName  | string   | Last name |
| BirthDate | DateTime | Date of birth |
| Email     | string   | Email address |

💡 Keep the model minimal — it represents only data required for the overview grid.

#### Step 2 — Add PersonOverviewQuery  
In the same folder, create `PersonOverviewQuery.cs`.

| Property  | Type | Description |
|-----------|------|-------------|
| PageIndex | int  | Current page number |
| PageSize  | int  | Number of items per page |

💡 This query will later include filters.

### ⚙️ Section 4 — Implement the ServiceClient

#### Step 1 — Create the interface  
In the `Features/Persons/Overview/ServiceClients` folder, create `IPersonOverviewServiceClient.cs`.

```csharp
public interface IPersonOverviewServiceClient
{
    Task<PaginatedResult<PersonOverviewModel>> GetPersonsAsync(PersonOverviewQuery query);
}
```

#### Step 2 — Implement the ServiceClient  
In the same folder, create `PersonOverviewServiceClient.cs` that implements it similar to this pseudocode:

```csharp
private readonly List<PersonOverviewModel> _mockData =
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

GetPersonsAsync(query):
    - Apply filtering (none for now)
    - Set total counts (filtered/unfiltered)
    - Slice with Skip(PageIndex * PageSize).Take(PageSize)
    - Return PaginatedResult<T>
```

💡 Hint: For more on pagination, see: [Pagination Pattern Overview – Microsoft Docs](https://learn.microsoft.com/en-us/ef/core/querying/pagination)

### ⚙️ Section 5 — Implement the ViewModel

#### Step 1 — Create the interface  
In the `Features/Persons/Overview/ViewModels` folder, create `IPersonOverviewViewModel.cs`.

```csharp
public interface IPersonOverviewViewModel
{
    bool IsLoading { get; }

    PersonOverviewQuery Query { get; }
    PaginatedResult<PersonOverviewModel>? PaginatedResult { get; }

    Task OnInitializedAsync(IErrorComponent errorComponent);
    Task SearchAsync();
}
```

#### Step 2 — Implement the ViewModel  
In the same folder, create `PersonOverviewViewModel.cs` that implements it similar to this pseudocode:

```
OnInitializedAsync(errorComponent):
    _errorComponent = errorComponent
    Query = new PersonOverviewQuery { PageIndex = 0, PageSize = 10 }
    await SearchAsync()

SearchAsync():
    IsLoading = true
    try:
        PaginatedResult = await _serviceClient.GetPersonsAsync(Query)
    catch ex:
        _errorComponent.ProcessError(ex)
    finally:
        IsLoading = false
```

💡 The ViewModel manages UI state, loading, page queries, and error handling.

### ⚙️ Section 6 — Create the Person Overview Page

Now connect everything in a Razor page that displays the list of persons.

This step introduces several Blazor concepts at once:

- Reusing shared components like `PageIntro`
- Displaying data in a `MudDataGrid`
- Using `[Inject]` for dependency injection
- Handling parameters via `[CascadingParameter]`
- Calling async methods during initialization

#### Step 1 — Add localization keys  
In the `Resources/Features/Persons` folder, create `Persons.resx` with:

| Resource Key               | Dutch                                                                 | French                                                                 |
|----------------------------|-----------------------------------------------------------------------|------------------------------------------------------------------------|
| PersonOverview_Title       | Personenoverzicht                                                     | Aperçu des personnes                                                   |
| PersonOverview_Description | Welkom op de personenoverzichtspagina. Hier vindt u informatie over verschillende personen in ons systeem. | Bienvenue sur la page d’aperçu des personnes. Vous trouverez ici des informations sur différentes personnes dans notre système. |
| Name                       | Naam                                                                  | Nom                                                                    |
| BirthDate                  | Geboortedatum                                                        | Date de naissance                                                      |
| Email                      | E-mailadres                                                           | Adresse e-mail                                                         |

#### Step 2 — Create the Razor Page  
In the `Features/Persons/Overview/Pages` folder, create `PersonOverview.razor`:

```razor
@page "/Persons"

<PageIntro Title="@PersonsResource.PersonOverview_Title">
    <MudGrid>
        <MudItem xs="12" xl="8">
            <MudText>@PersonsResource.PersonOverview_Description</MudText>
        </MudItem>
    </MudGrid>

    <MudDataGrid T="PersonOverviewModel"
                 SortMode="SortMode.None"
                 ServerData="ServerReload">
        <Columns>
            <PropertyColumn Property="x => x.LastName" Title="@PersonsResource.Name">
                <CellTemplate>
                    @($"{context.Item.LastName} {context.Item.FirstName}")
                </CellTemplate>
            </PropertyColumn>

            <PropertyColumn Property="x => x.BirthDate" Title="@PersonsResource.BirthDate">
                <CellTemplate>
                    @context.Item.BirthDate.ToShortDisplayFormat()
                </CellTemplate>
            </PropertyColumn>

            <PropertyColumn Property="x => x.Email" Title="@PersonsResource.Email" />
        </Columns>

        <PagerContent>
            <MudDataGridPager />
        </PagerContent>
    </MudDataGrid>
</PageIntro>
```

#### Step 3 — Create the code-behind file  
In the same folder, create `PersonOverview.razor.cs`:

```csharp
public partial class PersonOverview
{
    [CascadingParameter]
    public Error ErrorComponent { get; set; } = default!;

    [Inject]
    public IPersonOverviewViewModel ViewModel { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await ViewModel.OnInitializedAsync(ErrorComponent);
    }

    private async Task<GridData<PersonOverviewModel>> ServerReload(GridState<PersonOverviewModel> state)
    {
        ViewModel.Query.PageIndex = state.Page;
        ViewModel.Query.PageSize  = state.PageSize;

        await ViewModel.SearchAsync();

        var result = ViewModel.PaginatedResult;
        return new GridData<PersonOverviewModel>
        {
            TotalItems = result?.UnfilteredResultCount ?? 0,
            Items = result?.Items ?? [],
        };
    }
}
```

💡 Useful references:  
- [Blazor Code Separation - Youtube](https://www.youtube.com/watch?v=xiy0VHUbZJI)
- [Cascading Parameters in Blazor – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/cascading-values-and-parameters)  
- [Cascading Parameters in Blazor – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/dependency-injection)  

#### Step 4 — Run and verify
Run the application and navigate to:

```
https://localhost:7259/Persons
```

Verify that:

- The header shows the localized title and description via `PageIntro`.
- The grid loads the first page of people without errors.
- The pager works (next/previous updates the rows).
- Dates are formatted using `ToShortDisplayFormat()`.

🖼️ Example layout (expected result):    
<img width="1326" height="844" alt="image" src="https://github.com/user-attachments/assets/64a69b3b-28e4-4a2f-b06c-feae1db4d6c2" />


---

## 🧩 Focus Points
- Setting up the Person Overview feature using a clean and scalable folder structure
- Creating reusable models (PaginatedResult, PersonOverviewModel, PersonOverviewQuery)
- Implementing an in-memory ServiceClient with pagination
- Using a ViewModel to orchestrate UI logic and manage loading state
- Displaying paginated data using MudBlazor’s DataGrid
- Integrating localization for all UI labels

---

## 🧠 Next Steps
In the next exercise, you’ll centralize route management for your app.  

👉 Continue with [Exercise 05 - Routing Constants and Navigation Integration](./Exercise_05_Routing_Constants_and_Navigation_Integration.md)
