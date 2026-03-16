using System;
namespace ConsoleApp129.Exceptions
{
    public class GameException : Exception
    {
        public GameException(string message) : base(message) { }
    }
}