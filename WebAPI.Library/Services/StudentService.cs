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

            var student = _libraryUnitOfWork.StudentRepository.GetSingleStudent(studnetId);
            //var student = new Student();
            return student;
        }

        public bool SaveStudent(Student student)
        {
            bool status;
            try
            {
                _libraryUnitOfWork.StudentRepository.Insert(student);
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
            var studentList = _libraryUnitOfWork.StudentRepository.GetStudents();
            return studentList;
        }

        public bool EditStudent(Student student)
        {
            bool updated;
            try
            {
                //student = null;
                _libraryUnitOfWork.StudentRepository.UpdateStudent(student);
                _libraryUnitOfWork.Save();
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
                _libraryUnitOfWork.StudentRepository.DeleteStudent(student);
                _libraryUnitOfWork.Save();
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
            //studentId=null;
            return _libraryUnitOfWork.StudentRepository.CheckFine(studentId);
        }

        public void ReceiveStudentFine(Student student, decimal paymentAmount)
        {
            var fineBalance = student.FineAmount - paymentAmount;
            if (paymentAmount > student.FineAmount)
            {
                throw new InvalidOperationException("Sorry!! Your Payment is greater then Balance.");
            }
            else
            {
                _libraryUnitOfWork.StudentRepository.ReceiveFine(student, fineBalance);
                _libraryUnitOfWork.Save();
            }
        }




    }
}
