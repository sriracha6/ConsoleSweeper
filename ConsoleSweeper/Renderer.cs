using System;
using System.Linq;

namespace ConsoleSweeper
{
	public static class Renderer
	{
		public static void Render(MinesweeperBoard board)
		{
			Console.Clear();
			for(int x = 0; x < board.Width; x++)
			{
				for(int y = 0; y < board.Height; y++)
				{
					Console.Clear();
					if ((x, y) == Game.CursorPosition) Console.BackgroundColor = ConsoleColor.White;
					if (board.Flags[x, y]) { Console.Write('\\'); continue; }
					if (!board.Opened[x, y]) { Console.Write('#'); continue; }

					int mcount = board.GetCellNeighborMineCount(x, y);
					Console.ForegroundColor = Game.ColorNumberCodes.Where(x => x.number == mcount).First().color;
					Console.Write(mcount > 0 ? mcount : ' ');
					Console.ResetColor();
				}
				Console.Write('\n');
			}
		}
	}
}
