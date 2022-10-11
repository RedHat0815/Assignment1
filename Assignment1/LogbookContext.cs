using Assignment1.Model;
using Microsoft.EntityFrameworkCore;

namespace Assignment1
{
    public class LogbookContext : DbContext
    {

        public LogbookContext() { }

        public LogbookContext(DbContextOptions<LogbookContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Journey> Journeys { get; set; } = null!;


    }
}
