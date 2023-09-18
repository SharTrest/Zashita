using Diplom.Client.Model;
using Diplom.Utilities;
using MathCore.WPF;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Zashita.DAL.Context;
using Zashita.DAL.Entity;

namespace Diplom.Client.ViewModel.MainWindowViewModel
{
    public class UserListViewModel : ViewModelBase
    {
        private string _userName;
        private User _user;
        private ZashitaDB _db;
        private ObservableCollection<User> _users;
        public ObservableCollection<User> UserList
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public User SelectedUser
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }
        public string FindUserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        public ICommand BanUserCommand { get; }
        public ICommand RuledPassCommand { get; }
        public ICommand BanUserByNameCommand { get; }
        public ICommand AddNewUserCommand { get; }
        public ICommand ChangeRulesCommand { get; }

        public UserListViewModel(ZashitaDB db)
        {
            _db = db;
            _users = Methods.ShowUsers(db);
            ChangeRulesCommand = new RelayCommand(ExecuteChangeRulesCommand);
            BanUserByNameCommand = new RelayCommand(ExecuteBanUserByNameCommand, CanExecuteAddNewUserCommand);
            AddNewUserCommand = new RelayCommand(ExecuteAddNewUserCommand, CanExecuteAddNewUserCommand);
            BanUserCommand = new RelayCommand(ExecuteBanCommand, CanExecuteBanCommand);
            RuledPassCommand = new RelayCommand(ExecuteRulledCommand, CanExecuteBanCommand);
        }

        private void ExecuteChangeRulesCommand(object obj)
        {
            Methods.ChangePasswordRules(_db);
            MessageBox.Show("Включено ограничение паролей");
            UserList = Methods.ShowUsers(_db);
        }

        private void ExecuteBanUserByNameCommand(object obj)
        {
            if (UserList.Find(u => u.UserName == FindUserName) == null)
            {
                MessageBox.Show("Пользлователь не найден");
                return;
            }
            Methods.BanUserByName(_db, FindUserName);
            MessageBox.Show($"Пользователь {FindUserName} забанен");
            UserList = Methods.ShowUsers(_db);
        }

        private void ExecuteAddNewUserCommand(object obj)
        {
            if (UserList.Find(u => u.UserName == FindUserName) != null)
            {
                MessageBox.Show($"Пользлователь с именем {FindUserName} уже существует");
                return;
            }
            Methods.AddNewUSer(_db, FindUserName);
            MessageBox.Show($"Пользователь {FindUserName} добавлен");
            UserList = Methods.ShowUsers(_db);
        }

        private bool CanExecuteAddNewUserCommand(object arg)
        {
         
            return FindUserName != null;
        }

        private bool CanExecuteBanCommand(object arg)
        {
            return SelectedUser != null;
        }

        private void ExecuteRulledCommand(object obj)
        {
            if (Methods.ChangePassRiulesByName(_db, _user.UserName))
            {
                MessageBox.Show("Правила пароля изменены");
                UserList = Methods.ShowUsers(_db);
                SelectedUser = UserList.Find(u=>u.UserName == _user.UserName);

            }
            else
                MessageBox.Show("Ошибка");
        }

        private void ExecuteBanCommand(object obj)
        {
            var res = Methods.BanUserByName(_db, _user.UserName);
            if (res == "")
                MessageBox.Show("Ошибка");
            if (res == "B")
            {
                MessageBox.Show("Пользователь заблокирован");
            }
            if (res == "U")
                MessageBox.Show("Пользователь разблокирован");
            SelectedUser = _user;
            UserList = Methods.ShowUsers(_db);
        }
    }
}
