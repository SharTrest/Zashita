﻿using Diplom.Client.Model;
using Diplom.Utilities;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Diplom.Client.ViewModel.MainWindowViewModel
{
    public class RegViewModel : ViewModelBase
    {
        private string _username;
        private SecureString _password;
        private SecureString _repeatPassword;
        private string _errorMessage;
        private static UserList _db;
        private static UserData _data;

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
        public ICommand RegisterCommand { get; }


        public RegViewModel(UserData user, UserList db)
        {
            _data = user;
            _db = db;
            RegisterCommand = new RelayCommand(ExecuteRegCommand, CanExecuteRegCommand);
        }

        private bool CanExecuteRegCommand(object arg)
        {
            if (Password == null)
            {
                ErrorMessage = "Введите пароль";
                return false;
            }
            var p1 = ConvertToUnsecureString(Password);

            if (_data.Pass)
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
            _data.Password = ConvertToUnsecureString(Password);
            return true;
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

        private async void ExecuteRegCommand(object obj)
        {
            var isRegistered = await RegisterNewUser(Username, ConvertToUnsecureString(Password));
            if (isRegistered) 
            {
                MessageBox.Show("УСПЕХ!");
            }
        }



        public static async Task<bool> RegisterNewUser(string username, string password)
        {


            var isRegistered = Methods.ChangePassword(_db, _data);

            return isRegistered;

        }
    }
}