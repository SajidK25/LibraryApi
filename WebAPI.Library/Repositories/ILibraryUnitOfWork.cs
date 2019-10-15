using System;
namespace WebAPI.Library.Repositories
{
    public interface ILibraryUnitOfWork
    {
        IStudentRepository StudentRepository { get; }
        IBookIssueRepository BookIssueRepository { get; }
        IBookRepository BookRepository { get; }
        IReturnBookRepository ReturnBookRepository { get; }
        void Save();
    }
}
