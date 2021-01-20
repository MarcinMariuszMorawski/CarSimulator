using System;
using System.Threading.Tasks;
using CarSimulatorEngine.Enums;
using CarSimulatorEngine.Engine;

namespace CarSimulator
{
    internal class Program
    {
        private static async Task Main()
        {
            var carSimulatorEngine = new CarSimulatorEngine.Engine.CarSimulatorEngine(CarTypes.PassengerCar);

            carSimulatorEngine.FillFuelTank();

            carSimulatorEngine.StartCarEngine();

            carSimulatorEngine.GearUp();

            while (true)
            {
                carSimulatorEngine.Drive();
                Console.WriteLine($"Fuel: {carSimulatorEngine.Fuel} " +
                                  $"Oil: {carSimulatorEngine.EngineOil} " +
                                  $"Gear: {carSimulatorEngine.UsedGear} " +
                                  $"Engine speed: {carSimulatorEngine.EngineSpeed} " +
                                  $"Car speed {carSimulatorEngine.Speed} ");
                await Task.Delay(1000);
            }
        }
    }
}