using System;
namespace LibraryClient
{
    public class ConsoleReadProcessor : IInputProcessor
    {

        public string Read()
        {
            return Console.ReadLine();
        }
    }
}
