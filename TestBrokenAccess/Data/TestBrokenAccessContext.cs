using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestBrokenAccess.Models;

namespace TestBrokenAccess.Data
{
    public class TestBrokenAccessContext : DbContext
    {
        public TestBrokenAccessContext (DbContextOptions<TestBrokenAccessContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; } = default!;
        public DbSet<CookieUser> Users { get; set; } = default!;
    }
}
