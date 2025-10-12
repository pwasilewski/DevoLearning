# 🧩 Exercise 02 — Reusable PageIntro Component & Shared Date Logic

## 🎯 Goal
Learn to build and apply reusable structures — both in **UI** and **logic**.  
You’ll refactor your homepage layout into a standalone **`PageIntro`** component and move your date formatting into a dedicated **`DateTimeExtensions`** class.  
By the end, you’ll understand how separating reusable UI and logic simplifies updates, reduces duplication, and improves maintainability across your Blazor app.  

---

## 🧠 Context
In the previous exercise, you built a simple homepage showing a title and today’s date.  
While functional, both the layout and formatting logic are currently tied to a single page.  
In this exercise, you’ll extract them into reusable parts:

* **`PageIntro`**: a Blazor component that standardizes how page headers are displayed.
* **`DateTimeExtensions`**: a shared extension method that formats dates consistently across the app.

This dual approach reflects professional development practices — separating concerns, improving clarity, and making future changes (like localization or new layouts) easier to implement.  
You’re now moving from _page-level implementation_ to _application-level reusability_.

---

## 📚 Learn / Review Before Starting
* [Razor Components – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-8.0)
* [Component Parameters – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-8.0#component-parameters)  
* [Extension Methods – Microsoft Docs](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods)

---

## 🧱 Exercise Steps

### ⚙️ Section 1 — Reusable Blazor Component: PageIntro

#### Step 1 – Create the Component
At the **root level**, create a folder named **`Components`**.  
Inside, add a new Razor file named **`PageIntro.razor`**.  
This component will represent a reusable page introduction block, containing a title and optional child content.

---

#### Step 2 – Add Parameters
Add two public parameters to support flexibility and reuse:

```csharp
[Parameter]
public string Title { get; set; }

[Parameter]
public RenderFragment ChildContent { get; set; }
```

💡 The **`ChildContent`** parameter allows embedding any custom markup or components between your component tags, making it adaptable to different page needs.

---

#### Step 3 – Define the Layout
Inside **`PageIntro.razor`**, use simple markup to display the title and the optional child content underneath.
Keep the structure minimal and consistent with your design system.  

💡 Focus on clarity and flexibility; you’ll extend this pattern over time.

---

#### Step 4 – Use It on the Homepage
Open **`Index.razor`** and replace your existing layout with the new **`PageIntro`** component.
It should receive a title (“Hello World”) and render a description showing today’s date.

💡 Don’t forget to add the component’s namespace import before using it.

---

#### Step 5 – Add Namespace Import
Open **`_Imports.razor`** and include:

```csharp
@using Nihdi.DevoLearning.Presentation.Components
```

💡 Imports defined here become available to all Razor files in your project — no need to repeat them in every page. (You can remove the using from **`Index.razor`**)

---

### ⚙️ Section 2 — Reusable C# Logic: `DateTimeExtensions`

#### Step 1 - Add the Extension Class
Inside your existing **`Extensions`** folder, create **`DateTimeExtensions.cs`**.  
Add an extension method to handle consistent date formatting (e.g., `ToShortDisplayFormat()`).

💡 This centralizes date formatting logic — any display change can be made once and reflected everywhere.

---

#### Step 2 – Display the Formatted Date
In the homepage (**`Index.razor`**), use your new extension method inside the **`PageIntro`** description section to display the current date in the unified format.   

💡 Put the using in the **`_Import.razor`** file.

---

## 🧩 Focus Points
- **Reusability** – Extract both UI and logic into components and extensions.
- **Maintainability** – Update once, apply everywhere.
- **Separation of Concerns** – Keep formatting logic out of the UI layer.
- **Consistency** – Ensure uniform patterns across all pages.
- **Scalability** – Prepare for future features like localization or theming.

---

## 🧠 Next Steps  
In the next exercise, you’ll extend this foundation to handle dynamic data and more complex layouts — reinforcing how reusable components and extension methods work together to keep your Blazor app clean and scalable.   
👉 Continue with [Exercise 03 – ](./Exercise_03.md)
