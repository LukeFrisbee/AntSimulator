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

            MoveToTarget();
            return updatedTiles;
        }

        protected override void PickTarget()
        {
            foreach (Tile food in foods)
            {
                if (food.State == TileState.Air)
                {
                    target = food;
                    foods.Remove(food);
                    return;
                }
            }
        }
    }
}
