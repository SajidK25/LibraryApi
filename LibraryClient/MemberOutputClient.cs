using System;
namespace LibraryClient
{
    public class MemberOutputClient : IMemberOutputClient
    {
        private IOutPutClient _client;
        public MemberOutputClient(IOutPutClient client)
        {
            _client = client;
        }

        public void ShowInitialMessage()
        {
            const string msg1 = "To be a new member you have to provide your StudentID and Name :";
            const string msg2 = "===============================================================";
            _client.Write(msg1);
            _client.Write(msg2);

        }
        public void ShowEnterSIdMessage()
        {
            const string msg3 = "Please enter student Id: _:";
            _client.Write(msg3);
        }

        public void ShowEnterSNameMessage()
        {
            const string msg3 = "Please enter student name: _ :";
            _client.Write(msg3);
        }
    }
}
