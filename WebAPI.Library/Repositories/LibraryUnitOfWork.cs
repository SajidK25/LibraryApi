using System;
using WebAPI.Library.Repositories;

namespace WebAPI.Library.Services
{
    public class LibraryUnitOfWork : ILibraryUnitOfWork
    {
        public IStudentRepository StudentRepository { get; private set; }
        public IBookIssueRepository BookIssueRepository { get; private set; }
        public IBookRepository BookRepository { get; private set; }
        public IReturnBookRepository ReturnBookRepository { get; private set; }



        private LibraryContext _context;
        public LibraryUnitOfWork(string connection, string migrationAssemblyName)
        {
            _context = new LibraryContext(connection, migrationAssemblyName);

            StudentRepository = new StudentRepository(_context);
            BookIssueRepository = new BookIssueRepository(_context);
            BookRepository = new BookRepository(_context);
            ReturnBookRepository = new ReturnBookRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
