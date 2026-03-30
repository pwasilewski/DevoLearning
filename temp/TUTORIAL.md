# Building a CRUD API with Navigation, Service Layer & Unit Tests

## Introduction

In this exercise you will build a small .NET 8 Web API from scratch on top of an existing solution.  
The goal is to implement a **Product & Category** CRUD API that demonstrates:

- Two related entities with a navigation property (one-to-many)
- A clean layered architecture: Repository → Service → Controller
- Manual validation and custom exceptions
- `AsNoTracking` in Entity Framework Core
- Unit tests following the **Arrange-Act-Assert** pattern with mocking

> **Reference:** The existing `Employee` CRUD implementation in this solution is your reference. Study how the `EmployeesController`, `IEmployeeRepository`, `EmployeeRepository` and the DTOs are structured before you start.

---

## Solution Structure

Before starting, familiarise yourself with the structure you are going to build:

```
EmployeeAdminPortal/
├── Controllers/
│   ├── CategoriesController.cs       ← to create
│   └── ProductsController.cs         ← to create
├── Data/
│   └── ApplicationDbContext.cs       ← to modify
├── Exceptions/
│   ├── EntityNotFoundException.cs    ← to create
│   └── ValidationException.cs        ← to create
├── Models/
│   ├── Entities/
│   │   ├── Category.cs               ← to create
│   │   ├── CategoryConfiguration.cs  ← to create
│   │   ├── Product.cs                ← to create
│   │   └── ProductConfiguration.cs   ← to create
│   ├── AddCategoryDto.cs             ← to create
│   ├── AddProductDto.cs              ← to create
│   ├── UpdateCategoryDto.cs          ← to create
│   └── UpdateProductDto.cs           ← to create
├── Repositories/
│   ├── ICategoryRepository.cs        ← to create
│   ├── CategoryRepository.cs         ← to create
│   ├── IProductRepository.cs         ← to create
│   └── ProductRepository.cs          ← to create
├── Services/
│   ├── ICategoryService.cs           ← to create
│   ├── CategoryService.cs            ← to create
│   ├── IProductService.cs            ← to create
│   └── ProductService.cs             ← to create
└── Program.cs                        ← to modify

EmployeeAdminPortal.Unit.Tests/
├── CategoryServiceTests.cs           ← to create
└── ProductServiceTests.cs            ← to create
```

---

## Step 1 — Create the Folder Structure

Create the following folders inside `EmployeeAdminPortal/`:

- `Exceptions/`
- `Models/Entities/` (may already exist)
- `Repositories/`
- `Services/`

> You can create folders directly in Visual Studio Solution Explorer by right-clicking the project → **Add** → **New Folder**.

---

## Step 2 — Database Setup

Before writing any C# code, create the database tables by running the following script directly in **SQL Server Management Studio** (or Azure Data Studio).

Open a new query window against your local database and execute:

```sql
CREATE TABLE Category (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE Product (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    CategoryId INT NOT NULL,
    CONSTRAINT FK_Product_Category FOREIGN KEY (CategoryId) REFERENCES Category(Id)
);
```

> Notice the foreign key constraint: a `Product` must belong to an existing `Category`. This constraint is enforced at the database level and we will also enforce it in the service layer.

---

## Step 3 — Create the Entities

Entities are plain C# classes that map to database tables. EF Core uses them to generate queries and track changes.

**Create** `Models/Entities/Category.cs`:

```csharp
namespace EmployeeAdminPortal.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
```

**Create** `Models/Entities/Product.cs`:

```csharp
namespace EmployeeAdminPortal.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
```

### Key concepts

- `Category.Products` is a **navigation property** — it lets EF Core load related products automatically.
- `Product.Category` is the **inverse navigation property** and `Product.CategoryId` is the **foreign key**.
- The `?` on `Category?` means the navigation can be `null` when not explicitly loaded.

---

## Step 4 — Table Configuration (Fluent API)

EF Core lets you configure the database mapping using the **Fluent API**, which is the recommended approach over data annotations for complex configurations.

**Create** `Models/Entities/CategoryConfiguration.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeAdminPortal.Models.Entities
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.HasIndex(c => c.Name).IsUnique();
        }
    }
}
```

