using ECommerceSystem.models;
using ECommerceSystem.services;

try
{
    var cheese = new ExpirableShippableProduct("Cheese", 100, 5, DateTime.Now.AddDays(30), 200);
    var biscuits = new ExpirableShippableProduct("Biscuits", 150, 3, DateTime.Now.AddDays(30), 700);
    var tv = new ShippableProduct("TV", 1000, 2, 5000);
    var scratchCard = new NonExpirableProduct("Mobile Scratch Card", 50, 10);

    var customer = new Customer("John Doe", 1000);
    var cart = new ShoppingCart();
    var shippingService = new ShippingService();
    var checkoutService = new CheckoutService(shippingService);

    cart.Add(cheese, 2);
    cart.Add(biscuits, 1);
    cart.Add(scratchCard, 1);

    checkoutService.Checkout(customer, cart);

    Console.WriteLine("\nCorner case tests:");
    Console.WriteLine("-----------------------");

    var emptyCart = new ShoppingCart();
    try
    {
        checkoutService.Checkout(customer, emptyCart);
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    var poorCustomer = new Customer("Poor Customer", 50);
    try
    {
        checkoutService.Checkout(poorCustomer, cart);
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    try
    {
        cart.Add(cheese, 10);
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    var expiredCheese = new ExpirableShippableProduct("Expired Cheese", 90, 5, DateTime.Now.AddSeconds(3), 300);
    var cartWithExpiredItem = new ShoppingCart();
    Thread.Sleep(5000); // Wait for the cheese to expire
    try
    {
        cartWithExpiredItem.Add(expiredCheese, 1);
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

