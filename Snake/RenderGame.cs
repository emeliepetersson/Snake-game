using System;
using static System.Console;

namespace Project
{
    public class RenderGame
    {
        public void DrawBorder ()
        {
            for (int i = 0; i < WindowWidth; i++)
            {
                ForegroundColor = ConsoleColor.DarkMagenta;
                
                SetCursorPosition (i, 0);
                Write ("■");

                SetCursorPosition (i, WindowHeight - 1);
                Write ("■");
            }

            for (int i = 0; i < WindowHeight; i++)
            {
                SetCursorPosition (0, i);
                Write ("■");

                SetCursorPosition (WindowWidth - 1, i);
                Write ("■");
            }
        }
        
    }
}