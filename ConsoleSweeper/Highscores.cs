using System;
using System.Runtime.CompilerServices;

namespace ConsoleSweeper
{
    public static class Highscores
    {
        public const string FILE_NAME = "highscores.txt";

        public static void SaveNewHighScore(MinesweeperBoard board, int time)
        {
            if (!File.Exists(FILE_NAME)) File.Create(FILE_NAME);
            string startStr = $"{board.Width},{board.Height},{board.MineCount},";

            if (File.ReadAllLines(FILE_NAME).Where(x => x.StartsWith(startStr)).FirstOrDefault() == null)
                File.AppendAllText(FILE_NAME, $"{board.Width},{board.Height},{board.MineCount},{time}");
            else
            {
                string[] lines = File.ReadAllLines(FILE_NAME);
                lines[lines.ToList().FindIndex(x => x.StartsWith(startStr))] = startStr + time;
                File.WriteAllLines(FILE_NAME, lines);
            }
        }

        public static int GetHighScore(MinesweeperBoard board)
        {
            if (!File.Exists(FILE_NAME)) File.Create(FILE_NAME);
            string startStr = $"{board.Width},{board.Height},{board.MineCount},";
            int res = Convert.ToInt32(File.ReadAllLines(FILE_NAME).Where(x => x.StartsWith(startStr)).FirstOrDefault()?.Substring(startStr.Length));
            return res == 0 ? -1 : res;
        }
    }
}