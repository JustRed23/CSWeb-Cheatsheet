# Basic Authentication Example

## Requirements
| Package Name                                      | Version |
|---------------------------------------------------|---------|
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | 8.0.15  |
| Microsoft.EntityFrameworkCore.Tools               | 8.0.15  |
| Microsoft.EntityFrameworkCore.SqlServer           | 8.0.15  |
| Microsoft.VisualStudio.Web.CodeGeneration.Design  | 8.0.7   |

<b>Make sure to also have a DefaultConnection string in your `appsettings.json` file:</b>
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BasicAuthDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

## Setup
- Create a Data folder with your DbContext class, extend `IdentityDbContext`
- Add the DbContext to the service collection in `Program.cs`:
```csharp
// Db Context
builder.Services.AddDbContext<BasicAuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<BasicAuthDbContext>();
```
- Create a migration for the added identity tables and update the database
- Create your login and register view models
- Create an `AccountController` with actions for Login and Register
- Create views based on these models
```bash
# dotnet tool install -g dotnet-aspnet-codegenerator
dotnet aspnet-codegenerator view Login Create -outDir Views\Account -m LoginViewModel
dotnet aspnet-codegenerator view Register Create -outDir Views\Account -m RegisterViewModel
```
- Create a `_LoginPartial.cshtml` which controls the navbar buttons based on the user's authentication state
- Add the `_LoginPartial` to your `_Layout.cshtml` file