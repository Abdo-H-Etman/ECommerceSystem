namespace ECommerceSystem.models;

public class Customer
{
    public string Name { get; private set; }
    public double Balance { get; private set; }

    public Customer(string name, double balance)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty");
        if (balance < 0) throw new ArgumentException("Balance cannot be negative");
        Name = name;
        Balance = balance;
    }

    public void DeductBalance(double amount)
    {
        if (amount > Balance) throw new InvalidOperationException("Insufficient balance");
        Balance -= amount;
    }
}
