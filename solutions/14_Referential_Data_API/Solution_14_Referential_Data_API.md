# ✅ Solution — Exercise 14 — Referential Data API (Lookup Services Backend)

## 🧩 Overview
You added backend support for referential (lookup) data by introducing standardized DTOs in the Nihdi.DevoLearning.Contracts project and implementing a new ReferentialController in the Host BFF.
Gender and civil-state values are now delivered by backend endpoints instead of being mocked in the Presentation layer.
This establishes the backend as the single source of truth for lookup data.

## 🧱 Implementation

### Step 1 — Create the LocalizedStringDto contract
A LocalizedStringDto was created in the Shared folder to hold French and Dutch label variants.
The Presentation-only computed property was intentionally excluded.

**File:** LocalizedStringDto.cs  
[GITHUB-PLACEHOLDER]

---

### Step 2 — Create GenderDto
A GenderDto was added in the Referential folder to represent a gender entry with identifiers and localized labels.

**File:** GenderDto.cs  
[GITHUB-PLACEHOLDER]

---

### Step 3 — Create CivilStateDto
A CivilStateDto was added in the Referential folder to represent a civil-state entry with identifiers and localized labels.

**File:** CivilStateDto.cs  
[GITHUB-PLACEHOLDER]

---

### Step 4 — Implement the genders endpoint
The ReferentialController now exposes a GET endpoint returning mocked gender values through the backend.

**File:** ReferentialController.cs  
[GITHUB-PLACEHOLDER]

---

### Step 5 — Implement the civil states endpoint
The controller also exposes a GET endpoint returning mocked civil-state values.

**File:** ReferentialController.cs  
[GITHUB-PLACEHOLDER]
