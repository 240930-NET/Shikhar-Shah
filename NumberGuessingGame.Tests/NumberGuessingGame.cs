using Xunit; // Import xUnit for testing

public class NumberGuessingGameTests
{
    [Fact]
    public void CheckGuess_ShouldReturnCorrect_WhenGuessIsCorrect()
    {
        // Arrange
        NumberGuessingGame game = new NumberGuessingGame();
        game.NumberToGuess = 50; // Set a known value to guess

        // Act
        string result = game.CheckGuess(50);

        // Assert
        Assert.Equal("Correct!", result);
    }

    [Fact]
    public void CheckGuess_ShouldReturnTooHigh_WhenGuessIsTooHigh()
    {
        // Arrange
        NumberGuessingGame game = new NumberGuessingGame();
        game.NumberToGuess = 50;

        // Act
        string result = game.CheckGuess(60);

        // Assert
        Assert.Equal("Too high!", result);
    }

    [Fact]
    public void CheckGuess_ShouldReturnTooLow_WhenGuessIsTooLow()
    {
        // Arrange
        NumberGuessingGame game = new NumberGuessingGame();
        game.NumberToGuess = 50;

        // Act
        string result = game.CheckGuess(40);

        // Assert
        Assert.Equal("Too low!", result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(50)]
    [InlineData(100)]
    public void IsValidGuess_ShouldReturnTrue_WhenGuessIsInRange(int guess)
    {
        // Arrange
        NumberGuessingGame game = new NumberGuessingGame();

        // Act
        bool result = game.IsValidGuess(guess);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(101)]
    [InlineData(-10)]
    public void IsValidGuess_ShouldReturnFalse_WhenGuessIsOutOfRange(int guess)
    {
        // Arrange
        NumberGuessingGame game = new NumberGuessingGame();

        // Act
        bool result = game.IsValidGuess(guess);

        // Assert
        Assert.False(result);
    }
}
