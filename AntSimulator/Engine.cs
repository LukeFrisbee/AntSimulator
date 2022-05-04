using System.Threading;

namespace AntSimulator // Note: actual namespace depends on the project name.
{
    public class Engine
    {

        private Grid grid = new Grid();
        private List<Tile> foods = new List<Tile>();
        private List<Ant> ants = new List<Ant>();

        public int tickCount;
        public int delay;

        public int foodCount = 10;

        public Engine(int tickCount = 1000, int delay = 250)
        {
            this.tickCount = tickCount;
            this.delay = delay;
        }

        Tile GenerateFood()
        {
            Random randy = new Random();

            //random x and y values within the border, will be one less than the sides of the screen
            int randomX = randy.Next(1, grid.Width-1);
            int randomY = randy.Next(1, grid.Height-1);
            int randomFoodCount = randy.Next(0, 10);

            Tile food = grid.grid[randomY, randomX];
            food.foodCount = foodCount;
            foods.Add(food);
            return food;
        }

        void GenerateAnt()
        {
            Random randy = new Random();
            int randomAntSelector = randy.Next(0, 3);

            //simple ant
            if (randomAntSelector == 0)
            {

                ants.Add(new TrailAnt(10, 5, grid, foods, ants));
            }
            //dig ant
            else if (randomAntSelector == 1)
            {
                ants.Add(new DiggingAnt(10, 5, grid, foods, ants));

            }
            //fly ant
            else
            {
                ants.Add(new FlyingAnt(10, 5, grid, foods, ants));
            }
        }

        public void RunSimulation()
        {
            Random randy = new Random();
            grid.DrawGrid();

            TrailAnt timmy = new TrailAnt(10, 5, grid, foods, ants);
            FlyingAnt flik = new FlyingAnt(10, 5, grid, foods, ants);
            DiggingAnt doug = new DiggingAnt(35, 10, grid, foods, ants);

            ants.Add(timmy);
            ants.Add(flik);
            ants.Add(doug);

            grid.DrawGrid();
            Thread.Sleep(2000);

            while (tickCount > 0)
            {
                HashSet<Tile> tiles = new HashSet<Tile>();

                if (tickCount % 25 == 0)
                {
                    for (int i = 0; i < randy.Next(2, 5); i++)
                    {
                        tiles.Add(GenerateFood());
                    }
                }

                for (int i = 0; i < ants.Count; i++)
                    tiles.UnionWith(ants[i].Act());
                grid.DrawTiles(tiles);

                Thread.Sleep(delay);

                tickCount--;
            }
        }
    }
}
