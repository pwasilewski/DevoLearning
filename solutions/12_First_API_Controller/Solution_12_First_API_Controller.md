# ✅ Solution — Exercise 12 — First API Controller (PersonController)

## 🧩 Overview
You added the first backend endpoint to the application.
The PersonDetailsDto contract was introduced in the Contracts project, the PersonController was created in the Host BFF, and the Details endpoint now returns mocked data for testing.
The endpoint can be executed and verified in NSwag.

## 🧱 Implementation

### Step 1 — Create the Person Details Contract
Add PersonDetailsDto.cs to the Nihdi.DevoLearning.Contracts/Persons/Details folder.
This contract defines the structure of a person details response.

**File:** PersonDetailsDto.cs  
[GITHUB-PLACEHOLDER]

---

### Step 2 — Create the PersonController
Add PersonController.cs in the Nihdi.DevoLearning.Host.Bff/Controllers folder.
The controller inherits from ControllerBase and is decorated with the API controller and route attributes.

**File:** PersonController.cs  
[GITHUB-PLACEHOLDER]

---

### Step 3 — Implement the Details endpoint and return mocked data
Inside PersonController, add a method named GetPersonDetailsById.
The method receives the id from the route, constructs a mocked PersonDetailsDto using this id, and returns it as an HTTP 200 response.

**File:** PersonController.cs  
[GITHUB-PLACEHOLDER]
