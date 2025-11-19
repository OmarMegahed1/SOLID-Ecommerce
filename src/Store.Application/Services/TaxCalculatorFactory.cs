namespace Store.Application.Services;

public class TaxCalculatorFactory : ITaxCalculatorFactory
{
    public ITaxCalculator GetCalculatorInstance(string countryCode) => countryCode switch
    {
        "GBR" => new UkTaxCalculator(),
        "AUS" => new AustraliaTaxCalculator(),
        _ => new NoTaxCalculator()
    };
}