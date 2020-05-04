using System;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new SnakeGame();
            game.Start();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case ConsoleKey.P:

                            if (game.Paused)
                                game.Resume();
                            else 
                                game.Pause();

                            break;
                        
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.DownArrow:
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.RightArrow:

                            if (!game.Paused)
                                game.Input(key.Key);

                            break;

                        case ConsoleKey.Escape:

                            game.Stop();
                            return;
                    }
                }
            } 
        }
    }
}