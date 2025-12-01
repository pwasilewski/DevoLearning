# ✅ Solution — Exercise 03 — Localization Basics

## 🧩 Overview
Your homepage now supports **feature-based localization** using `HomeResource.resx` files.  
All UI text — the title, localized date description, and the encouragement line — is now retrieved from strongly typed resources instead of being hardcoded.  
You also correctly render localized HTML content using `MarkupString`.

## 🧱 Implementation

### Step 1 – Add feature resource files
👉 You created the following resource files under `Resources/Features/Home`:

- HomeResource.resx  
- HomeResource.nl.resx  
- HomeResource.fr.resx  

These contain the keys:
- Title  
- Description (with `{0}` placeholder for the date)  
- Encouragement (contains `<b>` HTML tags)

**Files:**  
[GITHUB-LINK-PLACEHOLDER]

### Step 2 – Localize the title (simple replacement)
👉 The homepage title now uses a direct resource lookup.   
This replaces the plain hardcoded string and confirms your localization pipeline works.

**File:** Index.razor — localized title  
[GITHUB-LINK-PLACEHOLDER]

### Step 3 – Localize the description with dynamic date insertion
👉 The description string contains a `{0}` placeholder, so you inject a **culture-aware formatted date**.   

This demonstrates:

- How to pass dynamic data into localized text  
- How to reuse your shared date formatting extension method  
- How to respect culture-specific formatting rules  

**File:** Index.razor — localized description with date  
[GITHUB-LINK-PLACEHOLDER]

### Step 4 – Render localized HTML using MarkupString
👉 The Encouragement resource contains `<b>` tags.  
If output normally, the HTML would be escaped.  

This ensures:

- HTML renders correctly  
- Text remains localized per culture  
- Output is safe because `.resx` files are controlled by developers  

**File:** Index.razor — encouragement using MarkupString  
[GITHUB-LINK-PLACEHOLDER]
