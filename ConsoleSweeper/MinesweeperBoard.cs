using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleSweeper
{
    public class MinesweeperBoard
    {
        public int Width { get; }
        public int Height { get; }
        public int MineCount { get; }

        public bool[,] Mines;
        public bool[,] Flags;
        public bool[,] Opened;
        public int[,] Numbers;
        
        public int GetTotalFlagCount()
        {
            int total = 0;
            for(int x = 0; x < Width; x++)
            {
                for(int y = 0; y < Height; y++)
                {
                    if (Flags[x, y]) total++;
                }
            }
            return total;
        }

        public int GetCellNeighborMineCount(int x, int y)
        {
            int total = 0;
            for(int i = -1; i <= 1; i++)
            {
                for(int j = -1; j <= 1; j++)
                {
                    if (x + i < 0 || x + i >= Width || y + j >= Height || y + j < 0) continue;
                    if (Mines[x + i,y + j])
                        total++;
                }
            }
            return total;
        }

        public int GetCellNeighborFlagCount(int x, int y)
        {
            int total = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (x + i < 0 || x + i >= Width || y + j >= Height || y + j < 0) continue;
                    if (Flags[x + i, y + j])
                        total++;
                }
            }
            return total;
        }

        public int GetNumberOfUnopenedCells()
        {
            int total = 0;
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    if (!Opened[x, y]) total++;
            return total;
        }

        public MinesweeperBoard(int width, int height, int mines)
        {
            if (mines >= width * height) throw new ArgumentOutOfRangeException();
            
            Mines = new bool[width, height];
            Flags = new bool[width, height];
            Opened = new bool[width, height];
            Numbers = new int[width, height];
            
            Width = width;
            Height = height;
            MineCount = mines;

            Random rnd = new Random();

            for(int i = 0; i < mines; i++)
            {
                while (true) 
                {
                    int x = rnd.Next(0, width);
                    int y = rnd.Next(0, height);
                    if (!Mines[x,y])
                    {
                        Mines[x, y] = true;
                        break;
                    }
                }
            }

            for(int x = 0; x < Width; x++)
                for(int y = 0; y < Height; y++)
                    Numbers[x, y] = GetCellNeighborMineCount(x,y);
        }
    }
}