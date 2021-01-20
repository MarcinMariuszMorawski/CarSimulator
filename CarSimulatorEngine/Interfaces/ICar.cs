using CarSimulatorEngine.Enums;
using CarSimulatorEngine.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarSimulatorEngine.Interfaces
{
    internal interface ICar
    {
        void StartCarEngine();
        void StopCarEngine();
        void FillFuelTank();
        void Calculate();
    }

    internal abstract class Car : ICar
    {
        public double Speed { get; protected internal set; } = 0;
        public abstract double SpeedMaxValue { get; protected internal set; }
        public double EngineSpeed { get; protected internal set; } = 0;
        public abstract double EngineSpeedMaxValue { get; protected internal set; }
        public abstract double Fuel { get; protected internal set; }
        public abstract double FuelCapacity { get; protected internal set; }
        public abstract double EngineOil { get; protected internal set; }
        public abstract double EngineOilGoodMaxValue { get; protected internal set; }
        public abstract double EngineOilGoodMinValue { get; protected internal set; }
        public double FuelConsumption => CalculateFuelConsumptionInKilometersPerHour();
        public abstract Gear Gear { get; protected internal set; }
        public CarStates CarState { get; protected internal set; } = CarStates.Off;
        public DrivingDirections DrivingDirection { get; protected internal set; } = DrivingDirections.Forward;
        public HashSet<CarFaults> CarFaults { get; protected internal set; } = new HashSet<CarFaults>();

        public void StartCarEngine()
        {
            if (CarFaults.Any())
                throw new BrokenCarEngineException("");

            CarState = CarStates.On;
            EngineSpeed = 800;
        }

        public void StopCarEngine()
        {
            EngineSpeed = 0;
            CarState = CarStates.Off;
        }

        public void FillFuelTank()
        {
            Fuel = FuelCapacity;
        }

        public void Calculate()
        {
            if (CarState == CarStates.Off)
            {
                throw new CanNotDriveWhileCarOff("Can not drive while car is off");
            }

            CheckOil();
            CheckFuel();
            BurnFuel();
            BurnOil();

            if (Fuel < 0)
            {
                Fuel = 0;
            }
        }

        public void Accelerate()
        {
            if (CarState == CarStates.Off)
            {
                throw new CanNotDriveWhileCarOff("Can not drive while car is off");
            }

            if (Gear.UsedGear.Value == Gears.Neutral)
            {
                if (DrivingDirection == DrivingDirections.Forward)
                    Gear.GearUp();
                else
                    Gear.GearDown();
                return;
            }

            if (Gear.MinGear == Gear.UsedGear.Value)
            {
                EngineSpeed += 100;
                return;
            }

            if (Gear.MaxGear == Gear.UsedGear.Value)
            {
                EngineSpeed += 100;
                return;
            }

            if (EngineSpeed >= 3000)
            {
                Gear.GearUp();
                return;
            }

            EngineSpeed += 100;
        }

        public void ChangeDrivingDirection()
        {
            if (Gear.UsedGear.Value != Gears.Neutral)
            {
                throw new CanNotChangeDrivingDirectionException(
                    "Can not change driving direction while gear is not on neutral direction.");
            }

            if (Speed != 0)
            {
                throw new CanNotChangeDrivingDirectionException("Can not change driving direction while car driving.");
            }

            DrivingDirection = DrivingDirection == DrivingDirections.Backward
                ? DrivingDirections.Forward
                : DrivingDirections.Backward;
        }

        private void BurnOil()
        {
            var oilConsumption = CalculateOilConsumptionInKilometersPerHour();
            const int secondsInOneHour = 3600;
            var burnedOil = oilConsumption / secondsInOneHour;

            EngineOil -= burnedOil;
        }

        private void BurnFuel()
        {
            var fuelConsumption = CalculateFuelConsumptionInKilometersPerHour();
            const int secondsInOneHour = 3600;
            var burnedFuel = fuelConsumption / secondsInOneHour;

            Fuel -= burnedFuel;
        }

        private void CheckFuel()
        {
            if (Fuel <= 0)
            {
                throw new NoFuelException("No fuel in tank.");
            }
        }

        private void CheckOil()
        {
            if (EngineOil < EngineOilGoodMinValue)
            {
                CarFaults.Add(Enums.CarFaults.BrokenEngine);
                throw new OilLevelTooLowException("Oil level is too low.");
            }

            if (EngineOil > EngineOilGoodMaxValue)
            {
                CarFaults.Add(Enums.CarFaults.BrokenEngine);
                throw new OilLevelTooHighException("Oil level is too high.");
            }
        }

        private double CalculateFuelConsumptionInKilometersPerHour()
        {
            if (CarState == CarStates.Off)
            {
                return 0;
            }

            if (Gear.UsedGear.Value == Gears.Neutral)
            {
                return 1;
            }

            var rand = new Random().Next(1, 10) / 10;
            var consumptionFromEngineSpeed = EngineSpeed / 1000;
            const int constValue = 7;

            var result = rand + consumptionFromEngineSpeed + constValue;

            return result < 0 ? 0.1 : result;
        }

        private double CalculateOilConsumptionInKilometersPerHour()
        {
            return 0.001;
        }
    }
}