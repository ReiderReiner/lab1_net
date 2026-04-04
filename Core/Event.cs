namespace Core;

public class Event
{
    public string EventCode { get; set; }    // Код події (для словника)
    public string Title { get; set; }        // Назва події (string)
    public DateTime Date { get; set; }       // Дата проведення (DateTime)
    public double TicketPrice { get; set; }  // Вартість квитка (double)

    public override string ToString()
    {
        return $"[{EventCode}] {Title} ({Date.ToShortDateString()})";
    }
}