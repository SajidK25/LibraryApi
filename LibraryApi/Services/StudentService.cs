using System;
using System.Collections.Generic;
using LibraryApi.Repositories;

namespace LibraryApi.Services
{
    public class StudentService : IStudentService
    {
        private IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public Student GetStudent(int? studnetId)
        {

            var student = _studentRepository.GetSingleStudent(studnetId);
            return student;
        }

        public bool SaveStudent(Student student)
        {
            bool status;
            try
            {
                _studentRepository.Insert(student);
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
            var studentList = _studentRepository.GetStudents();
            return studentList;
        }

        public bool EditStudent(Student student)
        {
            bool updated;
            try
            {
                _studentRepository.UpdateStudent(student);
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
                _studentRepository.DeleteStudent(student);
                isDeleted = true;
            }
            catch (Exception)
            {
                isDeleted = false;
            }
            return isDeleted;

        }


    }
}
