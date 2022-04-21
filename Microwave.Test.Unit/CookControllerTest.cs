using System;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class CookControllerTest
    {
        private CookController uut;

        private ITimer timer;
        private IDisplay display;
        private IPowerTube powerTube;
        private IButton addButton;
        private IButton subButton;

        [SetUp]
        public void Setup()
        {
            timer = Substitute.For<ITimer>();
            display = Substitute.For<IDisplay>();
            powerTube = Substitute.For<IPowerTube>();
            addButton = Substitute.For<IButton>();
            subButton = Substitute.For<IButton>();

            uut = new CookController(timer, display, powerTube, addButton, subButton);
        }

        [Test]
        public void StartCooking_ValidParameters_TimerStarted()
        {
            uut.StartCooking(50, 60);

            timer.Received().Start(60);
        }

        [Test]
        public void StartCooking_ValidParameters_PowerTubeStarted()
        {
            uut.StartCooking(50, 60);

            powerTube.Received().TurnOn(50);
        }

        [Test]
        public void Cooking_TimerTick_DisplayCalled()
        {
            uut.StartCooking(50, 60);

            timer.TimeRemaining.Returns(115);
            timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            display.Received().ShowTime(1, 55);
        }

        [Test]
        public void Cooking_TimerExpired_PowerTubeOff()
        {
            uut.StartCooking(50, 60);

            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            powerTube.Received(1).TurnOff();
        }


        [Test]
        public void Cooking_Stop_PowerTubeOff()
        {
            uut.StartCooking(50, 60);
            uut.Stop();

            powerTube.Received().TurnOff();
        }
        [Test]
        public void Cooking_TimerAddPressed_CorrectTime()
        {
            uut.StartCooking(50, 60);

            timer.TimeRemaining.Returns(105);
            addButton.Press();
            timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            display.Received().ShowTime(1, 55);
        }
        [Test]
        public void Cooking_TimerSubButton_CorrectTime()
        {
            uut.StartCooking(50, 60);

            timer.TimeRemaining.Returns(115);
            subButton.Press();
            timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            display.Received().ShowTime(1, 45);
        }

        [TestCase(50)]
        [TestCase(1000)]
        public void GetMaxPower_SetPower_ReturnsCorrectPower(int maxPower)
        {
            powerTube.MaxPower.Returns(maxPower);

            Assert.That(uut.GetMaxPower(), Is.EqualTo(maxPower));
        }

    }
}