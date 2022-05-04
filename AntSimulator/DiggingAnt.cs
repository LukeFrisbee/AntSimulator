namespace AntSimulator
{
    public class DiggingAnt : Ant
    {
        public DiggingAnt(int x, int y, Grid grid, List<Tile> foods, List<Ant> ants) : base(x, y, grid, foods, ants) 
        {
            Symbol = 'D';
        }

        public override HashSet<Tile> Act()
        {
            if (grid.grid[y, x].State != TileState.Wall)
                grid.grid[y, x].State = TileState.Normal;

            base.Act();
            if (life <= 0) return updatedTiles;

            MoveToTarget();
            return updatedTiles;
        }

        protected override void PickTarget()
        {
            foreach (Tile food in foods)
            {
                if (food.State == TileState.Dirt)
                {
                    target = food;
                    foods.Remove(food);
                    return;
                }
            }
        }
    }
}
