namespace ECommerceSystem.models;

public class ExpirableProduct : Product
{
    public DateTime ExpiryDate { get; private set; }

    public ExpirableProduct(string name, double price, int quantity, DateTime expiryDate) 
        : base(name, price, quantity)
    {
        if (expiryDate < DateTime.Now) throw new ArgumentException("Expiry date must be in the future");
        ExpiryDate = expiryDate;
    }

    public bool IsExpired => ExpiryDate < DateTime.Now;
}
