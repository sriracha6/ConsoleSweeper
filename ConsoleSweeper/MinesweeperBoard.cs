using System;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleSweeper
{
    public class MinesweeperBoard
    {
        bool[,] brd;

        public MinesweeperBoard(int width, int height, int mines)
        {
            if (mines >= width * height) throw new ArgumentOutOfRangeException();
            brd = new bool[width, height];

            Random rnd = new Random();

            for(int i = 0; i < mines; i++)
            {
                while (true) 
                {
                    int x = rnd.Next(0, width);
                    int y = rnd.Next(0, height);
                    if (!brd[x,y])
                    {
                        brd[x, y] = true;
                        break;
                    }
                }
            }
        }
    }
}