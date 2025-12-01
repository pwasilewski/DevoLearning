# ✅ Solution — Exercise 09 — FluentValidation Integration

## 🧩 Overview
FluentValidation is now integrated into the Person Create workflow.  
You added global validation resource files, implemented the `PersonCreateModelValidator` with all rules, connected the validator to the form, and enabled inline localized validation using MudBlazor.  
Invalid submissions are stopped before reaching the ViewModel or service layer.

## 🧱 Implementation

### Step 1 – Add validation resource files and keys  
👉 You created the shared validation resource files and added all required validation messages, including translations.

**Files:**  
- ValidationMessage.resx  
- ValidationMessage.fr.resx  
- ValidationMessage.nl.resx  

https://github.com/pwasilewski/DevoLearning/tree/main/solutions/09_FluentValidation/Resources

### Step 2 – Create the PersonCreateModelValidator and implement rules  
👉 Added a dedicated validator file and defined all FluentValidation rules for the create form (names, gender, civil state, dates, email, phone, etc.).

**File:** PersonCreateModelValidator.cs  
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/09_FluentValidation/Features/Persons/Create/Validators/01_PersonCreateModelValidator.cs#L9-L51

### Step 3 – Implement the ValidateValue member  
👉 Added the `ValidateValue` function to support MudBlazor field-level validation.

**File:** PersonCreateModelValidator.cs  
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/09_FluentValidation/Features/Persons/Create/Validators/02_PersonCreateModelValidator.cs#L52-L61

### Step 4 – Initialize the validator inside PersonCreateForm  
👉 Added the validator instance and connected it to the `<MudForm Validation="...">`.

**File:** PersonCreateForm.razor  
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/09_FluentValidation/Features/Persons/Create/Components/PersonCreateForm.razor#L71-L72
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/09_FluentValidation/Features/Persons/Create/Components/PersonCreateForm.razor#L13

### Step 5 – Add per-field validation bindings  
👉 Updated all inputs to use:

- `For="..."`  
- `Required` indicators  

so inline error messages display correctly.

**File:** PersonCreateForm.razor  
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/09_FluentValidation/Features/Persons/Create/Components/PersonCreateForm.razor#L16
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/09_FluentValidation/Features/Persons/Create/Components/PersonCreateForm.razor#L19
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/09_FluentValidation/Features/Persons/Create/Components/PersonCreateForm.razor#L22
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/09_FluentValidation/Features/Persons/Create/Components/PersonCreateForm.razor#L25
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/09_FluentValidation/Features/Persons/Create/Components/PersonCreateForm.razor#L28
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/09_FluentValidation/Features/Persons/Create/Components/PersonCreateForm.razor#L36
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/09_FluentValidation/Features/Persons/Create/Components/PersonCreateForm.razor#L44
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/09_FluentValidation/Features/Persons/Create/Components/PersonCreateForm.razor#L47

### Step 6 – Validate before submitting  
👉 Updated the submit handler to run validation before calling the ViewModel.

**File:** PersonCreateForm.razor.cs  
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/09_FluentValidation/Features/Persons/Create/Components/PersonCreateForm.razor#L74-L82
