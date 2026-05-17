using System.Globalization;
using System.Windows;
using Core;

namespace WpfEventsUI;

public partial class EventEditDialog : Window
{
    public EventBase? ResultEvent { get; private set; }

    public EventEditDialog()
    {
        InitializeComponent();
        ComboType.ItemsSource = new[] { "Концерт", "Конференція" };
        ComboType.SelectedIndex = 0;
        PickerDate.SelectedDate = DateTime.Today;
        TextBasePrice.Text = "0";
        TextCoffeeBreaks.Text = "0";
        ComboType.SelectionChanged += (_, _) => UpdateFieldsForType();
        UpdateFieldsForType();
    }

    public EventEditDialog(EventBase existing) : this()
    {
        TextTitle.Text = existing.Title;
        PickerDate.SelectedDate = existing.Date;
        TextBasePrice.Text = existing.BasePrice.ToString(CultureInfo.CurrentCulture);
        if (existing is ConcertEvent c)
        {
            ComboType.SelectedItem = "Концерт";
            CheckVip.IsChecked = c.IsVipZoneAvailable;
        }
        else if (existing is ConferenceEvent cf)
        {
            ComboType.SelectedItem = "Конференція";
            TextCoffeeBreaks.Text = cf.CoffeeBreaksCount.ToString(CultureInfo.InvariantCulture);
        }
        UpdateFieldsForType();
    }

    private void UpdateFieldsForType()
    {
        var concert = Equals(ComboType.SelectedItem as string, "Концерт");
        CheckVip.Visibility = concert ? Visibility.Visible : Visibility.Collapsed;
        LabelCoffee.Visibility = concert ? Visibility.Collapsed : Visibility.Visible;
        TextCoffeeBreaks.Visibility = concert ? Visibility.Collapsed : Visibility.Visible;
    }

    private void BtnOk_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TextTitle.Text))
        {
            MessageBox.Show("Введіть назву події.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var priceText = TextBasePrice.Text.Trim().Replace(',', '.');
        if (!double.TryParse(priceText, NumberStyles.Any, CultureInfo.InvariantCulture, out var basePrice) &&
            !double.TryParse(TextBasePrice.Text.Trim(), NumberStyles.Any, CultureInfo.CurrentCulture, out basePrice))
        {
            MessageBox.Show("Некоректна базова ціна.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var title = TextTitle.Text.Trim();
        var date = PickerDate.SelectedDate ?? DateTime.Today;

        if (Equals(ComboType.SelectedItem as string, "Концерт"))
        {
            ResultEvent = new ConcertEvent(title, date, basePrice, CheckVip.IsChecked == true);
        }
        else
        {
            if (!int.TryParse(TextCoffeeBreaks.Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var coffee) &&
                !int.TryParse(TextCoffeeBreaks.Text.Trim(), out coffee))
            {
                MessageBox.Show("Некоректна кількість кава-брейків.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (coffee < 0)
            {
                MessageBox.Show("Кількість кава-брейків не може бути від'ємною.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            ResultEvent = new ConferenceEvent(title, date, basePrice, coffee);
        }

        DialogResult = true;
        Close();
    }
}
