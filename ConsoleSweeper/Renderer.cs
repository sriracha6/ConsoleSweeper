using System;
using System.Linq;

namespace ConsoleSweeper
{
	public static class Renderer
	{
		public static void Render(MinesweeperBoard board)
		{
			Console.Clear();
			for(int y = 0; y < board.Height; y++)
			{
				for(int x = 0; x < board.Width; x++)
				{
					Console.ResetColor();
					if ((x, y) == Game.CursorPosition) Console.BackgroundColor = ConsoleColor.DarkYellow;
					if (board.Flags[x, y]) { Console.Write('\\'); continue; }
					if (!board.Opened[x, y]) { Console.Write('#'); continue; }

					int mcount = board.Numbers[x, y];
					Console.ForegroundColor = Game.ColorNumberCodes.Where(x => x.number == mcount).First().color;
					Console.Write(mcount > 0 ? mcount.ToString() : " ");
				}
				Console.Write('\n');
			}
            Console.ResetColor();
            Console.Write('\n');
			Console.WriteLine($"Flags: {board.GetTotalFlagCount()} | Mines: {board.MineCount} | {Game.Timer} | {board.Width}x{board.Height} | {Game.CursorPosition}");
		}
	}
}
