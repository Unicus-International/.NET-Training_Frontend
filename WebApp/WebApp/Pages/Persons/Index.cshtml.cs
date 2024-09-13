using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using WebApp.Models;
using NuGet.Protocol;

namespace WebApp.Pages.Persons
{
    public class IndexModel : PageModel
    {
        private readonly Data.PersonContext _context;
        private readonly HttpClient _client;

        public IndexModel(Data.PersonContext context, HttpClient client)
        {
            _context = context;
            _client = client;
        }

        public IList<Person> Person { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Person = await _client.GetFromJsonAsync<List<Person>>("http://localhost:5165/api/PeopleAPI");
        }
    }
}
