using Store.Application.Models;

namespace Store.Application.Services;
public class TaxCalculator : ITaxCalculator
{
    public decimal CalculateTax(Order order, User user)
    {
        var orderTotal = order.Items.Sum(x => x.Quantity * x.Product.Price);
        var totalToTax = orderTotal + order.DeliveryCost;

        var taxRate = user.CountryCode switch
        {
            "GBR" => 0.20m,
            "AUS" => 0.10m,
            _ => 0
        };

        return totalToTax * taxRate;
    }
}
