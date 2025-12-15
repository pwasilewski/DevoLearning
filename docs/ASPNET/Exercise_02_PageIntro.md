# Exercise 02 — PageIntro Component & DateTimeExtensions

## 🎯 Goal
In this exercise, you will extract your homepage’s title and date formatting into reusable building blocks: a `PageIntro` component for consistent page headers, and a `DateTimeExtensions` class for shared date formatting. This establishes clean separation of concerns and prepares your application for structured, maintainable UI patterns used in all future features.

## 🧠 Context
In the previous exercise, your homepage displayed a title and today’s date directly in the Razor page. While functional, the layout and formatting logic were tightly coupled to that single page.

This exercise generalizes both concepts:
- `PageIntro` becomes a **shared UI component**.
- `DateTimeExtensions` becomes a **shared logic component**.

This mirrors real-world professional practices: you move from page-level implementation to reusable, application-wide patterns that support cleaner markup, improved readability, and easier maintenance.

## 📚 Learn / Review Before Starting
- [Razor Components Overview – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/components)
- [Component Parameters – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-8.0#component-parameters)
- [DateTime Formatting in .NET – Microsoft Docs](https://learn.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings)

---

## 🧱 Exercise Steps

## ⚙️ Section 1 — Create the PageIntro Component

### Step 1 — Create the component  
At the **root level**, create a folder named `Components`.   
Inside, add a new Razor file named `PageIntro.razor`.   
This component will represent a reusable page introduction block, containing a title and optional child content.

### Step 2 — Add component parameters  
Add two public parameters to support flexibility and reuse:

```csharp
[Parameter]
public string Title { get; set; }

[Parameter]
public RenderFragment ChildContent { get; set; }
```

💡 The `ChildContent` parameter allows embedding any markup or components between your `<PageIntro>` tags, making it adaptable to different page needs.

### Step 3 — Define the layout  
Inside `PageIntro.razor`, use simple markup to display the title and the optional child content underneath.  
Keep the structure minimal and consistent with your design system.

### Step 4 — Use it on the homepage  
Open `Features/Home/Pages/Index.razor` and replace your existing layout with the new `PageIntro` component.  
It should receive a title (`"Hello World"`) and render a description showing today’s date.

🖼️ **Example layout (expected result):**  
<img width="1362" height="305" alt="image" src="https://github.com/user-attachments/assets/e2ef0fff-734a-4897-9787-f5389450b126" />

💡 Don’t forget to add the component’s namespace import before using it.

### Step 5 — Add namespace import  
Open `_Imports.razor` and include:

```csharp
@using Nihdi.DevoLearning.Presentation.Components
```

💡 Imports defined here become available to all Razor files — you can remove the `using` from `Index.razor`.

## ⚙️ Section 2 — Create the DateTimeExtensions Class

### Step 1 — Add the extension class  
In the `Extensions` folder, create `DateTimeExtensions.cs`.  
Add an extension method that provides consistent date formatting across the application (for example: `ToShortDisplayFormat()`).

💡 Centralizing formatting ensures that if the display format changes later, you update it once and the entire application benefits.

### Step 2 — Use the extension on the homepage  
In `Features/Home/Pages/Index.razor`, use your new extension method inside the `PageIntro` component to display today’s date using the standardized format.

💡 Add the required `using` statement in `_Imports.razor` so the extension becomes available everywhere.

---

## 🧩 Focus Points
- Building reusable UI using a dedicated component (`PageIntro`)
- Sharing logic across pages with extension methods
- Keeping markup clean by moving layout and formatting concerns out of the page
- Understanding project-wide imports using `_Imports.razor`
- Preparing the foundation for consistent UI patterns across the application

---

## 🧠 Next Steps
In the next exercise, you’ll introduce **localization** and learn how to move hardcoded text into `.resx` files while keeping feature modules isolated.  

👉 Continue with [Exercise 03 – Localization Basics](./Exercise_03_Localization_Basics.md)
