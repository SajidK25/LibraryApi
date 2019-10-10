using System;
using WebAPI.Library.Repositories;

namespace WebAPI.Library.Services
{
    public class LibraryUnitOfWork : ILibraryUnitOfWork
    {
        public IStudentRepository studentRepository { get; private set; }
        public IBookIssueRepository bookIssueRepository { get; private set; }
        public IBookRepository bookRepository { get; private set; }
        public IReturnBookRepository returnBookRepository { get; private set; }

        private LibraryContext _context;
        public LibraryUnitOfWork(string connection, string migrationAssemblyName)
        {
            _context = new LibraryContext(connection, migrationAssemblyName);

            studentRepository = new StudentRepository(_context);
            bookIssueRepository = new BookIssueRepository(_context);
            bookRepository = new BookRepository(_context);
            returnBookRepository = new ReturnBookRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
