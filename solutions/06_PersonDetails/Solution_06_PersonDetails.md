# ✅ Solution — Exercise 06 — Person Details Page

## 🧩 Overview
The Person Details feature is now fully implemented.  
You added the feature structure, model, ServiceClient, ViewModel, localized labels, and the Razor page to display a person’s information.  
You also enabled navigation **from Overview → Details**, and added **optional Backlink support** to PageIntro.

## 🧱 Implementation

### Step 1 – Add optional Backlink parameters to PageIntro  
👉 You extended PageIntro with `BacklinkHref` and `BacklinkLabel` so detail pages can show a “Back to Overview” link.

**File:** PageIntro.razor  
https://github.com/pwasilewski/DevoLearning/blob/4e0b7d56ebf42f9ba4eae71d31c177f8b07fd292/solutions/06_PersonDetails/Components/PageIntro.razor#L24-L28
https://github.com/pwasilewski/DevoLearning/blob/4e0b7d56ebf42f9ba4eae71d31c177f8b07fd292/solutions/06_PersonDetails/Components/PageIntro.razor#L1-L7

### Step 2 – Create the Person Details folder structure  
👉 You created the standard feature structure under:

```
Features/Persons/Details/  
    Models/  
    Pages/  
    ServiceClients/  
    ViewModels/
```

**Folder:**  
https://github.com/pwasilewski/DevoLearning/tree/main/solutions/06_PersonDetails/Features/Persons/Details

### Step 3 – Add the PersonDetailsModel  
👉 Represents all fields shown on the details page.

**File:** PersonDetailsModel.cs  
https://github.com/pwasilewski/DevoLearning/blob/00b6cdefc3674d3e0d337f297ab5045bb513daa0/solutions/06_PersonDetails/Features/Persons/Details/Models/PersonDetailsModel.cs#L3-L69

### Step 4 – Implement the ServiceClient  
👉 Returns a single person entry based on the provided ID.

**Files:**  
- IPersonDetailsServiceClient.cs  
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/06_PersonDetails/Features/Persons/Details/ServiceClients/IPersonDetailsServiceClient.cs#L1-L9
- PersonDetailsServiceClient.cs  
https://github.com/pwasilewski/DevoLearning/blob/00b6cdefc3674d3e0d337f297ab5045bb513daa0/solutions/06_PersonDetails/Features/Persons/Details/ServiceClients/PersonDetailsServiceClient.cs#L7-L31

### Step 5 – Implement the ViewModel  
👉 Loads the person record and exposes loading/error state.

**Files:**  
- IPersonDetailsViewModel.cs  
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/06_PersonDetails/Features/Persons/Details/ViewModels/IPersonDetailsViewModel.cs#L6-L20
- PersonDetailsViewModel.cs  
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/06_PersonDetails/Features/Persons/Details/ViewModels/PersonDetailsViewModel.cs#L8-L37

### Step 6 – Add the Person Details route  
👉 Added a route constant with an `{id:int}` parameter.

**File:** Routing.cs (Persons.Details)  
https://github.com/pwasilewski/DevoLearning/blob/70a606c62a0209af35594e09aa76f07988152855/solutions/06_PersonDetails/Shared/Routing.cs#L13

### Step 7 – Add localization keys  
👉 Labels for all displayed fields were added.

**Files:** PersonsResource.resx (and variants)  
https://github.com/pwasilewski/DevoLearning/tree/main/solutions/06_PersonDetails/Resources/Features/Persons

### Step 8 – Implement the PersonDetails page  
👉 Displays localized labels, formatted values, and lookup-based fields.

**File:** PersonDetails.razor  
https://github.com/pwasilewski/DevoLearning/blob/00b6cdefc3674d3e0d337f297ab5045bb513daa0/solutions/06_PersonDetails/Features/Persons/Details/Pages/PersonDetails.razor#L4-L89

### Step 9 – Add page code-behind logic  
👉 Reads the route parameter and calls the ViewModel to load the person.

**File:** PersonDetails.razor.cs  
https://github.com/pwasilewski/DevoLearning/blob/b8ac35f8c7088d2c41d81c404cd3a4ae5927e068/solutions/06_PersonDetails/Features/Persons/Details/Pages/PersonDetails.razor.cs#L8-L32

### Step 10 – Connect Overview → Details navigation  
👉 Overview now navigates to the details page when selecting a person.

**File:** PersonOverview.razor  
https://github.com/pwasilewski/DevoLearning/blob/70a606c62a0209af35594e09aa76f07988152855/solutions/06_PersonDetails/Features/Persons/Overview/Pages/PersonOverview.razor#L20-L24

**File:** PersonOverview.razor.cs  
https://github.com/pwasilewski/DevoLearning/blob/70a606c62a0209af35594e09aa76f07988152855/solutions/06_PersonDetails/Features/Persons/Overview/Pages/PersonOverview.razor.cs#L24-L28
https://github.com/pwasilewski/DevoLearning/blob/70a606c62a0209af35594e09aa76f07988152855/solutions/06_PersonDetails/Features/Persons/Overview/Pages/PersonOverview.razor.cs#L50-L53
