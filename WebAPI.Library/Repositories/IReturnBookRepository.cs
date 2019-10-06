using System;
using LibraryCore;
namespace WebAPI.Library.Repositories
{
    public interface IReturnBookRepository
    {
        void InsertReturnBook(ReturnBook returnBook);
        void IncreaseBook(Book book);
        void UpdateFine(Student student);

    }
}
