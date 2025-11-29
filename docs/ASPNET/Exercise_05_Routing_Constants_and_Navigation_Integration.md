# Exercise 05 — Routing Constants and Navigation Integration

## 🎯 Goal
Centralize all application routes into a single `Routing.cs` file and connect it to your navigation. This approach eliminates hard-coded paths, simplifies maintenance, and ensures consistent naming across your app. It also prepares the application for future Person-related and non-Person modules that will rely on reliable, centralized routing.

## 🧠 Context
Until now, page routes were embedded directly in components, which is workable for small prototypes but becomes fragile as soon as multiple features start sharing navigation logic. By extracting routes into a dedicated `Routing` class, you create a single, reliable source of truth for your application’s navigation. This approach scales well across all future modules and ensures navigation remains consistent and easy to maintain.

## 📚 Learn / Review Before Starting
- [ASP.NET Core Blazor routing – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing)
- [Don’t Repeat Yourself (DRY) – Wikipedia](https://en.wikipedia.org/wiki/Don%27t_repeat_yourself)

---

## 🧱 Exercise Steps

### ⚙️ Section 1 — Create the Central Routing File

#### Step 1 — Add the Routing class
In your `Shared` folder, create `Routing.cs`.
This file will act as the **single source of truth** for all application route definitions.

#### Step 2 — Define grouped routing constants
Create `Routing.cs` with the following structure:

```csharp
public static class Routing
{
    public static class Home
    {
        public const string Index = "/";
    }

    public static class Persons
    {
        public const string Overview = "/Persons/Overview";
    }
}
```

💡 **Group your routes inside nested static classes** so each feature keeps its own routing section.  

💡 **Why this matters:** Centralizing routes ensures consistent naming and makes renaming or updating a path a single-line change, reducing navigation errors.

### ⚙️ Section 2 — Update Routes to Use Routing Constants

#### Step 1 — Refactor the Home Page route
Open `Features/Home/Pages/Index.razor`.  
Replace the existing `@page` directive with an attribute-based route that uses your new routing constant.

💡 Read this first: 
[Attribute-based routing in Blazor – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing#route-templates)

#### Step 2 — Refactor the Person Overview Page
Open `Features/Persons/Overview/Pages/PersonOverview.razor`.  
Perform the same refactor as above by replacing the `@page` directive with an attribute-based route that uses your new routing constant.

### ⚙️ Section 3 — Update Navigation and Add Localization

#### Step 1 — Add the missing navigation key
Open your `Navigation.resx` file and add the following key:

| Resource Key | Dutch    | French     |
|--------------|----------|------------|
| Persons      | Personen | Personnes  |

#### Step 2 — Update links inside the navigation component
Open `Core/Header.razor` and update the two existing MudLinks so they use your routing constants rather than hard-coded paths:

- The **Home** link should use the route constant for the home page  
- The **Persons** link should use the route constant for the Persons overview page  

Also ensure both links use the localized labels from your `Navigation.resx` file.

---

## 🧩 Focus Points
- Maintaining all routes in one central `Routing` class  
- Improving flexibility through attribute-based routing  
- Eliminating hardcoded URLs by relying on constants  
- Ensuring navigation UI uses localized labels  

---

## 🧠 Next Steps
In the next exercise, you’ll build a **Person Details page**, loading a single person based on the `id` provided in the route and enabling navigation from the overview grid.  
👉 Continue with [Exercise 06 — Person Details Page](./Exercise_06_PersonDetails.md).
