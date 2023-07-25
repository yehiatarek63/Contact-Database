using EdgeDB;
using static ContactDatabase.Contact;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;

namespace ContactDatabase.Pages
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        public Contact EditContact { get; set; }
        private readonly EdgeDBClient _client;
        private readonly IPasswordHasher<Contact> _passwordHasher;
        public CreateModel(EdgeDBClient client, IPasswordHasher<Contact> passwordHasher)
        {
            _client = client;
            _passwordHasher = passwordHasher;

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                EditContact.Password = _passwordHasher.HashPassword(EditContact, EditContact.Password);
                var query = "INSERT Contact {first_name := <str>$first_name, last_name := <str>$last_name, email := <str>$email, title := <State>$title, description := <str>$description, date_of_birth := <datetime>$date_of_birth, marriage_status := <bool>$marriage_status, username := <str>$username, password := <str>$password, roles := <str>$roles}";
                await _client.ExecuteAsync(query, new Dictionary<string, object?>
                {
                    {"first_name", EditContact.FirstName},
                    {"last_name", EditContact.LastName},
                    {"email", EditContact.Email},
                    {"title", EditContact.Title},
                    {"description", EditContact.Description},
                    {"date_of_birth", EditContact.DateOfBirth},
                    {"marriage_status", EditContact.MarriageStatus},
                    {"username", EditContact.Username},
                    {"password", EditContact.Password},
                    {"roles", EditContact.Roles}
                });
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
