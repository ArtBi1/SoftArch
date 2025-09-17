// m 1) Пример бизнес логики
public record OrderItem(string Sku, int Quantity, decimal UnitPrice); 
// Запись (record) описывает один товар в заказе: артикул (Sku), количество (quantity) и цену за единицу (unitprice).

// Services/OrderCalculator.cs
public class OrderCalculator
{
    private const decimal MaxDiscountPercent = 30m; 
    // Максимальный процент скидки, который можно применить (30%).

    private const decimal VatPercent = 20m; 
    // НДС (20%).

    // promoDiscountPercent: 0..100
    public OrderResult Calculate(IEnumerable<OrderItem> items, decimal promoDiscountPercent = 0m)
    {
        if (items == null) throw new ArgumentNullException(nameof(items)); 
        // Проверка: список товаров не должен быть null.

        var itemList = items.ToList();
        if (!itemList.Any()) throw new ArgumentException("Order must contain at least one item.", nameof(items));
        // Проверка: заказ не должен быть пустым.

        foreach (var it in itemList)
        {
            if (it.Quantity <= 0) throw new ArgumentException("Quantity must be > 0", nameof(items));
            // Количество должно быть больше 0.

            if (it.UnitPrice <= 0) throw new ArgumentException("UnitPrice must be > 0", nameof(items));
            // Цена за единицу должна быть больше 0.
        }

        var subtotal = itemList.Sum(i => i.Quantity * i.UnitPrice);
        // Подсчёт промежуточной суммы (стоимость всех товаров без скидок и налогов).

        
        // Скидка за объём: 5%, если сумма >= 100, или 10%, если >= 500.
        decimal volumeDiscount = 0m;
        if (subtotal >= 500) volumeDiscount = 10m;
        else if (subtotal >= 100) volumeDiscount = 5m;

        var discountPercent = Math.Min(MaxDiscountPercent, volumeDiscount + promoDiscountPercent);
        // Итоговый процент скидки = скидка за объём + промо-скидка,
        // но не больше максимума (30%).

        var discountAmount = subtotal * discountPercent / 100m;
        // Сумма скидки в деньгах.

        var taxedBase = subtotal - discountAmount;
        // Сумма, на которую начисляется НДС (после скидки).

        var vatAmount = taxedBase * VatPercent / 100m;
        // Сумма НДС.

        var total = taxedBase + vatAmount;
        // Итоговая сумма заказа (после скидок и с НДС).

        return new OrderResult(subtotal, discountPercent, discountAmount, vatAmount, total);
        // Возврат результата с деталями: промежуточная сумма, скидка (% и сумма),
        // НДС и итоговая сумма.
    }
}

// Services/OrderResult.cs
public record OrderResult(decimal Subtotal, decimal DiscountPercent, decimal DiscountAmount, decimal Vat, decimal Total);
// Запись (record), описывающая результат расчёта заказа: 
// Subtotal — промежуточная сумма, DiscountPercent — скидка в %, 
// DiscountAmount — сумма скидки, Vat — налог, Total — итоговая сумма.
