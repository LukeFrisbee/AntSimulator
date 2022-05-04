using System.Threading;

namespace AntSimulator // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Grid grid = new Grid();

            int tickCount = 1000;
            int delay = 250;

            grid.Draw();
            Thread.Sleep(2000);

            List<Tile> foods = new List<Tile>();
            grid.grid[10, 30].foodCount = 5;
            grid.grid[15, 47].foodCount = 1;
            grid.grid[2, 40].foodCount = 7;
            grid.grid[13, 5].foodCount = 9;

            foods.Add(grid.grid[10, 30]);
            foods.Add(grid.grid[15, 47]);
            foods.Add(grid.grid[2, 40]);
            foods.Add(grid.grid[13, 5]);

            grid.Draw();
            Thread.Sleep(2000);

            List<Ant> ants = new List<Ant>();
            Ant andy = new Ant(10, 5, grid, foods, ants);
            FlyingAnt flik = new FlyingAnt(10, 5, grid, foods, ants);
            DiggingAnt doug = new DiggingAnt(35, 10, grid, foods, ants);

            ants.Add(andy);
            ants.Add(flik);
            ants.Add(doug);

            while(tickCount > 0)
            {
                grid.Draw();

                for(int i = 0; i < ants.Count; i++)
                    ants[i].Act();

                Thread.Sleep(delay);

                tickCount--;
            }
        }
    }
}