**Create** `Models/Entities/ProductConfiguration.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeAdminPortal.Models.Entities
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Price).IsRequired();
            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
```

### Key concepts

- `HasOne(...).WithMany(...).HasForeignKey(...)` explicitly defines the one-to-many relationship.
- `OnDelete(DeleteBehavior.Cascade)` means deleting a `Category` will also delete all its `Products`.
- `HasIndex(...).IsUnique()` enforces uniqueness on the category name at the EF Core level.

---

## Step 5 — Add DbSets to ApplicationDbContext

Open `Data/ApplicationDbContext.cs` and add the `DbSet` properties and apply the configurations in `OnModelCreating`:

```csharp
using EmployeeAdminPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }       // ← add
        public DbSet<Category> Categories { get; set; }    // ← add

        protected override void OnModelCreating(ModelBuilder modelBuilder)  // ← add
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}
```

> `ApplyConfiguration` reads the `IEntityTypeConfiguration` classes you created in Step 4 and applies them to the model builder.

---

## Step 6 — Repository Layer (without AsNoTracking)

The repository pattern abstracts the data access logic behind an interface. This makes the code easier to test and maintain.

### 6.1 — Interfaces

**Create** `Repositories/ICategoryRepository.cs`:

```csharp
using EmployeeAdminPortal.Models.Entities;

namespace EmployeeAdminPortal.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(int id);
        Task<List<Category>> GetAllAsync();
        Task SaveAsync(Category category);
        Task DeleteAsync(int id);
        Task<bool> NameExistsAsync(string name);
    }
}
```

**Create** `Repositories/IProductRepository.cs`:

```csharp
using EmployeeAdminPortal.Models.Entities;

namespace EmployeeAdminPortal.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task SaveAsync(Product product);
        Task DeleteAsync(int id);
    }
}
```

> Note: `SaveAsync` handles both insert and update. When `Id == 0` the entity is new; otherwise it is an update. The service layer decides the intent; the repository just persists.

### 6.2 — Eager Loading vs Lazy Loading

When you load an entity that has navigation properties (like `Category.Products`), EF Core does **not** load the related data automatically. You have to choose a strategy.

**Eager loading** — load the related data in the same database query using `.Include()`:

```csharp
// One query — returns categories AND their products together
return await _context.Categories
    .Include(c => c.Products)
    .ToListAsync();
```

SQL generated:
```sql
SELECT c.*, p.*
FROM Category c
LEFT JOIN Product p ON p.CategoryId = c.Id
```

**Lazy loading** — related data is loaded automatically the first time you access the navigation property, triggering a *separate* query per entity. This requires additional EF Core packages and proxies, and can cause the **N+1 problem**: loading 100 categories would fire 101 queries (1 for categories + 1 per category for its products).

```csharp
// Lazy loading — Products query fires lazily when accessed (NOT what we use here)
var categories = await _context.Categories.ToListAsync();
foreach (var cat in categories)
{
    var count = cat.Products.Count; // ← triggers a new SQL query per category!
}
```

**In this project we use eager loading exclusively** via `.Include()`. It is predictable, explicit, and avoids hidden performance problems.

### 6.3 — Implementations

**Create** `Repositories/CategoryRepository.cs`:

```csharp
using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories
                .Include(c => c.Products)
                .ToListAsync();
        }

        public async Task SaveAsync(Category category)
        {
            if (category.Id == 0)
            {
                _context.Categories.Add(category);
            }
            else
            {
                _context.Categories.Update(category);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> NameExistsAsync(string name)
        {
            return await _context.Categories
                .AnyAsync(c => c.Name.ToLower() == name.ToLower());
        }
    }
}
```

**Create** `Repositories/ProductRepository.cs`:

```csharp
using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task SaveAsync(Product product)
        {
            if (product.Id == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                _context.Products.Update(product);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
```

### 6.4 — Register repositories in Program.cs

Open `Program.cs` and add after the existing `IEmployeeRepository` registration:

```csharp
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
```

---

## Step 7 — AsNoTracking Deep Dive

### What is EF Core tracking?

By default, every entity loaded via EF Core is **tracked** by the `DbContext`. This means EF Core keeps a snapshot of the entity in memory and uses it to detect changes when `SaveChangesAsync()` is called.

