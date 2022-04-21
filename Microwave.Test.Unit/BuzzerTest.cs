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

        [TestCase(1,1000)]
        [TestCase(2,1000)]
        [TestCase(3,1000)]
        public void Buzz_WasCalledCorrectNumberOfTimes_FixedDelay(int numOfBursts, int delay)
        {
            uut.Buzz(numOfBursts, delay);
            BuzzerOutput.Received(numOfBursts).OutputLine("Bzzzzzzzzzzz");
        }

        [TestCase(-1, 1000)]
        [TestCase(3, -500)]
        public void Buzz_ThrowsOutOfRangeException_NegativeInputs(int numOfBursts, int delay)
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => uut.Buzz(numOfBursts, delay));
        }
    }
}
