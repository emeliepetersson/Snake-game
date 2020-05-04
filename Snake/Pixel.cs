using System;
using static System.Console;

namespace Project
{
    public class Pixel
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        private ConsoleColor ScreenColor { get; set; }

        public Pixel (int xPos, int yPos, ConsoleColor color)
        {
            XPos = xPos;
            YPos = yPos;
            ScreenColor = color;
        }
        
        public void DrawPixel ()
        {
            SetCursorPosition (XPos, YPos);
            ForegroundColor = ScreenColor;
            WriteLine("■");
            SetCursorPosition (0, 0);
        }
    }
    
    
}