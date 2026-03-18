namespace Core;

public class Participant
{
    public string FullName { get; set; }     // ПІБ (string)
    public int Age { get; set; }             // Вік (int)
    public bool HasVipStatus { get; set; }   // Чи є VIP (bool)

    public override string ToString()
    {
        return $"Учасник: {FullName}, Вік: {Age}, VIP: {(HasVipStatus ? "Так" : "Ні")}";
    }
}