This is useful for update operations, but **has a cost**: memory overhead and CPU time for change detection — especially for large result sets.

### When to use AsNoTracking

Use `AsNoTracking()` for **read-only queries** where you don't intend to modify the entity and save it back.

| Scenario | Use tracking? |
|---|---|
| Loading data to display in a list | ❌ Use `AsNoTracking` |
| Loading an entity to update it | ✅ Use tracking |
| Loading an entity to return as API response | ❌ Use `AsNoTracking` |

### How to use it

Add `.AsNoTracking()` to your read queries in the repository:

```csharp
// Without AsNoTracking — entity is tracked by the DbContext (default)
return await _context.Products
    .Include(p => p.Category)
    .FirstOrDefaultAsync(p => p.Id == id);

// With AsNoTracking — entity is NOT tracked (read-only, faster)
return await _context.Products
    .Include(p => p.Category)
    .AsNoTracking()
    .FirstOrDefaultAsync(p => p.Id == id);
```

### Update your repositories

Update **all read methods** (`GetByIdAsync` and `GetAllAsync`) in both `ProductRepository` and `CategoryRepository` to use `AsNoTracking()`:

```csharp
public async Task<Product?> GetByIdAsync(int id)
{
    return await _context.Products
        .Include(p => p.Category)
        .AsNoTracking()
        .FirstOrDefaultAsync(p => p.Id == id);
}

public async Task<List<Product>> GetAllAsync()
{
    return await _context.Products
        .Include(p => p.Category)
        .AsNoTracking()
        .ToListAsync();
}
```

> ⚠️ Do **not** add `AsNoTracking` to `SaveAsync` or `DeleteAsync` — those methods rely on EF Core tracking to function correctly.

---

## Step 8 — Custom Exceptions

Instead of returning raw status codes from deep in the code, we use custom exceptions to express business intent. These are thrown in the service layer and caught at the controller level.

**Create** `Exceptions/EntityNotFoundException.cs`:

```csharp
using System;

namespace EmployeeAdminPortal.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message) { }
    }
}
```

**Create** `Exceptions/ValidationException.cs`:

```csharp
using System;

namespace EmployeeAdminPortal.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }
}
```

---

## Step 9 — DTOs (Data Transfer Objects)

DTOs are lightweight objects used to transfer data between layers. They decouple the API contract from the internal entity model.

- **Add DTOs** contain only the fields needed to create an entity (no `Id`).
- **Update DTOs** include an `Id` to identify which entity to update.

**Create** `Models/AddCategoryDto.cs`:

```csharp
namespace EmployeeAdminPortal.Models
{
    public class AddCategoryDto
    {
        public string Name { get; set; } = string.Empty;
    }
}
```

**Create** `Models/UpdateCategoryDto.cs`:

```csharp
namespace EmployeeAdminPortal.Models
{
    public class UpdateCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
```

**Create** `Models/AddProductDto.cs`:

```csharp
namespace EmployeeAdminPortal.Models
{
    public class AddProductDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
```

**Create** `Models/UpdateProductDto.cs`:

```csharp
namespace EmployeeAdminPortal.Models
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
```

---

## Step 10 — Service Layer: AddAsync

The service layer contains the **business logic**: validation, orchestration between repositories, and exception throwing. Controllers stay thin and only handle HTTP concerns.

### 10.1 — Interfaces

**Create** `Services/ICategoryService.cs`:

```csharp
using EmployeeAdminPortal.Models.Entities;

namespace EmployeeAdminPortal.Services
{
    public interface ICategoryService
    {
        Task<Category> GetByIdAsync(int id);
        Task<List<Category>> GetAllAsync();

        // Validation:
        // - Name must not be null/empty and must be at least 3 characters
        // - Name must be unique (case-insensitive) — throw ValidationException if it already exists
        Task AddAsync(Models.AddCategoryDto dto);

        // Validation:
        // - Name must not be null/empty and must be at least 3 characters
        // - Category with the given Id must exist — throw EntityNotFoundException if not
        // - If the name has changed, it must still be unique (case-insensitive) — throw ValidationException if taken
        Task UpdateAsync(Models.UpdateCategoryDto dto);

        // - Category with the given Id must exist — throw EntityNotFoundException if not
        Task DeleteAsync(int id);
    }
}
```

