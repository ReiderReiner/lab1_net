using System.Collections.ObjectModel;
using System.Windows;
using Core;

namespace WpfEventsUI;

public partial class MainWindow : Window
{
    public ObservableCollection<EventBase> Events { get; } = new();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
        SeedDemoData();
    }

    private void SeedDemoData()
    {
        Events.Add(new ConcertEvent("Rock Festival", new DateTime(2026, 8, 15), 800, true));
        Events.Add(new ConcertEvent("Jazz Night", new DateTime(2026, 9, 20), 500, false));
        Events.Add(new ConferenceEvent("Tech Summit", new DateTime(2026, 10, 5), 1200, 3));
        Events.Add(new ConferenceEvent("Business Forum", new DateTime(2026, 11, 12), 900, 2));
    }

    private void BtnAdd_Click(object sender, RoutedEventArgs e)
    {
        var dlg = new EventEditDialog { Owner = this };
        if (dlg.ShowDialog() == true && dlg.ResultEvent is { } created)
        {
            Events.Add(created);
        }
    }

    private void BtnEdit_Click(object sender, RoutedEventArgs e)
    {
        if (EventsGrid.SelectedItem is not EventBase selected)
        {
            MessageBox.Show("Виберіть подію для редагування.", "Редагування", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var dlg = new EventEditDialog(selected) { Owner = this };
        if (dlg.ShowDialog() == true && dlg.ResultEvent is { } updated)
        {
            var index = Events.IndexOf(selected);
            if (index >= 0)
            {
                Events[index] = updated;
            }
        }
    }

    private void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (EventsGrid.SelectedItem is not EventBase selected)
        {
            MessageBox.Show("Виберіть подію для видалення.", "Видалення", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var confirm = MessageBox.Show(
            $"Видалити подію «{selected.Title}»?",
            "Підтвердження видалення",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (confirm == MessageBoxResult.Yes)
        {
            Events.Remove(selected);
        }
    }
}
