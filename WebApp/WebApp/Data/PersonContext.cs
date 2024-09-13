using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace Data
{
    public class PersonContext : DbContext
    {
        public PersonContext (DbContextOptions<PersonContext> options)
            : base(options)
        {
        }

        public DbSet<WebApp.Models.Person> Person { get; set; } = default!;
    }
}
