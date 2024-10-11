using System;
using System.Collections.Generic;

public class Deck
{
    private List<Card> cards;
    private Random random;
    public Deck()
    {
        random = new Random();
        cards = new List<Card>();
        
        for (int i = 2; i <= 11; i++)
        {
            for (int j = 0; j < 4; j++) // 4 cards per value
            {
                cards.Add(new Card(i));
            }
        }
    }
    public Card DrawCard()
    {
        int index = random.Next(cards.Count);
        Card card = cards[index];
        cards.RemoveAt(index);
        return card;
    }
}