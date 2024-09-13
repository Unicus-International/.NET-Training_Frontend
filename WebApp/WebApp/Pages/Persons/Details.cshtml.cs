using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using WebApp.Models;

namespace WebApp.Pages.Persons
{
    public class DetailsModel : PageModel
    {
        private readonly Data.PersonContext _context;
        private readonly HttpClient _client;

        public DetailsModel(Data.PersonContext context, HttpClient client)
        {
            _context = context;
            _client = client;
        }

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
            else
            {
                Person = person;
            }
            return Page();
        }
    }
}
