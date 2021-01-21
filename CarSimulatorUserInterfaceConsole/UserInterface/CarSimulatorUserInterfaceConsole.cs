using CarSimulatorEngine.Enums;
using CarSimulatorEngine.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarSimulatorUserInterfaceConsole.UserInterface
{
    public class CarSimulatorUserInterfaceConsole
    {
        private string LastToUserMessage { get; set; } = "";
        private bool IsExitEventTriggered { get; set; } = false;
        private CarSimulatorEngine.Engine.CarSimulatorEngine CarSimulatorEngine { get; set; }

        public async Task Work()
        {
            InitConsole();
            await Loading();
            InitEngine();
            WorkingThread();
            ControlLoop();
        }

        private async Task Loading()
        {
            Console.Clear();
            var rand = new Random();
            for (var i = 1; i <= 100; i++)
            {
                Console.WriteLine($"{i}%");
                await Task.Delay(rand.Next(1, 20));
                Console.Clear();
            }
        }

        private void ControlLoop()
        {
            while (true)
            {
                if (IsExitEventTriggered)
                {
                    break;
                }

                if (!Console.KeyAvailable)
                {
                    continue;
                }

                KeyToSwitch();
            }
        }
        private void WorkingThread()
        {
            new Thread(async () =>
            {
                Thread.CurrentThread.IsBackground = true;

                try
                {
                    while (true)
                    {
                        if (IsExitEventTriggered)
                        {
                            break;
                        }

                        CarSimulatorEngine.Work();
                        DrawUserInterface();
                        await Task.Delay(1000);
                    }
                }
                catch (CarSimulatorException exception)
                {
                    LastToUserMessage = exception.Message;
                }
            }).Start();
        }

        private void KeyToSwitch()
        {
            var key = Console.ReadKey(true).Key;

            try
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        CarSimulatorEngine.Accelerate();
                        break;
                    case ConsoleKey.DownArrow:
                        CarSimulatorEngine.Decelerate();
                        break;
                    case ConsoleKey.W:
                        CarSimulatorEngine.GearUp();
                        break;
                    case ConsoleKey.S:
                        CarSimulatorEngine.GearUp();
                        break;
                    case ConsoleKey.D1:
                        CarSimulatorEngine.StartCarEngine();
                        break;
                    case ConsoleKey.D2:
                        CarSimulatorEngine.StartCarEngine();
                        break;
                    case ConsoleKey.F:
                        CarSimulatorEngine.FillFuelTank();
                        break;
                    case ConsoleKey.Escape:
                        IsExitEventTriggered = true;
                        break;
                }
            }
            catch (CarSimulatorException exception)
            {
                LastToUserMessage = exception.Message;
            }
        }

        private void DrawUserInterface()
        {
            Console.WriteLine($"Fuel: {CarSimulatorEngine.Fuel} " +
                              $"Oil: {CarSimulatorEngine.EngineOil} " +
                              $"Gear: {CarSimulatorEngine.UsedGear} " +
                              $"Fuel consumption: {CarSimulatorEngine.FuelConsumption} " +
                              $"Engine speed: {CarSimulatorEngine.EngineSpeed} " +
                              $"Car speed: {CarSimulatorEngine.Speed} " +
                              $"Message:  {LastToUserMessage}");
        }

        private void InitEngine()
        {
            CarSimulatorEngine = new CarSimulatorEngine.Engine.CarSimulatorEngine(CarTypes.PassengerCar);
        }

        private static void InitConsole()
        {
            Console.CursorVisible = false;
        }
    }
}