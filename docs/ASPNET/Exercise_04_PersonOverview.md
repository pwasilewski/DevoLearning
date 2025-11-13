# Exercise 04 — Person Overview Page

## 🎯 Goal
Build a **Person Overview Page** displaying a paginated list of people using a data grid. This exercise introduces feature folder structuring, ViewModels, and service communication patterns — essential for scalable Blazor applications.

## 🧠 Context
You’ll create a `Persons/Overview` feature that follows a layered architecture. It separates concerns clearly between UI, state management, and service access. You’ll also define mock data to simulate backend communication and prepare for real API integration later.

---

## 📚 Learn / Review Before Starting
- [Blazor Component Architecture](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/)
- [Dependency Injection in Blazor](https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/dependency-injection)
- [Interfaces in C#](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/interfaces/)

---

## 🧱 Exercise Steps

### ⚙️ Section 1 — Prepare the Feature Folder Structure
Before writing code, create the following structure under your `Features` folder. Each subfolder has a distinct role in maintaining clean architecture.

```
Features/
└── Persons/
    └── Overview/
        ├── Components/      → Reusable UI components (buttons, filters, etc.)
        ├── Pages/           → Razor pages with a @page route (entry points)
        ├── ServiceClients/  → Responsible for frontend-to-backend communication
        ├── ViewModels/      → Manages page state and logic between UI & services
        └── Models/          → Contains models and query/filter classes
```

💡 **Why this matters:** This structure enforces separation of concerns, ensuring each layer has a single responsibility and your app remains maintainable.

---

### ⚙️ Section 2 — Shared PaginatedResult Model

Create a new file in `Shared/Models/PaginatedResult.cs` and define a **generic** type that will represent a paginated data response.

| Property | Type | Description |
|-----------|------|-------------|
| `Items` | `List<T>` | The current page of data. |
| `PageIndex` | `int` | The current page number. |
| `PageSize` | `int` | Number of items per page. |
| `FilteredResultCount` | `int` | Total number of items after filtering. |
| `UnfilteredResultCount` | `int` | Total number of items without filters. |

💡 Hint: Refer to [Microsoft Docs – Generics in C#](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics).

---

### ⚙️ Section 3 — PersonModel & PersonQuery

Inside `Features/Persons/Overview/Models/`, create two files:

#### `PersonOverviewModel.cs`
| Property | Type |
|-----------|------|
| `Id` | `int` |
| `FirstName` | `string` |
| `LastName` | `string` |
| `BirthDate` | `DateTime` |
| `Email` | `string` |

#### `PersonOverviewQuery.cs`
| Property | Type | Description |
|-----------|------|-------------|
| `PageIndex` | `int` | Current page index. |
| `PageSize` | `int` | Items per page. |

💡 Hint: This query object will evolve to include filtering (by name, email, etc.) in future exercises.

---

### ⚙️ Section 4 — Service Client Interface & Mock Implementation

In `ServiceClients/`, create `IPersonOverviewServiceClient.cs`:

```csharp
public interface IPersonOverviewServiceClient
{
    Task<PaginatedResult<PersonOverviewModel>> GetPersonsAsync(PersonOverviewQuery query);
}
```

Then create `PersonOverviewServiceClient.cs` that implements it similar to this pseudocode:

```csharp
GetPersonsAsync(query):
  - Apply filtering (none for now)
  - Set total counts (filtered/unfiltered)
  - Slice with Skip(PageIndex * PageSize).Take(PageSize)
  - Return PaginatedResult<T>
```
💡 Hint: For more on pagination, see [Pagination Pattern Overview](https://learn.microsoft.com/en-us/ef/core/querying/pagination).


💡 To simulate backend data, initialize 25 mock entries:

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
```

---

### ⚙️ Section 5 — ViewModel Interface & Implementation

Create the interface in `ViewModels/IPersonOverviewViewModel.cs`:

```csharp
public interface IPersonOverviewViewModel
{
    bool IsLoading { get; }
    PersonOverviewQuery Query { get; }
    PaginatedResult<PersonOverviewModel> PaginatedResult { get; }
    Task OnInitializedAsync(IErrorComponent errorComponent);
    Task SearchAsync();
}
```

💡 Explanation:  
The ViewModel mediates between UI and service. It handles the **page lifecycle**, manages loading states, and communicates with the `IPersonServiceClient`.

Your `PersonOverviewViewModel` class will inject `PersonOverviewServiceClient` (via constructor) and implement logic similar to this pseudocode:

```csharp
OnInitializedAsync(errorComponent):
  - Store errorComponent
  - Initialize Query (defaults)
  - await SearchAsync()

SearchAsync():
  - IsLoading = true
  - try:
      PaginatedResult = await _serviceClient.GetPersonsAsync(Query)
    catch ex:
      _errorComponent.ProcessError(ex)
    finally:
      IsLoading = false
```

---

### ⚙️ Section 6 — Create the Page and Code-Behind

Now connect everything in a Razor page that displays the list of persons.

This step introduces several Blazor concepts at once:
- Reusing shared components like `PageIntro`
- Displaying data in a `MudDataGrid`
- Using `[Inject]` for dependency injection
- Handling parameters via `[CascadingParameter]`
- Calling async methods during initialization

#### Step 1 — Create the Razor Page
Add a new Razor file named `PersonOverview.razor` under `Features/Persons/Overview/Pages`.

Start with a simple scaffold to structure the page:
```razor
@page "/Persons"

@using Nihdi.DevoLearning.Presentation.Features.Persons.Overview.Models
@using Nihdi.DevoLearning.Presentation.Resources.Features.Persons

<PageIntro Title="@PersonsResource.PersonOverview_Title">
  <MudGrid>
    <MudItem xs="12" xl="8">
      <MudText>@PersonsResource.PersonOverview_Description</MudText>
    </MudItem>
  </MudGrid>

  <!-- TODO: Add DataGrid displaying persons -->
</PageIntro>
```

💡 This uses localization keys (_`@PersonsResource.*`_) and the _`PageIntro`_ component you created earlier.

#### Step 2 — Add the DataGrid
Next, use **MudBlazor’s DataGrid** to show paginated data. We’ll connect it to the `ViewModel` in the code-behind.
```razor
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
```
💡 Documentation: [MudBlazor DataGrid Overview](https://mudblazor.com/components/datagrid#server-side-filtering,-sorting-and-pagination).

#### Step 3 — Create the Code-Behind File
Add a new file: `PersonOverview.razor.cs`.

We’ll now connect the page to the `ViewModel` and handle the data load.

Start by injecting dependencies and cascading the `Error` component:
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
}
```

💡 References:
* [Cascading Parameters in Blazor – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/cascading-values-and-parameters)
* [Dependency Injection in Blazor – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/dependency-injection?view=aspnetcore-9.0)

#### Step 4 — Implement the ServerReload Logic
Add the method that MudDataGrid will call when new data needs to load.
```csharp
private async Task<GridData<PersonOverviewModel>> ServerReload(GridState<PersonOverviewModel> state)
{
    ViewModel.Query.PageIndex = state.Page;
    ViewModel.Query.PageSize  = state.PageSize;

    await ViewModel.SearchAsync();

    var paginatedResult = ViewModel.PaginatedResult;
    return new GridData<PersonOverviewModel>
    {
        TotalItems = paginatedResult?.UnfilteredResultCount ?? 0,
        Items      = paginatedResult?.Items ?? [],
    };
}
```
💡 This pattern allows the DataGrid to handle paging on the server (or simulated via your ServiceClient).

#### Step 5 — Run and Verify
Open your app and navigate to:
- https://localhost:7259/Persons

Verify:
- The header shows localized title and description via `PageIntro`.
- The grid loads the first page of people without errors.
- The pager works (next/previous updates rows).
- Dates are formatted via `ToShortDisplayFormat()`.

---

## 🧩 Focus Points
- Proper **feature folder organization**
- Creating **ViewModel** and **ServiceClient** layers
- Handling **async lifecycle and loading states**
- Understanding **pagination structures**

---

## 🧠 Next Steps  
In the next exercise, you’ll centralize route management for your app.  
👉 Continue with [Exercise 05 - Routing Constants and Navigation Integration](./Exercise_05_Routing_Constants_and_Navigation_Integration.md)
