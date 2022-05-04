namespace AntSimulator
{
    public class Ant
    {
        protected Grid grid;
        protected List<Tile> foods;
        protected List<Ant> ants;
        protected Tile? target;
        protected HashSet<Tile> updatedTiles = new HashSet<Tile>();

        protected int x;
        protected int y;

        public int Life { get => life; set => life = value; }
        protected int life = 300;
        protected int foodWorth = 10;

        public char Symbol { get; protected set; } = 'A';

        private Stack<Direction> backTrack = new Stack<Direction>();
        private bool[,] pheromoneTrail;

        public Ant(int x, int y, Grid grid, List<Tile> foods, List<Ant> ants)
        {
            this.x = x;
            this.y = y;

            this.grid = grid;
            this.foods = foods;
            this.ants = ants;

            grid.grid[y, x].ants.Add(this);
            updatedTiles.Add(grid.grid[y, x]);

            pheromoneTrail = new bool[grid.Height, grid.Width];
        }

        public virtual HashSet<Tile> Act()
        {
            updatedTiles.Clear();

            if (target == null)
                PickTarget();

            life--;
            if (life <= 0) Die();

            return updatedTiles;
        }

        protected void Die()
        {
            if (target != null)
                foods.Add(target);

            if (life <= -30)
            {
                grid.grid[y, x].ants.Remove(this);
                ants.Remove(this);
            }

            Symbol = 'X';
            updatedTiles.Add(grid.grid[y, x]);
        }

        protected virtual void MoveToTarget()
        {
            if (target == null)
                return;

            int x_diff = x - target.x;
            int y_diff = y - target.y;

            TileState up = grid.grid[y - 1, x].State;
            TileState down = grid.grid[y + 1, x].State;
            TileState left = grid.grid[y, x - 1].State;
            TileState right = grid.grid[y, x + 1].State;

            //if (x_diff == 0 && y_diff == 0) EatFood();
            if (y_diff > 0 && up != TileState.Wall) Move(Direction.Up);
            else if (y_diff < 0 && down != TileState.Wall) Move(Direction.Down);
            else if (x_diff > 0 && left != TileState.Wall) Move(Direction.Left);
            else if (x_diff < 0 && right != TileState.Wall) Move(Direction.Right);
            else EatFood();
        }
        protected void Move(Direction direction)
        {
            grid.grid[y, x].ants.Remove(this);
            updatedTiles.Add(grid.grid[y, x]);

            switch (direction)
            {
                case Direction.Up:
                    y--;
                    break;
                case Direction.Down:
                    y++;
                    break;
                case Direction.Left:
                    x--;
                    break;
                case Direction.Right:
                    x++;
                    break;
            }

            grid.grid[y, x].ants.Add(this);
            updatedTiles.Add(grid.grid[y, x]);
        }

        protected void EatFood()
        {
            if (target == null)
                return;

            // Eating Food
            if (target.foodCount > 0)
            {
                target.foodCount -= 1;
                life += foodWorth;
                return;
            }

            // Out Of Food
            updatedTiles.Add(grid.grid[y, x]);
            target = null;
        }
        protected void PathToTarget()
        {
            if (target == null)
                return;

            int x_diff = x - target.x;
            int y_diff = y - target.y;

            pheromoneTrail[y, x] = true;

            // Eat Food
            if (x_diff == 0 && y_diff == 0)
            {
                EatFood();
                pheromoneTrail = new bool[grid.Height, grid.Width];
                backTrack.Clear();
                return;
            }

            // Path to Food
            if (y_diff < 0) // Try Down
            {
                if (TryPath(Direction.Down)) return;
                if (x_diff > 0)
                {
                    if (TryPath(Direction.Left)) return;
                    if (TryPath(Direction.Right)) return;
                }
                if (TryPath(Direction.Right)) return;
                if (TryPath(Direction.Left)) return;
                if (TryPath(Direction.Up)) return;
            }
            else if (y_diff > 0) // Try Up
            {
                if (TryPath(Direction.Up)) return;
                if (x_diff > 0)
                {
                    if (TryPath(Direction.Left)) return;
                    if (TryPath(Direction.Right)) return;
                }
                if (TryPath(Direction.Right)) return;
                if (TryPath(Direction.Left)) return;
                if (TryPath(Direction.Down)) return;
            }
            else // Try Horizontal
            {
                if (x_diff > 0)
                {
                    if (TryPath(Direction.Left)) return;
                    if (TryPath(Direction.Right)) return;
                }
                if (TryPath(Direction.Right)) return;
                if (TryPath(Direction.Left)) return;
                if (TryPath(Direction.Up)) return;
                if (TryPath(Direction.Down)) return;
            }


            if (backTrack.Count > 0) Move(backTrack.Pop());
        }

        protected bool TryPath(Direction direction)
        {
            grid.grid[y, x].ants.Remove(this);
            updatedTiles.Add(grid.grid[y, x]);

            switch (direction)
            {
                case Direction.Up:
                    TileState up = grid.grid[y - 1, x].State;
                    if (up == TileState.Dirt || up == TileState.Wall || pheromoneTrail[y - 1, x]) return false;

                    y--;
                    backTrack.Push(Direction.Down);
                    break;

                case Direction.Down:
                    TileState down = grid.grid[y + 1, x].State;
                    if (down == TileState.Dirt || down == TileState.Wall || pheromoneTrail[y + 1, x]) return false;

                    y++;
                    backTrack.Push(Direction.Up);
                    break;

                case Direction.Left:
                    TileState left = grid.grid[y, x - 1].State;
                    if (left == TileState.Dirt || left == TileState.Wall || pheromoneTrail[y, x - 1]) return false;

                    x--;
                    backTrack.Push(Direction.Right);
                    break;

                case Direction.Right:
                    TileState right = grid.grid[y, x + 1].State;
                    if (right == TileState.Dirt || right == TileState.Wall || pheromoneTrail[y, x + 1]) return false;

                    x++;
                    backTrack.Push(Direction.Left);
                    break;
            }

            grid.grid[y, x].ants.Add(this);
            updatedTiles.Add(grid.grid[y, x]);

            return true;
        }

        protected virtual void PickTarget()
        {
            foreach (Tile food in foods)
            {
                if (food.State == TileState.Normal)
                {
                    target = food;
                    foods.Remove(food);
                    return;
                }
            }
        }
    }
}

