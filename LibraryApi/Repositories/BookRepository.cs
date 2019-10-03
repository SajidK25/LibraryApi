using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApi.Repositories
{
    public class BookRepository : IBookRepository
    {
        private LibraryContext _context;
        public BookRepository(LibraryContext context)
        {
            _context = context;
        }
        public void DeleteBook(Book book)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books.OrderBy(b => b.BookId).ToList();
        }

        public Book GetSingleBook(int? bookId)
        {
            return _context.Books.Where(b => b.BookId == bookId).FirstOrDefault();
        }

        public void Insert(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }




    }
}
