using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntSimulator
{
    public class Grid
    {
        public Tile[,] grid;

        public int Width { get => width; }
        public int Height { get => height; }

        int width;
        int height;

        public Grid(int width = 50, int height = 20)
        {
            this.width = width;
            this.height = height;

            // Create Grid
            grid = new Tile[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Tile tile = new Tile(x, y);

                    // if y height/2, It will not be air or dirt.
                    if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                        tile.isWall = true;
                    else if (y < height/2)
                        tile.isAir = true;
                    else if (y > height/2)
                        tile.isDirt = true;
                    
                    grid[y, x] = tile;
                }
            }
        }

        public void Draw()
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);

            //string horizontalBar = new('─', width);
            //string top = '┌' + horizontalBar + '┐';

            //Console.WriteLine(top);

            for (int y = 0; y < height; y++)
            {
                //Console.Write('│');
                for (int x = 0; x < width; x++)
                {
                    Tile tile = grid[y, x];
                    Char drawChar = ' ';

                    Console.ForegroundColor = ConsoleColor.Black;

                    if (tile.isWall)
                        Console.BackgroundColor = ConsoleColor.Gray;
                    else if (tile.isDirt)
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                    else if (tile.isAir)
                        Console.BackgroundColor = ConsoleColor.Blue;
                    else
                        Console.BackgroundColor = ConsoleColor.Yellow;

                    if (tile.ants.Count > 0)
                    {
                        drawChar = tile.ants[0].Symbol;
                    }
                    else if (tile.foodCount > 0 && tile.foodCount < 10)
                    {
                        drawChar = '@';
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    Console.Write(drawChar);

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine();
                //Console.WriteLine('│');
            }

            Console.WriteLine();
        }
    }
}
