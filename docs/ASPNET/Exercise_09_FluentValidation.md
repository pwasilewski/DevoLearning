# Exercise 09 — FluentValidation Integration

## 🎯 Goal
Add FluentValidation to the Person Creation workflow so that form fields are validated directly inside the Razor component using MudBlazor’s validation system.  
This ensures clear inline error feedback, improves the user experience, and prepares the feature for additional server-side validation in later exercises.

## 🧠 Context
Until now, your forms accepted any input. As you move deeper into real-world workflows, your application must validate data consistently, clearly, and in a reusable way.

In this exercise, you introduce **FluentValidation** — a powerful server-side validation library — and learn how to combine it with MudBlazor’s `<MudForm>` so that:

- invalid data is blocked before it reaches the backend  
- validation messages are localized  
- rules live in a single place instead of being scattered across components  

This creates a solid, scalable validation layer for all future Person features (Create, Edit, Details, etc.).

## 📚 Learn / Review Before Starting
- [Using FluentValidation with MudBlazor – MudBlazor Docs](https://mudblazor.com/components/form#using-fluent-validation)  
- [FluentValidation Rules – FluentValidation Docs](https://docs.fluentvalidation.net/en/latest/)  
- [Inline Validation & Error Report – Design System](https://webappsa.riziv-inami.fgov.be/styleguide-mudblazor8-wfe/pattern/form)

---

## 🧱 Exercise Steps

### ⚙️ Section 1 — Create Global Validation Resource Files

#### Step 1 — Create the resource files
In the `Resources` folder, create:

- `ValidationMessage.resx`  
- `ValidationMessage.fr.resx`  
- `ValidationMessage.nl.resx`  

This file set will store **all FluentValidation messages** shared across the entire application.

#### Step 2 — Add validation keys

| Resource Key                | Dutch                                                                 | French                                                                 |
|-----------------------------|------------------------------------------------------------------------|-------------------------------------------------------------------------|
| Required                    | Dit veld is verplicht.                                                 | Ce champ est obligatoire.                                               |
| InvalidEmail               | Ongeldig e-mailadres.                                                  | Adresse e-mail invalide.                                               |
| InvalidDate                | Ongeldige datum.                                                       | Date invalide.                                                         |
| DateNotInFuture           | De datum mag niet in de toekomst liggen.                               | La date ne peut pas être dans le futur.                                |
| DeceaseDate_AfterBirthDate | De overlijdensdatum kan niet vóór de geboortedatum liggen.             | La date de décès ne peut pas être antérieure à la naissance.           |
| Phone_Length               | De telefoon moet tussen {MinLength} en {MaxLength} tekens bevatten.    | Le numéro de téléphone doit contenir entre {MinLength} et {MaxLength} caractères. |

💡 *FluentValidation placeholders (e.g., `{MinLength}`) are replaced automatically.*  
More info: https://docs.fluentvalidation.net/en/latest/configuring.html

---

### ⚙️ Section 2 — Define Validation Rules

#### Step 1 — Create the validator file
In the `Features/Persons/Create/Validators` folder, create:

`PersonCreateModelValidator.cs`

#### Step 2 — Implement validation rules

| Field         | Rules |
|---------------|-------|
| **FirstName** | Required (`Required`) • Maximum length 100 |
| **LastName** | Required (`Required`) • Maximum length 100 |
| **GenderId** | Required (`Required`) |
| **CivilStateId** | Required (`Required`) |
| **BirthDate** | Not in the future (`DateNotInFuture`) • Only validated when a value is provided |
| **DeceasedDate** | Not in the future (`DateNotInFuture`) • Must be after BirthDate (`DeceaseDate_AfterBirthDate`) when both are provided |
| **Email** | Valid email (`InvalidEmail`) • Only validated when provided |
| **Mobile** | Length 7–20 (`Phone_Length`) • Only validated when provided |

💡 For help writing FluentValidation rules, see:  
https://docs.fluentvalidation.net/en/latest/start.html

---

### ⚙️ Section 3 — Integrate FluentValidation Into the Person Create Form

The goal of this section is to connect your validator to the Razor form so MudBlazor displays validation errors inline.  
Follow this checklist and rely on the MudBlazor documentation when needed.

Reference: https://mudblazor.com/components/form#using-fluent-validation

#### Step 1 — Add a `ValidateValue` property to your validator
Inside `PersonCreateModelValidator`:

- Add the `ValidateValue` property (as documented by MudBlazor).  
- This exposes a delegate MudBlazor uses to validate individual fields.

💡 This enables *automatic* per-field validation inside `<MudForm>`.

#### Step 2 — Initialize the validator inside the Razor component
Inside `PersonCreateForm.razor`:

- Create a private `_validator` field  
- Instantiate the validator  

💡 The instance will later be passed into the `Validation` parameter of `<MudForm>`.

#### Step 3 — Enable FluentValidation on `<MudForm>`
Create `PersonCreateForm.razor` that implements it similar to this pseudocode:

```
<MudForm Model="@Model"
         @ref="_form"
         Validation="@(_validator.ValidateValue)">
    ...
</MudForm>
```

💡 This connects the form with FluentValidation.

#### Step 4 — Add the `For` attribute on each input
For every field that is marked as Required in your FluentValidation rules, ensure the corresponding MudBlazor input has the `Required="true"` or simply `Required` attribute.
```
<MudTextField Label="First name" @bind-Value="Model.FirstName" Required />
```

💡 Why this matters:
Design System uses the `Required` attribute to visually mark required fields and ensure proper asterisk styling, but the **actual validation still comes from FluentValidation**.

#### Step 5 — Add the `For` attribute on each input
Example:

```
For="@(() => Model.FirstName)"
```

💡 The `For` attribute ensures FluentValidation can attach messages to the correct field.

#### Step 6 — Validate before submitting
Create `PersonCreateForm.razor.cs` that implements it similar to this pseudocode:

```
await _form.Validate();

if (!_form.IsValid)
    return;

await OnSubmit.InvokeAsync();
```

💡 This prevents invalid data from being submitted.

---

### ⚙️ Section 4 — (Optional) Add Validation at the ViewModel Level

UI validation is great, but **ViewModel validation provides an additional safety layer** — crucial once backend API calls are introduced.

#### Step 1 — Validate inside the ViewModel
Example pseudocode for `PersonCreateViewModel`:

```
var validator = new PersonCreateModelValidator();
var result = validator.Validate(Model);

if (!result.IsValid)
{
    // Display message.
    return;
}

CreatedPersonId = await _serviceClient.CreateAsync(Model);
```

#### Step 2 — Benefits of ViewModel validation
- Ensures validation still happens even if UI validation is bypassed  
- Protects against hidden/disabled/missing form fields  
- Blocks invalid API calls  
- Ensures consistent rules across UI, ViewModel, and server  
- Prepares the solution for API-side MediatR command validation  

💡 This step is optional now but essential later when API logic is introduced.

---

## 🧩 Focus Points
- FluentValidation is now fully integrated into your Blazor form  
- Validation messages are centralized using `ValidationMessage.resx`  
- Using the `For` attribute ensures field-level inline errors  
- ViewModel validation offers a second protection layer  
- Invalid submissions are blocked both on the client and (optionally) the ViewModel

---

## 🧠 Next Steps
In the next exercise, you’ll enhance your input components by creating a custom base component that trims text inputs on blur. This will improve data consistency and reduce validation errors caused by trailing spaces.    

👉 Continue with [Exercise 10 — Blazor Component Inheritance & Auto-Trim TextField Behavior](./Exercise_10_Blazor_Inheritance.md).
