using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data;
using WebApp.Models;

namespace WebApp.Pages.Persons
{
    public class CreateModel : PageModel
    {
        private readonly Data.PersonContext _context;
        private readonly HttpClient _client;

        public CreateModel(Data.PersonContext context, HttpClient client)
        {
            _context = context;
            _client = client;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Person Person { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _client.PostAsJsonAsync<Person>("http://localhost:5165/api/PeopleAPI", Person);

            // _context.Person.Add(Person);
            // await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
