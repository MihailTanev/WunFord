using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WunFord.Data.Models;

namespace WunFord.Data
{
    public class WunFordDbContext : IdentityDbContext<User>
    {
        public WunFordDbContext(DbContextOptions<WunFordDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Status> Statuses { get; set; }
    }
}
