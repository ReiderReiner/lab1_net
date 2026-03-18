namespace Core;

// Структура для дослідження Value Types (Завдання 2)
public struct TicketPrice
{
    public double Amount;
    public string Currency;

    public TicketPrice(double amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }
}