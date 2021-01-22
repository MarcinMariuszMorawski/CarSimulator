using CarSimulatorEngine.Enums;
using CarSimulatorEngine.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarSimulatorEngine.Interfaces
{
    internal interface ICar
    {
        void Work();
        void StartCarEngine();
        void StopCarEngine();
        void FillFuelTank();
        void Accelerate();
        void Decelerate();
        void GearUp();
        void GearDown();
    }

    internal abstract class Car : ICar
    {
        public double CarSpeed { get; protected internal set; } = 0;
        public abstract double CarSpeedMaxValue { get; protected internal set; }
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
        public HashSet<CarFaults> CarFaults { get; protected internal set; } = new HashSet<CarFaults>();

        public void StartCarEngine()
        {
            if (CarFaults.Any())
                throw new BrokenCarEngineException("Car has faults, can not start engine");

            CarState = CarStates.On;
            EngineSpeed = 800;
        }

        public void StopCarEngine()
        {
            EngineSpeed = 0;
            CarSpeed = 0;
            CarState = CarStates.Off;
        }

        public void FillFuelTank()
        {
            if (CarState == CarStates.On)
            {
                throw new CanNotFillFuelWhileWorking("Can not fill tank while car is working");
            }
            Fuel = FuelCapacity;
        }

        public void Work()
        {
            if (CarState == CarStates.Off)
            {
                return;
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
                throw new CanNotDriveWhileCarOff("Can not accelerate while car is off");
            }

            CalculateEngineSpeed(1);
            CalculateCarSpeed(1);
        }

        public void GearUp()
        {
            if (Gear.UsedGear.Next is null)
            {
                return;
            }

            if (Gear.UsedGear.Value != Gears.Neutral && !Gear.IsLastGearInUse)
            {
                EngineSpeed -= EngineSpeed * 0.3;
            }

            Gear.GearUp();
        }

        public void GearDown()
        {
            if (Gear.UsedGear.Previous is null)
            {
                return;
            }

            if (Gear.UsedGear.Value != Gears.Neutral && !Gear.IsLastGearInUse)
            {
                EngineSpeed += EngineSpeed * 0.3;
            }

            Gear.GearDown();
        }

        public void Decelerate()
        {
            if (CarState == CarStates.Off)
            {
                throw new CanNotDriveWhileCarOff("Can not decelerate drive while car is off");
            }


            CalculateEngineSpeed(-1);
            CalculateCarSpeed(-1);
        }

        private void CalculateEngineSpeed(int ratio)
        {
            var calculatedEngineSpeed = EngineSpeed + ratio * (2 * new Random().Next(10, 20) + EngineSpeed / 1000);

            if (calculatedEngineSpeed <= 800)
            {
                EngineSpeed = 800;
                return;
            }

            if (calculatedEngineSpeed >= EngineSpeedMaxValue)
            {
                EngineSpeed = EngineSpeedMaxValue;
                return;
            }

            EngineSpeed = calculatedEngineSpeed;
        }

        private void CalculateCarSpeed(int ratio)
        {
            double calculatedSpeed;

            if (Gear.UsedGear.Value == Gears.Neutral)
            {
                var slowingDownValue = new Random().Next(10, 20);
                calculatedSpeed = CarSpeed - slowingDownValue;
            }
            else
            {
                var positiveGearNumber =
                    Gear.UsedGear.Value < 0 ? (int) Gear.UsedGear.Value * -1 : (int) Gear.UsedGear.Value;
                calculatedSpeed = positiveGearNumber * 10 + EngineSpeed / 100 + ratio;
            }


            if (calculatedSpeed <= 0)
            {
                CarSpeed = 0;
                return;
            }

            if (calculatedSpeed >= CarSpeedMaxValue)
            {
                CarSpeed = CarSpeedMaxValue;
                return;
            }

            CarSpeed = calculatedSpeed;
        }

        private void BurnOil()
        {
            var oilConsumption = CalculateOilConsumptionInKilometersPerHour();
            const int secondsInOneHour = 3600;
            var burnedOil = oilConsumption / secondsInOneHour;

            var calculatedOil = EngineOil - burnedOil;

            if (calculatedOil <= 0)
            {
                EngineOil = 0;
                return;
            }

            EngineOil = calculatedOil;
        }

        private void BurnFuel()
        {
            var fuelConsumption = CalculateFuelConsumptionInKilometersPerHour();
            const int secondsInOneHour = 3600;
            var burnedFuel = fuelConsumption / secondsInOneHour;
            var calculatedFuel = Fuel - burnedFuel;

            if (calculatedFuel <= 0)
            {
                Fuel = 0;
                return;
            }

            Fuel = calculatedFuel;
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
            return 0.01;
        }
    }
}