using ECommerceSystem.interfaces;
namespace ECommerceSystem.models;

public class ShippableProduct : Product, IShippable
{
    public double Weight { get; private set; }

    public ShippableProduct(string name, double price, int quantity, double weight)
        : base(name, price, quantity)
    {
        if (weight <= 0) throw new ArgumentException("Weight must be positive");
        Weight = weight;
    }

    public double GetWeight() => Weight;
    public string GetName() => Name;
}
