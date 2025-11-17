# Exercise 06 — Person Details Page

## 🎯 Goal
Build the **Person Details Page**, fetch a single person using a route parameter, improve the `PageIntro` component with backlink support, and connect navigation from the overview page.

## 🧠 Context
You will extend your feature architecture to support detail pages. This includes:
- Route parameters
- Back-navigation patterns
- ViewModel-driven state handling
- Using `IsLoading` properly to avoid null reference issues
- Mocking detail records before real API integration

---

## 🧱 Exercise Steps

### ⚙️ Section 1 — Extend the PageIntro Component

Add **two optional parameters** to `PageIntro`:

- `string BacklinkLabel`
- `string BacklinkHref`

**💡 Implementation Reference:** Check the [Design System PageIntro pattern](https://webappsa.riziv-inami.fgov.be/styleguide-mudblazor8-wfe/pattern/page-intro) for styling and layout guidance when implementing the backlink functionality.

### ⚙️ Section 2 — Prepare Folder Structure

Create a new feature folder for details:

```csharp
Features/
└── Persons/
    └── Details/
        ├── Models/
        ├── ViewModels/
        ├── Pages/
        └── ServiceClients/
```

💡 Keep the same architecture as in the Overview feature to maintain consistency and separation of concerns.

### ⚙️ Section 3 — Create PersonDetailsModel

Inside `Features/Persons/Details/Models/PersonDetailsModel.cs`, create a model mirroring the database fields:

| Property | Type |
|---------|------|
| `Id` | `int` |
| `LastName` | `string` |
| `FirstName` | `string` |
| `Gender` | `int` |
| `CivilState` | `int` |
| `BirthDate` | `DateTime?` |
| `DeceasedDate` | `DateTime?` |
| `Email` | `string` |
| `Mobile` | `string` |
| `CreatedOn` | `DateTime` |
| `CreatedBy` | `string` |
| `ModifiedOn` | `DateTime` |
| `ModifiedBy` | `string` |

---

### ⚙️ Section 4 — Mock One Detail Record

Inside the ServiceClient, add a method:

```csharp
Task<PersonDetailsModel> GetPersonByIdAsync(int id);
```

Mock a **single record**, for example:

```csharp
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
```

---

### ⚙️ Section 5 — ViewModel for Person Details

Create `IPersonDetailsViewModel`:

```csharp
public interface IPersonDetailsViewModel
{
    bool IsLoading { get; }
    PersonDetailsModel Person { get; }
    Task OnInitializedAsync(IErrorComponent errorComponent, int id);
}
```

Your implementation should follow this pseudocode:

```csharp
OnInitializedAsync(id):
  IsLoading = true
  try:
      Person = await _serviceClient.GetPersonByIdAsync(id)
  catch ex:
      _errorComponent.ProcessError(ex)
  finally:
      IsLoading = false
```

---

💡 Hint: To easily observe the `IsLoading` indicator in action, you can add a `await Task.Delay(1000);` at the start of your `GetPersonByIdAsync` method in the ServiceClient. This simulates network latency and makes the loading spinner visible during development.

### ⚙️ Section 6 — Add Route Constant

Add this to `Routing.cs`:

```csharp
public const string PersonDetails = "/Persons/{id:int}/Details";
```

---

### ⚙️ Section 7 — Create the Person Details Page

#### Step 1 – Add the Razor Page and Code-Behind
Create the following files in your Details feature folder:
- `PersonDetails.razor`
- `PersonDetails.razor.cs` (code-behind)

#### Step 2 – Set Up Routing
Apply the `Route` attribute using the new routing constant.

#### Step 3 – Add the Page Intro with Backlink
At the top of your Razor page, include the `<PageIntro>` component with backlink support to the overview page.

#### Step 4 – Implement Loading and Null State Handling
Use the following pattern to safely handle loading and null states:

```csharp
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
    <!-- render details -->
}
```

💡 This prevents null references while waiting for data.

#### Step 4 – Reproduce the Interface

Try to match the layout and sections as shown in the design below.  

(You can keep the int values for `CivilState` and `Gender`. Those will be translated in the next exercice)

#### Step 5 – Use Translations for Labels
Refer to these resource keys for multilingual support in your UI:

| Resource Key                  | Dutch                    | French                      |
|-------------------------------|--------------------------|-----------------------------|
| PersonDetails_PersonalInfo    | Persoonsgegevens         | Données personnelles        |
| PersonDetails_ContactInfo     | Contactinformatie        | Informations de contact     |
| PersonDetails_AdminInfo       | Auditgegevens            | Données d'audit             |
| Gender                        | Geslacht                 | Genre                       |
| CivilState                    | Burgerlijke staat        | État civil                  |
| DeceasedDate                  | Overlijdensdatum         | Date de décès               |
| Mobile                        | Mobiel                   | Mobile                      |
| Created                       | Aangemaakt               | Créé                        |
| Modified                      | Gewijzigd                | Modifié                     |
| BackToOverview                | Terug naar overzicht     | Retour à l'aperçu           |
| By                            | door                     | par                         |


---

### ⚙️ Section 8 — Connect Overview → Details Navigation

#### Step 1 – Inject the Navigation Service
In your overview page, inject the `INavigationService` to handle navigation programmatically.

#### Step 2 – Adapt the MudDataGrid for Row Clicks
Update your `MudDataGrid` to handle row clicks by using the `RowClick` event. In the event handler, use the injected `NavigationService` to navigate to the details page for the selected person.

💡 Hint: Set the `Hover="true"` or simply `Hover` attribute on `MudDataGrid` to highlight rows on mouse hover, making the clickable area more visually clear and improving the user experience.

---

## 🧩 Focus Points
- Safe loading patterns with `IsLoading`
- Route parameters in Blazor (`[Parameter] public int Id { get; set; }`)
- ViewModel-driven page logic
- Back-navigation UX patterns
- Linking pages cleanly through Routing constants

---

## 🧠 Next Steps  
In the next exercise, you’ll implement **services for translated values** such as Gender and CivilState.  
👉 Continue with **Translation Service Integration**.
