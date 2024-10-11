using Xunit;
public class NumberGuessingGameTests
{
    [Fact]
    public void CheckGuess_ShouldReturnCorrect_WhenGuessIsCorrect()
    {
        NumberGuessingGame game = new NumberGuessingGame();
        game.NumberToGuess = 50; 
        string result = game.CheckGuess(50);
        Assert.Equal("Correct!", result);
    }

    [Fact]
    public void CheckGuess_ShouldReturnTooHigh_WhenGuessIsTooHigh()
    {
        NumberGuessingGame game = new NumberGuessingGame();
        game.NumberToGuess = 50;

        string result = game.CheckGuess(60);

        Assert.Equal("Too high!", result);
    }

    [Fact]
    public void CheckGuess_ShouldReturnTooLow_WhenGuessIsTooLow()
    {
        NumberGuessingGame game = new NumberGuessingGame();
        game.NumberToGuess = 50;

        string result = game.CheckGuess(40);

        Assert.Equal("Too low!", result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(50)]
    [InlineData(100)]
    public void IsValidGuess_ShouldReturnTrue_WhenGuessIsInRange(int guess)
    {
        NumberGuessingGame game = new NumberGuessingGame();

        bool result = game.IsValidGuess(guess);

        Assert.True(result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(101)]
    [InlineData(-10)]
    public void IsValidGuess_ShouldReturnFalse_WhenGuessIsOutOfRange(int guess)
    {
        NumberGuessingGame game = new NumberGuessingGame();

        bool result = game.IsValidGuess(guess);

        Assert.False(result);
    }
}

