using Store.Common.Helpers;

namespace Store.Application.Services;

public class TaxCalculatorFactory : ITaxCalculatorFactory
{
    private readonly IEnumerable<ITaxCalculator> _taxCalculators;

    public TaxCalculatorFactory(IEnumerable<ITaxCalculator> taxCalculators)
    {
        _taxCalculators = taxCalculators.NotNull();
    }

    public ITaxCalculator GetCalculatorInstance(string countryCode)
    {
        var taxCalculator = _taxCalculators.FirstOrDefault(x => x.CanHandle(countryCode));
        if (taxCalculator == null)
            return new NoTaxCalculator();

        return taxCalculator;
    }
}