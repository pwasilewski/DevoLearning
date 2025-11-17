# Exercise 06 — Person Details Page

## 🎯 Goal
In this exercise, you will build a simple and reusable lookup system (using Gender as the example) to support static reference data in your application. You’ll create models, a lookup service, dependency injection registration, and finally use the service inside a Razor Page. This pattern is used all the time in real systems for things like countries, civil states, categories, and more.

## 🧠 Context
Static reference data shouldn’t live in the middle of your business logic. Creating dedicated lookup services keeps your controllers/pages clean and your logic reusable. You’ll also be extending your architecture in a way that supports localization and future data-source changes (e.g., switching from in-memory lists to database tables later).

---

## 📚 Learn / Review Before Starting
- [ASP.NET Core Dependency Injection basics](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-basics)  
- [Computed properties in C#](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties)  

---

## 🧱 Exercise Steps

### ⚙️ Section 1 — Create Lookup Models

#### Step 1 — Add LocalizedStringModel

Create `LocalizedStringModel.cs` in your `Shared/Models` folder.  
This model will store both French and Dutch translations and expose a computed property for the current UI culture.

```
Shared/
└── Models/
    └── LocalizedStringModel.cs
```

| Property       | Type   | Description                                |
| -------------- | ------ | ------------------------------------------ |
| ValueFr        | string | French translation                         |
| ValueNl        | string | Dutch translation                          |
| LocalizedValue | string | Computed value based on `CurrentUICulture` |


**💡 Useful Documentation — Computed Properties:** [Computed properties in C#](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties#expression-body-definitions)

#### Step 2 — Add GenderModel

Create `GenderModel.cs` in the same folder.

| Property | Type                 |
| -------- | -------------------- |
| Id       | int                  |
| Name     | LocalizedStringModel |

#### Step 3 — Add CivilStateModel

Create `CivilStateModel.cs` in the same folder.

| Property | Type                 |
| -------- | -------------------- |
| Id       | int                  |
| Name     | LocalizedStringModel |

---

### ⚙️ Section 2 — Gender Lookup Service

#### Step 1 — Define the Interface

Create `IGenderLookupService.cs` in `Services/Genders/`:
```csharp
public interface IGenderLookupService
{
    Task<IReadOnlyList<GenderModel>> GetAsync();
}
```

#### Step 2 — Implement the Service

Create `GenderLookupService.cs` in the same folder.  
Mock the data with a static list:
```csharp
private static readonly IReadOnlyList<GenderModel> _genders =
[
    new() { Id = 0, Name = new LocalizedStringModel { ValueFr = "Masculin", ValueNl = "Mannelijk" } },
    new() { Id = 1, Name = new LocalizedStringModel { ValueFr = "Féminin", ValueNl = "Vrouwelijk" } },
    new() { Id = 2, Name = new LocalizedStringModel { ValueFr = "X", ValueNl = "X" } },
];
```

Return this list from your `GetAsync()` method.

---

### ⚙️ Section 3 — Civil State Lookup Service

#### Step 1 — Define the Interface

Create `ICivilStateLookupService.cs` in `Services/CivilStates/`:
```csharp
public interface ICivilStateLookupService
{
    Task<IReadOnlyList<CivilStateModel>> GetAsync();
}
```

#### Step 2 — Implement the Service

Create `CivilStateLookupService.cs` in the same folder.  
Mock the data with a static list:
```csharp
private static readonly IReadOnlyList<CivilStateModel> _states =
[
    new() { Id = 0, Name = new() { ValueFr = "Célibataire", ValueNl = "Ongehuwd" } },
    new() { Id = 1, Name = new() { ValueFr = "Marié", ValueNl = "Gehuwd" } },
    new() { Id = 2, Name = new() { ValueFr = "Veuf/veuve", ValueNl = "Weduwnaar/weduwe" } },
    new() { Id = 3, Name = new() { ValueFr = "Divorcé", ValueNl = "Gescheiden" } },
    new() { Id = 4, Name = new() { ValueFr = "Séparé de corps et de biens", ValueNl = "Scheiding van tafel en bed en van goederen" } },
    new() { Id = 5, Name = new() { ValueFr = "Dissolution du mariage sous une forme particulière", ValueNl = "Ontbinding van het huwelijk op een bijzondere wijze" } },
    new() { Id = 6, Name = new() { ValueFr = "Partenariat", ValueNl = "Partnerschap" } },
    new() { Id = 7, Name = new() { ValueFr = "Indéterminé", ValueNl = "Onbepaald" } }
];
```

Return this list from your `GetAsync()` method.

---

### ⚙️ Section 4 — Register Lookup Services in DI

Before using your lookup services, you must register them in dependency injection.

#### DI Lifetimes in Blazor WebAssembly

| Lifetime      | When to Use                  | Notes                                                   |
| ------------- | ---------------------------- | ------------------------------------------------------- |
| **Singleton** | Static, unchanging data      | **Best for lookup services** like Gender or Civil State |
| **Scoped**    | Per user session             | Used for API services; used here for consistency        |
| **Transient** | Recreated on every injection | Rarely needed in Blazor                                 |


**💡 Even though Singleton is the ideal lifetime for this scenario, we’ll register these services as Scoped to stay consistent with your current architecture and keep future API integration straightforward.**

For extra reading on DI and lifetimes:
https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection

You can add these registrations in your `PresentationModule.cs`.

---

### ⚙️ Section 5 — Using Lookup Data in a Razor Page

To display translated values for Gender and Civil State, follow this approach:

#### Step 1 – Update the ViewModel Interface

- Add `IReadOnlyList<GenderModel> Genders { get; }` and `IReadOnlyList<CivilStateModel> CivilStates { get; }` to your `IPersonDetailsViewModel` interface.

#### Step 2 – Inject Lookup Services

- Inject the new lookup services into your `PersonDetailsViewModel` implementation.
- Load the lookup data (genders and civil states) asynchronously when initializing the ViewModel.

#### Step 3 – Expose Lookup Data

- Make sure your ViewModel exposes the loaded lists via the properties you added in Step 1.

#### Step 4 – Use Lookup Data in the Razor Page

- In your Razor page, check that `Genders` and `CivilStates` are not null before attempting to use them.
- To display the translated value for a person’s gender or civil state, use `FirstOrDefault()` to find the matching item by ID.
- Show the `LocalizedValue` if found, or a fallback value like `"-"` if not.

---

## 🧩 Focus Points
- `LocalizedStringModel` introduces culture-aware computed properties
- Lookup services simplify complex UI translation logic
- Using interfaces makes mocking and future API migration trivial
- DI lifetime decisions can influence performance and architectur

---

## 🧠 Next Steps  
In the next exercise, you’ll implement the creation of a brand-new Person record, using FluentValidation to validate all fields (including gender and civil state).   
👉 Continue with 