using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class PowerTubeTest
    {
        private PowerTube uut;
        private IOutput output;
// comment on main
        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();
            uut = new PowerTube(output, 700);
        }

        [TestCase(50)]
        [TestCase(500)]
        [TestCase(700)]
        public void TurnOn_WasOffCorrectPower_CorrectOutput(int power)
        {
            uut.TurnOn(power);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"{power}")));
        }

        [TestCase(-1000)]
        [TestCase(49)]
        [TestCase(701)]
        [TestCase(1000)]
        public void TurnOn_WasOffOutOfRangePower_ThrowsException(int power)
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => uut.TurnOn(power));
        }

        [Test]
        public void TurnOff_WasOn_CorrectOutput()
        {
            uut.TurnOn(50);
            uut.TurnOff();
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void TurnOff_WasOff_NoOutput()
        {
            uut.TurnOff();
            output.DidNotReceive().OutputLine(Arg.Any<string>());
        }

        [Test]
        public void TurnOn_WasOn_ThrowsException()
        {
            uut.TurnOn(50);
            Assert.Throws<System.ApplicationException>(() => uut.TurnOn(60));
        }

        [TestCase(50)]
        [TestCase(500)]
        [TestCase(1000)]
        public void MaxPower_SetMaxPowerCorrect_TurnOnAtThatPowerWorks(int maxPower)
        {
            uut.MaxPower = maxPower;

            uut.TurnOn(maxPower);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"{maxPower}")));
        }

        [TestCase(49)]
        [TestCase(1001)]
        public void MaxPower_SetMaxPowerInCorrect_ArgumentOutOfRangeExceptionThrown(int maxPower)
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => uut.MaxPower = maxPower);
        }
    }
}