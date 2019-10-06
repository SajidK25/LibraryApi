using System;

namespace LibraryCore
{
    public class ReturnBook
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        //public Student Student { get; set; }
        public string Barcode { get; set; }
        public int BookId { get; set; }
        //public Book Book { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
