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
        {
            VM.Tasks.Remove(item);
        }
    }
}
