using System;
using LibraryCore;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Library.Repositories
{
    public interface ILibraryContext
    {
        DbSet<Student> Students { get; set; }
        DbSet<Book> Books { get; set; }
        DbSet<BookIssue> BookIssues { get; set; }
        DbSet<ReturnBook> ReturnBooks { get; set; }
    }
}
