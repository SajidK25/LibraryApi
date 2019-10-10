using System;
using System.IO;
using System.Net;
using System.Text;
using LibraryCore;
using Newtonsoft.Json;

namespace LibraryClient
{
    public class BookReturnProcess
    {
        public void ReturnBook(ReturnBook returnBook)
        {
            const string url = "https://localhost:5001/api/Library/ReturnBookFromMember";
            var request = WebRequest.Create(url);
            request.Method = "PUT";
            request.ContentType = "application/json";

            var requestContent = JsonConvert.SerializeObject(returnBook);
            var data = Encoding.UTF8.GetBytes(requestContent);
            request.ContentLength = data.Length;

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(data, 0, data.Length);
                requestStream.Flush();//
            }
            using (var response = request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        var result = reader.ReadToEnd();
                        Console.WriteLine(result);
                    }
                }
            }

        }

    }
}
