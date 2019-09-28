using Microsoft.EntityFrameworkCore;

namespace LibraryApi
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions options)
            : base(options)
        {

        }

    }
}
