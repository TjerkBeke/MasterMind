using System;
using System.Collections.Generic;

namespace MasterMind
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // The number of balls you can pick. The higher the number, the more difficult the game.
            int NumberOfBalls = 4;
            // The number of chances you get. The lower, the more difficult the game.
            int NumberOfChances = 12;

            // This string holds the overview of attempts already made
            string Outcome = "";

            // Take a random order of balls to guess
            var BallsToGuess = new List<Ball>();
            for (int i = 0; i < NumberOfBalls; i++)
            {
                BallsToGuess.Add(new());
            }

            // Show the computer's choice. Uncomment this section to cheat ;-)
            //Console.WriteLine("The computer chose the following balls");
            //Console.WriteLine(ConvertBallColorToString(BallsToGuess));

            // Show the intro and game rules
            ShowIntro();


            // Loop until you have no more chances left
            for (int i = 0; i < NumberOfChances; i++)
            {
                Console.WriteLine($"Chances left: {NumberOfChances - i}");

                // Choose your balls
                Console.WriteLine("Provide your choice:");
                List<Ball> BallsPicked = GetYourBalls(BallsToGuess.Count);

                // Calculate the exact and non-exact matches. An exact match gicves you a black pin, a non-exact match gives you a white pin
                int[] ReturnedPins = CalculatePins(BallsToGuess, BallsPicked);

                // Show the results to the screen, and show the previous results as well.
                Outcome = GetOutcome(ReturnedPins, BallsPicked, Outcome);
                Console.Clear();
                Console.WriteLine(Outcome);


                // Check if you won
                if (ReturnedPins[0] == 4)
                {
                    Console.WriteLine("Congratulations!! You have guessed the correct answer!");
                    Console.WriteLine($"You had { NumberOfChances - i } chances left");
                    return;
                }
            }

            // If you come to here, it means you didn't win :-(
            Console.WriteLine("Sorry, but you didn't find the correct solution within the needed number of chances.");
            Console.WriteLine("The computer chose the following balls");
            Console.WriteLine(ConvertBallColorToString(BallsToGuess));

        }

        #region Methods
        /// <summary>
        /// Shows the different pins, and the chosen ball collection.
        /// </summary>
        /// <param name="returnedPins"></param>
        /// <param name="ballsPicked"></param>
        private static string GetOutcome(int[] returnedPins, List<Ball> ballsPicked, string formerAttempt)
        {
            string ballColors = ConvertBallColorToString(ballsPicked);
            string pinColors = ConvertPinColorToString(returnedPins);
            return formerAttempt + (pinColors + ballColors) + "\n";

        }
        /// <summary>
        /// Takes an array of 2 integers, and transforms them into a string with the number of characters defined in the integers.
        /// # is the number of black pins.
        /// | is the number of white pins.
        /// </summary>
        /// <param name="ReturnedPins"></param>
        /// <returns></returns>
        private static string ConvertPinColorToString(int[] ReturnedPins)
        {
            string PinList = "";

            string Black = new string('#', ReturnedPins[0]);
            string White = new string('|', ReturnedPins[1]);

            PinList = (Black + White).PadRight(4);

            return ($"'{PinList}'");
        }

        /// <summary>
        /// Shows the goal of the game and the rules.
        /// </summary>
        private static void ShowIntro()
        {
            Console.WriteLine("Welcome to this game of MasterMind.");
            Console.WriteLine("The goal of the game is to guess what colors and the correct order of colors I have hidden.");
            Console.WriteLine("You can choose between the colors: BLUE - RED - GREEN - WHITE - PINK - YELLOW");
            Console.WriteLine("You provide input by taking the first letters of the colors; so for example for the color yellow, you need to press the Y-key.");
            Console.WriteLine("After every guess, I will tell you:");
            Console.WriteLine("The number of correct colors in the correct place: a black pin");
            Console.WriteLine("The number of correct colors not in the correct place: a white pin");
            Console.WriteLine("Press a key to start");
            Console.ReadLine();
            Console.Clear();
        }

        /// <summary>
        /// Asks for the balls you want to pick, using the readkey function, and creates a List of balls.
        /// </summary>
        /// <param name="NumberOfBalls"></param>
        /// <returns></returns>
        private static List<Ball> GetYourBalls(int NumberOfBalls)
        {
            var yourBalls = new List<Ball>();

            for (int i = 0; i < NumberOfBalls; i++)
            {
                var yourBallColor = Console.ReadKey().Key;

                switch (yourBallColor)
                {
                    case ConsoleKey.W:
                        yourBalls.Add(new(BallColor.White));
                        break;
                    case ConsoleKey.R:
                        yourBalls.Add(new(BallColor.Red));
                        break;
                    case ConsoleKey.G:
                        yourBalls.Add(new(BallColor.Green));
                        break;
                    case ConsoleKey.B:
                        yourBalls.Add(new(BallColor.Blue));
                        break;
                    case ConsoleKey.P:
                        yourBalls.Add(new(BallColor.Pink));
                        break;
                    case ConsoleKey.Y:
                        yourBalls.Add(new(BallColor.Yellow));
                        break;
                    default:
                        yourBalls.Add(new(BallColor.None));
                        break;
                }

            }

            return yourBalls;
        }

        /// <summary>
        /// Calculates the resulting pins of your guess. An exact match is a black pin, a non-exact match is a white pin.
        /// </summary>
        /// <param name="ballsToGuess"></param>
        /// <param name="ballsPicked"></param>
        /// <returns></returns>
        private static int[] CalculatePins(List<Ball> ballsToGuess, List<Ball> ballsPicked)
        {
            int blackPin = 0;
            int whitePin = 0;

            // First of all, create a duplicate of both ball objects,
            // as we don't want to change the balls of the original list
            var duplicateBallsToGuess = new List<Ball>();
            for (int ballIndex = 0; ballIndex < ballsToGuess.Count; ballIndex++)
            {
                duplicateBallsToGuess.Add(new(ballsToGuess[ballIndex].Color));
            }

            var duplicateBallsPicked = new List<Ball>();
            for (int ballIndex = 0; ballIndex < ballsPicked.Count; ballIndex++)
            {
                duplicateBallsPicked.Add(new(ballsPicked[ballIndex].Color));
            }

            // First check all exact matches
            for (int i = 0; i < duplicateBallsPicked.Count; i++)
            {
                // check if there is an exact match
                if (duplicateBallsToGuess[i].Equals(duplicateBallsPicked[i]))
                {
                    // Black pin, as it is found, and on the exact place
                    blackPin++;
                    duplicateBallsToGuess[i].Color = BallColor.None;
                    duplicateBallsPicked[i].Color = BallColor.None;
                    continue;
                }
            }

            // Now check all non-exact matches
            for (int i = 0; i < duplicateBallsPicked.Count; i++)
            {
                if (duplicateBallsPicked[i].Color == BallColor.None)
                {
                    // Ball is already mapped to an exact match
                    continue;
                }
                // Find the index of color to look for; index of -1 means it is not found
                int index = duplicateBallsToGuess.FindIndex(ball => ball.Color == duplicateBallsPicked[i].Color);
                if (index == -1)
                {
                    // Nothing found, so moving to the next
                    continue;
                }
                // White pin, as it is found, but not on the exact place
                duplicateBallsToGuess[index].Color = BallColor.None;
                whitePin++;
            }
            int[] ReturnedPins = { blackPin, whitePin };
            return ReturnedPins;
        }

        /// <summary>
        /// Converts a list of object balls into a string, where the colors are the first letters of their color name
        /// </summary>
        /// <param name="balls"></param>
        /// <returns></returns>
        public static string ConvertBallColorToString(List<Ball> balls)
        {
            string ballcolors = "";
            for (int i = 0; i < balls.Count; i++)
            {
                switch (balls[i].Color)
                {
                    case BallColor.White:
                        ballcolors += "W ";
                        break;
                    case BallColor.Red:
                        ballcolors += "R ";
                        break;
                    case BallColor.Blue:
                        ballcolors += "B ";
                        break;
                    case BallColor.Green:
                        ballcolors += "G ";
                        break;
                    case BallColor.Yellow:
                        ballcolors += "Y ";
                        break;
                    case BallColor.Pink:
                        ballcolors += "P ";
                        break;
                    default:
                        ballcolors += "X ";
                        break;
                }
            }

            return ballcolors.Trim();
        } 
        #endregion

    }

}
