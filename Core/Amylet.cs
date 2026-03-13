using ConsoleApp129;
using System;

class Amulet : MapObject
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsPicked { get; set; }
    public bool IsVisible { get; set; }  // ← ДОБАВИТЬ!

    public Amulet(int x, int y)
    {
        X = x;
        Y = y;
        IsPicked = false;
     
    }

    public override char Rendering_on_the_map()
    {
        if (!IsPicked && IsVisible)  // ← Показывать только если видим и не подобран
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            return '*';
        }
        return ' ';  // ← Скрыт
    }
}