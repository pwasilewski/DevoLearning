# Solution 01 – Homepage Layout

## 🏷️ Title
Implementing the Homepage Layout Using the Design System Template

## 🧩 Overview
In this solution, we apply the company’s Page Layout UX pattern to the Homepage. The page contains a **title** and a **description** formatted using Razor syntax.  
This exercise introduces basic Blazor component structure, markup binding, and usage of `DateTime.Today` for dynamic content.

---

## ⚙️ Implementation Steps

1. **Locate the Homepage**
   - Open the `Index.razor` file in your Blazor project (usually under `/Pages`).

2. **Apply the Page Layout Pattern**
   - Replace the existing content with the Page intro structure from the Design System.
   - The **title** should be “Hello World”.
   - The **description** should display the current date using `@DateTime.Today.ToString("dd/MM/yyyy")`.

---

## 🖼️ Expected Result
_Add a screenshot or visual reference here once ready (e.g., Homepage rendered with “Hello World” and today’s date)._

---

## 🧠 Key Takeaways
- Reinforces the concept of **reusing UX patterns** via shared components.
- Introduces **date formatting** and **Razor data binding**.
- Sets the stage for future exercises with localization and reusability.

---

## 🔁 Alternative Solution Ideas


---

## 📚 Further Reading
- [ASP.NET Core Razor Syntax Reference (Microsoft Docs)](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-9.0)
- [Razor Syntax in Blazor (LearnBlazor.com)](https://www.learnblazor.com/razor-syntax)
- [MudBlazor Components Overview](https://mudblazor.com/components/)
