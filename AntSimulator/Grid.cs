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

        public Grid(int width = 80, int height = 30)
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
                        tile.State = TileState.Wall;
                    else if (y < height/2)
                        tile.State = TileState.Air;
                    else if (y > height/2)
                        tile.State = TileState.Dirt;
                    
                    grid[y, x] = tile;
                }
            }
        }

        public void DrawTiles(HashSet<Tile> tiles)
        {
            foreach (Tile tile in tiles)
            {
                Console.SetCursorPosition(tile.x, tile.y);
                DrawTile(tile);
            }
        }

        public void DrawGrid()
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Tile tile = grid[y, x];
                    DrawTile(tile);
                }
                Console.WriteLine();
            }
        }

        private static void DrawTile(Tile tile)
        {
            Char drawChar = ' ';
            Console.ForegroundColor = ConsoleColor.Black;

            if (tile.State == TileState.Wall)
                Console.BackgroundColor = ConsoleColor.Gray;
            else if (tile.foodCount > 1 && tile.ants.Count > 0)
                Console.BackgroundColor = ConsoleColor.Red;
            else if (tile.State == TileState.Dirt)
                Console.BackgroundColor = ConsoleColor.DarkYellow;
            else if (tile.State == TileState.Air)
                Console.BackgroundColor = ConsoleColor.Blue;
            else
                Console.BackgroundColor = ConsoleColor.Yellow;

            if (tile.ants.Count > 0)
            {
                drawChar = tile.ants[0].Symbol;
            }
            else if (tile.foodCount > 0)
            {
                drawChar = '@';
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.Write(drawChar);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
