using System;
using System.Collections.Generic;
using static System.Console;

namespace Project
{
    public class SnakeGame
    {
        private int score = 0;
        private int length = 0;
        private int speed = 500;
        
        ScheduleTimer _timer;
        RenderGame board = new RenderGame();
        
        static Random rand = new Random ();
        
        Pixel head = new Pixel (WindowWidth / 2, WindowHeight / 2, ConsoleColor.Red);
        Pixel berry = new Pixel (rand.Next (1, WindowWidth - 2), rand.Next (1, WindowHeight - 2), ConsoleColor.Cyan);
        List<Pixel> body = new List<Pixel> ();
        
        enum Direction
        {
            Up,
            Down,
            Right,
            Left
        }
        Direction currentMovement = Direction.Right;
        
        
        
        public bool Paused { get; private set; }
        public bool GameOver { get; private set; }

        
        public void Start()
        {
            board.DrawBorder();

            berry.DrawPixel();

            ScheduleNextTick(speed);
            
        }

        
        public void Input(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    currentMovement = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    currentMovement = Direction.Down;
                    break;
                case ConsoleKey.LeftArrow:
                    currentMovement = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    currentMovement = Direction.Right;
                    break;
            }
        }

        public void Pause()
        {
            ForegroundColor = ConsoleColor.DarkMagenta;
            WriteLine("*************    Pause   *************");
            _timer.Pause();
            Paused = true;
        }

        public void Resume()
        {
            _timer.Resume();
            Paused = false;
        }

        public void Stop()
        {
            Clear();
            board.DrawBorder();
            SetCursorPosition(WindowWidth / 2 - 10, WindowHeight / 2);
            WriteLine("Game is stopped!");
            SetCursorPosition(WindowWidth / 2 - 10, WindowHeight / 2 + 1);
            WriteLine("Score:" + score);
        }

        public void Lose()
        {
            Clear();
            board.DrawBorder();
            SetCursorPosition(WindowWidth / 2 - 10, WindowHeight / 2);
            WriteLine("GAME OVER!");
            SetCursorPosition(WindowWidth / 2 - 10, WindowHeight / 2 + 1);
            WriteLine("Score:" + score);
            SetCursorPosition(WindowWidth / 2 - 10, WindowHeight / 2 + 2);
            WriteLine("Press R to play again.");
            
            while (true)
            {
                var keypress = Console.ReadKey(true);
                if (keypress.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
                if (keypress.Key == ConsoleKey.R)
                {
                    GameOver = false;
                    head = new Pixel (WindowWidth / 2, WindowHeight / 2, ConsoleColor.Red);
                    berry = new Pixel (rand.Next (1, WindowWidth - 2), rand.Next (1, WindowHeight - 2), ConsoleColor.Cyan);
                    body.Clear();
                    score = 0;
                    length = 0;
                    speed = 500;
                    Start();
                    break;
                }
            }
        }
        
        void Snake()
        {
            Clear();
            
            board.DrawBorder();
            
            
            if (head.XPos >= WindowWidth - 1
                || head.XPos <= 0 
                || head.YPos >= WindowHeight - 1
                || head.YPos <= 0)
            {
                GameOver = true;
            }

            if (berry.XPos == head.XPos && berry.YPos == head.YPos)
            {
                score++;
                length++;
                speed = speed - 50;
                berry = new Pixel(rand.Next(1, WindowWidth - 2), rand.Next(1, WindowHeight - 2), ConsoleColor.Cyan);
            }
            
            for (int i = 0; i < body.Count; i++)
            {
                body[i].DrawPixel();

                if (body[i].XPos == head.XPos && body[i].YPos == head.YPos)
                {
                    GameOver = true;
                }
            }

            head.DrawPixel();
            berry.DrawPixel();
            body.Add (new Pixel (head.XPos, head.YPos, ConsoleColor.Green));
            
            
            switch (currentMovement)
            {
                case Direction.Up:
                    head.YPos--;
                    break;
                case Direction.Down:
                    head.YPos++;
                    break;
                case Direction.Left:
                    head.XPos--;
                    break;
                case Direction.Right:
                    head.XPos++;
                    break;
            }
            
            
            if (body.Count > length)
            {
                body.RemoveAt (0);
            }

            if (GameOver)
            {
                Lose();
            }
            else
            {
                ScheduleNextTick(speed);
            }
            
        }
        

        void ScheduleNextTick(int speed)
        {
            _timer = new ScheduleTimer(speed, Snake);
        }
        
    }
}