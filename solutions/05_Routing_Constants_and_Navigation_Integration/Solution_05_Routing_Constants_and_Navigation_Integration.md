# ✅ Solution — Exercise 05 — Routing Constants and Navigation Integration

## 🧩 Overview
Your application now uses a **centralized Routing class** to define all navigation paths, removing hardcoded route strings across pages and components.  
Both the Home page and Person Overview page now declare their routes using attribute-based routing, and the navigation header uses localized labels combined with routing constants.

## 🧱 Implementation

### Step 1 – Add the Routing class
👉 You created a new central file containing grouped static route constants for Home and Persons.

**File:** Routing.cs  
https://github.com/pwasilewski/DevoLearning/blob/4e0b7d56ebf42f9ba4eae71d31c177f8b07fd292/solutions/05_Routing_Constants_and_Navigation_Integration/Shared/Routing.cs#L3-L14

### Step 2 – Update the Home page route
👉 The Home page now uses attribute-based routing with the centralized `Routing.Home.Index` constant.

**File:** Features/Home/Pages/Index.razor  
https://github.com/pwasilewski/DevoLearning/blob/4e0b7d56ebf42f9ba4eae71d31c177f8b07fd292/solutions/05_Routing_Constants_and_Navigation_Integration/Features/Home/Pages/Index.razor#L1

### Step 3 – Update the Person Overview page route
👉 The Person Overview page now uses attribute-based routing with `Routing.Persons.Overview`.

**File:** Features/Persons/Overview/Pages/PersonOverview.razor  
https://github.com/pwasilewski/DevoLearning/blob/4e0b7d56ebf42f9ba4eae71d31c177f8b07fd292/solutions/05_Routing_Constants_and_Navigation_Integration/Features/Persons/Overview/Pages/PersonOverview.razor#L5

### Step 4 – Add missing navigation localization key
👉 You added the “Persons” label (Dutch/French) to the navigation resource file.

**File:** Resources/Navigation.resx (and localized variants)  
https://github.com/pwasilewski/DevoLearning/tree/main/solutions/05_Routing_Constants_and_Navigation_Integration/Resources

### Step 5 – Update navigation component to use constants + localization
👉 The `Header.razor` component now builds links using `Routing` instead of string literals and uses `NavigationResource` for localized labels.

**File:** Core/Header.razor  
https://github.com/pwasilewski/DevoLearning/blob/4e0b7d56ebf42f9ba4eae71d31c177f8b07fd292/solutions/05_Routing_Constants_and_Navigation_Integration/Core/Header.razor#L43
https://github.com/pwasilewski/DevoLearning/blob/4e0b7d56ebf42f9ba4eae71d31c177f8b07fd292/solutions/05_Routing_Constants_and_Navigation_Integration/Core/Header.razor#L49
