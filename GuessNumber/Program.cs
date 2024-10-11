using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Number Guessing Game!");
        Console.WriteLine("Do you want to load a previous game? (y/n)");
        string loadGameChoice = Console.ReadLine();

        NumberGuessingGame game;
        if (loadGameChoice?.ToLower() == "y")
        {
            game = NumberGuessingGame.LoadGame();
        }
        else
        {
            game = new NumberGuessingGame();
        }

        while (true)
        {
            Console.Write("Enter your guess (1-100): ");
            string input = Console.ReadLine() ?? "";

            if (!int.TryParse(input, out int playerGuess))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                continue;
            }

            if (!game.IsValidGuess(playerGuess))
            {
                Console.WriteLine("Your guess is out of range! Please guess a number between 1 and 100.");
                continue;
            }

            string result = game.CheckGuess(playerGuess);
            Console.WriteLine(result);

            if (result == "Correct!")
            {
                Console.WriteLine($"Congratulations! You guessed the number in {game.GuessCount} attempts.");
                break;
            }

            // Option to save the game after each guess
            Console.WriteLine("Do you want to save the game? (y/n)");
            string saveChoice = Console.ReadLine();
            if (saveChoice?.ToLower() == "y")
            {
                game.SaveGame();
            }
        }

        Console.WriteLine("Thanks for playing!");
    }
}
