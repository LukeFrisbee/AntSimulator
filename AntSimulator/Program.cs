namespace AntSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Engine e = new Engine(5000, 50);
            e.RunSimulation();
        }
    }
}
