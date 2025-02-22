

using Collections;

public class OrderCache
{
    private readonly Dictionary<Guid, Order> _cache = new();

    public void AddOrder(Order order)
    {
        _cache.Add(order.Id, order);
        Console.WriteLine($"Заказ #{order.Id} добавлен в кэш");
    }

    public Order? GetOrder(Guid id)
    {
        if (_cache.TryGetValue(id, out var order))
        {
            Console.WriteLine($"Заказ #{id} найден в кэше");
            return order;
        }
        Console.WriteLine($"Заказ #{id} не найден в кэше.");
        return null;
    }

    public bool RemoveOrder(Guid id)
    {
        return _cache.Remove(id);
    }

    public void ShowAllOrders()
    {
        if(_cache.Count == 0)
        {
            Console.WriteLine("Кэш пуст");
            return;
        }
        Console.WriteLine("\nВсе заказы в кэше");
        foreach (var item in _cache.Values)
        {
            Console.WriteLine(item);
        }
    }

}