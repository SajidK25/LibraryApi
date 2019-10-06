using Microsoft.EntityFrameworkCore;
using LibraryCore;

namespace WebAPI.Library
{
    public class LibraryContext : DbContext
    {
        private string _connection;
        private string _migrationAssemblyName;
        public LibraryContext(string connection, string migrationAssemblyName)
        {
            _connection = connection;
            _migrationAssemblyName = migrationAssemblyName;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connection, m => m.MigrationsAssembly(_migrationAssemblyName));
            }
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookIssue> BookIssues { get; set; }
        public DbSet<ReturnBook> ReturnBooks { get; set; }

    }
}
