namespace AntSimulator
{
    public class DiggingAnt : Ant
    {
        public DiggingAnt(int x, int y, Grid grid, QueenAnt? queen) : base(x, y, grid, queen) 
        {
            Symbol = 'D';
            foodLoss = 2;
        }

        public override HashSet<Tile> Act()
        {
            if (grid.grid[y, x].State != TileState.Wall)
                grid.grid[y, x].State = TileState.Normal;

            base.Act();
            if (food <= 0 || feedQueen) return updatedTiles;

            if (target == null)
                return updatedTiles;
            else if (target.State == TileState.Dirt)
                MoveToTarget();
            else
                PathToTarget();

            return updatedTiles;
        }

        protected override void PickTarget()
        {
            target = FindFood(TileState.Dirt);
            if (target == null) target = FindFood(TileState.Normal);
        }
    }
}
