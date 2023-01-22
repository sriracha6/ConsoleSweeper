using System;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleSweeper
{
	public static class Game
	{
		public static (int x, int y) CursorPosition = (0, 0);
		public static int FlagsPlacedCount;
		public static int Timer;

		static List<(int x, int y)> CheckedPositions;

		public static (int number, ConsoleColor color)[] ColorNumberCodes = {
			(0,ConsoleColor.White),
			(1,ConsoleColor.Blue),
			(2,ConsoleColor.Green),
			(3,ConsoleColor.Red),
			(4,ConsoleColor.DarkBlue),
			(5,ConsoleColor.DarkRed),
			(6,ConsoleColor.DarkCyan),
			(7,ConsoleColor.DarkGray),
			(8,ConsoleColor.Gray)
		};

		public static void TimerThread()
		{
			Thread.Sleep(1000);
			Timer++;
		}

		public static void Click(MinesweeperBoard board, (int x, int y) pos)
		{
			if (board.Mines[pos.x, pos.y]) End();
			board.Opened[pos.x, pos.y] = true;
			CheckedPositions = new List<(int x, int y)>();
            FloodOpen(board, pos.x, pos.y);
		}

		public static void PowerClick(MinesweeperBoard board, (int x, int y) pos)
		{
			if (board.GetCellNeighborFlagCount(pos.x, pos.y) != board.Numbers[pos.x, pos.y]) return;

			for(int i = -1; i <= 1; i++)
				for(int j = -1; j <= 1; j++)
					Click(board, pos);
		}

		public static bool InBounds(MinesweeperBoard board, int x, int y)
		{
			return !((x >= board.Width) || (x < 0) || (y >= board.Height) || (y < 0));
        }

		public static void FloodOpen(MinesweeperBoard board, int x, int y)
		{
			if (CheckedPositions.Contains((x, y))) return;
			if (!InBounds(board, x, y)) return;
			if (board.Numbers[x, y] != 0) return;

			CheckedPositions.Add((x, y)); // this entire thing is because of a stupid bug I can't find the solution to. without using this list, there is a stackoverflow.

			for (int i = -1; i <= 1; i++)
				for (int j = -1; j <= 1; j++)
					if (!InBounds(board, x + i, y + j)) continue;
					else board.Opened[x + i, y + j] = true;

			FloodOpen(board, x + 1, y + 1);
			FloodOpen(board, x - 1, y - 1);
			FloodOpen(board, x - 1, y + 1);
			FloodOpen(board, x + 1, y - 1);
		}

		public static void End()
		{
			Console.Clear();
			Console.WriteLine("You lose.");
			while (true) { }
		}
	}
}