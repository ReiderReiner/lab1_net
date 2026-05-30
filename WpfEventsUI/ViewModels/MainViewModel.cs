using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using Core;
using WpfEventsUI.ViewModels;

namespace WpfEventsUI.ViewModels;

public class MainViewModel : BaseViewModel
{
    private EventBase? _selectedEvent;
    private const string StorageFileName = "events.json";

    public ObservableCollection<EventBase> Events { get; } = new();

    public EventBase? SelectedEvent
    {
        get => _selectedEvent;
        set
        {
            if (_selectedEvent != value)
            {
                _selectedEvent = value;
                OnPropertyChanged();
                UpdateCommandStates();
            }
        }
    }

    public RelayCommand AddCommand { get; }
    public RelayCommand EditCommand { get; }
    public RelayCommand DeleteCommand { get; }
    public RelayCommand SaveCommand { get; }
    public RelayCommand LoadCommand { get; }

    private string _statusMessage = string.Empty;
    public string StatusMessage
    {
        get => _statusMessage;
        set
        {
            if (_statusMessage != value)
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }
    }

    public MainViewModel()
    {
        AddCommand = new RelayCommand(_ => AddEvent());
        EditCommand = new RelayCommand(_ => EditEvent(), _ => SelectedEvent != null);
        DeleteCommand = new RelayCommand(_ => DeleteEvent(), _ => SelectedEvent != null);
        SaveCommand = new RelayCommand(_ => SaveEvents(), _ => Events.Count > 0);
        LoadCommand = new RelayCommand(_ => LoadEvents());

        LoadEvents();
    }

    private static string StoragePath => Path.Combine(AppContext.BaseDirectory, StorageFileName);

    public void LoadEvents()
    {
        Events.Clear();

        if (!File.Exists(StoragePath))
        {
            SeedDemoData();
            SaveEvents();
            StatusMessage = "Демонстраційні дані завантажено.";
            return;
        }

        var loaded = EventStorage.Load(StoragePath);
        foreach (var ev in loaded)
        {
            Events.Add(ev);
        }

        StatusMessage = $"Завантажено {Events.Count} подій з файлу.";
        UpdateCommandStates();
    }

    public void SaveEvents()
    {
        EventStorage.Save(StoragePath, Events);
        StatusMessage = $"Збережено {Events.Count} подій у файл.";
        UpdateCommandStates();
    }

    private void SeedDemoData()
    {
        Events.Add(new ConcertEvent("Rock Festival", new DateTime(2026, 8, 15), 800, true));
        Events.Add(new ConcertEvent("Jazz Night", new DateTime(2026, 9, 20), 500, false));
        Events.Add(new ConferenceEvent("Tech Summit", new DateTime(2026, 10, 5), 1200, 3));
        Events.Add(new ConferenceEvent("Business Forum", new DateTime(2026, 11, 12), 900, 2));
    }

    private void AddEvent()
    {
        var dialog = new EventEditDialog();
        if (dialog.ShowDialog() == true && dialog.ResultEvent is { } created)
        {
            Events.Add(created);
            SaveEvents();
            StatusMessage = $"Додано подію: {created.Title}.";
            UpdateCommandStates();
        }
    }

    private void EditEvent()
    {
        if (SelectedEvent is not { } selected)
        {
            MessageBox.Show("Виберіть подію для редагування.", "Редагування", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var dialog = new EventEditDialog(selected) { Owner = Application.Current.MainWindow };
        if (dialog.ShowDialog() == true && dialog.ResultEvent is { } updated)
        {
            var index = Events.IndexOf(selected);
            if (index >= 0)
            {
                Events[index] = updated;
                SelectedEvent = updated;
                SaveEvents();
                StatusMessage = $"Подію «{updated.Title}» успішно оновлено.";
                UpdateCommandStates();
            }
        }
    }

    private void DeleteEvent()
    {
        if (SelectedEvent is not { } selected)
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
            SelectedEvent = null;
            SaveEvents();
            StatusMessage = $"Подію «{selected.Title}» видалено.";
            UpdateCommandStates();
        }
    }

    private void UpdateCommandStates()
    {
        EditCommand.RaiseCanExecuteChanged();
        DeleteCommand.RaiseCanExecuteChanged();
        SaveCommand.RaiseCanExecuteChanged();
    }
}
