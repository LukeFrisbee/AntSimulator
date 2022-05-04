namespace AntSimulator
{
    public class QueenAnt : Ant
    {
        private int antCost = 200;

        public QueenAnt(int x, int y, Grid grid) : base(x, y, grid, null)
        {
            Symbol = 'Q';
            food = 500;
        }

        public override HashSet<Tile> Act()
        {
            base.Act();
            if (food <= 0) return updatedTiles;

            if (food >= antCost + maxFood)
            {
                food -= antCost;
                GenerateAnt();
            }

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
    }
}
