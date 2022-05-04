namespace AntSimulator
{
    sealed class SimpleAnt : AbstractAnt
    {
        public const char symbol = 'A';

        public override void Act(Tile tile, int[] xArr, int[] yArr)
        {
            int[] currentCoordinate = { 0, 0 };
            //default to first given food tile 
            int[] initialTarget = {xArr[0], xArr[1]};
            
            //go through each of the given food tile coordinate(x,y) and calculate which one is closest
            //to know where to move next
            foreach (int[] potentialFoodTilecoordinate in foodTileCoordinates)
            {
                int dX = currentCoordinate[0] - potentialFoodTilecoordinate[0];
                int dY = currentCoordinate[1] - potentialFoodTilecoordinate[1];
                double distanceToFoodBlock = Math.Sqrt(dX * dX + dY * dY);

                return;
            }
        }
    }
}
