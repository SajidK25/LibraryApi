using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using Autofac.Extras.Moq;
using LibraryCore;
using Moq;
using NUnit.Framework;
using WebAPI.Library.Repositories;
using WebAPI.Library.Services;
using Shouldly;

namespace WebAPI.Library.Test
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class LibraryManagementServiceTests
    {
        private readonly ContainerBuilder _builder;
        private IContainer _container;
        private ILibraryManagementService _libraryManagementService;
        private AutoMock _mock;

        public LibraryManagementServiceTests()
        {
            const string connection = "";
            const string migrationAssemblyName = "";

            _builder = new ContainerBuilder();
            //_builder.RegisterType<StudentRepository>().As<IStudentRepository>();
            //_builder.RegisterType<BookRepository>().As<IBookRepository>();
            //_builder.RegisterType<BookIssueRepository>().As<IBookIssueRepository>();
            //_builder.RegisterType<ReturnBookRepository>().As<IReturnBookRepository>();
            _builder.RegisterType<LibraryManagementService>().As<ILibraryManagementService>()
                .WithParameter(new TypedParameter(typeof(ILibraryUnitOfWork), new Mock<ILibraryUnitOfWork>().Object));



            //_builder.RegisterType<LibraryUnitOfWork>().As<ILibraryUnitOfWork>()
            //    .WithParameters(new List<TypedParameter>
            //    {
            //        new TypedParameter(typeof(string),connection),
            //        new TypedParameter(typeof(string),migrationAssemblyName)
            //    });
            //_builder.RegisterType<LibraryContext>().As<ILibraryContext>()
            //    .WithParameters(new List<TypedParameter>
            //    {
            //        new TypedParameter(typeof(string),connection),
            //        new TypedParameter(typeof(string),migrationAssemblyName)
            //    });

            _container = _builder.Build();

        }
        [OneTimeSetUp]
        public void ClassSetUp()
        {
            _mock = AutoMock.GetLoose();
        }

        [OneTimeTearDown]
        public void ClassCleanUp()
        {
            _mock?.Dispose();
        }

        [SetUp]
        public void TestSetup()
        {
            // _libraryManagementService = _container.Resolve<ILibraryManagementService>();
        }

        [TearDown]
        public void TestCleanUp()
        {
        }
        [Test]
        public void GetStudentList_WhenCall_ReturnListOfStudent()
        {
            // Arrange

            var aBook = new Book
            {
                BookId = 1,
                Barcode = "IT00001",
                Title = "C#",
                Author = "JALAL UDDIN",
                CopyCount = 10

            };
            var bBook = new Book
            {
                BookId = 2,
                Barcode = "IT00002",
                Title = "C++",
                Author = "Sadman Sakib",
                CopyCount = 15

            };
            var cBook = new Book
            {
                BookId = 3,
                Barcode = "IT00003",
                Title = "JS",
                Author = "Zia Ul Haque",
                CopyCount = 20

            };

            var bookRepositoryFake = _mock.Mock<IBookRepository>();
            var libraryUnitOfWork = _mock.Mock<ILibraryUnitOfWork>();

            bookRepositoryFake.Setup(x => x.GetAllBooks()).Returns(new List<Book> { aBook, bBook, cBook });
            libraryUnitOfWork.Setup(x => x.BookRepository).Returns(bookRepositoryFake.Object);

            _libraryManagementService = _mock.Create<LibraryManagementService>(new TypedParameter(typeof(ILibraryUnitOfWork), libraryUnitOfWork.Object));

            // Act
            _libraryManagementService.GetBooks();

            // Assert

            bookRepositoryFake.VerifyAll();
        }
        [Test]
        public void SaveBook_WhenSccussfullyCallWithBookObjectParameter_ReturnStatusTrue()
        {
            // Arrange

            var aBook = new Book
            {
                BookId = 1,
                Barcode = "IT00001",
                Title = "C#",
                Author = "JALAL UDDIN",
                CopyCount = 10

            };

            var bookRepositoryFake = _mock.Mock<IBookRepository>();
            var libraryUnitOfWork = _mock.Mock<ILibraryUnitOfWork>();

            bookRepositoryFake.Setup(x => x.Insert(aBook));
            libraryUnitOfWork.Setup(x => x.BookRepository).Returns(bookRepositoryFake.Object);

            _libraryManagementService = _mock.Create<LibraryManagementService>(new TypedParameter(typeof(ILibraryUnitOfWork), libraryUnitOfWork.Object));

            // Act
            _libraryManagementService.SaveBook(aBook);

            // Assert

            bookRepositoryFake.VerifyAll();
        }
        [Test]
        public void GetBook_WhenCallWithBarcodeParameter_ReturnABookObject()
        {
            // Arrange
            var barcode = "IT00001";
            var aBook = new Book
            {
                BookId = 1,
                Barcode = "IT00001",
                Title = "C#",
                Author = "JALAL UDDIN",
                CopyCount = 10

            };

            var bookRepositoryFake = _mock.Mock<IBookRepository>();
            var libraryUnitOfWork = _mock.Mock<ILibraryUnitOfWork>();

            bookRepositoryFake.Setup(x => x.GetSingleBook(barcode));
            libraryUnitOfWork.Setup(x => x.BookRepository).Returns(bookRepositoryFake.Object);

            _libraryManagementService = _mock.Create<LibraryManagementService>(new TypedParameter(typeof(ILibraryUnitOfWork), libraryUnitOfWork.Object));

            // Act
            _libraryManagementService.GetBook(barcode);

            // Assert

            bookRepositoryFake.VerifyAll();
        }
        [Test]
        public void IssueBookToMember_WhenHasCopy_IssueBookAndDecreaseCopyCount()
        {
            var newBookIssue = new BookIssue
            {
                StudentId = 1001,
                Barcode = "IT00001",
                BookId = 1,
                IssueDate = DateTime.Now
            };
            var aBook = new Book
            {
                BookId = 1,
                Barcode = "IT00001",
                Title = "C#",
                Author = "JALAL UDDIN",
                CopyCount = 10

            };
            var bookRepositoryFake = _mock.Mock<IBookRepository>();
            var bookIssueRepositoryFake = _mock.Mock<IBookIssueRepository>();
            var libraryUnitOfWorkFake = _mock.Mock<ILibraryUnitOfWork>();

            bookRepositoryFake.Setup(b => b.GetSingleBook(newBookIssue.Barcode)).Returns(aBook);
            bookIssueRepositoryFake.Setup(bi => bi.InsertBookIssue(newBookIssue));
            bookIssueRepositoryFake.Setup(bi => bi.DecreaseBook(aBook));

            libraryUnitOfWorkFake.Setup(lu => lu.BookRepository).Returns(bookRepositoryFake.Object);
            libraryUnitOfWorkFake.Setup(lu => lu.BookIssueRepository).Returns(bookIssueRepositoryFake.Object);

            _libraryManagementService = _mock.Create<LibraryManagementService>(new TypedParameter(typeof(ILibraryUnitOfWork), libraryUnitOfWorkFake.Object));

            _libraryManagementService.IssueBookToMember(newBookIssue);

            //Assert
            bookIssueRepositoryFake.VerifyAll();
            bookRepositoryFake.VerifyAll();
            libraryUnitOfWorkFake.VerifyAll();
        }
        [Test]
        public void IssueBookToMember_WhenHasNoCopy_InvalidOperationException()
        {
            var newBookIssue = new BookIssue
            {
                StudentId = 1001,
                Barcode = "IT00001",
                BookId = 1,
                IssueDate = DateTime.Now
            };
            var aBook = new Book
            {
                BookId = 1,
                Barcode = "IT00001",
                Title = "C#",
                Author = "JALAL UDDIN",
                CopyCount = 0

            };
            var bookRepositoryFake = _mock.Mock<IBookRepository>();
            var libraryUnitOfWorkFake = _mock.Mock<ILibraryUnitOfWork>();

            bookRepositoryFake.Setup(b => b.GetSingleBook(newBookIssue.Barcode)).Returns(aBook);

            libraryUnitOfWorkFake.Setup(lu => lu.BookRepository).Returns(bookRepositoryFake.Object);

            _libraryManagementService = _mock.Create<LibraryManagementService>(new TypedParameter(typeof(ILibraryUnitOfWork), libraryUnitOfWorkFake.Object));

            Should.Throw<InvalidOperationException>(() => _libraryManagementService.IssueBookToMember(newBookIssue));
            //_libraryManagementService.IssueBookToMember(newBookIssue);

            //Assert
            bookRepositoryFake.VerifyAll();
            libraryUnitOfWorkFake.VerifyAll();
        }
        [Test]
        public void SaveReturnBook_WhenHasReturnDelay_FineCalculationAndUpdate()
        {
            // Arrange
            var aIssuedBook = new BookIssue
            {
                StudentId = 1001,
                Barcode = "IT00001",
                BookId = 1,
                IssueDate = DateTime.Parse("2019-10-01 08:46:54.5394739")
            };
            var newReturnBook = new ReturnBook
            {
                StudentId = 1001,
                Barcode = "IT00001",
                BookId = 1,
                ReturnDate = DateTime.Now
            };
            var aBook = new Book
            {
                BookId = 1,
                Barcode = "IT00001",
                Title = "C#",
                Author = "JALAL UDDIN",
                CopyCount = 10

            };
            var aStudent = new Student
            {
                StudentID = 1001,
                StudentName = "Sajid Khan",
                FineAmount = 240
            };

            var returnBookRepositoryFake = _mock.Mock<IReturnBookRepository>();
            var libraryUnitOfWorkFake = _mock.Mock<ILibraryUnitOfWork>();
            var bookRepositoryFake = _mock.Mock<IBookRepository>();
            var bookIssueRepositoryFake = _mock.Mock<IBookIssueRepository>();
            var studentRepositoryFake = _mock.Mock<IStudentRepository>();

            returnBookRepositoryFake.Setup(x => x.InsertReturnBook(newReturnBook));
            bookRepositoryFake.Setup(x => x.GetSingleBook(newReturnBook.Barcode)).Returns(aBook);
            returnBookRepositoryFake.Setup(x => x.IncreaseBook(aBook));
            bookIssueRepositoryFake.Setup(x => x.SelectIssueDate(newReturnBook.StudentId, aBook.Barcode)).Returns(aIssuedBook.IssueDate);
            returnBookRepositoryFake.Setup(x => x.UpdateFine(aStudent));
            studentRepositoryFake.Setup(x => x.GetSingleStudent(newReturnBook.StudentId)).Returns(aStudent);

            libraryUnitOfWorkFake.Setup(x => x.ReturnBookRepository).Returns(returnBookRepositoryFake.Object);
            libraryUnitOfWorkFake.Setup(x => x.BookRepository).Returns(bookRepositoryFake.Object);
            libraryUnitOfWorkFake.Setup(x => x.BookIssueRepository).Returns(bookIssueRepositoryFake.Object);
            libraryUnitOfWorkFake.Setup(x => x.StudentRepository).Returns(studentRepositoryFake.Object);



            _libraryManagementService = _mock.Create<LibraryManagementService>(new TypedParameter(typeof(ILibraryUnitOfWork), libraryUnitOfWorkFake.Object));

            // Act
            _libraryManagementService.SaveReturnBook(newReturnBook);

            // Assert

            returnBookRepositoryFake.VerifyAll();
            bookRepositoryFake.VerifyAll();
            bookIssueRepositoryFake.VerifyAll();
            studentRepositoryFake.VerifyAll();
        }
    }
}