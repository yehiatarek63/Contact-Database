using EdgeDB;
using static ContactDatabase.Contact;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactDatabase.Pages
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        public Contact EditContact { get; set; }
        private readonly EdgeDBClient _client;
        public CreateModel(EdgeDBClient client)
        {
            _client = client;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                byte[] salt = GenerateSalt();
                EditContact.Password = HashPassword(EditContact.Password, salt);
                var query = "INSERT Contact {first_name := <str>$first_name, last_name := <str>$last_name, email := <str>$email, title := <State>$title, description := <str>$description, date_of_birth := <datetime>$date_of_birth, marriage_status := <bool>$marriage_status, username := <str>$username, password := <str>$password, roles := <str>$roles, salt := <bytes>$salt}";
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
                    {"roles", EditContact.Roles},
                    {"salt", salt}
                });
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
