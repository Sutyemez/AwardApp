using Microsoft.EntityFrameworkCore;
namespace AwardApp1.Models
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) // Hangi veri tabanına karşılık geleceğini açıklar.
        {                                           // optionsu program cs içerisinde dolduracağız.

        }

        public DbSet<uye> uye { get; set; }
        //Veri tabanı ismi ile aynı isim olsun.





    }

}
