using EdgeDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactDatabase.Pages
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        public Contact NewContact { get; set; }
        private readonly EdgeDBClient _client;
        public CreateModel(EdgeDBClient client)
        {
            _client = client;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var query = "INSERT Contact {first_name := <str>$first_name, last_name := <str>$last_name, email := <str>$email, title := <State>$title, description := <str>$description, date_of_birth := <datetime>$date_of_birth, marriage_status := <bool>$marriage_status}";
                await _client.ExecuteAsync(query, new Dictionary<string, object?>
                {
                    {"first_name", NewContact.FirstName},
                    {"last_name", NewContact.LastName},
                    {"email", NewContact.Email},
                    {"title", NewContact.Title},
                    {"description", NewContact.Description},
                    {"date_of_birth", NewContact.BirthDate},
                    {"marriage_status", NewContact.IsMarried}
                });
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