**Create** `Services/IProductService.cs`:

```csharp
using EmployeeAdminPortal.Models.Entities;

namespace EmployeeAdminPortal.Services
{
    public interface IProductService
    {
        Task<Product> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();

        // Validation:
        // - Name must not be null/empty and must be at least 3 characters
        // - Price must be greater than 0 and less than 10,000
        // - CategoryId must refer to an existing Category — throw ValidationException if not found
        Task AddAsync(Models.AddProductDto dto);

        // Validation:
        // - Name must not be null/empty and must be at least 3 characters
        // - Price must be greater than 0 and less than 10,000
        // - CategoryId must refer to an existing Category — throw ValidationException if not found
        // - Product with the given Id must exist — throw EntityNotFoundException if not
        Task UpdateAsync(Models.UpdateProductDto dto);

        // - Product with the given Id must exist — throw EntityNotFoundException if not
        Task DeleteAsync(int id);
    }
}
```

### 10.2 — CategoryService with AddAsync

**Create** `Services/CategoryService.cs` with only `AddAsync` implemented for now:

```csharp
using EmployeeAdminPortal.Exceptions;
using EmployeeAdminPortal.Models.Entities;
using EmployeeAdminPortal.Repositories;

namespace EmployeeAdminPortal.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new EntityNotFoundException($"Category with id {id} not found.");
            }
            return category;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task AddAsync(Models.AddCategoryDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || dto.Name.Length < 3)
            {
                throw new ValidationException("Category name must be at least 3 characters.");
            }
            if (await _categoryRepository.NameExistsAsync(dto.Name))
            {
                throw new ValidationException($"Category name '{dto.Name}' already exists.");
            }
            var category = new Category { Name = dto.Name };
            await _categoryRepository.SaveAsync(category);
        }

        // To be implemented in Step 11
        public Task UpdateAsync(Models.UpdateCategoryDto dto) => throw new NotImplementedException();
        public Task DeleteAsync(int id) => throw new NotImplementedException();
    }
}
```

### 10.3 — ProductService with AddAsync

**Create** `Services/ProductService.cs` with only `AddAsync` implemented for now:

```csharp
using EmployeeAdminPortal.Exceptions;
using EmployeeAdminPortal.Models.Entities;
using EmployeeAdminPortal.Repositories;

namespace EmployeeAdminPortal.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new EntityNotFoundException($"Product with id {id} not found.");
            }
            return product;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task AddAsync(Models.AddProductDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || dto.Name.Length < 3)
            {
                throw new ValidationException("Product name must be at least 3 characters.");
            }
            if (dto.Price <= 0 || dto.Price >= 10000)
            {
                throw new ValidationException("Product price must be greater than 0 and less than 10,000.");
            }
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
            if (category == null)
            {
                throw new ValidationException($"Category with id {dto.CategoryId} does not exist.");
            }
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                CategoryId = dto.CategoryId
            };
            await _productRepository.SaveAsync(product);
        }

        // To be implemented in Step 11
        public Task UpdateAsync(Models.UpdateProductDto dto) => throw new NotImplementedException();
        public Task DeleteAsync(int id) => throw new NotImplementedException();
    }
}
```

### 10.4 — Register services in Program.cs

Add the following lines after the repository registrations in `Program.cs`:

```csharp
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
```

---

## Step 11 — Service Layer: UpdateAsync & DeleteAsync

Replace the `NotImplementedException` stubs with full implementations.

### CategoryService

```csharp
public async Task UpdateAsync(Models.UpdateCategoryDto dto)
{
    if (string.IsNullOrWhiteSpace(dto.Name) || dto.Name.Length < 3)
    {
        throw new ValidationException("Category name must be at least 3 characters.");
    }
    var existing = await _categoryRepository.GetByIdAsync(dto.Id);
    if (existing == null)
    {
        throw new EntityNotFoundException($"Category with id {dto.Id} not found.");
    }
    // Only check for name uniqueness if the name has changed
    if (!string.Equals(existing.Name, dto.Name, StringComparison.OrdinalIgnoreCase)
        && await _categoryRepository.NameExistsAsync(dto.Name))
    {
        throw new ValidationException($"Category name '{dto.Name}' already exists.");
    }
    existing.Name = dto.Name;
    await _categoryRepository.SaveAsync(existing);
}

public async Task DeleteAsync(int id)
{
    var category = await _categoryRepository.GetByIdAsync(id);
    if (category == null)
    {
        throw new EntityNotFoundException($"Category with id {id} not found.");
    }
    await _categoryRepository.DeleteAsync(id);
}
```

