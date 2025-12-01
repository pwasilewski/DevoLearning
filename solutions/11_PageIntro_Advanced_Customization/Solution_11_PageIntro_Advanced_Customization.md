# ✅ Solution — Exercise 11 — PageIntro Advanced Customization (TitleTemplate)

## 🧩 Overview
The PageIntro component now supports a **TitleTemplate** parameter, enabling fully customized title layouts.  
This enhancement allows pages to place buttons, chips, or other UI elements alongside the title.  
The Person Overview page now uses this feature to display the “Create person” button inline with the page title.

## 🧱 Implementation

### Step 1 – Add the TitleTemplate parameter to PageIntro  
👉 Introduced a new `RenderFragment` parameter named `TitleTemplate`, allowing consumers to override the entire title section while keeping the existing `Title` property for backward compatibility.

**File:** PageIntro.razor  
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/11_PageIntro_Advanced_Customization/Components/PageIntro.razor#L28-L29

### Step 2 – Update the PageIntro layout to prefer TitleTemplate  
👉 Modified the component so that:
- If `TitleTemplate` is provided → render it  
- Otherwise → fall back to the default `<MudText>` title  

**File:** PageIntro.razor  
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/11_PageIntro_Advanced_Customization/Components/PageIntro.razor#L9-L20

### Step 3 – Update the Person Overview page to use TitleTemplate  
👉 Replaced the standard Title usage with a `TitleTemplate` containing both:
- The localized title text  
- The inline “Create person” action button

**File:** PersonOverview.razor  
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/11_PageIntro_Advanced_Customization/Features/Persons/Overview/Pages/PersonOverview.razor#L8-L16

**File:** PersonOverview.razor.cs  
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/11_PageIntro_Advanced_Customization/Features/Persons/Overview/Pages/PersonOverview.razor.cs#L55-L58