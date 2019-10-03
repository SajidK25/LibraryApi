using System;

namespace LibraryApi
{
    public class BookIssue
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        //public Student Student { get; set; }
        public int BookId { get; set; }
        //public Book Book { get; set; }
        public string Barcode { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
