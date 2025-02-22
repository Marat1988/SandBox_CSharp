using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    public class Order
    {
        public Guid Id { get; init; }
        public string CustomerName { get; init; } = string.Empty;
        public decimal Amount { get; init; }
        public DateTime OrderDate { get; init; }

        public override string ToString()
        {
            return $"Заказ #{Id} | Клиент: {CustomerName} | Сумма: {Amount:C} | Дата: {OrderDate}";
        }
    }
}
