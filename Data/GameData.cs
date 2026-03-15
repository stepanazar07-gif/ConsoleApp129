using System.Collections.Generic;

namespace ConsoleApp129.Save
{
    public class GameData
    {
        public int MapLevel { get; set; } = 1;

        public int HeroX { get; set; }
        public int HeroY { get; set; }
        public int HeroHP { get; set; }
        public int HeroMaxHP { get; set; }
        public int HeroArmor { get; set; }
        public int HeroLevel { get; set; }

      
        public bool HasAmulet { get; set; }
        public bool HasCrown { get; set; }
        public bool HasScepter { get; set; }

        
        public int FreezeTurns { get; set; }
        public int SandTurns { get; set; }
        public int PoisonTurns { get; set; }

        public List<MapItem> Items { get; set; } = new List<MapItem>();
    }

    public class MapItem
    {
        public string Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}