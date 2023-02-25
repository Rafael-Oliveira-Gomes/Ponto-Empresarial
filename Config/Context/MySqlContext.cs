using ForPonto.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ForPonto.Config.Context
{
    public class MySqlContext : IdentityDbContext<ApplicationUser>
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {
        }

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Ponto> Pontos { get; set; }
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<ApplicationRole> Role { get; set; }
        public object Ponto { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

            modelBuilder.Entity<Funcionario>();
            modelBuilder.Entity<Ponto>();

        }
    }
}

