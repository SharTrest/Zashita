using Diplom.Client.Model;
using Diplom.Utilities;
using Diplom.View.Windows;
using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Input;

namespace Diplom.Client.ViewModel.LoginWindowViewModel
{
    public class LogViewModel : ViewModelBase
    {
        private static UserList _db;
        private static UserData _data;
        private LoginWindow _window;
        private int _countAttempts;
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;
        private object _currentView;

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public SecureString Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public int CountAttempts
        {
            get => _countAttempts;
            set { _countAttempts = value; }
        }
        public bool IsViewVisible
        {
            get { return _isViewVisible; }
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }
        public object CurrentView
        {
            get
            {
                return _currentView;
            }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        //команды
        public ICommand LoginCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand ShowRemakePasswordViewCommand { get; }

        //
        public LogViewModel(UserData user, LoginWindow wind, UserList dB)
        {
            CountAttempts = 0;
            LoginCommand = new RelayCommand(ExucuteLoginCommand, CanExecuteLoginCommand);
            ShowRemakePasswordViewCommand = new RelayCommand(ExucuteShowRemakePasswordViewCommand, CanExecuteShowRemakePasswordViewCommand);
            _data = user;
            _window = wind;
            _db = dB;
        }


        private bool CanExecuteShowRemakePasswordViewCommand(object arg)
        {
            return true;
        }

        private void ExucuteShowRemakePasswordViewCommand(object obj)
        {

        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username))
                validData = false;
            else
                validData = true;
            return validData;
        }

        private async void ExucuteLoginCommand(object obj)
        {
            _data.UserName = Username;
            _data.Password = ConvertToUnsecureString(Password);
            if (CountAttempts == 3) _window.Close();
            var isValid = Methods.IsValidUser(_db, _data);
            if (isValid)
            {
                _window.Hide();
                ErrorMessage = null;
                IsViewVisible = false;
                _data.Authed = true;
            }
            else
            {
                CountAttempts += 1;
                ErrorMessage = "Неверный логин или пароль";
            }
            if (_data.Status == "B")
                MessageBox.Show("Вы забанены. УВЫ!");
        }
        public static string ConvertToUnsecureString(SecureString secureString)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            catch
            {
                return "";
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}