using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace TodoList.ViewModel
{

    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection< TaskItem> Tasks { get; set; } = new();

        private TaskItem _selectedTask;
        public TaskItem SelectedTask
        {
            get => _selectedTask;
            set { _selectedTask = value; OnPropertyChanged(nameof(SelectedTask)); }
        }

        private string _newTaskText;
        public string NewTaskText
        {
            get => _newTaskText;
            set { _newTaskText = value; OnPropertyChanged(nameof(NewTaskText)); }
        }

        private DateTime? _newDeadline;
        public DateTime? NewDeadline
        {
            get => _newDeadline;
            set { _newDeadline = value; OnPropertyChanged(nameof(NewDeadline)); }
        }

        public RelayCommand AddCommand { get; }
        public RelayCommand RemoveCommand { get; }

        public MainViewModel()
        {
            AddCommand = new RelayCommand(AddTask, () => !string.IsNullOrWhiteSpace(NewTaskText));
            RemoveCommand = new RelayCommand(RemoveTask, () => SelectedTask != null);
        }

        private void AddTask()
        {
            Tasks.Add(new TaskItem
            {
                Text = NewTaskText,
                Deadline = NewDeadline
            });

            NewTaskText = "";
            NewDeadline = null;
            OnPropertyChanged(nameof(NewTaskText));
            OnPropertyChanged(nameof(NewDeadline));
        }

        private void RemoveTask()
        {
            Tasks.Remove(SelectedTask);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
