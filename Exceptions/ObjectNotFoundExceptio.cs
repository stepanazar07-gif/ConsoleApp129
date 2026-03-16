using System;
namespace ConsoleApp129.Exceptions
{
    public class ObjectNotFoundException : GameException
    {
        public ObjectNotFoundException(string message) : base(message) { }
    }
}