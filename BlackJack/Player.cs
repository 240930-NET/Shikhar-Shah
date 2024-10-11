public class Player
{
    public List<Card> Hand { get; private set; }
    public string Name { get; private set; }

    public Player(string name)
    {
        Name = name;
        Hand = new List<Card>();
    }
    public void AddCard(Card card)
    {
        Hand.Add(card);
    }
    public int CalculateHandValue()
    {
        int total = 0;
        int aceCount = 0;

        foreach (Card card in Hand)
        {
            total += card.Value;
            if (card.Value == 11)
            {
                aceCount++;
            }
        }
        while (total > 21 && aceCount > 0)
        {
            total -= 10; // Subtract 10 (convert Ace from 11 to 1)
            aceCount--;
        }

        return total;
    }
        public void ShowHand()
    {
        foreach (Card card in Hand)
        {
            System.Console.Write(card + " ");
        }
        System.Console.WriteLine();
    }
}