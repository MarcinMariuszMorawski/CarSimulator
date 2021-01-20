using CarSimulatorEngine.Enums;
using System.Collections.Generic;

namespace CarSimulatorEngine.Interfaces
{
    internal interface IGear
    {
        public Gears GearUp();
        public Gears GearDown();
    }

    internal abstract class Gear : IGear
    {
        public abstract LinkedList<Gears> AvailableGearTypes { get; protected internal set; }
        public LinkedListNode<Gears> UsedGear { get; protected internal set; }
        public Gears MaxGear { get; protected internal set; }
        public Gears MinGear { get; protected internal set; }
        public bool IsLastGearInUse => UsedGear.Value == MaxGear || UsedGear.Value == MinGear; 

        public Gears GearUp()
        {
            var gear =  UsedGear.Next;
            UsedGear = gear;
            return gear.Value;
        }

        public Gears GearDown()
        {
            var gear = UsedGear.Previous;
            UsedGear = gear;
            return gear.Value;
        }
    }
}