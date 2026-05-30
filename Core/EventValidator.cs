namespace Core;

public static class EventValidator
{
    public static bool IsValid(EventBase? eventBase, out string errorMessage)
    {
        if (eventBase is null)
        {
            errorMessage = "Подія не має значення.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(eventBase.Title))
        {
            errorMessage = "Назва події не може бути порожньою.";
            return false;
        }

        if (eventBase.BasePrice < 0)
        {
            errorMessage = "Базова ціна має бути невід'ємною.";
            return false;
        }

        if (eventBase.Date < DateTime.Today.AddYears(-1))
        {
            errorMessage = "Дата події має бути не раніше ніж за минулий рік.";
            return false;
        }

        if (eventBase is ConferenceEvent conference && conference.CoffeeBreaksCount < 0)
        {
            errorMessage = "Кількість кава-брейків не може бути від'ємною.";
            return false;
        }

        errorMessage = string.Empty;
        return true;
    }
}
