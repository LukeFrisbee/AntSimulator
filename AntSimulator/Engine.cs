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

        public Engine(int tickCount = 1000, int delay = 250)
        {
            this.tickCount = tickCount;
            this.delay = delay;
        }

        void GenerateFood()
        {
            Random randy = new Random();
            //random x and y values within the border, will be one less than the sides of the screen
            int randomX = randy.Next(1, grid.Height - 1);
            int randomY = randy.Next(1, grid.Width - 1);
            int randomFoodCount = randy.Next(0, 10);
            grid.grid[randomX, randomY].foodCount = randomFoodCount;
            foods.Add(grid.grid[randomX, randomY]);

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
            grid.Draw();
            Thread.Sleep(2000);

            for (int i = 0; i < randy.Next(2,5); i++){
                GenerateFood();
                GenerateAnt();
            }
            grid.Draw();
            Thread.Sleep(2000);

            while (tickCount > 0)
            {
                grid.Draw();

                for (int i = 0; i < ants.Count; i++)
                    ants[i].Act();

                Thread.Sleep(delay);

                tickCount--;
            }
        }
        static void foo(string[] args)
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

            while (tickCount > 0)
            {
                grid.Draw();

                for (int i = 0; i < ants.Count; i++)
                    ants[i].Act();

                Thread.Sleep(delay);

                tickCount--;
            }
        }
    }
}
