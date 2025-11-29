# Exercise 08 — Person Creation (Feature Structure & Workflow Only)

## 🎯 Goal
Implement the Person Creation workflow by structuring a new feature, loading lookup data, managing UI state through a ViewModel, and using a mocked ServiceClient to return a generated ID. This establishes the foundation for future validation and API integrations.

## 🧠 Context
The Person module currently supports browsing (Overview) and inspecting (Details) records using localized lookup values.  
This exercise introduces the **Create** workflow, focusing on:

- creating a dedicated feature folder  
- defining a Person creation model  
- loading lookup values  
- orchestrating UI state transitions  
- mocking persistence through a ServiceClient  

Validation will be added in **Exercise 09**.

## 📚 Learn / Review Before Starting
- [MudBlazor Form Components – MudBlazor](https://mudblazor.com/components/form)
- [Form – RIZIV / INAMI Design System](https://webappsa.riziv-inami.fgov.be/styleguide-mudblazor8-wfe/pattern/form)

---

## 🧱 Exercise Steps

### ⚙️ Section 1 — Create Feature Folder Structure

```
Features/
└── Persons/
    └── Create/
        ├── Components/
        ├── Models/
        ├── Pages/
        ├── ServiceClients/
        └── ViewModels/
```

### ⚙️ Section 2 — Implement PersonCreateModel
In the `Features/Persons/Create/Models` folder, create `PersonCreateModel.cs`.

| Property      | Type      |
|---------------|-----------|
| FirstName     | string    |
| LastName      | string    |
| GenderId      | int?      |
| CivilStateId  | int?      |
| BirthDate     | DateTime? |
| DeceasedDate  | DateTime? |
| Email         | string    |
| Mobile        | string    |

💡 *Making fields nullable prevents accidental default values (like `0` for `integers` or the minimum possible `DateTime`) and ensures validation can correctly detect missing input.*

### ⚙️ Section 3 — Implement ServiceClient

#### Step 1 — Create the interface
In the `Features/Persons/Create/ServiceClients` folder, create `IPersonCreateServiceClient.cs`.

```csharp
public interface IPersonCreateServiceClient
{
    Task<int> CreateAsync(PersonCreateModel model);
}
```

#### Step 2 — Implement the ServiceClient
In the same folder, create `PersonCreateServiceClient.cs` that implements it similar to this pseudocode:

```
CreateAsync(model):
    optionally await Task.Delay(...) to simulate saving
    return a newly generated integer ID
```

### ⚙️ Section 4 — Implement ViewModel

#### Step 1 — Create the interface
In `Features/Persons/Create/ViewModels`, create `IPersonCreateViewModel.cs`.

```csharp
public interface IPersonCreateViewModel
{
    bool IsLoading { get; }
    bool IsSubmitting { get; }

    PersonCreateModel Model { get; }

    IReadOnlyList<GenderModel> Genders { get; }
    IReadOnlyList<CivilStateModel> CivilStates { get; }

    int? CreatedPersonId { get; }

    Task OnInitializedAsync(IErrorComponent errorComponent);
    Task SubmitAsync();
}
```

#### Step 2 — Implement the ViewModel
In the same folder, create `PersonCreateViewModel.cs` that implements it similar to this pseudocode:

```
OnInitializedAsync(errorComponent):
    _errorComponent = errorComponent
    IsLoading = true
    try:
        Genders = genderLookupService.GetAsync()
        CivilStates = civilStateLookupService.GetAsync()
        Model = new PersonCreateModel()
    catch ex:
        _errorComponent.ProcessError(ex)
    finally:
        IsLoading = false


SubmitAsync:
    IsSubmitting = true
    try:
        CreatedPersonId = await personCreateServiceClient.CreateAsync(Model)
    catch ex:
        _errorComponent.ProcessError(ex)
    finally:
        IsSubmitting = false
```

### ⚙️ Section 5 — Add Routing Constant
Add inside `Routing.Persons`:

```csharp
public const string Create = "/Persons/Create";
```

### ⚙️ Section 6 — Implement PersonCreate Razor Page
In the `Features/Persons/Create/Pages` folder, create `PersonCreate.razor` that implements it similar to this pseudocode:

```razor
@attribute [Route(Routing.Persons.Create)]

@if (ViewModel.IsLoading || ViewModel.IsSubmitting)
{
    show loader
}
else if (ViewModel.Model == null || ViewModel.Genders == null || ViewModel.CivilStates == null)
{
    // Nothing to show
}
else if (ViewModel.CreatedPersonId.HasValue)
{
    <PersonCreateConfirmation CreatedPersonId="ViewModel.CreatedPersonId.Value" />
}
else
{
    <PersonCreateForm
        Model="ViewModel.Model"
        Genders="ViewModel.Genders"
        CivilStates="ViewModel.CivilStates"
        OnSubmit="ViewModel.SubmitAsync" />
}
```

💡 *This page acts purely as a UI state machine.*

### ⚙️ Section 7 — Implement PersonCreateForm Component

#### Step 1 — Add Localization Keys (PersonsResource)

| Resource Key             | Dutch              | French               |
|-------------------------|--------------------|----------------------|
| PersonCreateForm_Title | Persoon aanmaken   | Créer une personne   |
| FirstName              | Voornaam           | Prénom               |
| LastName               | Achternaam         | Nom                  |

#### Step 2 — Add Localization Keys (Global)

| Resource Key | Dutch     | French   |
|--------------|-----------|----------|
| Cancel       | Annuleren | Annuler  |
| Submit       | Verzenden | Envoyer  |

💡 *These belong in the Global resource file, since they are reused in multiple features.*

#### Step 3 — Implement the component
In the `Features/Persons/Create/Components` folder, create `PersonCreateForm.razor` that implements it similar to this pseudocode:

```razor
<PageIntro Title="@PersonsResource.PersonCreateForm_Title">

  <MudGrid>
    <MudItem xs=12 xl=8>

      <MudForm>

        <MudGrid>

          MudTextField Label="@PersonsResource.FirstName" bind=Model.FirstName
          MudTextField Label="@PersonsResource.LastName" bind=Model.LastName

          MudDatePicker Label="BirthDate" bind=Model.BirthDate Editable Clearable
          MudDatePicker Label="DeceasedDate" bind=Model.DeceasedDate Editable Clearable

          MudSelect<int?> Label="Gender" bind=Model.GenderId Clearable:
             foreach gender in Genders:
                 MudSelectItem<int?> Value=gender.Id: gender.Name.LocalizedValue

          MudSelect<int?> Label="CivilState" bind=Model.CivilStateId Clearable:
             foreach state in CivilStates:
                 MudSelectItem<int?> Value=state.Id: state.Name.LocalizedValue

          MudTextField Label="Email" bind=Model.Email
          MudTextField Label="Mobile" bind=Model.Mobile

          ButtonGroup:
             MudButton Label="@Global.Cancel" OnClick=Cancel
             MudButton Label="@Global.Submit" OnClick=Submit

        </MudGrid>

      </MudForm>

    </MudItem>
  </MudGrid>

</PageIntro>
```

🖼️ **Example layout (expected result):**  
<img width="1327" height="840" alt="image" src="https://github.com/user-attachments/assets/3f0adce8-ebbc-4379-846f-12aacba7adbf" />


💡 *Refer to the NIHDI MudBlazor style guide for [form layout patterns](https://webappsa.riziv-inami.fgov.be/styleguide-mudblazor8-wfe/pattern/form).*

### ⚙️ Section 8 — Implement PersonCreateConfirmation Component

#### Step 1 — Add Localization Keys (PersonsResource)

| Resource Key                      | Dutch                      | French                       |
|----------------------------------|----------------------------|------------------------------|
| PersonCreateConfirmation_Title   | Persoon aangemaakt         | Personne créée               |
| NavigateToDetails                | Details bekijken           | Voir les détails             |
| NavigateToOverview               | Naar overzicht             | Aller à l’aperçu             |
| NavigateToForm                   | Nog een persoon aanmaken   | Créer une autre personne     |

#### Step 2 — Implement the component
In the `Features/Persons/Create/Components` folder, create `PersonCreateConfirmation.razor` that implements it similar to this pseudocode:

```razor
<PageIntro Title="@PersonsResource.PersonCreateConfirmation_Title">

  <MudGrid>
    <MudItem xs=12 xl=8>

      ButtonGroup:

        MudButton Label="@PersonsResource.NavigateToDetails" OnClick=NavigateToDetails
        MudButton Label="@PersonsResource.NavigateToOverview" OnClick=NavigateToOverview
        MudButton Label="@PersonsResource.NavigateToForm" OnClick=NavigateToForm

    </MudItem>
  </MudGrid>

</PageIntro>
```

🖼️ **Example layout (expected result):**  
<img width="1315" height="840" alt="image" src="https://github.com/user-attachments/assets/a9a263a8-beb4-44e5-955b-ee67e9988191" />


---

## 🧩 Focus Points
- Clean feature-based structure  
- Lookup-driven select fields  
- ViewModel as workflow orchestrator  
- Razor Page as a UI state switcher  
- Mock persistence for predictable development flow  

---

## 🧠 Next Steps
Next exercise introduces **FluentValidation** to implement field-level and cross-field validation.

👉 Continue with **Exercise 09 — Person Creation Validation (FluentValidation)**

