# Exercise 10 — Blazor Component Inheritance & Auto-Trim TextField Behavior.

## 🎯 Goal
Create a custom text field component that automatically trims leading and trailing spaces when the user leaves the input field. This ensures cleaner data entry, reduces common validation errors, and demonstrates how to reuse and extend existing components through Blazor inheritance.

## 🧠 Context
Forms often contain text inputs where users accidentally enter trailing spaces—especially for names, emails, and phone numbers. These invisible characters frequently cause validation issues (e.g., “email not valid”) and pollute stored data.

Rather than cleaning values manually in every form, you will build a reusable component that automatically trims values *on blur*.  
By extending `MudTextField<string>`, you centralize the trimming logic in one place while keeping your existing forms clean and declarative.

This small improvement reflects professional UI development practices: reducing repetitive logic, enforcing consistent behavior, and improving data quality across the entire application.

## 📚 Learn / Review Before Starting
- [Component Inheritance – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-8.0#specify-a-base-class)
- [Input Events (OnBlur) – MDN Web Docs](https://developer.mozilla.org/en-US/docs/Web/API/Element/blur_event)
- [MudTextField – MudBlazor Docs](https://mudblazor.com/components/textfield)

---

# 🧱 Exercise Steps

## ⚙️ Section 1 — Create the Auto-Trim Component

#### Step 1 — Create the component and implement trimming behavior  
In the `Components` folder, create **AutoTrimTextField.razor** that implements it similar to this code:

```razor
@inherits MudTextField<string>

@RenderBase()

@code {
    // Render the base MudTextField UI
    RenderFragment RenderBase() => builder => base.BuildRenderTree(builder);

    protected override void OnInitialized()
    {
        base.OnInitialized();

        // Override the OnBlur handler to run trimming logic
        OnBlur = EventCallback.Factory.Create<FocusEventArgs>(this, HandleOnBlur);
    }

    private async Task HandleOnBlur(FocusEventArgs args)
    {
        // If the value is null or empty, nothing to trim
        if (string.IsNullOrEmpty(Value))
        {
            return;
        }

        // Trim the value
        var trimmed = Value.Trim();

        // Update the field only if the trimmed value is different
        if (!string.Equals(Value, trimmed, StringComparison.InvariantCultureIgnoreCase))
        {
            // Use MudTextField's built-in SetValueAsync to update the value.
            // This ensures correct event triggering, validation updates,
            // and two-way binding synchronization — preferable to modifying Value directly.
            await SetValueAsync(trimmed);
        }
    }
}
```

💡 This inherits all behavior from `MudTextField<string>` while injecting a custom `OnBlur` handler.

---

## ⚙️ Section 2 — Replace Text Fields in the Person Create Form

#### Step 1 — Identify which fields should auto-trim  
Typically these contain user-typed text and benefit from automatic trimming:

- FirstName  
- LastName  
- Email  
- Mobile  

#### Step 2 — Replace `MudTextField` with your new `AutoTrimTextField`

Example update inside `PersonCreateForm.razor`:

```razor
<AutoTrimTextField Label="First name"
                   For="@(() => Model.FirstName)"
                   @bind-Value="Model.FirstName"
                   Required="true" />
```

💡 Your custom component behaves exactly like `MudTextField`, so no additional configuration is required.

---

## ⚙️ Section 3 — Verify Behavior in the UI

#### Step 1 — Run the form  
Navigate to:

```
/Persons/Create
```

#### Step 2 — Test trimming behavior  
Try entering values like:

- `" Alice"` → becomes `"Alice"`  
- `"Bob "` → becomes `"Bob"`  
- `" Carol "` → becomes `"Carol"`  

#### Step 3 — Confirm compatibility with FluentValidation  
Trimming ensures fields no longer suffer from:

- Email validation failures due to trailing spaces  
- Required-field false negatives  
- Phone number length issues  

💡 Trimming before validation ensures cleaner, more predictable form behavior.

---

## 🧩 Focus Points
- Blazor component inheritance allows you to extend existing components without rewriting them
- Centralizing trimming logic eliminates duplication across forms
- Auto-trimming reduces validation noise and improves user experience
- The component remains fully compatible with MudBlazor’s validation pipeline

---

## 🧠 Next Steps
In the next exercise, you’ll extend your PageIntro component by adding a new TitleTemplate parameter that allows more complex content (such as buttons) to appear next to the page title.   
You will then apply this enhancement on the Person Overview page to include an action button in the title area.   

👉 Continue with Exercise 11 — Advanced PageIntro Customization Using TitleTemplate
