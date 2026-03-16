using System;
namespace ConsoleApp129.Exceptions
{
    public class SaveGameException : GameException
    {
        public SaveGameException(string message) : base(message) { }
    }
}