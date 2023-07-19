using EdgeDB;
using Htmx;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactDatabase.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly EdgeDBClient _client;
        public List<Contact> ContactList { get; set; } = new();
        public List<Contact> Results { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public string? FirstName { get; set; } = "";
        [BindProperty(SupportsGet = true)]
        public string? LastName { get; set; } = "";
        [BindProperty(SupportsGet = true)]
        public string? Email { get; set; } = "";
        public IndexModel(EdgeDBClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ContactList = (await _client.QueryAsync<Contact>("SELECT Contact {*}")).ToList();
            if (string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(LastName) && string.IsNullOrEmpty(Email))
            {
                Results = ContactList;
            }
            else
            {
                var searchQuery = "SELECT Contact {*}" +
                   "FILTER .first_name ILIKE '%' ++ <str>$first_name ++ '%' AND" +
                   ".last_name ILIKE '%' ++ <str>$last_name ++ '%' AND" +
                   ".email ILIKE '%' ++ <str>$email ++ '%'";
                Results = (await _client.QueryAsync<Contact>(searchQuery, new Dictionary<string, object?>
                {
                    { "first_name", FirstName},
                    { "last_name", LastName },
                    { "email", Email }
                })).ToList();
            }
            if (!Request.IsHtmx())
                return Page();
            return Partial("_Results", this);
        }
    }
}