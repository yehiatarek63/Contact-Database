using EdgeDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactDatabase.Pages
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly EdgeDBClient _client;
        public Contact EditContact { get; set; }
        public EditModel(EdgeDBClient client)
        {
            _client = client;
        }
        public async void OnGetAsync(Guid? Id)
        {
            if (User.IsInRole("Admin"))
            {
                if (Id is not null)
                {
                    EditContact = await _client.QuerySingleAsync<Contact>("SELECT Contact{*} FILTER .id = <uuid>$id", new Dictionary<string, object?> { { "id", Id } });
                }
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var query = "UPDATE Contact FILTER .id = <uuid>$id SET {first_name := <str>$first_name, last_name := <str>$last_name, email := <str>$email, title := <State>$title, description := <str>$description, date_of_birth := <datetime>$date_of_birth, marriage_status := <bool>$marriage_status}";
                await _client.ExecuteAsync(query, new Dictionary<string, object?>
                {
                    {"id", EditContact.Id},
                    {"first_name", EditContact.FirstName},
                    {"last_name", EditContact.LastName},
                    {"email", EditContact.Email},
                    {"title", EditContact.Title},
                    {"description", EditContact.Description},
                    {"date_of_birth", EditContact.DateOfBirth},
                    {"marriage_status", EditContact.MarriageStatus}
                });
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
