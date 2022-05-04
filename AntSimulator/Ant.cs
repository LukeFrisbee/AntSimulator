namespace AntSimulator
{
    public class Ant
    {
        protected Grid grid;
        protected List<Tile> foods;
        protected List<Ant> ants;
        protected Tile? target;

        protected int x;
        protected int y;

        public int Life { get => life; set => life = value; }
        protected int life = 50;
        protected int foodWorth = 10;

        public virtual char Symbol { get; } = 'A';

        public Ant(int x, int y, Grid grid, List<Tile> foods, List<Ant> ants)
        {
            this.x = x;
            this.y = y;

            this.grid = grid;
            this.foods = foods;
            this.ants = ants;

            grid.grid[y, x].ants.Add(this);
        }

        public virtual void Act()
        {
            if (target == null)
                PickTarget();

            MoveToTarget();

            life--;
            if (life <= 0) Die();
        }

        protected void Die()
        {
            if (target != null)
                foods.Add(target);

            grid.grid[y, x].ants.Remove(this);
            ants.Remove(this);
        }

        protected virtual void MoveToTarget()
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
        }

        protected void EatFood()
        {
            if (target == null)
                return;

            if (target.foodCount > 0)
            {
                target.foodCount -= 1;
                life += foodWorth;
            }
            else
            {
                target = null;
            }
        }

        protected void Move(Direction direction)
        {
            grid.grid[y, x].ants.Remove(this);

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

            if (x < 0 || x >= grid.Width || y < 0 || y >= grid.Height)
                return;

            grid.grid[y, x].ants.Add(this);
        }

        protected virtual void PickTarget()
        {
            foreach (Tile food in foods)
            {
                if(!food.isAir && !food.isDirt)
                {
                    target = food;
                    foods.Remove(food);
                    return;
                }
            }
        }
    }
}
