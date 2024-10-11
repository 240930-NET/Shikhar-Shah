using System;
using System.IO;
using System.Text.Json;

public class NumberGuessingGame
{
    public int NumberToGuess { get; set; }
    public int GuessCount { get; private set; }
    private const string SaveFilePath = "gameState.json"; // File to persist data

    public NumberGuessingGame(int? numberToGuess = null)
    {
        Random random = new Random();
        NumberToGuess = numberToGuess ?? random.Next(1, 101); // Random number or provided number for testing
        GuessCount = 0;
    }

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

    public bool IsValidGuess(int playerGuess)
    {
        return playerGuess >= 1 && playerGuess <= 100;
    }

    // The PlayGame method
    public void PlayGame()
    {
        Console.WriteLine("Welcome to the Number Guessing Game!");
        Console.WriteLine("I have picked a number between 1 and 100. Try to guess it!");

        int playerGuess = 0;
        while (true)
        {
            Console.Write("Enter your guess (1-100): ");
            string input = Console.ReadLine() ?? "";

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

                // Ask to save the game after the game ends
                while (true)
                {
                    Console.WriteLine("Do you want to save the game? (y/n)");
                    string saveChoice = Console.ReadLine()?.ToLower();

                    if (saveChoice == "y")
                    {
                        SaveGame();
                        break;
                    }
                    else if (saveChoice == "n")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter 'y' for yes or 'n' for no.");
                    }
                }
                break; // End the game loop
            }
        }

        Console.WriteLine("Thanks for playing!");
    }

    public void SaveGame()
    {
        var gameState = new GameState
        {
            NumberToGuess = this.NumberToGuess,
            GuessCount = this.GuessCount
        };

        string json = JsonSerializer.Serialize(gameState);
        File.WriteAllText(SaveFilePath, json);
        Console.WriteLine("Game state saved.");
    }

    public static NumberGuessingGame LoadGame()
    {
        if (!File.Exists(SaveFilePath))
        {
            Console.WriteLine("No saved game found.");
            return new NumberGuessingGame();
        }

        string json = File.ReadAllText(SaveFilePath);
        var gameState = JsonSerializer.Deserialize<GameState>(json);

        if (gameState != null)
        {
            var game = new NumberGuessingGame(gameState.NumberToGuess);
            game.GuessCount = gameState.GuessCount;
            Console.WriteLine("Game state loaded.");
            return game;
        }

        return new NumberGuessingGame();
    }
}

// Class to hold game state for serialization
public class GameState
{
    public int NumberToGuess { get; set; }
    public int GuessCount { get; set; }
}
