using System;
using CarSimulatorEngine.Enums;
using CarSimulatorEngine.Interfaces;
using CarSimulatorEngine.Models;

namespace CarSimulatorEngine.Providers
{
    internal static class CarProvider
    {
        public static Car ProvideCar(this CarTypes carType)
        {
            return carType switch
            {
                CarTypes.PassengerCar =>
                    new PassengerCar(),
                CarTypes.Truck =>
                    new PassengerCar(),
                _ => throw new ArgumentOutOfRangeException(nameof(carType), carType, "This carType is not supported")
            };
        }
    }
}