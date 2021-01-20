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


            while (true)
            {
                carSimulatorEngine.Work();

                Console.WriteLine($"Fuel: {carSimulatorEngine.Fuel} " +
                                  $"Oil: {carSimulatorEngine.EngineOil} " +
                                  $"Gear: {carSimulatorEngine.UsedGear} " +
                                  $"Fuel comsumption: {carSimulatorEngine.FuelConsumption} " +
                                  $"Engine speed: {carSimulatorEngine.EngineSpeed} " +
                                  $"Car speed {carSimulatorEngine.Speed}");

                var cos = Console.ReadKey();

                if (cos.Key == ConsoleKey.UpArrow)
                {
                    carSimulatorEngine.Accelerate();
                }

                if (cos.Key == ConsoleKey.DownArrow)
                {
                    carSimulatorEngine.Decelerate();
                }


                if (cos.Key == ConsoleKey.P)
                {
                    carSimulatorEngine.GearUp();
                }

                if (cos.Key == ConsoleKey.L)
                {
                    carSimulatorEngine.GearDown();
                }

            }
        }
    }
}