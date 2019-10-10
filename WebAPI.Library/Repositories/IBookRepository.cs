using System;
using System.Collections.Generic;
using LibraryCore;

namespace WebAPI.Library.Repositories
{
    public interface IBookRepository
    {
        void Insert(Book book);
        List<Book> GetAllBooks();
        Book GetSingleBook(string barcode);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
    }
}
