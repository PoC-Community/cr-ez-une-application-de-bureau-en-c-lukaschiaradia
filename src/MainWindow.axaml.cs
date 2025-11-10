using System;
using System.IO;
using System.Text.Json;
using Avalonia.Controls;
using Avalonia.Interactivity;
using TodoListApp.Models;
using TodoListApp.ViewModels;

namespace TodoListApp;

public partial class MainWindow : Window
{
    private MainWindowViewModel VM => (MainWindowViewModel)DataContext!;

    public MainWindow()
    {
        InitializeComponent();
        DataContext ??= new MainWindowViewModel();

        AddButton.Click += OnAdd;
        DeleteButton.Click += OnDelete;
        SaveButton.Click += OnSave;
    }

    private void OnAdd(object? sender, RoutedEventArgs e)
    {
        var title = TaskInput.Text?.Trim();
        if (!string.IsNullOrEmpty(title))
        {
            VM.Tasks.Add(new TaskItem { Title = title });
            TaskInput.Text = string.Empty;
        }
    }

    private void OnDelete(object? sender, RoutedEventArgs e)
    {
        if (TaskList.SelectedItem is TaskItem item)
            VM.Tasks.Remove(item);
    }

    private async void OnSave(object? sender, RoutedEventArgs e)
    {
        try
        {
            var dataDir = Path.Combine(AppContext.BaseDirectory, "data");
            Directory.CreateDirectory(dataDir);

            var filePath = Path.Combine(dataDir, "tasks.json");

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(VM.Tasks, options);
            await File.WriteAllTextAsync(filePath, json);

            StatusText.Text = $"Saved {VM.Tasks.Count} task(s) → {filePath}";
        }
        catch (Exception ex)
        {
            StatusText.Text = $"Save failed: {ex.Message}";
        }
    }
}
