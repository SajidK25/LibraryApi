using System;
namespace WebAPI.Library.Repositories
{
    public interface ILibraryUnitOfWork
    {
        IStudentRepository studentRepository { get; }
        IBookIssueRepository bookIssueRepository { get; }
        IBookRepository bookRepository { get; }
        IReturnBookRepository returnBookRepository { get; }
        void Save();
    }
}
