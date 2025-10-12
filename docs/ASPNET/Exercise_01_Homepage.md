# Exercise 01 – Homepage Layout & Date Formatting

## 🎯 Goal
Introduce the base layout pattern from the Design System and practice Razor syntax and `DateTime` formatting within Blazor.  
By the end of this exercise, the homepage will display a **“Hello World”** title and today’s date formatted according to your chosen pattern.

---

## 📚 Learn / Review Before Starting
Before you begin, review these resources to refresh the fundamentals:

- [Blazor Components Overview – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/components)  
- [ASP.NET Core Razor Syntax Reference – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-9.0)  
- [Razor Syntax in Blazor – LearnBlazor.com](https://www.learnblazor.com/razor-syntax)  
- [DateTime Formatting in .NET](https://learn.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings)  
- [MudBlazor Overview](https://mudblazor.com/getting-started/installation)

---

## 🧠 Context
Your team uses a Design System that defines a reusable “Page intro” pattern: a **Title** and **Description**.  
For this first exercise, you’ll implement that layout directly on the homepage without yet creating a reusable component.  
Later, in Exercise 02, you’ll refactor it into a shared component.

---

## ⚙️ Steps

### Step 1 – Locate the Homepage
Open the file:
```
Nihdi.DevoLearning.Presentation/Features/Pages/Home/Index.razor
```
This page will serve as your starting point for displaying the Design System pattern.

---

### Step 2 – Apply the Layout
Use the Design System’s **Page intro** pattern to structure the homepage with:
- A title showing “Hello World”
- A description that displays today’s date

You can use MudBlazor components such as `MudText` or `MudContainer` to align with the overall style.

---

### Step 3 – Format the Date
Use `DateTime.Today` and format it using one of .NET’s standard or custom date format strings.  
Experiment to find a clean and readable output.

💡 **Hint:** Keep the formatting logic simple — we’ll refine date handling later when introducing the `DateTimeProvider`.

---

### Step 4 – Run and Verify
Start the application and open the homepage.  
Confirm that:
- The title and description are displayed correctly.  
- The date appears in the expected format.  

---

## 🧩 Result
When complete, your homepage should resemble the following:

📸 *[Insert Screenshot Here]*  
_(You can paste your UI image here later.)_

---

## 🧭 Discussion / Takeaways
- Practiced basic Razor syntax and data binding.  
- Used MudBlazor typography components for consistency.  
- Gained early familiarity with layout patterns from the Design System.

---

## 🔗 Additional Resources
- [Custom Date and Time Format Strings](https://learn.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings)  
- [Razor Pages Lifecycle](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle)

---

## 🧠 Next Steps
**Next:** [Exercise 02 – Create a Reusable PageLayout Component](./Exercise_02_PageLayout.md)
