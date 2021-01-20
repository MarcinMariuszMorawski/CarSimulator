using CarSimulatorEngine.Enums;
using CarSimulatorEngine.Interfaces;
using System.Collections.Generic;

namespace CarSimulatorEngine.Models
{
    internal class PassengerCarGear : Gear
    {
        public  override LinkedList<Gears> AvailableGearTypes { get; protected internal set; }

        internal PassengerCarGear()
        {
            var gears = new HashSet<Gears>
            {
                Gears.Reverse,
                Gears.Neutral,
                Gears.First,
                Gears.Second,
                Gears.Third,
                Gears.Fourth,
                Gears.Fifth
            };

            AvailableGearTypes = new LinkedList<Gears>();
            foreach (var cos in gears)
            {
                AvailableGearTypes.AddLast(cos);
            }

            UsedGear = AvailableGearTypes.Find(Gears.Neutral);
            MinGear = AvailableGearTypes.First.Value;
            MaxGear = AvailableGearTypes.Last.Value;
        }
    }
}