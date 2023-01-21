using System;

namespace ConsoleSweeper
{
	public static class Game
	{
		public static (int x, int y) CursorPosition = (0,0);
		public static int FlagsPlacedCount;

		public static (int number, ConsoleColor color)[] ColorNumberCodes = {
			(0, ConsoleColor.White),
			(1,ConsoleColor.Blue), 
			(2,ConsoleColor.Green),
			(3,ConsoleColor.Red),
			(4,ConsoleColor.DarkBlue),
			(5,ConsoleColor.DarkRed),
			(6,ConsoleColor.DarkCyan),
			(7,ConsoleColor.DarkGray),
			(8,ConsoleColor.Gray)
		};
	}
}