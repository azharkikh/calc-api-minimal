namespace CalcApiMinimal.Currency;

public class UnknownCurrencyException : Exception
{
    public UnknownCurrencyException(string from, string to)
        : base($"Неизвестная валюта: {from} или {to}.")
    {
    }
}
