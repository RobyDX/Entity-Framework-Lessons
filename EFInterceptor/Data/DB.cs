using Microsoft.EntityFrameworkCore;

namespace EFInterceptor.Data
{
    /// <summary>
    /// DB
    /// </summary>
    public class DB : DbContext
    {
        /// <summary>
        /// Products
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Categories
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Logs
        /// </summary>
        public DbSet<LogEvent> Logs { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Options</param>
        public DB(DbContextOptions<DB> options) : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Model Creationg
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<BaseItem>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
