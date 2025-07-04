using ECommerceSystem.interfaces;
using ECommerceSystem.models;
namespace ECommerceSystem.services;

public class CheckoutService
{
    private const double SHIPPING_RATE_PER_KG = 30;
    private readonly ShippingService shippingService;

    public CheckoutService(ShippingService shippingService)
    {
        this.shippingService = shippingService;
    }

    public void Checkout(Customer customer, ShoppingCart cart)
    {
        if (cart.IsEmpty) throw new InvalidOperationException("Cart is empty");

        double subtotal = 0;
        var shippableItems = new List<IShippable>();
        foreach (var item in cart.Items)
        {
            var product = item.Key;
            var quantity = item.Value;

            if (product is ExpirableProduct expirable && expirable.IsExpired)
                throw new InvalidOperationException($"Product {product.Name} is expired. try to purchase another product");
            if (quantity > product.Quantity)
                throw new InvalidOperationException($"Product {product.Name} is out of stock. try to purchase less than {product.Quantity}");

            subtotal += product.Price * quantity;
            if (product is IShippable shippable)
            {
                for (int i = 0; i < quantity; i++)
                    shippableItems.Add(shippable);
            }
        }

        double totalWeight = shippableItems.Sum(item => item.GetWeight());
        double shippingCost = totalWeight / 1000 * SHIPPING_RATE_PER_KG;
        double totalAmount = subtotal + shippingCost;

        if (totalAmount > customer.Balance)
            throw new InvalidOperationException($"Insufficient customer balance available: {customer.Balance}, required: {totalAmount}");

        shippingService.Ship(shippableItems);

        customer.DeductBalance(totalAmount);

        Console.WriteLine("** Checkout receipt **");
        foreach (var item in cart.Items)
        {
            Console.WriteLine($"{item.Value}x {item.Key.Name} {item.Key.Price * item.Value}");
        }
        Console.WriteLine("----------------------");
        Console.WriteLine($"Subtotal {subtotal}");
        Console.WriteLine($"Shipping {shippingCost}");
        Console.WriteLine($"Amount {totalAmount}");
        Console.WriteLine($"Customer balance after payment: {customer.Balance}");
    }
}

