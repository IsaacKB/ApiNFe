using Microsoft.EntityFrameworkCore;
using Models;
using ApiNFe.MappingEntities;

namespace ApiNFe.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Todo> Todos => Set<Todo>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.ApplyConfiguration(new TodoMap());
        }
    }
}
