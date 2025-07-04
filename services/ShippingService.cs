using ECommerceSystem.interfaces;
using ECommerceSystem.models;
namespace ECommerceSystem.services;

public class ShippingService
{
    public void Ship(IEnumerable<IShippable> items)
    {
        if (!items.Any()) return;

        Console.WriteLine("** Shipment notice **");
        double totalWeight = 0;
        foreach (var item in items)
        {
            Console.WriteLine($"1x {item.GetName()} {item.GetWeight()}g");
            totalWeight += item.GetWeight();
        }
        Console.WriteLine($"Total package weight {totalWeight / 1000}kg");
    }
}

