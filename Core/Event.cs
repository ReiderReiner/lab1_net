namespace Core;

public class Event
{
    public required string Title { get; set; }        // Назва події (string)
    public DateTime Date { get; set; }       // Дата проведення (DateTime)
    public double TicketPrice { get; set; }  // Вартість квитка (double)

    public override string ToString()
    {
        return $"Подія: {Title}, Дата: {Date:dd.MM.yyyy}, Ціна: {TicketPrice} грн";
    }
}