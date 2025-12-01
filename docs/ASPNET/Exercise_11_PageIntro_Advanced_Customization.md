# Exercise 11 — PageIntro Advanced Customization (TitleTemplate)

## 🎯 Goal
Enhance the PageIntro component by introducing a TitleTemplate parameter that gives each feature full control over how the page’s title area is rendered. This allows you to inject custom UI elements such as chips, action buttons, and advanced styling.  
You will use this extension in the Person Overview page to display a “Create person” button beside the page title.

## 🧠 Context
Today, PageIntro displays a title using a fixed <MudText> layout. That’s fine for simple screens, but real production pages often require more complex header structures:

• Chips that show a status  
• Buttons aligned next to the title  
• Custom typography or responsive arrangements  
• Conditional rendering depending on permissions  

Adding a new parameter for every variation would make PageIntro rigid and hard to maintain.

By adding a TitleTemplate parameter, you give feature pages full control of the title area while keeping PageIntro small and reusable.  
This matches the customization model used in MudBlazor (Template, HeaderTemplate, CellTemplate, etc.) and prepares the component for future scalability.

## 📚 Learn / Review Before Starting
- [Blazor Component Parameters & Templates – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-8.0#templated-components)  
- [RenderFragment Essentials – Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-8.0#renderfragments)  
- [Page Intro Pattern – Design System](https://webappsa.riziv-inami.fgov.be/styleguide-mudblazor8-wfe/pattern/page-intro)

---

# 🧱 Exercise Steps

## ⚙️ Section 1 — Add a TitleTemplate Parameter to PageIntro (Locked)

#### Step 1 — Add the new parameter

In the Components folder, update PageIntro.razor:

Create PageIntro.razor that implements it similar to this pseudocode:

```razor
@code {
    [Parameter]
    public RenderFragment TitleTemplate { get; set; }
}
```

💡 This optional template allows callers to override the entire title area (chips, buttons, custom styles, clamp-2, etc.).

#### Step 2 — Update the markup to support the template

Modify the title container so it chooses between the custom template and the default markup:

```razor
<div class="d-flex flex-column flex-lg-row align-items-lg-center justify-content-lg-between gap-2 my-3">

    @if (TitleTemplate is not null)
    {
        @TitleTemplate
    }
    else
    {
        <div class="d-flex align-items-center gap-2">
            <MudText Class="m-0" Typo="Typo.h1" Color="Color.Primary">
                @Title
            </MudText>
        </div>
    }

</div>
```

💡 If TitleTemplate is provided, the standard title is skipped entirely.

#### Step 3 — Leave existing Backlink and ChildContent untouched

No changes are needed for:

- BacklinkLabel  
- BacklinkHref  
- ChildContent

💡 The component remains fully backward compatible — existing pages continue to work without any modifications.

---

## ⚙️ Section 2 — Apply TitleTemplate in the Person Overview Page

#### Step 1 — Add a “Create Person” localization key

In the Resources/Features/Persons/PersonsResource files (.resx, .fr.resx, .nl.resx), add:

| Resource Key | Dutch | French |
|--------------|--------|--------|
| PersonOverview_CreatePerson | Persoon aanmaken | Créer une personne |

Use this key as the label for the title-area action button.

#### Step 2 — Override the title area using TitleTemplate

In Features/Persons/Overview/Pages/PersonOverview.razor, update <PageIntro> so that it provides:

- a TitleTemplate block (replaces the default title markup)  
- a ChildContent block (required whenever TitleTemplate is used)

🖼️ Example layout (expected result):  
<img width="1339" height="841" alt="image" src="https://github.com/user-attachments/assets/21c0d406-f4c5-4929-b624-08634fe43a5b" />


#### Step 3 — Add navigation logic

Add a method that navigates to the person creation page when the button is clicked, using the existing Routing.Persons.Create route.

---

## 🧩 Focus Points

- Extending a component using RenderFragment templates provides maximum flexibility without breaking existing pages.
- Adding TitleTemplate makes PageIntro future-proof, allowing chips, buttons, or entirely custom markup next to the title.
- The component stays fully backward compatible, because the default title rendering is preserved when no template is supplied.
- Overriding the title area requires explicitly defining ChildContent, ensuring page body rendering remains clear and intentional.
- The Person Overview page now demonstrates how to combine reusable components, localization, and navigation inside a clean UI pattern.

---

## 🧠 Next Steps

This exercise concludes the **ASP.NET Introduction** phase.  
You now have a reusable component library, localization, validation, routing, ViewModels, and structured feature folders — all the tools needed to build complete features on your own.

In the next exercise, you will begin working on the REST API side:

- implementing your first controller  
- exposing endpoints for Person data  
- learning how to integrate NSwag to generate strongly-typed C# API clients  

👉 Continue with Exercise 12 — Building Your First REST API Controller & Introducing NSwag.
