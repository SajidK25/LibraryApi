using System;
using System.IO;
using System.Net;
using System.Text;
using LibraryCore;
using Newtonsoft.Json;

namespace LibraryClient
{
    public class FineCheckAndReceiveProcess
    {
        public FineCheckAndReceiveProcess()
        {

        }
        public void Checkfine(int studentId)
        {
            const string url = "https://localhost:5001/api/student/CheckFineAmount";
            int sid = studentId;
            var request = WebRequest.Create(url + "?StudentId=" + sid);
            request.Method = "GET";
            request.ContentType = "application/json";
            using (var response = request.GetResponse())
            {
                using (var reponseStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(reponseStream))
                    {
                        var result = reader.ReadToEnd();
                        Console.WriteLine(result);
                        dynamic items = JsonConvert.DeserializeObject(result);
                        foreach (var item in items)
                        {
                            Console.WriteLine(item);
                        }
                    }
                }

            }
        }

        public void ReceiveFine(Student student)
        {
            const string url = "https://localhost:5001/api/student/ReceiveStudentFine";
            var request = WebRequest.Create(url);
            request.Method = "PUT";
            request.ContentType = "application/json";

            var requestContent = JsonConvert.SerializeObject(student);
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

