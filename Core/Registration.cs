namespace Core;

public class Registration
{
    public int RegistrationId { get; set; }  // Номер реєстрації (int)
    public DateTime RegisteredAt { get; set; } // Час реєстрації (DateTime)
    public bool IsConfirmed { get; set; }    // Чи підтверджено (bool)
}