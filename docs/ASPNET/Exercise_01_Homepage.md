# Exercise 01 — Homepage

## 🎯 Goal  
Set up your project’s first page using the official Design System layout, display the title and today’s date using Razor expressions, and prepare the foundation for reusable UI patterns used throughout the rest of the application.

## 🧠 Context  
This first page introduces you to the basic building blocks of a Blazor application. You’ll render simple text, use Razor expressions, and apply your team’s Design System pattern to structure the layout. Although the page is simple, it establishes the foundation you’ll build on in later exercises, where this structure becomes reusable and more dynamic.

## 📚 Learn / Review Before Starting  
- [Blazor Components Overview – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/components)  
- [ASP.NET Core Razor Syntax Reference – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-8.0)  
- [DateTime Formatting in .NET – Microsoft Docs](https://learn.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings)  
- [MudBlazor Overview – MudBlazor Docs](https://mudblazor.com/getting-started/installation)

---

## 🧱 Exercise Steps

### ⚙️ Section 1 — Implement the Homepage Layout

#### Step 1 — Apply the Page Intro layout  

Open:

```
Features/Home/Pages/Index.razor
```

Update the page to follow the official **Page Intro** pattern from the design system:  
https://webappsa.riziv-inami.fgov.be/styleguide-mudblazor8-wfe/pattern/page-intro

Your homepage should display:

- A title: "Hello World"  
- A description containing today’s date  

🖼️ **Example layout (expected result):**  
<img width="1362" height="305" alt="image" src="https://github.com/user-attachments/assets/e2ef0fff-734a-4897-9787-f5389450b126" />

💡 Use this visual reference to replicate the expected layout.

#### Step 2 — Format the date  

Use `DateTime.Today` with a clean, readable .NET date format.  
Experiment with different formats to find a clean and readable style.

#### Step 3 — Run and verify  

Confirm that:

- The title renders correctly  
- The date displays in the expected format  
- The layout matches the screenshot  

---

## 🧩 Focus Points  

- Using Razor expressions to render dynamic values  
- Applying the Design System’s Page Intro layout consistently  
- Formatting dates with basic .NET `DateTime` APIs  
- Getting comfortable with the project’s feature and page structure  

---

## 🧠 Next Steps  

In the next exercise, you’ll extract this layout and date formatting into reusable building blocks: a shared `PageIntro` component and a `DateTimeExtensions` helper.  

👉 Continue with [Exercise 02 – Create and Use the PageIntro Component](./Exercise_02_PageIntro.md)
