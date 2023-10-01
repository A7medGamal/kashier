
using Microsoft.EntityFrameworkCore;
using kashier.DAL.Entities;
namespace kashier.DAL.Database
{
    public class DBContainer : DbContext
    {
       // public DbSet<Ticket>
         public DBContainer(DbContextOptions<DBContainer> opts ):base(opts)
        {
            
        }
        public DbSet<Tickets> Tickets { get; set; }
      //  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       // {
       //     optionsBuilder.UseSqlServer("server=. ; database= Tickets ;integrated security = true ;Encrypt=False");
      //  }
    }
} 
