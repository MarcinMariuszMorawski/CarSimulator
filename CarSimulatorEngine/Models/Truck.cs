using CarSimulatorEngine.Interfaces;

namespace CarSimulatorEngine.Models
{
    internal class Truck : Car
    {
        public override double CarSpeedMaxValue { get; protected internal set; } = 120;
        public override double EngineSpeedMaxValue { get; protected internal set; } = 6000;
        public override double Fuel { get; protected internal set; } = 50;
        public override double FuelCapacity { get; protected internal set; } = 100;
        public override double EngineOil { get; protected internal set; } = 7.5;
        public override double EngineOilGoodMaxValue { get; protected internal set; } = 7;
        public override double EngineOilGoodMinValue { get; protected internal set; } = 8;
        public override Gear Gear { get; protected internal set; } = new TruckGear();
    }
}