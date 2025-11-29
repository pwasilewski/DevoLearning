# Exercise 03 — Localization Basics

## 🎯 Goal
In this exercise, you will introduce localization into your Blazor app and create the structure needed to support multiple languages. You will set up resource files, organize them per feature, and use localized strings in components. This establishes a scalable approach to multilingual UI development that future features will build upon.

## 🧠 Context
Until now, UI text has been hardcoded, and while some architectures solve this with a global Resources folder, that approach becomes messy as soon as many features start sharing translations. Keys mix together, unrelated strings collide, and maintaining clarity becomes difficult.
A feature-based localization model avoids this problem by letting each feature own its own `.resx` files. This isolates translations, keeps them relevant, reduces naming conflicts, and scales naturally as the application grows.
In this exercise, you establish that structure by localizing the Homepage PageIntro: its title, a localized date string, and an encouragement line displayed using MarkupString.

## 📚 Learn / Review Before Starting
- [Globalization and Localization in ASP.NET Core – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-9.0)
- [Blazor Localization – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/globalization-localization?view=aspnetcore-9.0)
- [ResXManager – Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=TomEnglert.ResXManager)

💡 *ResXManager helps manage and synchronize `.resx` files across different languages and keeps feature-based resource folders organized.*

---

## 🧱 Exercise Steps

## ⚙️ Section 1 — Create Feature-Based Resource Files

### Step 1 — Create the resource files
In the `Resources/Features/Home` folder, create:

```
HomeResource.resx
HomeResource.nl.resx
HomeResource.fr.resx
```

💡 Set **Code generator → Public** (or use **Custom Tool → PublicResXFileCodeGenerator**) for each file.
This allows you to reference translations directly in components, for example: `@HomeResource.Title`.

🖼️ 
**Via ResXManager**   
<img width="1224" height="254" alt="image" src="https://github.com/user-attachments/assets/9657d24a-dfe6-4c99-8894-4b4ad9307025" />

**Via file properties**   
<img width="553" height="179" alt="image" src="https://github.com/user-attachments/assets/839edd5a-b71f-44f0-8cdf-8f673d193801" />


### Step 2 — Add translation keys
Add the following keys to all three resource files:

| Resource Key | Dutch | French |
|--------------|--------|---------|
| Title | Welkom | Bienvenue |
| Description | Vandaag is {0} | Aujourd’hui, nous sommes le {0} |
| Encouragement | <b>Blijf leren en ontdek meer!</b> | <b>Continuez à apprendre et à explorer !</b> |

💡 *The encouragement line includes `<b>` tags — you’ll later render it using `MarkupString` so the text appears bold instead of escaped.*


## ⚙️ Section 2 — Update the Homepage to Use Localized Texts

### Step 1 — Replace hardcoded strings
Open `Features/Home/Pages/Index.razor` and replace the hardcoded title and description with localized values from `HomeResource`.

💡 Use `string.Format(HomeResource.Description, DateTime.Today.ToShortDisplayFormat())` to insert the culture-aware date.

🖼️ **Example layout (expected result):**

<img width="1351" height="300" alt="image" src="https://github.com/user-attachments/assets/a70c38ba-2bb5-4485-a317-140f7a5ee504" />
<img width="1361" height="300" alt="image" src="https://github.com/user-attachments/assets/1407195b-edb4-4490-a500-8171d080ace0" />

## ⚙️ Section 3 — Verify Localization in the Application

### Step 1 — Run the application
Start your application and navigate to the Home page.
Confirm that the title, date message, and encouragement line now use the localized values from `HomeResource`.

### Step 2 — Switch the UI culture
Change your UI culture (for example by adjusting the browser language or the culture defined in `Program.cs`) and verify that:

- Dutch translations appear for `nl`
- French translations appear for `fr`

---

## 🧩 Focus Points
- Feature-based `.resx` files keep translations organized and avoid key collisions
- Localized strings replace hardcoded text, preparing the app for multilingual support
- Formatting culture-aware values (like dates) with localization
- Rendering translation text containing HTML using `MarkupString`

---

## 🧠 Next Steps
In the next exercise, you’ll use your new localization structure in a real feature by building the first person-related page. This includes creating ViewModels, rendering data from a ServiceClient, and applying localized UI text.   
👉 Continue with [Exercise 04 - PersonOverview](./Exercise_04_PersonOverview.md)
