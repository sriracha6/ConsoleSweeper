using System;

namespace ConsoleSweeper
{
	public static class GameSetup
	{
        public static void End() // keeping this the same as win because i might add viewing where the mines were
        {
            Console.Clear();
            Console.WriteLine("You lose.");
            Console.WriteLine("Press [ n ] to start a new game and [ q ] to quit.");
            var key = Console.ReadKey().Key;
            if (key == ConsoleKey.N)
            {
                Game.ActiveBoard = NewGame();
                Game.DoTimer = true;
                Game.BestTime = Highscores.GetHighScore(Game.ActiveBoard);
            }
            else Environment.Exit(0);
        }

        public static void Win()
        {
            Console.Clear();
            Console.WriteLine($"You won! You won in {Game.Timer} seconds.");
            string hsstring = (Game.BestTime == -1 ? "not set." : Game.BestTime.ToString() + "seconds");
            if (Game.Timer < Game.BestTime || Game.BestTime != -1)
                Console.WriteLine($"Your highscore is {hsstring}");
            else
            {
                Console.WriteLine($"That is a new highscore! Your old highscore was {hsstring}");
                Highscores.SaveNewHighScore(Game.ActiveBoard, Game.Timer);
            }
            Console.WriteLine();
            Console.WriteLine("Press [ n ] to start a new game and [ q ] to quit.");
            var key = Console.ReadKey().Key;
            if (key == ConsoleKey.N)
            {
                Game.ActiveBoard = NewGame();
                Game.DoTimer = true;
                Game.BestTime = Highscores.GetHighScore(Game.ActiveBoard);
            }
            else Environment.Exit(0);
            Console.Clear();
        }

        public static MinesweeperBoard NewGame()
        {
            Game.Timer = 0;
            Game.DoTimer = false;

            Console.Clear();
            Console.WriteLine("ConsoleSweeper v1.0\n\n");
            Console.WriteLine("[ n ] New Game");
            Console.WriteLine("[ i ] View Info");
            Console.WriteLine("[ q ] Quit");
            var key = Console.ReadKey().Key;
            if (key == ConsoleKey.N) return NewGameType();
            else if (key == ConsoleKey.I) ViewInfo();
            else Environment.Exit(0);
            return null;
        }
        
        public static void ViewInfo()
        {
            Console.Clear();
            Console.WriteLine("Minesweeper with standard rules.");
            Console.WriteLine("To move the cursor (selected tile), use WASD.");
            Console.WriteLine("To flag a tile, press [ i ].");
            Console.WriteLine("To click a tile, press [ o ].");
            Console.WriteLine("To click all open bordering tiles of a cell, press [ p ].\n");
            Console.WriteLine("To go back, press any key.");
            Console.ReadKey();
            NewGame();
        }

        public static MinesweeperBoard NewGameType()
        {
            Console.Clear();
            Console.WriteLine("[ e ] New Easy Game (8x8 10 mines)");
            Console.WriteLine("[ m ] New Medium Game (16x16 40 mines)");
            Console.WriteLine("[ h ] New Expert Game (24x24 99 mines)");
            Console.WriteLine("[ c ] New Custom Game");
            Console.WriteLine("[ q ] Go Back");
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.E) return new MinesweeperBoard(8, 8, 10);
            if (key == ConsoleKey.M) return new MinesweeperBoard(16, 16, 40);
            if (key == ConsoleKey.H) return new MinesweeperBoard(24, 24, 99);
            Console.WriteLine("Insert a game size with mines in the follow format, ie: 10,10,5");
            if (key == ConsoleKey.C)
            {
                while (true)
                {
                    string line = Console.ReadLine();
                    var spl = line.Split(',');
                    int[] param = new int[3];
                    for (int i = 0; i < spl.Length; i++)
                        if (int.TryParse(spl[i], out int par))
                            param[i] = par;
                        else
                            continue;

                    if (param[0] > Console.WindowWidth || param[0] <= 0) { Console.WriteLine("Width too big or small for console size"); continue; }
                    if (param[1] > Console.WindowHeight - 1 || param[1] <= 0) { Console.WriteLine("Height too big or small for console size"); continue; }
                    if (param[2] > param[0] * param[1] - 1 || param[2] <= 0) { Console.WriteLine("Too many/few mines"); continue; }
                    return new MinesweeperBoard(param[0], param[1], param[2]);
                }
            }
            NewGame();
            return null;
        }
    }
}