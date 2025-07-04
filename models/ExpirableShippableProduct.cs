using ECommerceSystem.interfaces;

namespace ECommerceSystem.models;

public class ExpirableShippableProduct : ExpirableProduct, IShippable
{
    public double Weight { get; private set; }

    public ExpirableShippableProduct(string name, double price, int quantity, DateTime expiryDate, double weight) 
        : base(name, price, quantity, expiryDate)
    {
        if (weight <= 0) throw new ArgumentException("Weight must be positive");
        Weight = weight;
    }

    public double GetWeight() => Weight;
    public string GetName() => Name;
}

