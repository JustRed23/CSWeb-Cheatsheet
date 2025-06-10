# Basic Authentication Example

## Setup
- Enable Blazor Server in your project by adding two lines to your `Program.cs`:
```csharp
// Under builder.Services.AddControllersWithViews();
builder.Services.AddServerSideBlazor();

// Under app.MapControllerRoute(...)
app.MapBlazorHub();
```
- Create a new blazor component in your root directory called `_Imports.razor` and add the following base imports:
```csharp
@using Microsoft.AspNetCore.Components
@using Microsoft.AspNetCore.Components.Forms
@using Microsoft.AspNetCore.Components.Routing
@using Microsoft.AspNetCore.Components.Web
@using Microsoft.JSInterop
```
(you can add your model and service namespaces here as well)
- Create a new folder in your root directory called `Blazor` and add a new folder inside it called `Manage`
- Create a new component in your `Blazor/Manage` directory called `Routed.razor` and add the following code:
```csharp
<Router AppAssembly="typeof(Program).Assembly">
    <Found>
        <RouteView RouteData="@context"></RouteView>
    </Found>
    <NotFound>
        <h4 class="bg-danger text-white text-center p-2">
            No matching route found
        </h4>
    </NotFound>
</Router>
```
- Create a new component in your `Views/Shared` directory called `_BlazorServer_Host.cshtml` and add the following code:
```csharp
@page "/"
@{
    Layout = null;
}
<!DOCTYPE html>
<html>
<head>
    <title>@ViewBag.Title</title>
    <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />
    <base href="~/" />
</head>
<body>
<div class="m-2">
    <component type="typeof(Blazor.Blazor.Manage.Routed)" render-mode="Server" />
</div>
<script src="_framework/blazor.server.js"></script>
</body>
</html>
```
(Blazor.Blazor.Manage.Routed will now point to the Routed component you created earlier)
- You can now call blazor from any controller you want, for this example we will use `BlazorController`:
```csharp
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Controllers;

public class BlazorController : Controller
{
    public IActionResult Index()
    {
        return View("_BlazorServer_Host");
    }
}
```
- You can now map a default route to this controller in your `Program.cs` under `app.MapBlazorHub()`:
```csharp
app.MapFallbackToController("/manage/{*path:nonfile}", "Index", "Blazor");
```