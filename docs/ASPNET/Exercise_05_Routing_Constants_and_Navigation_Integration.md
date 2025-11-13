# Exercise 05 — Routing Constants and Navigation Integration

## 🎯 Goal
Centralize all application routes into a single `Routing.cs` file and connect it to your navigation. This approach eliminates hardcoded paths, simplifies maintenance, and ensures consistent naming across your app.

## 🧠 Context
Until now, each page defined its own route with an `@page` directive. In this exercise, you’ll move those routes into a central **Routing constants file**, then refactor your pages and navigation to reference those constants.  
You’ll also extend the existing navigation localization to include a label for the Person Overview page.

---

## 📚 Learn / Review Before Starting
- [ASP.NET Core Blazor routing](https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing)  
- [DRY Principle – Don’t Repeat Yourself](https://en.wikipedia.org/wiki/Don%27t_repeat_yourself)  

---

## 🧱 Exercise Steps

### ⚙️ Section 1 — Create the Central Routing File

Create a new static class named `Routing.cs` in your `Shared` folder.  
This class will act as the **single source of truth** for all route definitions.

```
Shared/
└── Routing.cs
```

💡 Each feature should group its routes inside nested static classes.  
Example:
```csharp
namespace Nihdi.DevoLearning.Presentation
{
    public static class Routing
    {
        public static class Home
        {
            public const string Index = "/";
        }

        public static class Persons
        {
            public const string Overview = "/Persons";
        }
    }
}
```

💡 **Why this matters:**  
Updating or renaming a route becomes a single-line change — improving reliability and keeping navigation consistent.

---

### ⚙️ Section 2 — Refactor the Home Page Route

Open `Features/Home/Pages/Index.razor`.  
You’ll replace the `@page` directive with an **attribute-based route** referencing your new constant.

💡 Read this first: [Attribute-based routing in Blazor](https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing#route-templates)

Your goal:  
Use the `RouteAttribute` instead of the `@page` directive, and make it reference `Routing.Home.Index`.

---

### ⚙️ Section 3 — Refactor the Person Overview Page

Open `Features/Persons/Overview/Pages/PersonOverview.razor`.  
Perform the same refactor as above, removing the `@page` directive and adding the route attribute referencing `Routing.Persons.Overview`.

---

### ⚙️ Section 4 — Update the Navigation and Add Localization

Navigate to your navigation component, typically `Core/Header.razor`, and update route references to use your new constants from `Routing.cs`.

Then, open your existing **`Navigation.resx`** resource file and add the following key and translations:

| Key | French | Dutch |
|-----|--------|--------|
| Persons | Personnes | Personen |

💡 Use these localized keys in your navigation component instead of hardcoded text.

---

## 🧩 Focus Points
- Centralizing routes in one `Routing` file  
- Using **attribute-based routing** for flexibility  
- Replacing **hardcoded paths** with constants  
- Integrating **localized navigation labels**

---

## 🧠 Next Steps  
In the next exercise, you’ll build a **Person Details page**, enabling navigation from the overview grid to a dedicated detail view.  
👉 Continue with **Exercise 06 — Person Details Page**.
