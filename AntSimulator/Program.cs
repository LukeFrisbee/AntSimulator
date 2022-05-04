using System.Threading;

namespace AntSimulator // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Engine e = new Engine();
            e.RunSimulation();
        }
    }
}
