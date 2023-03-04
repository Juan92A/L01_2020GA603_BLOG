using Microsoft.EntityFrameworkCore;

namespace L01_2020GA603_BLOG.Models
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> dbContext) : base(dbContext)
        {

        }

        public DbSet<usuarios> usuarios { get; set; }
        public DbSet<comentarios> comentarios { get; set; }
        public DbSet<calificaciones> calificaciones { get; set; }



    }
}
