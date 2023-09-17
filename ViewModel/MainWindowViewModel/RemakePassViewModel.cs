using Diplom.Client.Model;
using Diplom.Utilities;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Input;
using Zashita.DAL.Context;

namespace Diplom.Client.ViewModel.MainWindowViewModel
{
    public class RemakePassViewModel:ViewModelBase
    {
        private SecureString _password;
        private SecureString _repeatPassword;
        private SecureString _newPassword;
        private static ZashitaDB _db;
        private static UserData _user;
        private string _errorMessage;

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
        public SecureString RepeatPassword
        {
            get
            {
                return _repeatPassword;
            }
            set
            {
                _repeatPassword = value;
                OnPropertyChanged(nameof(RepeatPassword));
            }
        }
        public SecureString NewPassword
        {
            get
            {
                return _newPassword;
            }
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
            }
        }

        public ICommand RegisterCommand { get; }

        public RemakePassViewModel(UserData user, ZashitaDB db)   
        {
            _db = db;
            _user = user;
            RegisterCommand = new RelayCommand(ExecuteRegisterCommand, CanExecuteRegisterCommand);
        }

        private bool CanExecuteRegisterCommand(object arg)
        {
            
            if (Password == null) { return false; }
            var p = ConvertToUnsecureString(Password);
            
            if (p != _user.Password)
            {
                ErrorMessage = "Введите старый пароль";
                return false;
            }

            if (NewPassword == null) 
            {
                ErrorMessage = "Введите новый пароль";
                return false; 
            }
            var p1 = ConvertToUnsecureString(NewPassword);

            if (_user.Pass)
                foreach (char c in p1)
                {
                    var count = p1.Count(chr => chr == c);
                    if (count > 1)
                    {
                        ErrorMessage = "Символы в пароле не должны совпадать";
                        return false;
                    }
                }

            if (RepeatPassword == null)
            {
                ErrorMessage = "Пароли должны совпадать";
                return false;
            }
            else
            {
                var p2 = ConvertToUnsecureString(RepeatPassword);
                if (p1 != p2)
                {
                    ErrorMessage = "Пароли должны совпадать";
                    return false;
                }
            }
            ErrorMessage = "";
            return true;
        }

        private void ExecuteRegisterCommand(object obj)
        {
            _user.Password = ConvertToUnsecureString(NewPassword);
            Password.Clear();
            NewPassword.Clear();
            RepeatPassword.Clear();
            Methods.ChangePassword(_db, _user);
            MessageBox.Show("Пароль изменен");
            return;
        }

        public static string ConvertToUnsecureString(SecureString secureString)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        
    }
}
