using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using shoeVerse.Models;

namespace shoeVerse.Data
{
    public class shoeVerseContext : DbContext
    {
        public shoeVerseContext (DbContextOptions<shoeVerseContext> options)
            : base(options)
        {
        }

        public DbSet<shoeVerse.Models.Product> Product { get; set; } = default!;
    }
}
