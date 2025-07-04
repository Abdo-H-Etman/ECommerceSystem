namespace ECommerceSystem.models;

public class NonExpirableProduct : Product
{
    public NonExpirableProduct(string name, double price, int quantity) 
        : base(name, price, quantity) { }
}