### ProductService

```csharp
public async Task UpdateAsync(Models.UpdateProductDto dto)
{
    if (string.IsNullOrWhiteSpace(dto.Name) || dto.Name.Length < 3)
    {
        throw new ValidationException("Product name must be at least 3 characters.");
    }
    if (dto.Price <= 0 || dto.Price >= 10000)
    {
        throw new ValidationException("Product price must be greater than 0 and less than 10,000.");
    }
    var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
    if (category == null)
    {
        throw new ValidationException($"Category with id {dto.CategoryId} does not exist.");
    }
    var existing = await _productRepository.GetByIdAsync(dto.Id);
    if (existing == null)
    {
        throw new EntityNotFoundException($"Product with id {dto.Id} not found.");
    }
    existing.Name = dto.Name;
    existing.Price = dto.Price;
    existing.CategoryId = dto.CategoryId;
    await _productRepository.SaveAsync(existing);
}

public async Task DeleteAsync(int id)
{
    var product = await _productRepository.GetByIdAsync(id);
    if (product == null)
    {
        throw new EntityNotFoundException($"Product with id {id} not found.");
    }
    await _productRepository.DeleteAsync(id);
}
```

---

## Step 12 — Controllers

Controllers handle HTTP requests and delegate all logic to the service layer. They catch custom exceptions and translate them into appropriate HTTP responses.

**Create** `Controllers/CategoriesController.cs`:

```csharp
using EmployeeAdminPortal.Exceptions;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using EmployeeAdminPortal.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAll()
        {
            return Ok(await _categoryService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            try
            {
                return Ok(await _categoryService.GetByIdAsync(id));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AddCategoryDto dto)
        {
            try
            {
                await _categoryService.AddAsync(dto);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateCategoryDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("Id mismatch.");
            }
            try
            {
                await _categoryService.UpdateAsync(dto);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
```

**Create** `Controllers/ProductsController.cs`:

```csharp
using EmployeeAdminPortal.Exceptions;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using EmployeeAdminPortal.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAll()
        {
            return Ok(await _productService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            try
            {
                return Ok(await _productService.GetByIdAsync(id));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AddProductDto dto)
        {
            try
            {
                await _productService.AddAsync(dto);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateProductDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("Id mismatch.");
            }
            try
            {
                await _productService.UpdateAsync(dto);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
```

### HTTP Status Code Reference

| HTTP Status | When | Method used |
|---|---|---|
| `200 OK` | Successful GET or POST | `Ok(...)` |
| `204 No Content` | Successful PUT or DELETE | `NoContent()` |
| `400 Bad Request` | Validation failed or id mismatch | `BadRequest(...)` |
| `404 Not Found` | Entity does not exist | `NotFound(...)` |

---

## Step 13 — Unit Tests (Arrange-Act-Assert)

### 13.1 — What is Arrange-Act-Assert?

Every unit test is structured in three clearly separated phases:

| Phase | Purpose |
|---|---|
| **Arrange** | Set up test data and configure mock behaviour |
| **Act** | Call the method under test |
| **Assert** | Verify the expected outcome |

### 13.2 — What is Moq?

Moq allows you to create **fake implementations** of interfaces. Instead of hitting a real database, you control exactly what the repository returns. This isolates the service logic from infrastructure.

```csharp
// Create a mock of ICategoryRepository
var mock = new Mock<ICategoryRepository>();

// Configure: when GetByIdAsync(1) is called, return a specific Category
mock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Category { Id = 1, Name = "Electronics" });

// Verify: SaveAsync was called exactly once with any Category argument
mock.Verify(r => r.SaveAsync(It.IsAny<Category>()), Times.Once);
```

### 13.3 — ProductService Tests

**Create** `ProductServiceTests.cs` in the test project:

