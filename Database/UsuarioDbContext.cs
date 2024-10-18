using AuthApplication.Database.Maps;
using AuthApplication.Model;
using Microsoft.EntityFrameworkCore;


namespace AuthApplication.Database
{
    public class UsuarioDbContext : DbContext
    {

        public DbSet<UsuarioModel> Usuarios { get; set; }

        public UsuarioDbContext( DbContextOptions<UsuarioDbContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
