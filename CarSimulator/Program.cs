using System;
using GameCarSimulatorEngine;

namespace CarSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var carSimulatorEngine = new GameCarSimulatorEngine.CarSimulator();

            while (true)
            {
                var cos = carSimulatorEngine.Loop();



            }
            

        }
    }
}
