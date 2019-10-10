using System;
using System.Collections.Generic;
using WebAPI.Library.Repositories;
using LibraryCore;

namespace WebAPI.Library.Services
{
    public class StudentService : IStudentService
    {
        private ILibraryUnitOfWork _libraryUnitOfWork;
        public StudentService(ILibraryUnitOfWork libraryUnitOfWork)
        {
            _libraryUnitOfWork = libraryUnitOfWork;
        }

        public Student GetStudent(int? studnetId)
        {

            var student = _libraryUnitOfWork.studentRepository.GetSingleStudent(studnetId);
            return student;
        }

        public bool SaveStudent(Student student)
        {
            bool status;
            try
            {
                _libraryUnitOfWork.studentRepository.Insert(student);
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public List<Student> GetStudentList()
        {
            var studentList = _libraryUnitOfWork.studentRepository.GetStudents();
            return studentList;
        }

        public bool EditStudent(Student student)
        {
            bool updated;
            try
            {
                _libraryUnitOfWork.studentRepository.UpdateStudent(student);
                updated = true;
            }
            catch (Exception)
            {
                updated = false;
            }
            return updated;
        }

        public bool RemoveStudent(Student student)
        {
            bool isDeleted;
            try
            {
                _libraryUnitOfWork.studentRepository.DeleteStudent(student);
                isDeleted = true;
            }
            catch (Exception)
            {
                isDeleted = false;
            }
            return isDeleted;

        }


        public decimal CheckFineAmount(int? studentId)
        {
            return _libraryUnitOfWork.studentRepository.CheckFine(studentId);
        }

        public void ReceiveStudentFine(Student student, decimal paymentAmount)
        {

            _libraryUnitOfWork.studentRepository.ReceiveFine(student, paymentAmount);
        }

        public decimal RemainingFineBalance(decimal fineAmount, decimal paymentAmount)
        {
            var fineBalance = fineAmount - paymentAmount;
            return fineBalance;
        }


    }
}
