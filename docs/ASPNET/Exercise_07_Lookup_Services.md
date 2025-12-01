# Exercise 07 — Lookup Services (Gender and Civil State)

## 🎯 Goal
In this exercise, you will build a simple and reusable lookup system (using Gender as the example) to support static reference data in your application. You’ll create models, lookup service interfaces and implementations, register them in dependency injection, and finally use the service inside a Razor Page. This pattern is used all the time in real systems for things like countries, civil states, categories, and more.

## 🧠 Context
Static reference data should never live inside business logic. By introducing lookup services, you ensure:

- cleaner controllers/pages
- reusable and isolated reference data logic
- easy localization
- future scalability (switching from in-memory to database-backed lookups)

This exercise extends your existing structure to support localized reference lists. You will implement lookups for both **Gender** and **Civil State**, using `LocalizedStringModel`.

## 📚 Learn / Review Before Starting
- [ASP.NET Core Dependency Injection basics – Microsoft Docs](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-basics)
- [Computed properties in C# – Microsoft Docs](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties)

---

## 🧱 Exercise Steps

### ⚙️ Section 1 — Create Lookup Models

#### Step 1 — Create `LocalizedStringModel`
In the `Shared/Models` folder, create `LocalizedStringModel.cs`.

| Property       | Type   | Description                                |
|----------------|--------|--------------------------------------------|
| ValueFr        | string | French translation                         |
| ValueNl        | string | Dutch translation                          |
| LocalizedValue | string | Computed value based on `CurrentUICulture` |

💡 Useful documentation:  
https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties#expression-body-definitions

#### Step 2 — Create `GenderModel`
In the same folder, create `GenderModel.cs`.

| Property | Type                 |
|---------|----------------------|
| Id      | int                  |
| Name    | LocalizedStringModel |

#### Step 3 — Create `CivilStateModel`
In the same folder, create `CivilStateModel.cs`.

| Property | Type                 |
|---------|----------------------|
| Id      | int                  |
| Name    | LocalizedStringModel |

### ⚙️ Section 2 — Implement Gender Lookup Service

#### Step 1 — Create the interface
In the `Services/Genders` folder, create `IGenderLookupService.cs`.

```csharp
public interface IGenderLookupService
{
    Task<IReadOnlyList<GenderModel>> GetAsync();
}
```

#### Step 2 — Implement the service
In the same folder, create `GenderLookupService.cs` that implements it similar to this pseudocode:

```
private static readonly IReadOnlyList<GenderModel> _genders =
[
    new() { Id = 1, Name = new LocalizedStringModel { ValueFr = "Masculin", ValueNl = "Mannelijk" } },
    new() { Id = 2, Name = new LocalizedStringModel { ValueFr = "Féminin", ValueNl = "Vrouwelijk" } },
    new() { Id = 3, Name = new LocalizedStringModel { ValueFr = "X", ValueNl = "X" } },
];

GetAsync:
    return _genders
```

### ⚙️ Section 3 — Implement Civil State Lookup Service

#### Step 1 — Create the interface
In the `Services/CivilStates` folder, create `ICivilStateLookupService.cs`.

```csharp
public interface ICivilStateLookupService
{
    Task<IReadOnlyList<CivilStateModel>> GetAsync();
}
```

#### Step 2 — Implement the service
In the same folder, create `CivilStateLookupService.cs` that implements it similar to this pseudocode:

```
private static readonly IReadOnlyList<CivilStateModel> _states =
[
    new() { Id = 1, Name = new() { ValueFr = "Célibataire", ValueNl = "Ongehuwd" } },
    new() { Id = 2, Name = new() { ValueFr = "Marié", ValueNl = "Gehuwd" } },
    new() { Id = 3, Name = new() { ValueFr = "Veuf/veuve", ValueNl = "Weduwnaar/weduwe" } },
    new() { Id = 4, Name = new() { ValueFr = "Divorcé", ValueNl = "Gescheiden" } },
    new() { Id = 5, Name = new() { ValueFr = "Séparé de corps et de biens", ValueNl = "Scheiding van tafel en bed en van goederen" } },
    new() { Id = 6, Name = new() { ValueFr = "Dissolution du mariage sous une forme particulière", ValueNl = "Ontbinding van het huwelijk op een bijzondere wijze" } },
    new() { Id = 7, Name = new() { ValueFr = "Partenariat", ValueNl = "Partnerschap" } },
    new() { Id = 8, Name = new() { ValueFr = "Indéterminé", ValueNl = "Onbepaald" } },
];

GetAsync:
    return _states
```

### ⚙️ Section 4 — Register Lookup Services in DI

Before using your lookup services, you must register them in dependency injection.

| Lifetime      | When to Use                  | Notes                                                   |
|---------------|------------------------------|---------------------------------------------------------|
| Singleton     | Static, unchanging data       | Ideal for lookup services                               |
| Scoped        | Per user session              | Used here for consistency with your existing setup      |
| Transient     | Recreated on every injection  | Rarely needed in Blazor                                 |

💡 Even though Singleton fits lookup tables best, we intentionally use **Scoped** to match your current architecture.

➡️ Add these registrations in your `PresentationModule.cs`.

Reference:  
https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection

### ⚙️ Section 5 — Use Lookup Data in a Razor Page

#### Step 1 — Update the ViewModel interface

```csharp
public interface IPersonDetailsViewModel
{
    // existing members...

    IReadOnlyList<GenderModel> Genders { get; }
    IReadOnlyList<CivilStateModel> CivilStates { get; }

    // existing members...
}
```

#### Step 2 — Update the ViewModel initialization

```
OnInitializedAsync:
    ...
    try:
        ...
        Genders = await genderLookupService.GetAsync()
        CivilStates = await civilStateLookupService.GetAsync()
        ...
    catch ex:
    ...
```

💡 Don’t forget to inject the lookup services into the existing ViewModel.

#### Step 3 — Render localized lookup values in the Razor Page

```razor
@{
    var genderRecord = ViewModel.Genders.FirstOrDefault(_ => _.Id == ViewModel.Person.GenderId);
    var gender = genderRecord?.Name?.LocalizedValue ?? "-";
}
<MudText Class="list-definition">@gender</MudText>
```

```razor
@{
    var civilStateRecord = ViewModel.CivilStates.FirstOrDefault(_ => _.Id == ViewModel.Person.CivilStateId);
    var civilState = civilStateRecord?.Name?.LocalizedValue ?? "-";
}
<MudText Class="list-definition">@civilState</MudText>
```

🖼️ Example layout (expected result):  
<img width="1313" height="841" alt="image" src="https://github.com/user-attachments/assets/746fb049-1995-4861-b77c-56a24865a28e" />

---

## 🧩 Focus Points
- `LocalizedStringModel` enables culture-aware values  
- Lookup services simplify UI translation logic  
- Interfaces allow easy mocking and future API migration  
- DI lifetimes influence performance and architecture  

---

## 🧠 Next Steps
In the next exercise, you will build the **Person Create feature**: feature folder, ViewModel, mock ServiceClient, Razor components, and workflow (loading → form → confirmation).  

👉 Continue with [Exercise 08 — Person Creation (Feature Structure + Workflow)](./Exercise_08_Person_Create.md).
