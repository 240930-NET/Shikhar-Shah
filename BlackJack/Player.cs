public class Player
{
    public List<Card> Hand { get; private set; }
    public string Name { get; private set; }

    public Player(string name)
    {
        Name = name;
        Hand = new List<Card>();
    }

    // Add a card to the hand
    public void AddCard(Card card)
    {
        Hand.Add(card);
    }

    // Calculate the hand value, treating Ace (11) as 1 if necessary
    public int CalculateHandValue()
    {
        int total = 0;
        int aceCount = 0;

        foreach (Card card in Hand)
        {
            total += card.Value;
            if (card.Value == 11) // Track how many Aces we have
            {
                aceCount++;
            }
        }

        // Adjust for Aces: if total is over 21, treat Aces as 1 instead of 11
        while (total > 21 && aceCount > 0)
        {
            total -= 10; // Subtract 10 (convert Ace from 11 to 1)
            aceCount--;
        }

        return total;
    }

    // Display the hand in a readable format
    public void ShowHand()
    {
        foreach (Card card in Hand)
        {
            System.Console.Write(card + " ");
        }
        System.Console.WriteLine();
    }
}