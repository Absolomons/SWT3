using System;
using System.Security.Cryptography;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class PowerTube : IPowerTube
    {
        private IOutput myOutput;

        private bool IsOn = false;
        private int _maxPower;
        public int MaxPower 
        {
            get => _maxPower;
            set =>
                _maxPower = (value <= 1000 && value >= 50
                    ? value
                    : throw new ArgumentOutOfRangeException("maxPower", value, "Must be between 50 and 1000 (incl.)"));
        }


        public PowerTube(IOutput output, int maxPower = 700)
        {
            myOutput = output;
            MaxPower = maxPower;
        }

        public void TurnOn(int power)
        {
            if (power < 1 || MaxPower < power)
            {
                throw new ArgumentOutOfRangeException("power", power, "Must be between 50 and " + MaxPower + " (incl.)");
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