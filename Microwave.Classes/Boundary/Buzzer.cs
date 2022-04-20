using Microwave.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microwave.Classes.Boundary
{
    public class Buzzer : IBuzzer
    {
        private IOutput BuzzerOutput;

        public Buzzer(IOutput output)
        {
            BuzzerOutput = output;
        }

        public void Buzz(int numOfBursts, int delay)
        {
            for (int i = 0; i < numOfBursts; i++)
            {
                BuzzerOutput.OutputLine("Bzzzzzzzzzzz");
                Thread.Sleep(delay);
            }
        }
    }
}
