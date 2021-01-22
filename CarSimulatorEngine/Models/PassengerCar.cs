using CarSimulatorEngine.Interfaces;

namespace CarSimulatorEngine.Models
{
    internal class PassengerCar : Car
    {
        public override double CarSpeedMaxValue { get; protected internal set; } = 250;
        public override double EngineSpeedMaxValue { get; protected internal set; } = 6000;
        public override double Fuel { get; protected internal set; } = 1;
        public override double FuelCapacity { get; protected internal set; } = 50;
        public override double EngineOil { get; protected internal set; } = 4.5;
        public override double EngineOilGoodMaxValue { get; protected internal set; } = 5;
        public override double EngineOilGoodMinValue { get; protected internal set; } = 4;
        public override Gear Gear { get; protected internal set; } = new PassengerCarGear();
    }
}