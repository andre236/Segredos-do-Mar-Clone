
public abstract class Item
{
    public string Name;
    public string Description;
    public int Price;
    public int Quantity;

    public Item(string name, string description, int price, int quantity)
    {
        Name = name;
        Description = description;
        Price = price;
        Quantity = quantity;
    }
}

public class Food : Item
{
    public int TierFood;
    public Food(string name, string description, int price, int quantity) : base(name, description, price, quantity)
    {
    }
}

public class Remedy : Item
{
    public Remedy(string name, string description, int price, int quantity) : base(name, description, price, quantity)
    {
    }
}

