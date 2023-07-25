using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using EdgeDB;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Data.SqlTypes;
using Microsoft.AspNetCore.Identity;

namespace ContactDatabase.Pages;
[BindProperties]
public class SignInModel : PageModel
{
    public LoginInput UserLogin { get; set; }
    private readonly EdgeDBClient _client;
    private readonly IPasswordHasher<Contact> _passwordHasher;
    public SignInModel(EdgeDBClient client, IPasswordHasher<Contact> passwordHasher)
    {
        _client = client;
        _passwordHasher = passwordHasher;
    }
    public void OnGet()
    {
    }
    public async Task<IActionResult> OnPostSignIn()
    {
        if (ModelState.IsValid)
        {
            Contact? user = await AuthenticateUser(UserLogin, _client, _passwordHasher);
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

    public static async Task<Contact?> AuthenticateUser(LoginInput user, EdgeDBClient client, IPasswordHasher<Contact> passwordHasher)
    {
        var saltQuery = "SELECT Contact {*} FILTER .username = <str>$username";
        var saltParameters = new Dictionary<string, object?>
        {
            { "username", user.Username }
        };
        Contact? userSalt = await client.QuerySingleAsync<Contact>(saltQuery, saltParameters);
        if (userSalt is not null)
        {
            PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(userSalt, userSalt.Password, user.Password);
            if(result == PasswordVerificationResult.Success)
            {
                return userSalt;
            }
            else
            {
                return null;
            }
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
