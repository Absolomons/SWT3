using System;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;

namespace Microwave.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Button startCancelButton = new Button();
            Button powerButton = new Button();
            Button timeButton = new Button();
            Button timeSubtractButton = new Button();
            Button timeAddButton = new Button();
            Door door = new Door();

            Output output = new Output();

            Display display = new Display(output);

            PowerTube powerTube = new PowerTube(output);

            Light light = new Light(output);

            Microwave.Classes.Boundary.Timer timer = new Timer();

            CookController cooker = new CookController(timer, display, powerTube,timeAddButton, timeSubtractButton);

            Buzzer buzzer = new Buzzer(output);

            UserInterface ui = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, cooker, buzzer);


            // Simulate a simple sequence

            powerButton.Press();

            timeButton.Press();

            startCancelButton.Press();

            // The simple sequence should now run

            System.Console.WriteLine("When you press enter, the program will stop");
            // Wait for input


            var input = "";

            while (input != "q")
            {
                input = System.Console.ReadLine();
                if (input == "o")
                {
                    timeAddButton.Press();
                }
                if (input == "l")
                {
                    timeSubtractButton.Press();
                }
            }
        }
    }
}
