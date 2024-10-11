using System;

class Program
{
    static void Main(string[] args)
    {
        NumberGuessingGame game = new NumberGuessingGame();
        game.PlayGame();
    }
}

public class NumberGuessingGame
{
    public int NumberToGuess { get; set; }
    public int GuessCount { get; private set; }

    public NumberGuessingGame()
    {
        Random random = new Random();
        NumberToGuess = random.Next(1, 101); // Random number between 1 and 100
        GuessCount = 0;
    }

    // This method checks if the guess is too high, too low, or correct
    public string CheckGuess(int playerGuess)
    {
        GuessCount++;
        if (playerGuess > NumberToGuess)
        {
            return "Too high!";
        }
        else if (playerGuess < NumberToGuess)
        {
            return "Too low!";
        }
        else
        {
            return "Correct!";
        }
    }

    // This method validates if the input is within range (1-100)
    public bool IsValidGuess(int playerGuess)
    {
        return playerGuess >= 1 && playerGuess <= 100;
    }

    public void PlayGame()
    {
        Console.WriteLine("Welcome to the Number Guessing Game!");
        Console.WriteLine("I have picked a number between 1 and 100. Try to guess it!");

        int playerGuess = 0;
        while (true)
        {
            Console.Write("Enter your guess (1-100): ");
            string input = Console.ReadLine() ?? ""; // Handle null input by assigning an empty string

            if (!int.TryParse(input, out playerGuess))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                continue;
            }

            if (!IsValidGuess(playerGuess))
            {
                Console.WriteLine("Your guess is out of range! Please guess a number between 1 and 100.");
                continue;
            }

            string result = CheckGuess(playerGuess);
            Console.WriteLine(result);

            if (result == "Correct!")
            {
                Console.WriteLine($"Congratulations! You guessed the number in {GuessCount} attempts.");
                break;
            }
        }
    }
}
