using System;

namespace ConsoleSweeper
{
    /*
     * CONTROLS
     * 
     * WASD: change cursor position
     * E: set flag
     * Q: click
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            MinesweeperBoard board = new MinesweeperBoard(15,15, 45);
            
            Renderer.Render(board);
            while (true)
            {
                var key = Console.ReadKey().Key;
                var cpos = Game.CursorPosition;
                if (key == ConsoleKey.W && cpos.y + 1 < board.Height)   Game.CursorPosition.y++;
                if (key == ConsoleKey.S && cpos.y - 1 >= 0)             Game.CursorPosition.y--;
                if (key == ConsoleKey.D && cpos.x + 1 < board.Width)    Game.CursorPosition.x++;
                if (key == ConsoleKey.A && cpos.x - 1 >= 0)             Game.CursorPosition.x--;

                if (key == ConsoleKey.E) board.Flags[cpos.x, cpos.y] = !board.Flags[cpos.x,cpos.y];

                Renderer.Render(board);
            }
        }
    }
}