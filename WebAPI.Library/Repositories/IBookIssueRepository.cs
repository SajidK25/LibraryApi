using System;
using LibraryCore;
namespace WebAPI.Library.Repositories
{
    public interface IBookIssueRepository
    {
        void InsertBookIssue(BookIssue bookIssue);
        BookIssue SingleBookFromIssuedBooks(int studentId, string barcode);
        void DecreaseBook(Book book);
        DateTime SelectIssueDate(int studentId, string barcode);

    }
}
