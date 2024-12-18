using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ParkingSystemApp.Common
{
    class RelayCommand : ICommand
    {
        private readonly Action _execute;     // The action that should be executed
        private readonly Func<bool> _canExecute;  // The condition that decides if the command can execute

        // Constructor to accept execute action and canExecute condition
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute)); // Ensure execute action is not null
            _canExecute = canExecute;
        }

        // This method checks if the command can be executed
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(); // If no condition, command can always execute
        }

        // This method performs the action when the command is executed
        public void Execute(object parameter)
        {
            _execute();  // Call the provided action
        }

        // Event to notify changes in CanExecute state
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;  // Hook into the event to update CanExecute
            remove => CommandManager.RequerySuggested -= value;
        }

        // Method to raise CanExecuteChanged, to force the View to recheck if it can execute
        public void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();
    }

}
