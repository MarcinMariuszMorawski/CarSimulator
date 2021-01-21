using System.Threading.Tasks;


namespace CarSimulator
{
    internal class Program
    {
        private static async Task Main()
        {
            var carSimulatorUserInterfaceConsole =
                new CarSimulatorUserInterfaceConsole.UserInterface.CarSimulatorUserInterfaceConsole();
            await carSimulatorUserInterfaceConsole.Work();
        }
    }
}