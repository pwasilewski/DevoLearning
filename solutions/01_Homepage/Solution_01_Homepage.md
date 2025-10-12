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
<img width="1362" height="305" alt="image" src="https://github.com/user-attachments/assets/050c37f7-5989-4d66-a595-202a015361d6" />

https://github.com/pwasilewski/DevoLearning/blob/d46622a2eb59635a2ce21c42186dc9772fb3ff73/solutions/01_Homepage/Features/Home/Pages/01_Index.razor#L3-L16

---

## 🧠 Key Takeaways
- Reinforces the concept of **reusing UX patterns** via shared components.
- Introduces **date formatting** and **Razor data binding**.
- Sets the stage for future exercises with localization and reusability.

---

## 🔁 Alternative Solution Ideas

https://github.com/pwasilewski/DevoLearning/blob/d46622a2eb59635a2ce21c42186dc9772fb3ff73/solutions/01_Homepage/Features/Home/Pages/02_Index.razor#L3-L20

https://github.com/pwasilewski/DevoLearning/blob/d46622a2eb59635a2ce21c42186dc9772fb3ff73/solutions/01_Homepage/Features/Home/Pages/03_Index.razor#L3-L20

https://github.com/pwasilewski/DevoLearning/blob/d46622a2eb59635a2ce21c42186dc9772fb3ff73/solutions/01_Homepage/Features/Home/Pages/04_Index.razor#L3-L25
---

## 📚 Further Reading
- [ASP.NET Core Razor Syntax Reference (Microsoft Docs)](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-9.0)
- [Razor Syntax in Blazor (LearnBlazor.com)](https://www.learnblazor.com/razor-syntax)
- [MudBlazor Components Overview](https://mudblazor.com/components/)
