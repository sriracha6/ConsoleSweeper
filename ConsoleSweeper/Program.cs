using System;

namespace ConsoleSweeper
{
    /*
     * CONTROLS
     * 
     * WASD: change cursor position
     * I: set flag
     * O: click
     * P: power click
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            Game.ActiveBoard = GameSetup.NewGame();

            Thread timerThread = new Thread(Game.TimerThread);
            timerThread.Start();
            Game.BestTime = Highscores.GetHighScore(Game.ActiveBoard);
            Game.DoTimer = true;
            Renderer.Render(Game.ActiveBoard);
            while (true)
            {
                var board = Game.ActiveBoard;
                var key = Console.ReadKey().Key;
                var cpos = Game.CursorPosition;
                if (key == ConsoleKey.S && cpos.y + 1 < board.Height)   Game.CursorPosition.y++;
                if (key == ConsoleKey.W && cpos.y - 1 >= 0)             Game.CursorPosition.y--;
                if (key == ConsoleKey.D && cpos.x + 1 < board.Width)    Game.CursorPosition.x++;
                if (key == ConsoleKey.A && cpos.x - 1 >= 0)             Game.CursorPosition.x--;

                if (key == ConsoleKey.I && !board.Opened[cpos.x, cpos.y]) board.Flags[cpos.x, cpos.y] = !board.Flags[cpos.x,cpos.y];
                if (key == ConsoleKey.O) Game.Click(board, cpos);
                if (key == ConsoleKey.P) Game.PowerClick(board, cpos);

                Renderer.Render(Game.ActiveBoard);
            }
        }
    }
}