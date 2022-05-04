namespace AntSimulator
{
    public class TrailAnt : Ant
    {
        public TrailAnt(int x, int y, Grid grid, QueenAnt? queen) : base(x, y, grid, queen)
        {
            Symbol = 'A';
        }

        public override HashSet<Tile> Act()
        {
            base.Act();
            if (food <= 0 || feedQueen) return updatedTiles;

            PathToTarget();

            return updatedTiles;
        }
    }
}
