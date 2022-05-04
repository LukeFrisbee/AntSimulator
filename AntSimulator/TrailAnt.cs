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

            Direction direction;
            Direction trail;

            pheromoneTrail[y, x] = true;

            Tile up = grid.grid[y - 1, x];
            Tile down = grid.grid[y + 1, x];
            Tile left = grid.grid[y, x - 1];
            Tile right = grid.grid[y, x + 1];

            if (x_diff == 0 && y_diff == 0)
            {
                EatFood();
                pheromoneTrail = new bool[grid.Height, grid.Width];
                backTrack.Clear();
                return;
            }
            else if (!up.isWall && !up.isDirt && !up.isAir && !pheromoneTrail[y - 1, x])
            {
                direction = Direction.Up;
                trail = Direction.Down;
            }
            else if (!down.isWall && !down.isDirt && !pheromoneTrail[y + 1, x])
            {
                direction = Direction.Down;
                trail = Direction.Up;
            }
            else if (!left.isWall && !left.isDirt && !left.isAir && !pheromoneTrail[y, x - 1])
            {
                direction = Direction.Left;
                trail = Direction.Right;
            }
            else if (!right.isWall && !right.isDirt && !right.isAir && !pheromoneTrail[y, x + 1])
            {
                direction = Direction.Right;
                trail = Direction.Left;
            }
            else
            {
                if (backTrack.Count > 0)
                {
                    direction = backTrack.Pop();
                    Move(direction);
                }
                return;
            }

            Move(direction);
            backTrack.Push(trail);
        }


        protected override void PickTarget()
        {
            foreach (Tile food in foods)
            {
                if (!food.isAir && !food.isDirt)
                {
                    target = food;
                    foods.Remove(food);
                    return;
                }
            }
        }
    }
}
