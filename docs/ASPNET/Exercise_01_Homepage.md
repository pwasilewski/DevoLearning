# Exercise 01 — Homepage Layout & Date Formatting

## 🎯 Goal  
Set up the homepage using your team’s Design System pattern to display a welcoming title and today’s date.  
This exercise will help you practice **Razor syntax**, **data binding**, and **basic formatting** within Blazor while introducing the foundation for future reusable components.

---

## 🧠 Context  
Your project follows a shared **Design System** where each page starts with a clear **title** and **description** section, known as the “Page intro.”  
In this first exercise, you’ll implement this structure directly on the homepage.  
Later, in Exercise 02, you’ll refactor it into a reusable component to reduce duplication and improve maintainability.

---

## 📚 Learn / Review Before Starting  
📚 **Review these resources before starting:**  
* [Blazor Components Overview – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/components)  
* [ASP.NET Core Razor Syntax Reference – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-8.0)  
* [DateTime Formatting in .NET](https://learn.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings)  
* [MudBlazor Overview](https://mudblazor.com/getting-started/installation)  

---

## 🧱 Exercise Steps  

### ⚙️ Section 1 — Implementing the Homepage Layout  

#### Step 1 – Locate the Homepage  
Open the file:  
```
Nihdi.DevoLearning.Presentation/Features/Pages/Home/Index.razor
```
This is where you’ll apply the base **Page intro** structure from your [Design System](https://webappsa.riziv-inami.fgov.be/styleguide-mudblazor8-wfe/pattern/page-intro).

---

#### Step 2 – Apply the Layout  
Use the Design System’s **Page intro** pattern to structure the homepage with:  
- A title showing **“Hello World”**  
- A description that displays today’s date  

💡 **Hint:** You can refer to the expected final result here:  
<img width="1362" height="305" alt="image" src="https://github.com/user-attachments/assets/e2ef0fff-734a-4897-9787-f5389450b126" />

---

#### Step 3 – Format the Date  
Use `DateTime.Today` and format it using one of .NET’s standard or custom date format strings.  
Experiment with different formats to find a clean and readable style.


---

#### Step 4 – Run and Verify  
Start the application and open the homepage.  
Check that:  
- The title and description display correctly.  
- The date appears in the expected format.  

---

## 🧩 Focus Points  
- Understand and apply the **Design System’s Page intro pattern**.  
- Use **Razor syntax** and **data binding** for dynamic content.  
- Practice **basic `DateTime` formatting** in Blazor.  
- Build a foundation for future component reusability.  

---

## 🧠 Next Steps  
In the next exercise, you’ll refactor the layout logic into a **reusable `PageIntro` component**, improving reusability and maintainability across all pages.  
👉 Continue with [Exercise 02 – Create and Use the PageIntro Component](./Exercise_02_PageIntro.md)
