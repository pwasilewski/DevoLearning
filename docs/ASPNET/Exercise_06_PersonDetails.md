# Exercise 06 — Person Details Page

## 🎯 Goal
Build the **Person Details Page**, fetch a single person using a route parameter, improve the `PageIntro` component with backlink support, and connect navigation from the overview page.

## 🧠 Context
The Person Overview page lets users browse multiple records, but real applications always pair this with a focused **details view** for inspecting a single person. In this exercise, you extend the Person feature by introducing a details page that loads a record based on a route parameter (`id`). The page uses a ViewModel to handle lifecycle events and loading state, and improves the existing `PageIntro` component by adding backlink support.  
Although the data is still mocked, the workflow—route → service client → ViewModel → page—matches a real production setup and prepares the feature for later enhancements such as lookup translations and data coming from an API.

## 📚 Learn / Review Before Starting
- [Blazor Routing – Microsoft Docs](https://learn.microsoft.com/aspnet/core/blazor/fundamentals/routing)
- [Cascading Values & Parameters – Microsoft Docs](https://learn.microsoft.com/aspnet/core/blazor/components/cascading-values-and-parameters)
- [Dependency Injection in Blazor – Microsoft Docs](https://learn.microsoft.com/aspnet/core/blazor/fundamentals/dependency-injection)
- [MudBlazor DataGrid RowClick – MudBlazor Docs](https://mudblazor.com/components/datagrid#row-click)

---

## 🧱 Exercise Steps

### ⚙️ Section 1 — Extend the PageIntro Component

#### Step 1 — Add optional backlink parameters
Update the `PageIntro` component by adding:

- `string BacklinkLabel`
- `string BacklinkHref`

💡 Check the official design system’s PageIntro pattern for layout inspiration:  
https://webappsa.riziv-inami.fgov.be/styleguide-mudblazor8-wfe/pattern/page-intro

### ⚙️ Section 2 — Prepare the Folder Structure

#### Step 1 — Create the feature folder
```
Features/
└── Persons/
    └── Details/
        ├── Models/
        ├── Pages/
        ├── ServiceClients/
        └── ViewModels/
```

### ⚙️ Section 3 — Create PersonDetailsModel

#### Step 1 — Create the model
In the `Features/Persons/Details/Models` folder, create `PersonDetailsModel.cs`.

| Property     | Type      |
|--------------|-----------|
| Id           | int       |
| LastName     | string    |
| FirstName    | string    |
| Gender       | int       |
| CivilState   | int       |
| BirthDate    | DateTime? |
| DeceasedDate | DateTime? |
| Email        | string    |
| Mobile       | string    |
| CreatedOn    | DateTime  |
| CreatedBy    | string    |
| ModifiedOn   | DateTime  |
| ModifiedBy   | string    |

### ⚙️ Section 4 — Prepare the ServiceClient

#### Step 1 — Create the interface
In the `Features/Persons/Details/ServiceClients` folder, create `IPersonDetailsServiceClient.cs`.

```csharp
public interface IPersonDetailsServiceClient
{
    Task<PersonDetailsModel> GetPersonByIdAsync(int id);
}
```

#### Step 2 — Implement the ServiceClient
In the same folder, create `PersonDetailsServiceClient.cs` that implements it similar to this pseudocode:

```
private readonly PersonDetailsModel _mock =
    new()
    {
        Id = 42,
        LastName = "Vermeer",
        FirstName = "Alice",
        BirthDate = new DateTime(1992, 4, 10),
        DeceasedDate = null,
        Gender = 2,
        CivilState = 1,
        Email = "alice.vermeer@example.com",
        Mobile = "+32 475 11 22 33",
    };

GetPersonByIdAsync(id):
    return _mock
```

💡 Add `await Task.Delay(1000);` inside the mock method to visualize the loading indicator.

### ⚙️ Section 5 — Create the Person Details ViewModel

#### Step 1 — Create the interface
In the `Features/Persons/Details/ViewModels` folder, create `IPersonDetailsViewModel.cs`:

```csharp
public interface IPersonDetailsViewModel
{
    bool IsLoading { get; }
    PersonDetailsModel Person { get; }
    Task OnInitializedAsync(IErrorComponent errorComponent, int id);
}
```

#### Step 2 — Implement the ViewModel
Create `PersonDetailsViewModel.cs` in the same folder that implements it similar to this pseudocode:

```
OnInitializedAsync(errorComponent, id):
    _errorComponent = errorComponent
    IsLoading = true
    try:
        Person = await serviceClient.GetPersonByIdAsync(id)
    catch ex:
        _errorComponent.ProcessError(ex)
    finally:
        IsLoading = false
```

💡 This ensures no null references occur while data is loading.

### ⚙️ Section 6 — Add the Route Constant

#### Step 1 — Add the routing entry
In `Routing.Persons`, add:

```csharp
public const string Details = "/Persons/{id:int}/Details";
```

### ⚙️ Section 7 — Create the Person Details Page

#### Step 1 — Add Localization Keys (PersonsResource)

| Resource Key               | Dutch                | French                    |
|----------------------------|----------------------|---------------------------|
| PersonDetails_PersonalInfo | Persoonsgegevens     | Données personnelles      |
| PersonDetails_ContactInfo  | Contactinformatie    | Informations de contact   |
| PersonDetails_AdminInfo    | Auditgegevens        | Données d’audit           |
| Gender                     | Geslacht             | Genre                     |
| CivilState                 | Burgerlijke staat    | État civil                |
| DeceasedDate               | Overlijdensdatum     | Date de décès             |
| Mobile                     | Mobiel               | Mobile                    |
| Created                    | Aangemaakt           | Créé                      |
| Modified                   | Gewijzigd            | Modifié                   |
| BackToOverview             | Terug naar overzicht | Retour à l’aperçu         |
| By                         | door                 | par                       |

#### Step 2 — Create the Person Details Page
In the `Features/Persons/Details/Pages` folder, create:

- `PersonDetails.razor`
- `PersonDetails.razor.cs`

#### Step 3 — Implement the Page Rendering Logic
Create `PersonDetails.razor` that implements it similar to this pseudocode:

```
@attribute [Route(Routing.Persons.Details)]

<PageIntro
        Title="..."
        BacklinkLabel="..."
        BacklinkHref="..." >

    @if (ViewModel.IsLoading)
    {
        <MudOverlay Visible DarkBackground>
            <MudProgressCircular Color="Color.Secondary" Indeterminate="true" />
        </MudOverlay>
    }
    else if (ViewModel.Person == null)
    {
        // Nothing to show
    }
    else
    {
        <!-- Display person fields -->
    }

</PageIntro>
```

🖼️ Example layout (expected result):  
<img width="1315" height="841" alt="image" src="https://github.com/user-attachments/assets/fd719c2b-dcc2-4aa2-a062-a7bb1bdc7806" />

### ⚙️ Section 8 — Connect Overview → Details Navigation

#### Step 1 — Inject NavigationService
Inject `INavigationService` into your overview page.

#### Step 2 — Handle row clicks
Use `RowClick` on `MudDataGrid` to navigate to the details page.

💡 Add `Hover` on the grid for a clearer UX.

---

## 🧩 Focus Points
- Route parameters for detail pages
- ViewModel-driven page logic
- Safe loading patterns (`IsLoading`)
- Back-navigation using PageIntro
- Linking Overview → Details cleanly via routing constants

---

## 🧠 Next Steps
In the next exercise, you will extend the Person Details page by adding translation support for Gender and Civil State using dedicated lookup services.  

👉 Continue with [Exercise 07 — Lookup Services](./Exercise_07_Lookup_Services.md).
