using CarSimulatorEngine.Enums;
using CarSimulatorEngine.Interfaces;
using CarSimulatorEngine.Providers;
using System.Collections.ObjectModel;
using System.Linq;

namespace CarSimulatorEngine.Engine
{
    public class CarSimulatorEngine : ICarSimulatorEngine
    {
        public CarTypes CarType { get; private set; }
        public double Speed => Car.CarSpeed;
        public double SpeedMaxValue => Car.CarSpeedMaxValue;
        public double EngineSpeed => Car.EngineSpeed;
        public double EngineSpeedMaxValue => Car.EngineSpeedMaxValue;
        public double Fuel => Car.Fuel;
        public double FuelCapacity => Car.FuelCapacity;
        public double EngineOil => Car.EngineOil;
        public double EngineOilGoodMaxValue => Car.EngineOilGoodMaxValue;
        public double EngineOilGoodMinValue => Car.EngineOilGoodMinValue;
        public double FuelConsumption => Car.FuelConsumption;
        public CarStates CarState => Car.CarState;
        public ReadOnlyCollection<CarFaults> CarFaults => Car.CarFaults.ToList().AsReadOnly();

        public ReadOnlyCollection<Gears> AvailableGearTypes => Car.Gear.AvailableGearTypes.ToList().AsReadOnly();
        public Gears UsedGear => Car.Gear.UsedGear.Value;
        public Gears MaxGear => Car.Gear.MaxGear;
        public Gears MinGear => Car.Gear.MinGear;

        private Car Car { get; }

        public CarSimulatorEngine(CarTypes carType)
        {
            Car = carType.ProvideCar();
            CarType = carType;
        }

        public void StartCarEngine()
        {
            Car.StartCarEngine();
        }

        public void StopCarEngine()
        {
            Car.StopCarEngine();
        }

        public void FillFuelTank()
        {
            Car.FillFuelTank();
        }

        public void Work()
        {
            Car.Work();
        }

        public void Accelerate()
        {
            Car.Accelerate();
        }

        public void Decelerate()
        {
            Car.Decelerate();
        }

        public void GearUp()
        {
            Car.GearUp();
        }

        public void GearDown()
        {
            Car.GearDown();
        }
    }
}