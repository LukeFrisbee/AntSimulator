using System.Threading;

namespace AntSimulator // Note: actual namespace depends on the project name.
{
    public class Engine
    {

        private Grid grid = new Grid();
        private List<Tile> foods = new List<Tile>();
        private List<Ant> ants = new List<Ant>();

        private int tickCount;
        private int delay;

        public int foodCount = 10;

        public Engine(int tickCount = 1000, int delay = 250)
        {
            this.tickCount = tickCount;
            this.delay = delay;
        }

        Tile GenerateFood()
        {
            Random randy = new Random();

            int terrainSelection = randy.Next(0, 10);

            //30% chance to spawn food in the air
            if (terrainSelection <= 2)
            {
                int randomX = randy.Next(1, grid.Width-1);
                int randomY = randy.Next(grid.Height/2 + 1, grid.Height-1);

                Tile food = grid.grid[randomY, randomX];
                food.foodCount = foodCount;
                foods.Add(food);
                return food;
            }
            //Remainder percentage should be on dirt
            else
            {
                int randomX = randy.Next(1, grid.Width-1);
                int randomY = randy.Next(1, grid.Height-1);

                Tile food = grid.grid[randomY, randomX];
                food.foodCount = foodCount;
                foods.Add(food);
                return food;
            }

            //random x and y values within the border, will be one less than the sides of the screen
        }


        public void RunSimulation()
        {
            Random randy = new Random();
            grid.DrawGrid();

            TrailAnt timmy = new TrailAnt(10, grid.Height/2, grid, foods, ants);
            FlyingAnt flik = new FlyingAnt(2, grid.Height/2, grid, foods, ants);
            DiggingAnt doug = new DiggingAnt(35, grid.Height/2, grid, foods, ants);
            QueenAnt qua = new QueenAnt(grid.Width-2, grid.Height-2, grid, foods, ants);

            ants.Add(timmy);
            ants.Add(flik);
            ants.Add(doug);
            ants.Add(qua);

            grid.DrawGrid();
            Thread.Sleep(2000);

            while (tickCount > 0)
            {
                HashSet<Tile> tiles = new HashSet<Tile>();

                if (tickCount % 50 == 0)
                {
                    //why round down when I can just type cast to int haha
                    //for (int i = 0; i < (int)(ants.Count * 1.5); i++)
                    for (int i = 0; i < 5; i++)
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
