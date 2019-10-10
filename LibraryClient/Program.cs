using System;
using System.IO;
using System.Net;
using System.Text;
using LibraryCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace LibraryClient
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("*****************************************");
            Console.WriteLine("       Welcome to library system.        ");
            Console.WriteLine("*****************************************");

            LibraryDashboard();

            while (true)
            {
                var choice = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(choice))
                {
                    break;
                }
                else
                {
                    if (int.Parse(choice) == 1)
                    {
                        EntryStudent();
                        LibraryDashboard();

                    }
                    else if (int.Parse(choice) == 2)
                    {
                        EntryBook();
                        LibraryDashboard();
                    }
                    else if (int.Parse(choice) == 3)
                    {
                        IssueBook();
                        LibraryDashboard();
                    }
                    else if (int.Parse(choice) == 4)
                    {
                        ReturnBook();
                        LibraryDashboard();
                    }
                    else if (int.Parse(choice) == 5)
                    {
                        CheckFine();
                        LibraryDashboard();
                    }
                    else if (int.Parse(choice) == 6)
                    {
                        ReceiveFine();
                        LibraryDashboard();
                    }
                    else
                    {
                        Console.WriteLine("Invalid option\nRetry !!!");
                    }
                }
            }

        }

        public static void LibraryDashboard()
        {

            Console.WriteLine("Please enter your choice:");

            Console.WriteLine("# To entry student information enter: 1\n" +
                            "# To entry book information enter: 2\n" +
                            "# To issue a book, enter: 3\n" +
                            "# To return a book enter: 4\n" +
                            "# To check fine, enter: 5\n" +
                            "# To receive fine, enter: 6");
            Console.WriteLine("Enter your choice (1-6) :");

        }
        public static void EntryStudent()
        {

            Console.WriteLine("Please enter student Id: _ :");
            var studentId = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter student name: _ :");
            var studentName = Console.ReadLine();

            var student = new Student
            {
                StudentID = studentId,
                StudentName = studentName,
                FineAmount = 0
            };
            var member = new MemberEntryProcess();
            member.SaveMember(student);

            Console.WriteLine("===========================");

        }
        public static void EntryBook()
        {
            Console.WriteLine("Please enter Book Title: _ :");
            var title = Console.ReadLine();
            Console.WriteLine("Please enter Book Author: _ :");
            var author = Console.ReadLine();
            Console.WriteLine("Please enter Book Edition: _ :");
            var edition = Console.ReadLine();
            Console.WriteLine("Please enter Book Barcode: _ :");
            var barcode = Console.ReadLine();
            Console.WriteLine("Please enter Number of Copy: _ :");
            var copyCount = int.Parse(Console.ReadLine());
            var booktosave = new Book
            {
                Title = title,
                Author = author,
                Edition = edition,
                Barcode = barcode,
                CopyCount = copyCount
            };
            var book = new BookEntryProcess();
            book.SaveBook(booktosave);

            Console.WriteLine("===========================");
        }
        public static void IssueBook()
        {
            Console.WriteLine("Issue to (student Id): _ :");
            var studentId = int.Parse(Console.ReadLine());
            Console.WriteLine("Issued Book (student Id): _ :");
            var bookId = int.Parse(Console.ReadLine());
            var issueDate = DateTime.Now;


            var issueBook = new BookIssue
            {
                StudentId = studentId,
                BookId = bookId,
                IssueDate = issueDate
            };

            var issueBookProcess = new IssueBookProcess();
            issueBookProcess.IssueBook(issueBook);
            Console.WriteLine("=====================================================");


        }

        public static void ReturnBook()
        {
            Console.WriteLine("Return from (student Id): _ :");
            var studentId = int.Parse(Console.ReadLine());
            Console.WriteLine("Return Book (Barcode): _ :");
            var barcode = Console.ReadLine();
            var returnDate = DateTime.Now;
            var returnedBook = new ReturnBook
            {
                StudentId = studentId,
                //BookId=returnedBook.BookId,
                Barcode = barcode,
                ReturnDate = returnDate
            };
            var returnBookProcess = new BookReturnProcess();
            returnBookProcess.ReturnBook(returnedBook);
            Console.WriteLine("=====================================================");
        }

        public static void ReceiveFine()
        {
            Console.WriteLine("Please Enter Student Id : ");
            var studentId = int.Parse(Console.ReadLine());

            Console.WriteLine("Please Enter Fine payment amount : ");
            var paymentFineAmount = decimal.Parse(Console.ReadLine());

            var member = new Student
            {
                StudentID = studentId,
                FineAmount = paymentFineAmount
            };
            var fineReceive = new FineCheckAndReceiveProcess();
            fineReceive.ReceiveFine(member);
            Console.WriteLine("=============================================");

        }
        public static void CheckFine()
        {
            Console.WriteLine("Total fine  of (student Id): _ :");

            var studentId = int.Parse(Console.ReadLine());
            var fc = new FineCheckAndReceiveProcess();
            fc.Checkfine(studentId);

        }
        //public static void BookParser()
        //{
        //    const string url = "https://localhost:5001/api/Library/book/GetBooks";
        //    var request = WebRequest.Create(url);
        //    request.Method = "GET";
        //    request.ContentType = "application/json";
        //    using (var response = request.GetResponse())
        //    {
        //        using (var reponseStream = response.GetResponseStream())
        //        {
        //            using (var reader = new StreamReader(reponseStream))
        //            {
        //                var result = reader.ReadToEnd();
        //                Console.WriteLine(result);
        //                dynamic items = JsonConvert.DeserializeObject(result);
        //                foreach (var item in items)
        //                {
        //                    Console.WriteLine(item);
        //                }
        //            }
        //        }

        //    }
        //}
    }

}