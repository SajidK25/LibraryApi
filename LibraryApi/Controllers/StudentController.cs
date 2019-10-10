using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Library.Services;
using Microsoft.AspNetCore.Mvc;
using LibraryCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        //GET: api/student/GetStudentList
        [HttpGet("/api/student/GetStudentList")]
        public IActionResult ListStudents()
        {
            try
            {
                var students = _studentService.GetStudentList();
                if (students == null)
                    return NotFound();
                return Ok(students);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        // GET api/student/GetStudent
        [HttpGet("/api/student/GetStudent")]
        public ActionResult<Student> ShowStudent(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var student = _studentService.GetStudent(id);
                if (student == null)
                    return NotFound();
                return Ok(student);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST api/student/SaveStudent
        [HttpPost("/api/student/SaveStudent")]
        public ActionResult CreateStudent([FromBody] Student student)
        {
            try
            {
                var status = _studentService.SaveStudent(student);
                if (status)
                    return Ok("New Student Inserted");
                return NotFound("Oops!");
            }
            catch (Exception)
            {
                return BadRequest();
            }


        }

        // PUT /api/student/EditStudent
        [HttpPut("/api/student/EditStudent")]
        public ActionResult StudentUpdate([FromBody] Student student)
        {
            try
            {

                var updated = _studentService.EditStudent(student);
                if (updated)
                    return Ok("Student Data updated successfully");
                return NotFound("not updated");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // DELETE /api/student/RemoveStudent
        [HttpDelete("/api/student/RemoveStudent")]
        public ActionResult StudentRemove(Student student)
        {
            try
            {
                var deleted = _studentService.RemoveStudent(student);
                if (deleted)
                    return Ok("Student deleted Succesfully");
                return NotFound("Not Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET api/student/CheckStudent
        [HttpGet("/api/student/CheckFineAmount")]
        public ActionResult CheckFineAmount(int? studentId)
        {
            if (studentId == null)
            {
                return BadRequest();
            }
            try
            {
                var FineAmount = _studentService.CheckFineAmount(studentId);
                return Ok(FineAmount);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT /api/student/ReceiveStudentFine
        [HttpPut("/api/student/ReceiveStudentFine")]
        public ActionResult FineUpdate([FromBody] Student student)
        {
            try
            {

                var member = _studentService.GetStudent(student.StudentID);
                var fineAmount = member.FineAmount;
                var RemainingFineBalance = _studentService.RemainingFineBalance(fineAmount, student.FineAmount);

                if (member.FineAmount < student.FineAmount)
                {
                    return Ok("Sorry!! Your Payment is greater then Balance.");
                }
                else
                {
                    _studentService.ReceiveStudentFine(member, RemainingFineBalance);
                    return Ok(member.FineAmount);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
