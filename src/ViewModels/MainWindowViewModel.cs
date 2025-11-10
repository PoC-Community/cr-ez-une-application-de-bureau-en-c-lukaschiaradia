using System.Collections.ObjectModel;
using TodoListApp.Models;

namespace TodoListApp.ViewModels
{
    public class MainWindowViewModel
    {
        public ObservableCollection<TaskItem> Tasks { get; } = new()
        {
            new TaskItem { Title = "Découvrir Avalonia" },
            new TaskItem { Title = "Créer la liste", IsCompleted = true },
            new TaskItem { Title = "Relier le CheckBox" }
        };
    }
}
