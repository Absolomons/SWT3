using System;
using System.Threading;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Timer = Microwave.Classes.Boundary.Timer;

namespace Microwave.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Button startCancelButton = new Button();
            Button powerButton = new Button();
            Button timeButton = new Button();

            Door door = new Door();

            Output output = new Output();

            Display display = new Display(output);

            // let user choose power
            int maxPower = 0;
            do
            {
                Console.WriteLine("Type max power of microwave: ");
                string maxPowerInput = Console.ReadLine();
                bool success = Int32.TryParse(maxPowerInput, out maxPower);
                if (!success)
                {
                    Console.WriteLine("Failed to parse input");
                }
                else
                {
                    if (!(50 <= maxPower && maxPower <= 1000))
                        Console.WriteLine("Invalid maxpower inputted. Should be between 50 and 1000 inclusive");
                }
            } while (!(50 <= maxPower && maxPower <= 1000));

            PowerTube powerTube = new PowerTube(output, maxPower);

            Light light = new Light(output);

            Microwave.Classes.Boundary.Timer timer = new Timer();

            CookController cooker = new CookController(timer, display, powerTube);

            Buzzer buzzer = new Buzzer(output);

            UserInterface ui = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, cooker, buzzer);

            // Finish the double association
            cooker.UI = ui;

            // Simulate a simple sequence

            powerButton.Press();

            timeButton.Press();

            startCancelButton.Press();

            Thread.Sleep(10000);
            timeButton.Press();

            // The simple sequence should now run

            System.Console.WriteLine("When you press enter, the program will stop");
            // Wait for input

            System.Console.ReadLine();
        }
    }
}
