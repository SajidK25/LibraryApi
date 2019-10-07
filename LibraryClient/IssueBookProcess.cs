using System;
using System.IO;
using System.Net;
using System.Text;
using LibraryCore;
using Newtonsoft.Json;
namespace LibraryClient
{
    public class IssueBookProcess
    {
        public void IssueBook(BookIssue bookIssue)
        {
            const string url = "https://localhost:5001/api/Library/IssueBookToMember";
            var request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";

            var requestContent = JsonConvert.SerializeObject(bookIssue);
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
