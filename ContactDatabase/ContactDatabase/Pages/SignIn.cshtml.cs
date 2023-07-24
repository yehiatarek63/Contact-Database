using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using EdgeDB;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Data.SqlTypes;

namespace ContactDatabase.Pages;
[BindProperties]
public class SignInModel : PageModel
{
    public LoginInput UserLogin { get; set; }
    private readonly EdgeDBClient _client;
    public SignInModel(EdgeDBClient client)
    {
        _client = client;
    }
    public void OnGet()
    {
    }
    public async Task<IActionResult> OnPostSignIn()
    {
        if (ModelState.IsValid)
        {
            Contact? user = await AuthenticateUser(UserLogin, _client);
            if (user is not null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, UserLogin.Username),
                    new Claim(ClaimTypes.Role, user.Roles.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IssuedUtc = DateTimeOffset.UtcNow,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                return RedirectToPage("Index");
            }
        }
        ModelState.AddModelError("", "Invalid Login Attempt");
        return Page();
    }

    public static async Task<Contact?> AuthenticateUser(LoginInput user, EdgeDBClient client)
    {
        var saltQuery = "SELECT Contact {*} FILTER .username = <str>$username";
        var saltParameters = new Dictionary<string, object?>
        {
            { "username", user.Username }
        };
        Contact? userSalt = await client.QuerySingleAsync<Contact>(saltQuery, saltParameters);
        if (userSalt is not null)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                           password: user.Password,
                           salt: userSalt.Salt,
                           prf: KeyDerivationPrf.HMACSHA512,
                           iterationCount: 10000,
                           numBytesRequested: 256 / 8));
            var query = "SELECT Contact {*} FILTER .username = <str>$username AND .password = <str>$password";
            var parameters = new Dictionary<string, object?>
            {
                { "username", user.Username },
                { "password", hashed }
            };
            Contact? result = await client.QuerySingleAsync<Contact>(query, parameters);
            return result;
        }
        return null;
    }
}

public class LoginInput
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
}
