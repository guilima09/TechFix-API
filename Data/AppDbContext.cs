using Microsoft.EntityFrameworkCore;
using TechFix_API.Models;

namespace TechFix_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<OrdemServico> OrdensServico { get; set; }
    }
}