using System.Threading;

namespace AntSimulator // Note: actual namespace depends on the project name.
{
    public class Engine
    {
        private Grid grid = new Grid();

        private double initialTickCount;  
        private double tickCount;
        private double delay;

        public int foodCount = 10;

        public Engine(double tickCount = 1000, double delay = 250)
        {
            this.tickCount = tickCount;
            this.delay = delay;
            this.initialTickCount = tickCount;
        }

        Tile GenerateFood()
        {
            Random randy = new Random();

            int terrainSelection = randy.Next(0, 10);

            /*For the initial part of the simulation food spawning in dirt doesn't matter,
            //but to stop dig ants from devouring the entire underground we need to stop putting food under the ground
            */

            //first third of the game, generate total random food spots
            if (tickCount / initialTickCount >= .66)
            {
                //30% chance to spawn food in the air
                if (terrainSelection <= 2)
                {
                    int randomX = randy.Next(1, grid.Width - 1);
                    int randomY = randy.Next(1, grid.Height - 1);

                    Tile food = grid.grid[randomY, randomX];
                    food.foodCount = foodCount;
                    grid.foods.Add(food);
                    return food;
                }
                //spawn anywhere else instead
                else
                {
                    int randomX = randy.Next(1, grid.Width - 1);
                    int randomY = randy.Next(grid.Height/2, grid.Height - 1);

                    Tile food = grid.grid[randomY, randomX];
                    food.foodCount = foodCount;
                    grid.foods.Add(food);
                    return food;
                }
            }


            //remainder of the game need to carefully place random food tiles on non-dirt blocks
            else
            {
                List<Tile> nonDirtTiles = new List<Tile>();
                foreach (Tile tile in grid.grid)
                {
                    if (tile.State != TileState.Dirt && tile.State != TileState.Wall && tile.x != grid.Width && tile.y != grid.Height) { 
                        nonDirtTiles.Add(tile); 
                    }
                }
                //20% chance the tile in the sky
                if (terrainSelection <= 2)
                {
                    int randomNonDirtTileIndex = randy.Next(nonDirtTiles.Count);

                    while (nonDirtTiles[randomNonDirtTileIndex].State != TileState.Air){
                        randomNonDirtTileIndex = randy.Next(nonDirtTiles.Count); 
                    }
                
                    Tile food = grid.grid[nonDirtTiles[randomNonDirtTileIndex].y, nonDirtTiles[randomNonDirtTileIndex].x];
                    food.foodCount = foodCount;
                    grid.foods.Add(food);
                    return food;
                }
                //put the tile on a normal non-dirt tile
                else { 
                    int randomNonDirtTileIndex = randy.Next(nonDirtTiles.Count);

                    while (nonDirtTiles[randomNonDirtTileIndex].State != TileState.Dirt && nonDirtTiles[randomNonDirtTileIndex].State != TileState.Air){
                        randomNonDirtTileIndex = randy.Next(nonDirtTiles.Count); 
                    }
                    Tile food = grid.grid[nonDirtTiles[randomNonDirtTileIndex].y, nonDirtTiles[randomNonDirtTileIndex].x];
                    food.foodCount = foodCount;
                    grid.foods.Add(food);
                    return food;

                }

            }

        }


        public void RunSimulation()
        {
            Random randy = new Random();
            grid.DrawGrid();

            QueenAnt qua = new QueenAnt(1, grid.Height / 2, grid);
            TrailAnt timmy = new TrailAnt(10, grid.Height / 2, grid, qua);
            FlyingAnt flik = new FlyingAnt(2, grid.Height/2, grid, qua);
            DiggingAnt doug = new DiggingAnt(35, grid.Height/2, grid, qua);
            

            grid.DrawGrid();
            Thread.Sleep(2000);

            while (tickCount > 0)
            {
                HashSet<Tile> tiles = new HashSet<Tile>();

                if (tickCount % 50 == 0)
                {
                    //why round down when I can just type cast to int haha
                    //for (int i = 0; i < (int)(ants.Count * 1.5); i++)
                    for (int i = 0; i < 2; i++)
                    {
                        tiles.Add(GenerateFood());
                    }
                }

                for (int i = 0; i < grid.ants.Count; i++)
                    tiles.UnionWith(grid.ants[i].Act());
                grid.DrawTiles(tiles);

                Thread.Sleep((int)delay);

                tickCount--;
            }
        }
    }
}
