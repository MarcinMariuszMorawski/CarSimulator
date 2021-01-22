#nullable enable
using System;

namespace CarSimulatorEngine.Exceptions
{
    public class CarSimulatorException : Exception
    {
        public CarSimulatorException(string? message) : base(message)
        {
        }
    }

    public class BrokenCarEngineException : CarSimulatorException
    {
        public BrokenCarEngineException(string? message) : base(message)
        {
        }
    }

    public class NoFuelException : CarSimulatorException
    {
        public NoFuelException(string? message) : base(message)
        {
        }
    }

    public class OilLevelTooLowException : CarSimulatorException
    {
        public OilLevelTooLowException(string? message) : base(message)
        {
        }
    }

    public class OilLevelTooHighException : CarSimulatorException
    {
        public OilLevelTooHighException(string? message) : base(message)
        {
        }
    }

    public class CanNotDriveWhileCarOff : CarSimulatorException
    {
        public CanNotDriveWhileCarOff(string? message) : base(message)
        {
        }
    }

    public class CanNotFillFuelWhileWorking : CarSimulatorException
    {
        public CanNotFillFuelWhileWorking(string? message) : base(message)
        {
        }
    }

    public class CanNotChangeDrivingDirectionException : CarSimulatorException
    {
        public CanNotChangeDrivingDirectionException(string? message) : base(message)
        {
        }
    }
}