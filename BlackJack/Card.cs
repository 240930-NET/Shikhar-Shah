public class Card
{
    public int Value { get; private set; } 
    //value property created
    //get set act as a shield around card value. set prevents numbers outside 2-11, and private makes it accessible to only Card class.

    public Card(int value) //parameter created within Card() constructor. called when new card object created 
    {
        this.Value = value; //this. used to prevent ambiguity. clearly differentiates between 2 "values"
    }

    public override string ToString() //overrides string object to ToString()
    {
        return Value.ToString(); //converts integer "Value" into a string
    }
}
