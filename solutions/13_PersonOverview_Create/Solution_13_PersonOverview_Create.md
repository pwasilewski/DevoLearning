# ✅ Solution — Exercise 13 — Person Overview & Create API

## 🧩 Overview
You extended the backend by completing the PersonController with two additional endpoints:
a paginated Overview endpoint and a Create endpoint.
You also introduced several new DTOs in the Nihdi.DevoLearning.Contracts project to standardize API contracts across backend and frontend.
Mocked data is still used for now, but the API structure is ready for future Application-layer integration.

## 🧱 Implementation

### Step 1 — Add PaginatedResultDto
Create PaginatedResultDto.cs in Shared/Pagination.
This DTO defines the structure of a paginated response shared across the system.

**File:** PaginatedResultDto.cs  
[GITHUB-PLACEHOLDER]

---

### Step 2 — Add PersonOverviewQueryDto
Create PersonOverviewQueryDto.cs in Persons/Overview.
This file contains the parameters used for querying the person overview list.

**File:** PersonOverviewQueryDto.cs  
[GITHUB-PLACEHOLDER]

---

### Step 3 — Add PersonOverviewDto
Create PersonOverviewDto.cs in Persons/Overview.
This DTO defines the shape of one row of the overview table.

**File:** PersonOverviewDto.cs  
[GITHUB-PLACEHOLDER]

---

### Step 4 — Add PersonCreateDto
Create PersonCreateDto.cs in Persons/Create.
This DTO includes fields required to create a new person.

**File:** PersonCreateDto.cs  
[GITHUB-PLACEHOLDER]

---

### Step 5 — Implement the Overview endpoint
Extend PersonController with a GET endpoint that receives a PersonOverviewQueryDto from the query string and returns a paginated list of persons.
The mocked list is reused from the Presentation layer, wrapped in a PaginatedResultDto.

**File:** PersonController.cs  
[GITHUB-PLACEHOLDER]

---

### Step 6 — Implement the Create endpoint
In PersonController, add a POST endpoint accepting PersonCreateDto from the request body.
A fixed or random identifier is generated and returned as the created person's Id.

**File:** PersonController.cs  
[GITHUB-PLACEHOLDER]
