using System;
using System.Collections.Generic;
using LibraryCore;

namespace WebAPI.Library.Services
{
    public interface IStudentService
    {
        Student GetStudent(int? studnetId);
        bool SaveStudent(Student student);
        List<Student> GetStudentList();
        bool EditStudent(Student student);
        bool RemoveStudent(Student student);
        decimal CheckFineAmount(int? studentId);
        void ReceiveStudentFine(Student student, decimal amount);

    }
}
