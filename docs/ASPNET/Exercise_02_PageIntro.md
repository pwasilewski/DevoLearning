# 🧩 Exercise 02 — Create and Use the PageIntro Component

### 🎯 Goal
Learn how to build a reusable component (`PageIntro`) that defines a consistent page introduction section with a title and dynamic content.  
This will introduce you to **component reusability**, **parameters**, and **maintainability**.

---

### 🧠 Context
Many pages will have a similar structure — a title, a short description, and possibly extra content.  
Instead of repeating this structure on every page, you’ll create a **PageIntro** component once and reuse it across your project.

Later, you’ll enhance this component and connect it with other patterns such as localization or responsive layouts.

---
### 🧱 Exercise 2 — Reusable Components and Logic

### ⚙️ Steps: Reusable Blazor Component: PageIntro

#### Step 1 – Create the Component
In your **root-level** create a **`Components`** folder and then create a new Razor component called **`PageIntro.razor`**.  
This component will act as a standard Page intro for any page.

It should define two parameters:
- `Title`: The main title of the page.
- `ChildContent`: The content rendered inside the component (the page’s additional info, description, or actions).

---

#### Step 2 – Add Parameters
In the component code, define the following parameters:

```csharp
[Parameter]
public string Title { get; set; }

[Parameter]
public RenderFragment ChildContent { get; set; }
```

These allow flexibility — the component doesn’t need to know what exact content it will render, making it reusable anywhere.

💡 **Hint**: In Blazor, `ChildContent` is a reserved parameter name that lets you pass any markup or components between the opening and closing tags of your component. The component will render this content where you reference `@ChildContent`.

---

#### Step 3 – Define the Layout
Copy the markup from your existing `Index.razor` page into your new `PageIntro.razor` component.  
Then, replace the hardcoded title and the description with the before mentioned parameters.

---

#### Step 4 – Use It on the Homepage
Open your **`Index.razor`** and replace your existing layout with the `PageIntro` component.

💡 **Hint**: Don’t forget to import its namespace before using it.

---

#### Step 5 – Add Namespace Import
Open **`_Imports.razor`** and add

```csharp
@using YourApp.Components
```

💡 **Hint**: **`_Imports.razor`** allows you to make components or namespaces available globally so you don’t have to import them manually in every Razor file.

---

### ⚙️ Reusable C# Logic: `DateTimeExtensions`

#### Step 1 - Add the Extension Class
Inside your existing **`Extensions`** folder, create or update **`DateTimeExtensions.cs`**.  
Add an extension method to handle consistent date formatting (e.g., `ToShortDisplayFormat()`).

This prepares you for upcoming exercises where you’ll improve the date logic further.

---

#### Step 2 – Display the Formatted Date
Use your new extension method to format and display the current date in the `Index.razor` description section.

---

### 🧩 Focus Points
- **Reusability** – You define once and use everywhere.
- **Maintainability** – Any design or logic update affects all pages automatically.
- **Extensibility** – This pattern can grow (add icons, tags, buttons) without modifying every page.

---

### 🔗 Helpful Links
- [Microsoft Docs – Razor Components](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-8.0)
- [Microsoft Docs – Component Parameters](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-8.0#component-parameters)
- [Microsoft Docs – Extension Methods](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods)
