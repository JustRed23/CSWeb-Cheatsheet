# Basic Authentication Example

## Requirements
| Package Name                                      | Version |
|---------------------------------------------------|---------|
| Microsoft.AspNetCore.Authentication.Facebook      | 8.0.15  |
| Microsoft.AspNetCore.Authentication.Google        | 8.0.15  |
| Microsoft.AspNetCore.Authentication.OpenIdConnect | 8.0.15  |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | 8.0.15  |
| Microsoft.EntityFrameworkCore.Tools               | 8.0.15  |
| Microsoft.EntityFrameworkCore.SqlServer           | 8.0.15  |
| Microsoft.VisualStudio.Web.CodeGeneration.Design  | 8.0.7   |

<b>Make sure to also have a DefaultConnection string in your `appsettings.json` file:</b>
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ExternalAuthDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

## Setup
- Follow the steps from the basic auth example, using the correct db context and connection string.
- Add authentication services to your `Program.cs`:
```csharp
builder.Services.AddAuthentication()
    .AddFacebook(fbOpts =>
    {
        fbOpts.AppId = "";
        fbOpts.AppSecret = "";
    })
    .AddGoogle(options =>
    {
        options.ClientId = "";
        options.ClientSecret = "";
        options.SignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:5001"; //Duende https port, check duende launch settings
        options.ClientId = "mvc";
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        options.SaveTokens = true;
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("email");
        options.GetClaimsFromUserInfoEndpoint = true;
    });

// Above app.UseAuthorization();
app.UseAuthentication();
```
- Create three new link in your `_LoginPartial.cshtml` file:
```html
<a class="nav-link text-dark" asp-controller="Account" asp-action="FacebookLogin">Facebook Login</a>
<a class="nav-link text-dark" asp-controller="Account" asp-action="GoogleLogin">Google Login</a>
<a class="nav-link text-dark" asp-controller="Account" asp-action="ExternalLogin">External Login</a>
```
- Implement them in your `AccountController`