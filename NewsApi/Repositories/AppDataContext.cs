using Microsoft.EntityFrameworkCore;
using NewsApi.Models.EntityModels;

namespace NewsApi.Repositories
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) {}

        public DbSet<News> News { get; set; }
    }
}