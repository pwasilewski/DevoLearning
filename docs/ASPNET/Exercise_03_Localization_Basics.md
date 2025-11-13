# Exercise 03 — Localization Basics

## 🎯 Goal
Introduce multilingual support by localizing the Homepage content in **French** and **Dutch**.  
You’ll create feature-based `.resx` files for the **Home** epic and replace hardcoded text with localized resources.

---

## 🧠 Context
In many architectures, translations are stored globally or in a central “Resources” component.  
However, this approach often becomes unmanageable as the project grows — global keys pile up, and related strings spread across multiple files.

Instead, this exercise follows a **feature-based localization** approach:  
each epic (like *Home*, *Person*, or *Case*) owns its own `.resx` files.  
This makes maintenance easier, keeps translations relevant, and avoids name collisions between features.

You’ll focus on translating the Homepage `PageIntro` texts — the **title**, a **localized date message**, and an **encouragement line** using `MarkupString`.

---

## 📚 Learn / Review Before Starting
- [Globalization and localization in ASP.NET Core – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-9.0)  
- [ASP.NET Core Blazor localization – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/globalization-localization?view=aspnetcore-9.0)  
- [ResXManager – Marketplace Extension](https://marketplace.visualstudio.com/items?itemName=TomEnglert.ResXManager) – a Visual Studio extension that helps you manage and synchronize `.resx` files efficiently across multiple languages.

---

## 🧱 Exercise Steps

### ⚙️ Section 1 — Create Feature-Based Resource Files

#### Step 1 – Add Resource Files for the Homepage
In the `Resources/Features/Home` folder, create three resource files:
```
HomeResource.resx
HomeResource.fr.resx
HomeResource.nl.resx
```
💡 After creating each file, open its properties and set **Access Modifier → Public** (or use **Custom Tool → PublicResXFileCodeGenerator**).  
This ensures your translations can be referenced directly (e.g., `@Home.Title`).

---

### ⚙️ Section 2 — Define Translation Keys

#### Step 1 – Add Translations
Add the following keys and values for each language version:

| French | Dutch |
| --- | --- |
| Bienvenue sur DevoLearning | Welkom bij DevoLearning |
| `Nous sommes le {0}.` | `Vandaag is het {0}.` |
| `<b>Continuez à apprendre et à grandir !</b>` | `<b>Blijf leren en groeien!</b>` |

💡 The encouragement line includes `<b>` tags — you’ll later render it using `MarkupString` so it appears bold instead of escaped.

---

### ⚙️ Section 3 — Update the Homepage to Use Localized Texts

#### Step 1 – Replace Hardcoded Strings
Open your `Index.razor` page and update the title and description to use the localized resources.

💡 Hint: The description should display the current date using `string.Format(@Home.Description, DateTime.Today.ToShortDisplayFormat())`.

🖼️ Example layout (expected result):

<img width="1351" height="300" alt="image" src="https://github.com/user-attachments/assets/a70c38ba-2bb5-4485-a317-140f7a5ee504" />
<img width="1361" height="300" alt="image" src="https://github.com/user-attachments/assets/1407195b-edb4-4490-a500-8171d080ace0" />


---

### ⚙️ Section 4 — Test the Language Switcher

#### Step 1 – Verify Language Changes
Use the **culture selector** in the top bar to switch between **French** and **Dutch**.  
Confirm that all texts on the Homepage are correctly translated.

---

## 🧩 Focus Points
- Prefer **feature-based `.resx` files** over global ones to improve maintainability.  
- Always set resource **Access Modifier → Public** to allow direct access.  
- Use **`MarkupString`** for localized strings containing HTML.  
- Use **ResXManager** to synchronize and manage all translations easily.

## 🧠 Next Steps  
In the next exercise, you’ll build your first paginated list of people using a data grid.  
👉 Continue with [Exercise 04 - PersonOverview](./Exercise_04_PersonOverview.md)