```csharp
using System.Threading.Tasks;
using EmployeeAdminPortal.Exceptions;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using EmployeeAdminPortal.Repositories;
using EmployeeAdminPortal.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EmployeeAdminPortal.Unit.Tests
{
    [TestClass]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _productRepoMock = null!;
        private Mock<ICategoryRepository> _categoryRepoMock = null!;
        private ProductService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _productRepoMock = new Mock<IProductRepository>();
            _categoryRepoMock = new Mock<ICategoryRepository>();
            _service = new ProductService(_productRepoMock.Object, _categoryRepoMock.Object);
        }

        [TestMethod]
        public async Task AddAsync_ValidProduct_AddsProduct()
        {
            // Arrange
            var dto = new AddProductDto { Name = "Test", Price = 10, CategoryId = 1 };
            _categoryRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Category { Id = 1, Name = "Cat" });

            // Act
            await _service.AddAsync(dto);

            // Assert
            _productRepoMock.Verify(r => r.SaveAsync(It.Is<Product>(p =>
                p.Name == dto.Name &&
                p.Price == dto.Price &&
                p.CategoryId == dto.CategoryId)), Times.Once);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("ab")]
        public async Task AddAsync_InvalidName_ThrowsValidationException(string name)
        {
            // Arrange
            var dto = new AddProductDto { Name = name, Price = 10, CategoryId = 1 };

            // Act & Assert
            try
            {
                await _service.AddAsync(dto);
                Assert.Fail("Expected ValidationException was not thrown.");
            }
            catch (ValidationException) { }
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(10000)]
        [DataRow(10001)]
        public async Task AddAsync_InvalidPrice_ThrowsValidationException(double price)
        {
            // Arrange
            var dto = new AddProductDto { Name = "Test", Price = (decimal)price, CategoryId = 1 };

            // Act & Assert
            try
            {
                await _service.AddAsync(dto);
                Assert.Fail("Expected ValidationException was not thrown.");
            }
            catch (ValidationException) { }
        }

        [TestMethod]
        public async Task AddAsync_NonExistentCategory_ThrowsValidationException()
        {
            // Arrange
            var dto = new AddProductDto { Name = "Test", Price = 10, CategoryId = 99 };
            _categoryRepoMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Category?)null);

            // Act & Assert
            try
            {
                await _service.AddAsync(dto);
                Assert.Fail("Expected ValidationException was not thrown.");
            }
            catch (ValidationException) { }
        }

        [TestMethod]
        public async Task UpdateAsync_ValidProduct_UpdatesProduct()
        {
            // Arrange
            var dto = new UpdateProductDto { Id = 1, Name = "Test", Price = 10, CategoryId = 1 };
            _categoryRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Category { Id = 1, Name = "Cat" });
            _productRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Product { Id = 1, Name = "Old", Price = 5, CategoryId = 1 });

            // Act
            await _service.UpdateAsync(dto);

            // Assert
            _productRepoMock.Verify(r => r.SaveAsync(It.Is<Product>(p =>
                p.Id == dto.Id &&
                p.Name == dto.Name &&
                p.Price == dto.Price &&
                p.CategoryId == dto.CategoryId)), Times.Once);
        }

        [TestMethod]
        public async Task UpdateAsync_NonExistentProduct_ThrowsEntityNotFoundException()
        {
            // Arrange — category must exist because it is validated before product existence
            var dto = new UpdateProductDto { Id = 1, Name = "Test", Price = 10, CategoryId = 1 };
            _categoryRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Category { Id = 1, Name = "Cat" });
            _productRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Product?)null);

            // Act & Assert
            try
            {
                await _service.UpdateAsync(dto);
                Assert.Fail("Expected EntityNotFoundException was not thrown.");
            }
            catch (EntityNotFoundException) { }
        }
    }
}
```

### 13.4 — CategoryService Tests

**Create** `CategoryServiceTests.cs` in the test project:

