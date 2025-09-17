using Xunit;
using System.Collections.Generic;
using System;

public class OrderCalculatorTests
{
    private readonly OrderCalculator _calc = new();

    [Fact]
    public void Calculate_SimpleOrder_CalculatesCorrectly()
    {
        var items = new List<OrderItem> {
            new OrderItem("A1", 2, 10m),
            new OrderItem("B2", 1, 30m)
        };

        var result = _calc.Calculate(items, promoDiscountPercent: 0m);

        // итого = 2*10 + 1*30 = 50
        Assert.Equal(50m, result.Subtotal);
        // скидка за объем = 0 (ниж 100)
        Assert.Equal(0m, result.DiscountPercent);
        Assert.Equal(0m, result.DiscountAmount);
        // ндс 20% на 50 => 10
        Assert.Equal(10m, result.Vat);
        Assert.Equal(60m, result.Total);
    }

    [Fact]
    public void Calculate_WithVolumeDiscountAndPromo_RespectsMaxDiscount()
    {
        var items = new List<OrderItem> {
            new OrderItem("X", 10, 60m) // итого 600
        };

        // Скидка по объему = 10, промо = 25 => всего 35, но максиму 30
        var result = _calc.Calculate(items, promoDiscountPercent: 25m);
        Assert.Equal(600m, result.Subtotal);
        Assert.Equal(30m, result.DiscountPercent); // огранич
        Assert.Equal(600m * 0.30m, result.DiscountAmount);
    }

    [Theory]
    [InlineData(0, 10)]
    [InlineData(1, 0)]
    public void Calculate_InvalidItems_Throws(int quantity, decimal price)
    {
        var items = new List<OrderItem> { new OrderItem("X", quantity, price) };
        Assert.Throws<ArgumentException>(() => _calc.Calculate(items));
    }

    [Fact]
    public void Calculate_NullItems_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => _calc.Calculate(null));
    }
}
