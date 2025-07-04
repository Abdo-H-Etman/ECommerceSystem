namespace ECommerceSystem.models;

public abstract class Product
{
    public string Name { get; private set; }
    public double Price { get; private set; }
    public int Quantity { get; protected set; }

    public Product(string name, double price, int quantity)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty");
        if (price < 0) throw new ArgumentException("Price cannot be negative");
        if (quantity < 0) throw new ArgumentException("Quantity cannot be negative");

        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public virtual void ReduceQuantity(int amount)
    {
        if (amount > Quantity) throw new InvalidOperationException($"Insufficient stock for {Name}");
        Quantity -= amount;
    }
}
