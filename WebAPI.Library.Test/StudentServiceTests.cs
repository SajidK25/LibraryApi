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
using System.Runtime.CompilerServices;

namespace WebAPI.Library.Test
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class StudentServiceTests
    {
        //private readonly ContainerBuilder _builder;
        //private IContainer _container;
        private IStudentService _studentService;
        private AutoMock _mock;

        public StudentServiceTests()
        {
            //const string connection = "";
            //const string migrationAssemblyName = "";

            //_builder = new ContainerBuilder();
            ////_builder.RegisterType<StudentRepository>().As<IStudentRepository>();
            ////_builder.RegisterType<BookRepository>().As<IBookRepository>();
            ////_builder.RegisterType<BookIssueRepository>().As<IBookIssueRepository>();
            ////_builder.RegisterType<ReturnBookRepository>().As<IReturnBookRepository>();
            //_builder.RegisterType<StudentService>().As<IStudentService>()
            //    .WithParameter(new TypedParameter(typeof(ILibraryUnitOfWork), new Mock<ILibraryUnitOfWork>().Object));



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

            //_container = _builder.Build();

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
        public void GetStudent_WhenCallWithStudentId_ReturnTheStudentObjectOfThatId()
        {
            // Arrange
            var studentId = 1001;
            var aStudent = new Student
            {

                StudentID = 1001,
                StudentName = "Sajid"

            };

            var fakeStudentRepository = _mock.Mock<IStudentRepository>();
            var libraryUnitOfWork = _mock.Mock<ILibraryUnitOfWork>();

            fakeStudentRepository.Setup(x => x.GetSingleStudent(studentId)).Returns(aStudent);

            libraryUnitOfWork.Setup(x => x.StudentRepository).Returns(fakeStudentRepository.Object);

            _studentService = _mock.Create<StudentService>(new TypedParameter(typeof(ILibraryUnitOfWork), libraryUnitOfWork.Object));

            // Act
            _studentService.GetStudent(studentId);

            // Assert

            fakeStudentRepository.VerifyAll();
        }

        [Test]
        public void SaveStudent_WhenSccussfullyCallWithStudentObjectParameter_ReturnStatusTrue()
        {
            // Arrange

            var aStudent = new Student
            {
                StudentID = 1001,
                StudentName = "Sajid"
            };

            var fakeStudentRepository = _mock.Mock<IStudentRepository>();
            var libraryUnitOfWork = _mock.Mock<ILibraryUnitOfWork>();

            fakeStudentRepository.Setup(x => x.Insert(aStudent));
            libraryUnitOfWork.Setup(x => x.StudentRepository).Returns(fakeStudentRepository.Object);

            _studentService = _mock.Create<StudentService>(new TypedParameter(typeof(ILibraryUnitOfWork), libraryUnitOfWork.Object));

            // Act
            _studentService.SaveStudent(aStudent);

            // Assert

            fakeStudentRepository.VerifyAll();
        }

        [Test]
        public void GetStudentList_WhenCall_ReturnListOfStudent()
        {
            // Arrange

            var aStudent = new Student
            {
                StudentID = 1001,
                StudentName = "Sajid"
            };
            var bStudent = new Student
            {
                StudentID = 1002,
                StudentName = "Mollika"
            };
            var cStudent = new Student
            {
                StudentID = 1003,
                StudentName = "Raakin",

            };

            var fakeStudentRepository = _mock.Mock<IStudentRepository>();
            var libraryUnitOfWork = _mock.Mock<ILibraryUnitOfWork>();

            fakeStudentRepository.Setup(x => x.GetStudents()).Returns(new List<Student> { aStudent, bStudent, cStudent });
            libraryUnitOfWork.Setup(x => x.StudentRepository).Returns(fakeStudentRepository.Object);

            _studentService = _mock.Create<StudentService>(new TypedParameter(typeof(ILibraryUnitOfWork), libraryUnitOfWork.Object));

            // Act
            _studentService.GetStudentList();

            // Assert

            fakeStudentRepository.VerifyAll();
        }

        [Test]
        public void EditStudent_WhenSuccessfullyCallWithParatemerOfStudentObject_ReturnStatusTrue()
        {
            // Arrange

            var aStudent = new Student
            {
                StudentID = 1001,
                StudentName = "Sajid"
            };

            var fakeStudentRepository = _mock.Mock<IStudentRepository>();
            var libraryUnitOfWork = _mock.Mock<ILibraryUnitOfWork>();

            fakeStudentRepository.Setup(x => x.UpdateStudent(aStudent));
            libraryUnitOfWork.Setup(x => x.StudentRepository).Returns(fakeStudentRepository.Object);

            _studentService = _mock.Create<StudentService>(new TypedParameter(typeof(ILibraryUnitOfWork), libraryUnitOfWork.Object));

            // Act
            var result = _studentService.EditStudent(aStudent);


            // Assert
            result.ShouldBeTrue();
            //Assert.IsTrue(result);
            fakeStudentRepository.VerifyAll();
            libraryUnitOfWork.VerifyAll();
        }
        [Test]
        public void CheckFineAmount_WhenCallWithParatemerStudentID_ReturnADecimalValue()
        {
            // Arrange
            const int studentId = 1001;
            var aStudent = new Student
            {
                StudentID = 1001,
                StudentName = "Sajid",
                FineAmount = 240.00M
            };

            var fakeStudentRepository = _mock.Mock<IStudentRepository>();
            var libraryUnitOfWork = _mock.Mock<ILibraryUnitOfWork>();

            fakeStudentRepository.Setup(x => x.CheckFine(studentId)).Returns(aStudent.FineAmount);
            libraryUnitOfWork.Setup(x => x.StudentRepository).Returns(fakeStudentRepository.Object);

            _studentService = _mock.Create<StudentService>(new TypedParameter(typeof(ILibraryUnitOfWork), libraryUnitOfWork.Object));

            // Act
            var result = _studentService.CheckFineAmount(studentId);

            // Assert
            result.ShouldBe(240.00M);
            //Assert.AreEqual(240.00M, result);
            fakeStudentRepository.VerifyAll();
            libraryUnitOfWork.VerifyAll();
        }

        [Test]
        public void ReceiveStudentFine_WhenPaymentAmountisGreaterThanFineAmount_InvalidOperationException()
        {
            // Arrange
            //var studentId = 1001;
            const decimal paymentAmount = 300;
            var aStudent = new Student
            {
                StudentID = 1001,
                StudentName = "Sajid",
                FineAmount = 240.00M
            };

            var libraryUnitOfWork = _mock.Mock<ILibraryUnitOfWork>();

            _studentService = _mock.Create<StudentService>(new TypedParameter(typeof(ILibraryUnitOfWork), libraryUnitOfWork.Object));

            // Act

            Should.Throw<InvalidOperationException>(() => _studentService.ReceiveStudentFine(aStudent, paymentAmount));

        }
        [Test]
        public void ReceiveStudentFine_WhenPaymentAmountIsLessThanOrEqualToFineAmount_UpdateFineAmountValue()
        {
            // Arrange
            //var studentId = 1001;
            const decimal paymentAmount = 200;
            var aStudent = new Student
            {
                StudentID = 1001,
                StudentName = "Sajid",
                FineAmount = 240.00M
            };

            var fakeStudentRepository = _mock.Mock<IStudentRepository>();
            var libraryUnitOfWork = _mock.Mock<ILibraryUnitOfWork>();

            var remainingAmmount = aStudent.FineAmount - paymentAmount;
            fakeStudentRepository.Setup(x => x.ReceiveFine(aStudent, remainingAmmount));
            libraryUnitOfWork.Setup(x => x.StudentRepository).Returns(fakeStudentRepository.Object);

            _studentService = _mock.Create<StudentService>(new TypedParameter(typeof(ILibraryUnitOfWork), libraryUnitOfWork.Object));

            // Act
            _studentService.ReceiveStudentFine(aStudent, paymentAmount);

            // Assert
            fakeStudentRepository.VerifyAll();
            libraryUnitOfWork.VerifyAll();
        }
    }
}