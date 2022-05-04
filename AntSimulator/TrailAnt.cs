namespace AntSimulator
{
    public class TrailAnt : Ant
    {
        public TrailAnt(int x, int y, Grid grid, List<Tile> foods, List<Ant> ants) : base(x, y, grid, foods, ants) 
        {
            Symbol = 'A';
        }

        public override HashSet<Tile> Act()
        {
            base.Act();
            if (life <= 0) return updatedTiles;

            PathToTarget();
            return updatedTiles;
        }
    }
}
