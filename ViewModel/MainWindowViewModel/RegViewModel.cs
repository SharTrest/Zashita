﻿using Diplom.Client.Model;
using Diplom.Utilities;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Diplom.Client.ViewModel.MainWindowViewModel
{
    public class RegViewModel : ViewModelBase
    {
        private string _username;
        private SecureString _password;
        private SecureString _repeatPassword;
        private string _errorMessage;

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


        public RegViewModel()
        {
            RegisterCommand = new RelayCommand(ExecuteRegCommand, CanExecuteRegCommand);
        }

        private bool CanExecuteRegCommand(object arg)
        {
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3)
            {
                ErrorMessage = "Длина логина должна превышать 3 символа";
                return false;
            }
            if (Password == null || Password.Length < 5)
            {
                ErrorMessage = "Длина пароля должна превышать 5 символов";
                return false;
            }

            if (RepeatPassword == null)
            {
                ErrorMessage = "Пароли должны совпадать";
                return false;
            }
            else
            {
                var p1 = ConvertToUnsecureString(Password);
                var p2 = ConvertToUnsecureString(RepeatPassword);
                if (p1 != p2)
                {
                    ErrorMessage = "Пароли должны совпадать";
                    return false;
                }
            }
            if (ErrorMessage != "Данный логин занят")
                ErrorMessage = null;
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
            if (isRegistered == "True")
                ErrorMessage = null;
            else
                ErrorMessage = "Данный логин занят";
        }



        public static async Task<string> RegisterNewUser(string username, string password)
        {

            var response = new List<byte>();
            int bytesRead = 10;

            var messages = new string[] { $"Register,{username},{password},\n" };

            // получаем NetworkStream для взаимодействия с сервером
            foreach (var message in messages)
            {
                // считыванием строку в массив байт
                byte[] data = Encoding.UTF8.GetBytes(message);
                // отправляем данные
            }
            var isRegistered = Encoding.UTF8.GetString(response.ToArray());
            response.Clear();

            return isRegistered;

        }
    }
}