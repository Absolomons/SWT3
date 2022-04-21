using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Controllers
{
    public class CookController : ICookController
    {

        private bool isCooking = false;

        private IDisplay myDisplay;
        private IPowerTube myPowerTube;
        private ITimer myTimer;
        private IButton myTimeAddButton;
        private IButton myTimeSubtractButton;

        public CookController(
            ITimer timer,
            IDisplay display,
            IPowerTube powerTube,
            IButton timeAddButton,
            IButton timeSubtractButton)
        {
            myTimer = timer;
            myDisplay = display;
            myPowerTube = powerTube;
            myTimeAddButton = timeAddButton;
            myTimeSubtractButton = timeSubtractButton;

            timer.Expired += new EventHandler(OnTimerExpired);
            timer.TimerTick += new EventHandler(OnTimerTick);
            timeSubtractButton.Pressed += new EventHandler(OnTimeSubtractPressed);
            timeAddButton.Pressed += new EventHandler(OnTimeAddPressed);
        }

        public void StartCooking(int power, int time)
        {
            myPowerTube.TurnOn(power);
            myTimer.Start(time);
            isCooking = true;
        }

        public void Stop()
        {
            isCooking = false;
            myPowerTube.TurnOff();
            myTimer.Stop();
        }

        public void OnTimerExpired(object sender, EventArgs e)
        {
            if (isCooking)
            {
                isCooking = false;
                myPowerTube.TurnOff();
            }
        }

        public void OnTimerTick(object sender, EventArgs e)
        {
            if (isCooking)
            {
                int remaining = myTimer.TimeRemaining;
                myDisplay.ShowTime(remaining / 60, remaining % 60);
            }
        }
        public void OnTimeAddPressed(object sender, EventArgs e)
        {
           // add to timer
           myTimer.TimeRemaining += 10;
            
            

        }
        public void OnTimeSubtractPressed(object sender, EventArgs e)
        {
            //subtract time
            myTimer.TimeRemaining -= 10;

            //switch (myState)
            //{
            //    case States.COOKING:
            //        myTimer.TimeRemaining -= 10;
            //        myDisplay.ShowTime(time, 0);
            //        break;
            //}
        }
    }
}