```csharp
using System.Threading.Tasks;
using EmployeeAdminPortal.Exceptions;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using EmployeeAdminPortal.Repositories;
using EmployeeAdminPortal.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EmployeeAdminPortal.Unit.Tests
{
    [TestClass]
    public class CategoryServiceTests
    {
        private Mock<ICategoryRepository> _categoryRepoMock = null!;
        private CategoryService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _categoryRepoMock = new Mock<ICategoryRepository>();
            _service = new CategoryService(_categoryRepoMock.Object);
        }

        [TestMethod]
        public async Task AddAsync_ValidCategory_AddsCategory()
        {
            // Arrange
            var dto = new AddCategoryDto { Name = "Cat" };
            _categoryRepoMock.Setup(r => r.NameExistsAsync("Cat")).ReturnsAsync(false);

            // Act
            await _service.AddAsync(dto);

            // Assert
            _categoryRepoMock.Verify(r => r.SaveAsync(It.Is<Category>(c => c.Name == dto.Name)), Times.Once);
        }

        [TestMethod]
        public async Task AddAsync_DuplicateName_ThrowsValidationException()
        {
            // Arrange
            var dto = new AddCategoryDto { Name = "Cat" };
            _categoryRepoMock.Setup(r => r.NameExistsAsync("Cat")).ReturnsAsync(true);

            // Act & Assert
            try
            {
                await _service.AddAsync(dto);
                Assert.Fail("Expected ValidationException was not thrown.");
            }
            catch (ValidationException) { }
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("ab")]
        public async Task AddAsync_InvalidName_ThrowsValidationException(string name)
        {
            // Arrange
            var dto = new AddCategoryDto { Name = name };

            // Act & Assert
            try
            {
                await _service.AddAsync(dto);
                Assert.Fail("Expected ValidationException was not thrown.");
            }
            catch (ValidationException) { }
        }

        [TestMethod]
        public async Task UpdateAsync_ValidCategory_UpdatesCategory()
        {
            // Arrange
            var dto = new UpdateCategoryDto { Id = 1, Name = "NewCat" };
            var existing = new Category { Id = 1, Name = "OldCat" };
            _categoryRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);
            _categoryRepoMock.Setup(r => r.NameExistsAsync("NewCat")).ReturnsAsync(false);

            // Act
            await _service.UpdateAsync(dto);

            // Assert
            _categoryRepoMock.Verify(r => r.SaveAsync(It.Is<Category>(c =>
                c.Id == dto.Id &&
                c.Name == dto.Name)), Times.Once);
        }

        [TestMethod]
        public async Task UpdateAsync_NonExistentCategory_ThrowsEntityNotFoundException()
        {
            // Arrange
            var dto = new UpdateCategoryDto { Id = 1, Name = "Cat" };
            _categoryRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Category?)null);

            // Act & Assert
            try
            {
                await _service.UpdateAsync(dto);
                Assert.Fail("Expected EntityNotFoundException was not thrown.");
            }
            catch (EntityNotFoundException) { }
        }

        [TestMethod]
        public async Task UpdateAsync_DuplicateName_ThrowsValidationException()
        {
            // Arrange
            var dto = new UpdateCategoryDto { Id = 1, Name = "Cat" };
            var existing = new Category { Id = 1, Name = "OldCat" };
            _categoryRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);
            _categoryRepoMock.Setup(r => r.NameExistsAsync("Cat")).ReturnsAsync(true);

            // Act & Assert
            try
            {
                await _service.UpdateAsync(dto);
                Assert.Fail("Expected ValidationException was not thrown.");
            }
            catch (ValidationException) { }
        }
    }
}
```

### 13.5 — Run the tests

Open the **Test Explorer** panel in Visual Studio:

> **View** → **Test Explorer** (or `Ctrl+E, T`)

Click **Run All Tests** (the double-play button at the top of the panel). All tests should appear green.

If a test fails, click on it in the Test Explorer to see the failure message and the exact assertion that did not hold.

---

## Summary

You have built a complete .NET 8 Web API with:

| Layer | Responsibility |
|---|---|
| **Entities** | Map to database tables, define relationships |
| **Fluent API Config** | Configure constraints and relationships without attributes |
| **Repositories** | Abstract data access; use `AsNoTracking` for read-only queries |
| **Custom Exceptions** | Express business errors clearly and consistently |
| **DTOs** | Decouple API contracts from domain entities |
| **Services** | Contain business logic, validation, and exception throwing |
| **Controllers** | Handle HTTP, delegate to service, map exceptions to status codes |
| **Unit Tests** | Verify service logic in isolation using mocks and AAA pattern |
