using System;
using System.Linq;
namespace LibraryApi.Repositories
{
    public class BookIssueRepository : IBookIssueRepository
    {
        private LibraryContext _context;
        public BookIssueRepository(LibraryContext context)
        {
            _context = context;
        }

        public BookIssue SingleBookFromIssuedBooks(int studentId, string barcode)
        {
            var issuedBook = _context.BookIssues
                .Where(bi => bi.StudentId == studentId && bi.Barcode == barcode)
                .FirstOrDefault();
            return issuedBook;
        }

        public void InsertBookIssue(BookIssue bookIssue)
        {
            _context.BookIssues.Add(bookIssue);
            _context.SaveChanges();

        }

        public void DecreaseBook(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public DateTime SelectIssueDate(int studentId, string barcode)
        {
            var issueDate = _context.BookIssues
                    .Where(bi => bi.StudentId == studentId && bi.Barcode == barcode)
                    .Select(bi => bi.IssueDate)
                    .FirstOrDefault();
            return issueDate;
        }
    }
}
