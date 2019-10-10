using System;
using LibraryCore;
namespace WebAPI.Library.Repositories
{
    public class ReturnBookRepository : IReturnBookRepository
    {
        private LibraryContext _context;
        public ReturnBookRepository(LibraryContext context)
        {
            _context = context;
        }

        public void IncreaseBook(Book book)
        {
            _context.Books.Update(book);
            //_context.SaveChanges();
        }

        public void InsertReturnBook(ReturnBook returnBook)
        {
            _context.ReturnBooks.Add(returnBook);
            //_context.SaveChanges();
        }

        public void UpdateFine(Student student)
        {
            _context.Students.Update(student);
            //_context.SaveChanges();
        }
    }
}
