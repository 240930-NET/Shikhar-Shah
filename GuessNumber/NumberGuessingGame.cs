using System;
using System.IO;
using System.Text.Json;

public class NumberGuessingGame
{
    public int NumberToGuess { get; set; }
    public int GuessCount { get; private set; }
    private const string SaveFilePath = "gameState.json"; // File to persist data

    // Constructor that optionally loads a previously saved number to guess
    public NumberGuessingGame(int? numberToGuess = null)
    {
        Random random = new Random();
        NumberToGuess = numberToGuess ?? random.Next(1, 101); // Use provided number or generate randomly
        GuessCount = 0;
    }

    // Method to check the player's guess
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

    // Method to validate if the guess is within range (1-100)
    public bool IsValidGuess(int playerGuess)
    {
        return playerGuess >= 1 && playerGuess <= 100;
    }

    // Method to play the game
    public void PlayGame()
    {
        // Ask if the user wants to load the previous game
        while (true)
        {
            Console.WriteLine("Do you want to load a previously saved game? (y/n)");
            string loadChoice = Console.ReadLine()?.ToLower();

            if (loadChoice == "y")
            {
                // Attempt to load the saved game
                if (File.Exists(SaveFilePath))
                {
                    NumberGuessingGame loadedGame = LoadGame();
                    NumberToGuess = loadedGame.NumberToGuess;
                    GuessCount = loadedGame.GuessCount;
                    Console.WriteLine($"Game state loaded. You have guessed {GuessCount} times so far.");
                }
                else
                {
                    Console.WriteLine("No saved game found. Starting a new game.");
                }
                break;
            }
            else if (loadChoice == "n")
            {
                Console.WriteLine("Starting a new game.");
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'y' for yes or 'n' for no.");
            }
        }

        int playerGuess = 0;
        bool isGameOver = false;

        // Game loop: Continue until the player guesses the correct number
        while (!isGameOver)
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

            // End the game if the guess is correct
            if (result == "Correct!")
            {
                Console.WriteLine($"Congratulations! You guessed the number in {GuessCount} attempts.");
                isGameOver = true;
            }
        }

        // Ask to save the game only after the game ends
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

        Console.WriteLine("Thanks for playing!");
    }

    // Method to save the game state to a file
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

    // Method to load the game state from a file
    public static NumberGuessingGame LoadGame()
    {
        string json = File.ReadAllText(SaveFilePath);
        var gameState = JsonSerializer.Deserialize<GameState>(json);

        if (gameState != null)
        {
            return new NumberGuessingGame(gameState.NumberToGuess)
            {
                GuessCount = gameState.GuessCount
            };
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
