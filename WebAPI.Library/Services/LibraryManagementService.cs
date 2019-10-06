using System;
using System.Collections.Generic;
using WebAPI.Library.Repositories;
using LibraryCore;

namespace WebAPI.Library.Services
{
    public class LibraryManagementService : ILibraryManagementService
    {
        private IBookRepository _bookRepository;
        private IBookIssueRepository _bookIssueRepository;
        private IReturnBookRepository _returnBookRepository;
        private IStudentRepository _studentRepository;

        public LibraryManagementService(IBookRepository bookRepository, IBookIssueRepository bookIssueRepository, IReturnBookRepository returnBookRepository, IStudentRepository studentRepository)
        {
            _bookRepository = bookRepository;
            _bookIssueRepository = bookIssueRepository;
            _returnBookRepository = returnBookRepository;
            _studentRepository = studentRepository;
        }



        public List<Book> GetBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public bool SaveBook(Book book)
        {
            bool isSaved;
            try
            {
                _bookRepository.Insert(book);
                isSaved = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                isSaved = false;
            }
            return isSaved;

        }
        public void EditBook(Book book)
        {
            throw new NotImplementedException();
        }

        public Book GetBook(int? bookId)
        {
            var book = _bookRepository.GetSingleBook(bookId);
            return book;
        }
        public void RemoveBook(Book book)
        {
            throw new NotImplementedException();
        }

        public bool IssueBookToMember(BookIssue bookIssue)
        {
            bool isIssued;
            try
            {
                _bookIssueRepository.InsertBookIssue(bookIssue);
                isIssued = true;
            }
            catch (Exception ex)
            {
                isIssued = false;
            }
            return isIssued;
        }

        public void DecreaseBookCopy(Book book)
        {
            book.CopyCount -= 1;
            _bookIssueRepository.DecreaseBook(book);
        }


        public BookIssue GetAIssuedBook(int studentId, string barcode)
        {
            return _bookIssueRepository.SingleBookFromIssuedBooks(studentId, barcode);
        }

        public Student GetStudent(int? studentId)
        {
            return _studentRepository.GetSingleStudent(studentId);
        }


        public bool ReturnBookFromMember(ReturnBook returnBook)
        {
            bool isReturn;
            try
            {
                _returnBookRepository.InsertReturnBook(returnBook);
                isReturn = true;
            }
            catch (Exception ex)
            {
                isReturn = false;
            }
            return isReturn;
        }

        public void IncreaseBookCopy(Book book)
        {
            book.CopyCount += 1;
            _returnBookRepository.IncreaseBook(book);
        }

        public int DaysDelay(DateTime issueDate, DateTime returnDate)
        {
            var gracePeriod = 7;
            var totalDays = ((returnDate - issueDate).Days) - 1;
            var delays = (totalDays - gracePeriod);
            if (delays < 0)
                delays = 0;
            return delays;
        }

        public decimal CalculateFine(int delays)
        {
            decimal finePerDay = 10;
            var totalFine = delays * finePerDay;
            return totalFine;
        }



        public void UpdateStudentFine(Student student)
        {
            _returnBookRepository.UpdateFine(student);
        }

        public DateTime GetIssueDate(int studentId, string barcode)
        {
            return _bookIssueRepository.SelectIssueDate(studentId, barcode);
        }
    }
}
