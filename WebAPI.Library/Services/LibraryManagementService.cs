using System;
using System.Collections.Generic;
using WebAPI.Library.Repositories;
using LibraryCore;

namespace WebAPI.Library.Services
{
    public class LibraryManagementService : ILibraryManagementService
    {
        //private IBookRepository _bookRepository;
        //private IBookIssueRepository _bookIssueRepository;
        //private IReturnBookRepository _returnBookRepository;
        //private IStudentRepository _studentRepository;
        private ILibraryUnitOfWork _libraryUnitOfWork;

        public LibraryManagementService(ILibraryUnitOfWork libraryUnitOfWork)
        {
            _libraryUnitOfWork = libraryUnitOfWork;
        }



        public List<Book> GetBooks()
        {
            return _libraryUnitOfWork.BookRepository.GetAllBooks();
        }

        public bool SaveBook(Book book)
        {
            bool isSaved;
            try
            {
                _libraryUnitOfWork.BookRepository.Insert(book);
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

        public Book GetBook(string barcode)
        {
            var book = _libraryUnitOfWork.BookRepository.GetSingleBook(barcode);
            return book;
        }
        public void RemoveBook(Book book)
        {
            throw new NotImplementedException();
        }

        public void IssueBookToMember(BookIssue bookIssue)
        {
            var book = _libraryUnitOfWork.BookRepository.GetSingleBook(bookIssue.Barcode);
            //var student = GetStudent(bookIssue.StudentId);
            if (book.CopyCount > 0)
            {
                _libraryUnitOfWork.BookIssueRepository.InsertBookIssue(bookIssue);
                book.CopyCount -= 1;
                _libraryUnitOfWork.BookIssueRepository.DecreaseBook(book);
                _libraryUnitOfWork.Save();
            }
            else
            {
                throw new InvalidOperationException("Not enough copy");
            }


        }

        //public void DecreaseBookCopy(Book book)
        //{
        //    book.CopyCount -= 1;
        //    _libraryUnitOfWork.bookIssueRepository.DecreaseBook(book);
        //}


        public BookIssue GetAIssuedBook(int studentId, string barcode)
        {
            return _libraryUnitOfWork.BookIssueRepository.SingleBookFromIssuedBooks(studentId, barcode);
        }

        public Student GetStudent(int? studentId)
        {
            return _libraryUnitOfWork.StudentRepository.GetSingleStudent(studentId);
        }

        public void SaveReturnBook(ReturnBook returnBook)
        {
            _libraryUnitOfWork.ReturnBookRepository.InsertReturnBook(returnBook);

            var book = _libraryUnitOfWork.BookRepository.GetSingleBook(returnBook.Barcode);

            book.CopyCount += 1;
            _libraryUnitOfWork.ReturnBookRepository.IncreaseBook(book);

            var bookIssueDate = _libraryUnitOfWork.BookIssueRepository.SelectIssueDate(returnBook.StudentId, book.Barcode);

            var student = _libraryUnitOfWork.StudentRepository.GetSingleStudent(returnBook.StudentId);

            var gracePeriod = 7;
            var totalDays = ((returnBook.ReturnDate - bookIssueDate).Days) - 1;
            var delays = (totalDays - gracePeriod);
            if (delays < 0)
                delays = 0;

            decimal finePerDay = 10;
            var totalFine = delays * finePerDay;
            student.FineAmount = totalFine;

            _libraryUnitOfWork.ReturnBookRepository.UpdateFine(student);
            _libraryUnitOfWork.Save();

        }

        /*
        public bool ReturnBookFromMember(ReturnBook returnBook)
        {
            bool isReturn;
            try
            {
                _libraryUnitOfWork.returnBookRepository.InsertReturnBook(returnBook);
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
            _libraryUnitOfWork.returnBookRepository.IncreaseBook(book);
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
            _libraryUnitOfWork.returnBookRepository.UpdateFine(student);
        }

        public DateTime GetIssueDate(int studentId, string barcode)
        {
            return _libraryUnitOfWork.bookIssueRepository.SelectIssueDate(studentId, barcode);
        }
         */
    }
}
