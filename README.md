# DevoLearning

### 🧩 Hands-On Learning Path for C# / .NET Core, EF Core, and Clean Architecture

**DevoLearning** is a practical learning solution designed to help developers progressively build skills in C#, .NET Core, EF Core, and software architecture patterns (Clean Architecture + CQRS + Feature-oriented design).  

It’s structured as a guided, step-by-step series of **exercises** and **incremental solutions** that gradually evolve from a simple MudBlazor front-end to a fully functional, layered case management system.

---

## 📚 Curriculum Overview

### Phase 1 — ASP.NET Core Fundamentals
You’ll start by designing the front-end layer using MudBlazor:
- Implementing consistent UI patterns with reusable components.
- Creating a `PageLayout` system.
- Handling date and formatting with a `DateTimeProvider`.
- Building pages (Person, Case) with mocked data.
- Adding navigation, policies, and localized enums.
- Creating fluent forms and reusable input components.

### Phase 2 — REST API and EF Core
You’ll then move on to backend and persistence topics:
- Setting up the database and repositories.
- Introducing CQRS, transactions, and handlers.
- Implementing interceptors for auditing and state tracking.
- Managing user context and shared services.
- Handling case approval/rejection workflows.

---

## 🧭 Folder Structure
```
DevoLearning/
│
├── README.md
├── NOTICE.txt
│
├── docs/
│ ├── ASPNET/ # Exercises for UI & front-end logic
│ ├── REST_API/ # Exercises for API, EF Core, CQRS
│ └── 99_FinalProject.md # Wrap-up and final evaluation
│
├── solutions/
│ ├── 00_StartingSolution/
│ ├── 01_Homepage/
│ ├── 02_PageLayoutComponent/
│ ├── ...
│ └── Final_Solution/
```
Each exercise in `/docs/` has a corresponding working project in `/solutions/` so you can follow along and compare your work.

---

## ⚙️ Requirements

- **.NET 8.0 SDK** or newer  
- **Visual Studio 2022**
- Basic understanding of C# syntax  
- Curiosity to explore architecture and design principles  

---

## 🚀 Getting Started
 
1. Download the solution from `solutions/00_StartingSolution/DevoLearning.zip`.  
2. Follow the first guide in `/docs/ASPNET/Exercise_01_Homepage.md`.  
3. Progress through exercises step-by-step — each introduces a new concept or pattern.  

---

## 💬 Purpose

This project aims to bridge the gap between *tutorial-level learning* and *real project experience* — by encouraging exploration, mistakes, and progressive understanding of .NET architecture.

---

## 📜 License & Usage

See [NOTICE.txt](./NOTICE.txt) for terms of use.  
In short: personal and educational use only.

---

> “The best way to understand architecture is to build it piece by piece — not just read about it.”  
> — *DevoLearning*
