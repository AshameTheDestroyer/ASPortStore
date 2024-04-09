# Things That I Still Totally Do Not Understand in ASP.NET Core
<hr />

## Program.cs
```csharp
builder.Services.AddScoped<T, U>();
builder.Services.AddScoped(factory);
```
```csharp
builder.Services.AddDistributedMemoryCache();
```
```csharp
builder.Services.AddSingleton<T, U>();
```
```csharp
builder.Services.AddSession();
app.UseSession();
```
<hr />

## Database & Stuff
```csharp
private readonly StoreDBContext context;
public IQueryable<T> QueryableCollection => context.Collection;
```
<hr />

## Controllers
```csharp
public ViewResult Route<T>(params T[] args) {}
public IActionResult Route<T>(params T[] args) {}
```
```csharp
ViewBag.DynamicProperty = SomeValue;
```
<hr />

## ASP Attributes
```html
<input asp-for="Property" />
<div asp-validation-summary="All" />
<form asp-action="Action" />
<a asp-controller="Controller" />
```
<hr />

## Unit Testing
```csharp
using Moq;
using XUnit;
```
```csharp
[Fact]
public void CanPassTest() {}
```
<hr />

## View Components
```csharp
public IViewComponentResult Invoke() {}
```
<hr />

## LINQ Extension Methods
```csharp
context.AttachRange(collection);
```
```csharp
collection1
    .Include(collection => collection2)
    .ThenInclude(collection => collection3);
```
<hr />

## Razor Pages
```html
@page PageName
@model ModelName
```
```html
@functions {
    [BindProperty(SupportsGet = true)]
    public T? Property { get; set; }
}
```