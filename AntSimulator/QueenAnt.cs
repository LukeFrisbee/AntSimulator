namespace AntSimulator
{
    public class QueenAnt : Ant
    {
        private int antCost = 700;
        private Tile home;
        private int moveDelay = 10;
        private int moveTimer;

        public QueenAnt(int x, int y, Grid grid) : base(x, y, grid, null)
        {
            Symbol = 'Q';
            food = 1250;

            Random rand = new Random();
            int randomX = rand.Next(3, grid.Width - 3);
            target = grid.grid[grid.Height - 3, randomX];
            home = target;


            moveTimer = moveDelay;
        }

        public override HashSet<Tile> Act()
        {
            base.Act();
            if (food <= 0) return updatedTiles;

            if (moveTimer >= moveDelay)
            {
                MoveToTarget();
                moveTimer = 0;
            }
            else
            {
                moveTimer++;
            }

            if (food >= antCost + maxFood)
            {
                food -= antCost;
                GenerateAnt();
            }

            if (grid.grid[y, x].State != TileState.Wall)
                grid.grid[y, x].State = TileState.Normal;

            updatedTiles.Add(grid.grid[y, x]);
            return updatedTiles;
        }

        void GenerateAnt()
        {
            Random rand = new Random();
            int antType = rand.Next(0, 3);

            switch (antType)
            { 
                case 0:
                    new FlyingAnt(x, y, grid, this);
                    break;
                default:
                    new TrailAnt(x, y, grid, this);
                    break;
            }
        }
        protected override void PickTarget()
        {
            Random rand = new Random();
            int randomX = rand.Next(home.x-2, home.x+3);
            int randomY = rand.Next(home.y-1, home.y+2);
            target = grid.grid[randomY, randomX];
        }
    }
}
