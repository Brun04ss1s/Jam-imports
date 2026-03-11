using System.Security.Cryptography.X509Certificates;
using JamImports.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace JamImports.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        }

        public DbSet<Produto> produtos { get; set; }
    }
}