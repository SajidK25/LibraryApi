using System;
using System.Collections.Generic;

namespace LibraryApi.Repositories
{
    public interface IBookRepository
    {
        void Insert(Book book);
        List<Book> GetAllBooks();
        Book GetSingleBook(int? bookId);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
    }
}
