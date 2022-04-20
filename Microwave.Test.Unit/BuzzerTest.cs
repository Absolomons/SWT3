using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class BuzzerTest
    {
        private Buzzer uut;
        private IOutput BuzzerOutput;

        [SetUp]
        public void Setup()
        {
            BuzzerOutput = Substitute.For<IOutput>();
            uut = new Buzzer(BuzzerOutput);
        }
    }
}
