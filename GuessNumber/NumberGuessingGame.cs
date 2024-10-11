using System;
using System.IO;
using System.Text.Json;

public class NumberGuessingGame
{
    public int NumberToGuess { get; set; } //build errors when using private set
    public int GuessCount { get; private set; }
    private const string SaveFilePath = "gameState.json";
    public NumberGuessingGame(int? numberToGuess = null) //
    {
        Random random = new Random(); 
        NumberToGuess = numberToGuess ?? random.Next(1, 101); //new instance generates random number 
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
        return playerGuess >= 1 && playerGuess <= 100; //input validation condition
    }
    public void PlayGame() //method that runs the game loop
    {
        while (true)
        {
            Console.WriteLine("Do you want to load a previously saved game? (y/n)");
            string loadChoice = Console.ReadLine()?.ToLower();

            if (loadChoice == "y") //input validation
            {
                if (File.Exists(SaveFilePath)) //load the saved game by calling LoadGame() - serialization
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
            else if (loadChoice == "n") //input validation
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
        bool isGameOver = false; //command will indicate when the game is over

        while (!isGameOver) //runs while !isGameOver is true 
        {
            Console.Write("Enter your guess (1-100): ");
            string input = Console.ReadLine() ?? "";

            if (!int.TryParse(input, out playerGuess)) //input string to integer // 
            {
                Console.WriteLine("Invalid input. Please enter a valid number."); //input validation
                continue; //skips rest of program in loop and goes back to the condition
            }
            if (!IsValidGuess(playerGuess))
            {
                Console.WriteLine("Your guess is out of range! Please guess a number between 1 and 100.");
                continue;  //skips rest of program - back to top of loop
            }

            string result = CheckGuess(playerGuess);
            Console.WriteLine(result);

            if (result == "Correct!")
            {
                Console.WriteLine($"Congratulations! You guessed the number in {GuessCount} attempts.");
                isGameOver = true;
                break; //loop ends here
            }
            if (GuessCount % 3 == 0) //asks user to save game every 3 attempts
            {
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
            }
        }

        Console.WriteLine("Thanks for playing!");
    }
    public void SaveGame() //method to save game
    {
        var gameState = new GameState //object holds current values of NumberToGuess and GuessCount
        {
            NumberToGuess = this.NumberToGuess, //value of NumberToGuess property from NumberGuessingGame assigned to NumberToGuess property from GameState
            GuessCount = this.GuessCount
        };
        string json = JsonSerializer.Serialize(gameState); //converts string to json format
        File.WriteAllText(SaveFilePath, json);
        Console.WriteLine("Game state saved.");
    }
    public static NumberGuessingGame LoadGame() //method to load previous game or create new fresh game
    {
        string json = File.ReadAllText(SaveFilePath); //reads entire file into memory as a string
        var gameState = JsonSerializer.Deserialize<GameState>(json); //deserializing
        if (gameState != null) //checks if successfully deserialized
        {
            return new NumberGuessingGame(gameState.NumberToGuess)//ensures game will resume from where user left off
            {
                GuessCount = gameState.GuessCount
            };
        }
        return new NumberGuessingGame();
    }
}
public class GameState
{
        public int NumberToGuess { get; set; }
    public int GuessCount { get; set; }
}
