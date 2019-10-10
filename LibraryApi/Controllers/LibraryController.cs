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
    public class LibraryController : Controller
    {
        private ILibraryManagementService _managementService;
        public LibraryController(ILibraryManagementService managementService)
        {
            _managementService = managementService;
        }
        // GET: api/Library/book/GetBooks
        [HttpGet("/api/Library/book/GetBooks")]
        public IActionResult Get()
        {
            try
            {
                var bookList = _managementService.GetBooks();
                if (bookList == null)
                    return NotFound("No Book in library");
                return Ok(bookList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/Library/book/SaveBook
        [HttpPost("/api/Library/book/SaveBook")]
        public ActionResult Post([FromBody]Book book)
        {
            try
            {
                var isBookSaved = _managementService.SaveBook(book);
                if (isBookSaved)
                    return Ok(book);
                return NotFound("Oops");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // POST api/Library/IssueBookToMember
        [HttpPost("/api/Library/IssueBookToMember")]
        public ActionResult IssueBook([FromBody]BookIssue bookIssue)
        {
            try
            {
                _managementService.IssueBookToMember(bookIssue);
                return Ok("Book Issued Succesfully");

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST api/Library/ReturnBookFromMember
        [HttpPut("/api/Library/ReturnBookFromMember")]
        public ActionResult ReturnBook([FromBody]ReturnBook returnBook)
        {
            try
            {
                _managementService.SaveReturnBook(returnBook);

                return Ok("Book returned Succesfully");

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


    }
}
