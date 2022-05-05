namespace AntSimulator
{
    public class FlyingAnt : Ant
    {
        public FlyingAnt(int x, int y, Grid grid, QueenAnt? queen) : base(x, y, grid, queen)
        {
            Symbol = 'F';
        }

        public override HashSet<Tile> Act()
        {
            base.Act();
            if (food <= 0 || feedQueen) return updatedTiles;

            if (target == null)
                return updatedTiles;
            if (grid.grid[y, x].State == TileState.Air && target.State == TileState.Air)
                MoveToTarget();
            else
                PathToTarget();

            return updatedTiles;
        }

        protected override void PickTarget()
        {
            target = FindFood(TileState.Air);
            if (target == null) target = FindFood(TileState.Normal);
        }
    }
}
