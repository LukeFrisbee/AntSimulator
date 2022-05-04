using System.Threading;

namespace AntSimulator // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Grid grid = new Grid();

            int tickCount = 1000;
            int delay = 1000;

            for(int i = 0; i < tickCount; i++)
            {
                grid.Draw();
                Thread.Sleep(delay);
            }
        }
    }
}