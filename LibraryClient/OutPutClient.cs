using System;
namespace LibraryClient
{
    public class OutPutClient : IOutPutClient
    {
        public OutPutClient()
        {
        }

        public void Write(string text)
        {
            Console.WriteLine(text);
        }
    }
}
