namespace AntSimulator
{
    sealed class SimpleAnt : AbstractAnt, IMoveSelector
    {
        public const char symbol = 'A';
        public override void Act(Tile currentTile)
        {
            currentTile = nextTile;
            return;
        }

        public int[] LocateNearestFoodTile(int[][] coordinates)
        {
            int[] currentCoordinate = { 0, 0 };
            int[] nearestFoodTile;

            foreach (int[] foodTileCoordinate in coordinates)
            {
                int dX = currentCoordinate[0] - foodTileCoordinate[0];
                int dY = currentCoordinate[1] - foodTileCoordinate[1];
                double distanceToFoodBlock = Math.Sqrt(dX * dX + dY * dY);
            }

            return nearestFoodTile;
        }

        public double[] NextAvailableMoves(Tile currentTile)
        {
            throw new NotImplementedException();
        }
    }
}
