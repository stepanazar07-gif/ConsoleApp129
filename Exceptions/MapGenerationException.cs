using System;
namespace ConsoleApp129.Exceptions
{
    public class MapGenerationException : GameException
    {
        public int Level { get; }
        public MapGenerationException(string message, int level) : base(message)
        {
            Level = level;
        }
    }
}