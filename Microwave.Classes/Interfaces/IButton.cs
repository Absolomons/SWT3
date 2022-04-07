using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwave.Classes.Interfaces
{
    // This cool
    public interface IButton
    {
        event EventHandler Pressed;

        void Press();
    }
}
