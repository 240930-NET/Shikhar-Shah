using System;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();
        int numberToGuess = random.Next(1, 101); // Random number between 1 and 100
        int playerGuess = 0;
        int guessCount = 0;

        Console.WriteLine("Welcome to the Number Guessing Game!");
        Console.WriteLine("I have picked a number between 1 and 100. Try to guess it!");

        // Game loop: Continue until the player guesses the correct number
        while (playerGuess != numberToGuess)
        {
            guessCount++;

            Console.Write("Enter your guess: ");
            string input = Console.ReadLine();

            // Input validation: Check if the input is a valid integer
            if (!int.TryParse(input, out playerGuess))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                continue;
            }

            // Check if the guess is too high, too low, or correct
            if (playerGuess > numberToGuess)
            {
                Console.WriteLine("Too high! Try again.");
            }
            else if (playerGuess < numberToGuess)
            {
                Console.WriteLine("Too low! Try again.");
            }
            else
            {
                Console.WriteLine($"Congratulations! You guessed the number in {guessCount} attempts.");
            }
        }

        Console.WriteLine("Thanks for playing!");
    }
}
