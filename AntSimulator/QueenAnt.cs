namespace AntSimulator
{
    public class QueenAnt : Ant
    {
        private int spawnerCooldown = 50;
        public QueenAnt(int x, int y, Grid grid, List<Tile> foods, List<Ant> ants) : base(x, y, grid, foods, ants)
        {
            Symbol = 'Q';
        }


        void GenerateAnt()
        {
            Random randy = new Random();
            int randomAntSelector = randy.Next(0, 3);

            //simple ant
            if (randomAntSelector == 0)
            {

                ants.Add(new TrailAnt(this.x, this.y, grid, foods, ants));
            }
            //dig ant
            //else if (randomAntSelector == 1)
            //{
            //    ants.Add(new DiggingAnt(this.x, this.y, grid, foods, ants));

            //}
            //fly ant
            else
            {
                ants.Add(new FlyingAnt(this.x, this.y, grid, foods, ants));
            }
                
            this.spawnerCooldown = 50;
            return;
        }
        public override HashSet<Tile> Act()
        {
            base.Act();
            if (life <= 0) return updatedTiles;


            MoveToTarget();
            if (spawnerCooldown == 0 && this.Life >= 25)
            {
                GenerateAnt();
            }

            if (spawnerCooldown != 0)
            { 
            spawnerCooldown--;
            }

            return updatedTiles;

        }


    }
}
