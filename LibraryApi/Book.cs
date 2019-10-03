using System.Collections.Generic;

namespace LibraryApi
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Edition { get; set; }
        public string Barcode { get; set; }
        public int CopyCount { get; set; }
        //public IList<BookIssue> Students { get; set; }

    }
}
