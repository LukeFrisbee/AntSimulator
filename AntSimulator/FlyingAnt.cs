namespace AntSimulator
{
    public class FlyingAnt : Ant
    {
        public FlyingAnt(int x, int y, Grid grid, List<Tile> foods, List<Ant> ants) : base(x, y, grid, foods, ants) 
        {
            Symbol = 'F';
        }

        public override HashSet<Tile> Act()
        {
            base.Act();
            if (life <= 0) return updatedTiles;

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
            target = FindFood(TileState.Air);
            if (target == null) target = FindFood(TileState.Normal);
        }
    }
}
