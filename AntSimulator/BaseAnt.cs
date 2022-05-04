namespace AntSimulator
{
    class Ant
    {
        Grid grid;
        List<Tile> foods;
        List<Tile> ants;

        public Ant(Grid grid, List<Tile> foods, List<Tile> ants)
        {
            this.grid = grid;
            this.foods = foods;
            this.ants = ants;
        }

        public virtual void Act()
        {

        }
    }
}

