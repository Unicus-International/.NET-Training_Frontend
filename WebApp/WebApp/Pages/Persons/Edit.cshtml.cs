using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using WebApp.Models;

namespace WebApp.Pages.Persons
{
    public class EditModel : PageModel
    {
        private readonly Data.PersonContext _context;
        private readonly HttpClient _client;

        public EditModel(Data.PersonContext context, HttpClient client)
        {
            _context = context;
            _client = client;
        }

        [BindProperty]
        public Person Person { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _client.GetFromJsonAsync<Person>($"http://localhost:5165/api/PeopleAPI/{id}");
            if (person == null)
            {
                return NotFound();
            }
            Person = person;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _client.PutAsJsonAsync<Person>($"http://localhost:5165/api/PeopleAPI/{Person.Id}", Person);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(Person.Id).Result)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> PersonExists(int id)
        {
            var person =  await _client.GetFromJsonAsync<Person>($"http://localhost:5165/api/PeopleAPI/{id}");
            if(person != null)
            {
                return true;
            }
            return false;
        }
    }
}
