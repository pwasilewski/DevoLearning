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
https://github.com/pwasilewski/DevoLearning/blob/386c38b84c0f7ec207b68c61ae0972167a9235c6/solutions/14_Referential_Data_API/Nihdi.DevoLearning.Contracts/Shared/LocalizedStringDto.cs#L3-L14

---

### Step 2 — Create GenderDto
A GenderDto was added in the Referential folder to represent a gender entry with identifiers and localized labels.

**File:** GenderDto.cs  
https://github.com/pwasilewski/DevoLearning/blob/386c38b84c0f7ec207b68c61ae0972167a9235c6/solutions/14_Referential_Data_API/Nihdi.DevoLearning.Contracts/Referential/GenderDto.cs#L5-L16

---

### Step 3 — Create CivilStateDto
A CivilStateDto was added in the Referential folder to represent a civil-state entry with identifiers and localized labels.

**File:** CivilStateDto.cs  
https://github.com/pwasilewski/DevoLearning/blob/386c38b84c0f7ec207b68c61ae0972167a9235c6/solutions/14_Referential_Data_API/Nihdi.DevoLearning.Contracts/Referential/CivilStateDto.cs#L5-L16

---

### Step 4 — Implement the genders endpoint
The ReferentialController now exposes a GET endpoint returning mocked gender values through the backend.

**File:** ReferentialController.cs  
https://github.com/pwasilewski/DevoLearning/blob/386c38b84c0f7ec207b68c61ae0972167a9235c6/solutions/14_Referential_Data_API/Nihdi.DevoLearning.Host.Bff/Controllers/ReferentialController.cs#L29-L33

---

### Step 5 — Implement the civil states endpoint
The controller also exposes a GET endpoint returning mocked civil-state values.

**File:** ReferentialController.cs  
https://github.com/pwasilewski/DevoLearning/blob/386c38b84c0f7ec207b68c61ae0972167a9235c6/solutions/14_Referential_Data_API/Nihdi.DevoLearning.Host.Bff/Controllers/ReferentialController.cs#L35-L39
