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
        NumberToGuess = numberToGuess ?? random.Next(1, 101);
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