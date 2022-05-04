namespace AntSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Engine e = new Engine(1000, 25);
            e.RunSimulation();
        }
    }
}
