namespace AntSimulator
{
    public class FlyingAnt : Ant
    {
        public override char Symbol { get; } = 'F';

        public FlyingAnt(int x, int y, Grid grid, List<Tile> foods, List<Ant> ants) : base(x, y, grid, foods, ants) { }

        protected override void PickTarget()
        {
            foreach (Tile food in foods)
            {
                if (food.isAir && !food.isDirt)
                {
                    target = food;
                    foods.Remove(food);
                    return;
                }
            }
        }
    }
}
