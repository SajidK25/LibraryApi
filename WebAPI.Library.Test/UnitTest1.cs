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

namespace WebAPI.Library.Test
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class LibraryManagementServiceTests
    {
        private ContainerBuilder _builder;
        private IContainer _container;
        private ILibraryManagementService _libraryManagementService;

        public LibraryManagementServiceTests()
        {
            const string connection = "";
            const string migrationAssemblyName = "";

            //_builder = new ContainerBuilder();
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
            //.AddTransient<IStudentRepository, StudentRepository>()
            //.AddTransient<IStudentService, StudentService>()
            //.AddTransient<IBookRepository, BookRepository>()
            //.AddTransient<IBookIssueRepository, BookIssueRepository>()
            //.AddTransient<IReturnBookRepository, ReturnBookRepository>()
            //.AddTransient<ILibraryManagementService, LibraryManagementService>()
            //.AddTransient<ILibraryUnitOfWork, LibraryUnitOfWork>(x => new LibraryUnitOfWork(connection, migrationAssemblyName))
            //.AddTransient<LibraryContext>(x => new LibraryContext(connection, migrationAssemblyName));
        }
        //[OneTimeSetUp]
        //public void ClassSetUp()
        //{

        //}

        //[OneTimeTearDown]
        //public void ClassCleanUp()
        //{

        //}

        [SetUp]
        public void TestSetup()
        {
            _libraryManagementService = _container.Resolve<ILibraryManagementService>();
        }

        //[TearDown]
        //public void TestCleanUp()
        //{

        //}

        [Test]
        public void SaveReturnBook_()
        {
            // Arrange
            var returnBook = new ReturnBook
            {
                StudentId = 1001,
                Barcode = "IT00001",
                BookId = 1,
                ReturnDate = DateTime.Now
            };

            // Act
            _libraryManagementService.SaveReturnBook(returnBook);
            // Assert
        }
    }
}