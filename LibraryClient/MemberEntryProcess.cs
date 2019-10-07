using System;
using System.IO;
using System.Net;
using System.Text;
using LibraryCore;
using Newtonsoft.Json;

namespace LibraryClient
{
    public class MemberEntryProcess
    {
        private IInputProcessor _inputProcessor;
        private IMemberOutputClient _memberOutputClient;
        public MemberEntryProcess()
        {

        }
        public MemberEntryProcess(IInputProcessor inputProcessor, IMemberOutputClient memberOutputClient)
        {
            _inputProcessor = inputProcessor;
            _memberOutputClient = memberOutputClient;
        }

        public void StudentIdEntryMessage()
        {
            _memberOutputClient.ShowEnterSIdMessage();
        }
        public int StudentIdInput()
        {
            var studentId = int.Parse(_inputProcessor.Read());
            return studentId;
        }
        public void StudentNameEntryMessage()
        {
            _memberOutputClient.ShowEnterSNameMessage();
        }

        public string StudentNameInput()
        {
            var studentName = Console.ReadLine();
            return studentName;
        }


        public void entrySuccesfullMesage()
        {
            Console.WriteLine("Student Entry is succesful");
            Console.WriteLine("===========================");
        }

        public void SaveMember(Student student)
        {
            const string url = "https://localhost:5001/api/student/SaveStudent";
            var request = WebRequest.Create(url);
            request.Method = "POST";
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
