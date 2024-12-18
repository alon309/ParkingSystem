using ParkingSystemApp.AuthenticationService.Models;
using ParkingSystemApp.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ParkingSystemApp.Authentication
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IAuthService _authService;

        private string _username;
        private string _password;
        private bool _canLogin;

        public event PropertyChangedEventHandler PropertyChanged;

        // Username property
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
                UpdateCanLogin();
            }
        }

        // Password property
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                UpdateCanLogin();
            }
        }

        // CanLogin property (bindable)
        public bool CanLogin
        {
            get => _canLogin;
            private set
            {
                if (_canLogin != value)
                {
                    _canLogin = value;
                    OnPropertyChanged();
                }
            }
        }

        // Command for the Sign In button
        public ICommand SignInCommand { get; }

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService)); // Ensure it's not null

            // Initialize the Sign In command
            SignInCommand = new RelayCommand(ExecuteSignIn, () => CanLogin);
        }

        // Updates the CanLogin property based on Username and Password
        private void UpdateCanLogin()
        {
            CanLogin = !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
            (SignInCommand as RelayCommand)?.RaiseCanExecuteChanged(); // Notify that CanExecute has changed
        }

        // Sign In logic
        private void ExecuteSignIn()
        {
            bool result = _authService.Authenticate(Username, Password);
            string res = result ? "successfull" : "Failed";

            MessageBox.Show(res);
        }

        // Notify the View of property changes
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
