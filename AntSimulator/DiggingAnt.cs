namespace AntSimulator
{
    public class DiggingAnt : Ant
    {
        public override char Symbol { get; } = 'D';

        public DiggingAnt(int x, int y, Grid grid, List<Tile> foods, List<Ant> ants) : base(x, y, grid, foods, ants) { }

        public override void Act()
        {
            base.Act();
            grid.grid[y, x].isDirt = false;
        }


        protected override void PickTarget()
        {
            foreach (Tile food in foods)
            {
                if (!food.isAir && food.isDirt)
                {
                    target = food;
                    foods.Remove(food);
                    return;
                }
            }
        }
    }
}
