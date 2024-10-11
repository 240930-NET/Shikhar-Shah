public class Game
{
    private Deck deck;
    private Player player;
    private Player dealer;

    public Game()
    {
        deck = new Deck();
        player = new Player("Player");
        dealer = new Player("Dealer");
    }

    public void PlayGame()
    {
        DealInitialCards();

        // Player's turn
        PlayerTurn();

        // Dealer's turn if player hasn't busted
        if (player.CalculateHandValue() <= 21)
        {
            DealerTurn();
        }

        // Determine the winner
        DetermineWinner();
    }

    private void DealInitialCards()
    {
        // Deal two cards to both the player and dealer
        player.AddCard(deck.DrawCard());
        player.AddCard(deck.DrawCard());
        dealer.AddCard(deck.DrawCard());
        dealer.AddCard(deck.DrawCard());

        Console.WriteLine("Your hand:");
        player.ShowHand();
        Console.WriteLine("Dealer's hand:");
        Console.WriteLine(dealer.Hand[0] + " [Hidden]");
    }

   private void PlayerTurn()
{
    while (true)
    {
        Console.WriteLine("Your Score: " + player.CalculateHandValue());
        
        // Input validation loop
        string choice;
        do
        {
            Console.Write("Do you want to hit or stand? (h/s): ");
            choice = Console.ReadLine()?.ToLower();  // Convert input to lowercase for simplicity

            if (choice != "h" && choice != "s")
            {
                Console.WriteLine("Invalid input! Please enter 'h' for hit or 's' for stand.");
            }

        } while (choice != "h" && choice != "s");

        // If player chooses to hit
        if (choice == "h")
        {
            player.AddCard(deck.DrawCard());
            Console.WriteLine("Your new hand:");
            player.ShowHand();

            if (player.CalculateHandValue() > 21)
            {
                Console.WriteLine("Bust! You exceeded 21.");
                break;
            }
        }
        else if (choice == "s")
        {
            break;  // Player chooses to stand
        }
    }
}

    private void DealerTurn()
    {
        Console.WriteLine("Dealer's turn:");
        dealer.ShowHand();

        while (dealer.CalculateHandValue() < 17)
        {
            dealer.AddCard(deck.DrawCard());
            Console.WriteLine("Dealer's hand:");
            dealer.ShowHand();
        }
    }

    private void DetermineWinner()
    {
        int playerTotal = player.CalculateHandValue();
        int dealerTotal = dealer.CalculateHandValue();

        Console.WriteLine("Your Score: " + playerTotal);
        Console.WriteLine("Dealer's Score: " + dealerTotal);

        if (playerTotal > 21)
        {
            Console.WriteLine("Dealer wins! You busted.");
        }
        else if (dealerTotal > 21)
        {
            Console.WriteLine("You win! Dealer busted.");
        }
        else if (playerTotal > dealerTotal)
        {
            Console.WriteLine("You win!");
        }
        else if (dealerTotal > playerTotal)
        {
            Console.WriteLine("Dealer wins!");
        }
        else
        {
            Console.WriteLine("It's a tie!");
        }
    }
}
