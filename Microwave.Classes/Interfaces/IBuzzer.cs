using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwave.Classes.Interfaces
{
    public interface IBuzzer
    {
        public void Buzz(int numOfBursts, int delay);
    }
}
