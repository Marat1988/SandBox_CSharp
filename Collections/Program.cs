using Collections;
using System.Threading.Channels;
#region Array
/*double[] temperatures = [10];
var arr = (IList<double>)temperatures;
Random random = new();
for (int i = 0; i < temperatures.Length; i++)
{
    temperatures[i] = random.NextDouble() * 40;
}

double average = temperatures.Average();
double max = temperatures.Max();
double min = temperatures.Min();

Console.WriteLine("Температуры датчиков: " + string.Join(", ", temperatures.Select(t => t.ToString("F2"))));
Console.WriteLine($"Средняя температура: {average:F2}");
Console.WriteLine($"Максимальная температура: {max:F2}");
Console.WriteLine($"Минимальная температура: {min:F2}");*/


/*double[,] temperatures = new double[5, 5];
Random random = new();

for (int i = 0; i<temperatures.GetLength(0); i++)
{
    for (int j = 0; j < temperatures.GetLength(1);  j++)
    {
        temperatures[i, j] = random.NextDouble() * 40;
    }
}
Console.WriteLine("Температуры датчиков:");
for (int i = 0; i < temperatures.GetLength(0); i++)
{
    for (int j = 0; j < temperatures.GetLength(1); j++)
    {
        Console.Write($"{temperatures[i, j]:F2}");
    }
    Console.WriteLine();
}

double CalculateAverage(double[,] array)
{
    double sum = 0;
    int count = 0;
    for (int i = 0; i < array.GetLength(0); i++)
    {
        for (int j = 0; j < array.GetLength(1); j++)
        {
            sum += array[i, j];
            count++;
        }
    }
    return sum / count;
}*/
#endregion
#region List
/*List<Order> orders = [];

while (true)
{
    Console.WriteLine("\n1. Добавить заказ");
    Console.WriteLine("2. Показать заказы");
    Console.WriteLine("3. Найти заказ по ID");
    Console.WriteLine("4. Удалить заказ");
    Console.WriteLine("5. Выйти");
    Console.WriteLine("Выберите действие: ");
    string? choose = Console.ReadLine();
    switch (choose)
    {
        case "1": AddOrder();
            break;
        case "2": ShowOrders();
            break;
        case "3": FindOrder();
            break;
        case "4": RemoveOrder();
            break;
        case "5":
            return;
        default:
            Console.WriteLine("Неверный выбор, попробуйте снова.");
            break;
    }
}

void AddOrder()
{
    Console.Write("Введите имя клиента: ");
    string? name = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine("Некорректное имя.");
        return;
    }
    Console.WriteLine("Введите сумму заказа: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
    {
        Console.WriteLine("Некорректная сумма");
        return;
    }

    var order = new Order
    {
        Id = Guid.NewGuid(),
        CustomerName = name,
        Amount = amount,
        OrderDate = DateTime.Now,
    };
    orders.Add(order);
    Console.WriteLine("Заказ добавлен");
}

void ShowOrders()
{
    if (orders.Count == 0)
    {
        Console.WriteLine("Нет заказов.");
        return;
    }
    Console.WriteLine("\n Список заказов:");
    foreach (var order in orders.OrderBy(o=>o.OrderDate))
    {
        Console.WriteLine(order);
    }
}

void FindOrder()
{
    Console.Write("Введите ID заказа: ");
    if (!Guid.TryParse(Console.ReadLine(), out Guid id))
    {
        Console.WriteLine("Некорректный ID.");
        return;
    }
    Order? order = orders.FirstOrDefault(o => o.Id == id);
    if (order != null)
    {
        Console.WriteLine("Найден заказ: " + order);
    }
    else
    {
        Console.WriteLine("Заказ не найден.");
    }
}


void RemoveOrder()
{
    Console.Write("Введите ID заказа для удаления: ");
    if (!Guid.TryParse(Console.ReadLine(), out Guid id))
    {
        Console.WriteLine("Некорректный ID.");
        return;
    }
    int removeCount = orders.RemoveAll(o => o.Id == id);
    Console.WriteLine(removeCount > 0 ? "Заказ удален." : "Заказ не найден");
}

IEnumerable<Order> GetOrders()
{
    string[] lines = File.ReadAllLines("orders.csv");
    foreach (string line in lines)
    {
        string[] splitLine = line.Split(',');
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerName = splitLine[0],
            Amount = decimal.Parse(splitLine[1])
        };
        yield return order;
    }
    yield return new Order();
    yield return new Order();
    yield return new Order();
}*/
#endregion
#region LinkedList
/*LinkedList<Order> orders = new LinkedList<Order>();

while (true)
{
    Console.WriteLine("\n1. Добавить заказ");
    Console.WriteLine("2. Показать заказы");
    Console.WriteLine("3. Найти заказ по ID");
    Console.WriteLine("4. Удалить заказ");
    Console.WriteLine("5. Выйти");
    Console.WriteLine("Выберите действие: ");
    string? choose = Console.ReadLine();
    switch (choose)
    {
        case "1":
            AddOrder();
            break;
        case "2":
            ShowOrders();
            break;
        case "3":
            FindOrder();
            break;
        case "4":
            RemoveOrder();
            break;
        case "5":
            return;
        default:
            Console.WriteLine("Неверный выбор, попробуйте снова.");
            break;
    }
}

void AddOrder()
{
    Console.Write("Введите имя клиента: ");
    string? name = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine("Некорректное имя.");
        return;
    }
    Console.WriteLine("Введите сумму заказа: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
    {
        Console.WriteLine("Некорректная сумма");
        return;
    }

    var order = new Order
    {
        Id = Guid.NewGuid(),
        CustomerName = name,
        Amount = amount,
        OrderDate = DateTime.Now,
    };
    orders.AddLast(order);
    Console.WriteLine("Заказ добавлен");
}

void ShowOrders()
{
    if (orders.Count == 0)
    {
        Console.WriteLine("Нет заказов.");
        return;
    }
    Console.WriteLine("\n Список заказов:");
    foreach (var order in orders.OrderBy(o => o.OrderDate))
    {
        Console.WriteLine(order);
    }
}

void FindOrder()
{
    Console.Write("Введите ID заказа: ");
    if (!Guid.TryParse(Console.ReadLine(), out Guid id))
    {
        Console.WriteLine("Некорректный ID.");
        return;
    }
    Order? order = orders.FirstOrDefault(o => o.Id == id);
    if (order != null)
    {
        Console.WriteLine("Найден заказ: " + order);
    }
    else
    {
        Console.WriteLine("Заказ не найден.");
    }
}


void RemoveOrder()
{
    Console.Write("Введите ID заказа для удаления: ");
    if (!Guid.TryParse(Console.ReadLine(), out Guid id))
    {
        Console.WriteLine("Некорректный ID.");
        return;
    }
    var node = orders.First;
    while(node != null)
    {
        if (node.Value.Id == id)
        {
            orders.Remove(node);
            Console.WriteLine("Заказ удален");
            return;
        }
        node = node.Next;
    }

    Console.WriteLine("Заказ не найден");
}

IEnumerable<Order> GetOrders()
{
    string[] lines = File.ReadAllLines("orders.csv");
    foreach (string line in lines)
    {
        string[] splitLine = line.Split(',');
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerName = splitLine[0],
            Amount = decimal.Parse(splitLine[1])
        };
        yield return order;
    }
    yield return new Order();
    yield return new Order();
    yield return new Order();
}*/
#endregion
#region Dictionary
/*var dictionary = new Dictionary<int, string>
{
    { 1, "value1" },
    { 2, "value2" },
    { 3, "value3" }
};
var value = dictionary.TryGetValue(2, out string? str);

var orderCache = new OrderCache();

var order1 = new Order
{
    Id = Guid.NewGuid(),
    CustomerName = "Иван Иванов",
    Amount = 1200.500m,
    OrderDate = DateTime.Now,
};
var order2 = new Order
{
    Id = Guid.NewGuid(),
    CustomerName = "Мария Смирнова",
    Amount = 550.75m,
    OrderDate = DateTime.Now,
};

orderCache.AddOrder(order1);
orderCache.AddOrder(order2);
orderCache.ShowAllOrders();

var retrievedOrder = orderCache.GetOrder(order1.Id);
Console.WriteLine(retrievedOrder);

bool removed = orderCache.RemoveOrder(order1.Id);*/
#endregion

#region Queue
Queue<string> queue = new();
queue.Enqueue("Первый элемент");
queue.Enqueue("Второй элемент");
queue.Enqueue("Третий элемент");

Console.WriteLine("Элементы в очереди");
foreach (var item in queue)
{
    Console.WriteLine(item);
}

Console.WriteLine("\nИзвлекаем первй элемент из очереди....");
string dequeuedElement = queue.Dequeue();
Console.WriteLine($"Извлеченный элемент: {dequeuedElement}");

Console.WriteLine("\nЭлементы в очереди после извлечения");
foreach (var item in queue)
{
    Console.WriteLine(item);
}

string peekElement = queue.Peek();
Console.WriteLine($"\nЭлементы в начале очереди (не удален): { peekElement}");

Console.WriteLine($"\nОчередь пуста? {queue.Count == 0}");

queue.Clear();
Console.WriteLine("\nОчередь очищена.");
Console.WriteLine($"\nОчередь пуста? {queue.Count == 0}");

#endregion