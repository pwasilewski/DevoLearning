# ✅ Solution — Exercise 10 — Blazor Inheritance & Auto-Trim TextField

## 🧩 Overview
You created a reusable component that trims whitespace automatically on blur, improving data quality and removing repetitive trimming logic from forms.  
The Person Create form now uses this component for all user-typed fields.

## 🧱 Implementation

### Step 1 – Create the AutoTrimTextField component  
👉 Added a new component inheriting from `MudTextField<string>` and implemented trimming logic in the OnBlur handler.

**File:** Components/AutoTrimTextField.razor  
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/10_Blazor_Inheritance/Components/AutoTrimTextField.razor#L1-L37

### Step 2 – Replace MudTextField in the Person Create form  
👉 Updated relevant text fields (FirstName, LastName, Email, Mobile) to use the new component.

**File:** PersonCreateForm.razor  
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/10_Blazor_Inheritance/Features/Persons/Create/Components/PersonCreateForm.razor#L16
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/10_Blazor_Inheritance/Features/Persons/Create/Components/PersonCreateForm.razor#L19
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/10_Blazor_Inheritance/Features/Persons/Create/Components/PersonCreateForm.razor#L44
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/10_Blazor_Inheritance/Features/Persons/Create/Components/PersonCreateForm.razor#L47
