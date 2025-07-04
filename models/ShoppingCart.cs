namespace ECommerceSystem.models;

public class ShoppingCart
{
    private Dictionary<Product, int> items = new Dictionary<Product, int>();

    public void Add(Product product, int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be positive");
        if (product is ExpirableProduct expirable && expirable.IsExpired)
            throw new InvalidOperationException($"Product ({product.Name}) is expired. try to purchase another product");
        if (quantity > product.Quantity)
            throw new InvalidOperationException($"Insufficient stock for ({product.Name}) available: {product.Quantity}, requested: {quantity}");

        if (items.ContainsKey(product))
            items[product] += quantity;
        else
            items[product] = quantity;

        product.ReduceQuantity(quantity);
    }

    public IReadOnlyDictionary<Product, int> Items => items;

    public bool IsEmpty => !items.Any();
}

