namespace AntSimulator
{
    public class TrailAnt : Ant
    {
        public override char Symbol { get; } = 'T';

        private Stack<Direction> backTrack = new Stack<Direction>();
        private bool[,] pheromoneTrail;

        public TrailAnt(int x, int y, Grid grid, List<Tile> foods, List<Ant> ants) : base(x, y, grid, foods, ants) 
        {
            pheromoneTrail = new bool[grid.Height, grid.Width];
        }

        public override void Act()
        {
            base.Act();
            grid.grid[y, x].isDirt = false;
        }

        protected override void MoveToTarget()
        {
            if (target == null)
                return;

            int x_diff = x - target.x;
            int y_diff = y - target.y;

            if (y_diff > 0) Move(Direction.Up);
            else if (y_diff < 0) Move(Direction.Down);
            else if (x_diff > 0) Move(Direction.Left);
            else if (x_diff < 0) Move(Direction.Right);
            else EatFood();

            Direction direction = Direction.Up;
            Direction trail = Direction.Down;

            pheromoneTrail[y, x] = true;

            if (x_diff == 0 && y_diff == 0)
            {
                EatFood();
                return;
            }
            else if (y_diff > 0 && !grid.grid[y - 1, x].isDirt)
            {
                direction = Direction.Up;
                trail = Direction.Down;
            }
            else if (y_diff < 0 && !grid.grid[y + 1, x].isDirt)
            {
                direction = Direction.Down;
                trail = Direction.Up;
            }
            else if (x_diff > 0 && !grid.grid[y, x - 1].isDirt)
            {
                direction = Direction.Left;
                trail = Direction.Right;
            }
            else if (x_diff < 0 && !grid.grid[y, x + 1].isDirt)
            {
                direction = Direction.Right;
                trail = Direction.Left;
            }
            else
            {
                direction = backTrack.Pop();
                Move(direction);
            }

            Move(direction);
            backTrack.Push(trail);
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
