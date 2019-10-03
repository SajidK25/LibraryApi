using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;

namespace LibraryApi.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private LibraryContext _context;
        public StudentRepository(LibraryContext context)
        {
            _context = context;
        }

        public Student GetSingleStudent(int? studentId)
        {
            return _context.Students.Where(s => s.StudentID == studentId).FirstOrDefault();
        }

        public void Insert(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public List<Student> GetStudents()
        {
            var students = _context.Students.OrderBy(s => s.StudentID).ToList();
            return students;
        }

        public void UpdateStudent(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }

        public void DeleteStudent(Student student)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }

        public decimal CheckFine(int? studentId)
        {
            return _context.Students
                    .Where(s => s.StudentID == studentId)
                    .Select(s => s.FineAmount)
                    .FirstOrDefault();
        }

        public void ReceiveFine(Student student, decimal paymentAmount)
        {
            student.FineAmount = paymentAmount;
            _context.Students.Update(student);
            _context.SaveChanges();
        }
    }
}
