using System;
namespace ConsoleApp129.Exceptions
{
    public class HeroDeathException : GameException
    {
        public HeroDeathException(string message) : base(message) { }
    }
}