using Microsoft.EntityFrameworkCore;

namespace LibraryApi
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookIssue> BookIssues { get; set; }
        public DbSet<ReturnBook> ReturnBooks { get; set; }

    }
}
