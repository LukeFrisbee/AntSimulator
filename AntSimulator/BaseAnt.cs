namespace AntSimulator
{
    abstract class AbstractAnt
    {
        public abstract void Act(Tile currentTile);
    }

    interface IMoveSelector
    {
        int LocateNearestFoodTile(int[][] coordinates);
        int[] NextAvailableMoves(Tile currentTile);
    }
}

