using System;
using System.Collections.Generic;

namespace LibraryApi.Services
{
    public interface ILibraryManagementService
    {
        bool SaveBook(Book book);
        List<Book> GetBooks();
        Book GetBook(int? bookId);
        void EditBook(Book book);
        void RemoveBook(Book book);
        bool IssueBookToMember(BookIssue bookIssue);
        bool ReturnBookFromMember(ReturnBook returnBook);
        BookIssue GetAIssuedBook(int studdentId, string barcode);
        Student GetStudent(int? studentId);

        void DecreaseBookCopy(Book book);
        void IncreaseBookCopy(Book book);
        DateTime GetIssueDate(int studentId, string barcode);
        int DaysDelay(DateTime issueDate, DateTime ReturnDate);
        decimal CalculateFine(int delays);
        void UpdateStudentFine(Student student);

    }
}
