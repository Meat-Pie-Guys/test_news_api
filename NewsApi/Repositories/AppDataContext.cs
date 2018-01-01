using Microsoft.EntityFrameworkCore;
using NewsApi.Models.EntityModels;

namespace NewsApi.Repositories
{
    /// <summary>
    /// Our DbContext instance and database session.
    /// </summary>
    public class AppDataContext : DbContext
    {
        /// <summary>
        /// A constructor that allows for injecting in-memory SQlite options.
        /// </summary>
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) {}

        /// <summary>
        /// All news in database.
        /// </summary>
        public DbSet<News> News { get; set; }
    }
}