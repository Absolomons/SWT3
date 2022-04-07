using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class PowerTube : IPowerTube
    {
        private IOutput myOutput;
        private int _maxPower;

        private bool IsOn = false;

        public PowerTube(IOutput output, int maxPower = 700)
        {
            myOutput = output;
            _maxPower = maxPower > 0 && maxPower <= 1000 ? maxPower : 
                throw new ArgumentOutOfRangeException("maxPower", maxPower, "Must be between 1 and 1000 (incl.)");
        }

        public void TurnOn(int power)
        {
            if (power < 1 || _maxPower < power)
            {
                throw new ArgumentOutOfRangeException("power", power, "Must be between 1 and " + _maxPower + " (incl.)");
            }

            if (IsOn)
            {
                throw new ApplicationException("PowerTube.TurnOn: is already on");
            }

            myOutput.OutputLine($"PowerTube works with {power}");
            IsOn = true;
        }

        public void TurnOff()
        {
            if (IsOn)
            {
                myOutput.OutputLine($"PowerTube turned off");
            }

            IsOn = false;
        }
    }
}