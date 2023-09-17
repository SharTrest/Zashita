using Diplom.Client.Model;
using Diplom.Utilities;
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

        public ICommand BanUserCommand { get; }
        public ICommand RuledPassCommand { get; }

        public UserListViewModel(ZashitaDB db)
        {
            _db = db;
            _users = Methods.ShowUsers(db);
            BanUserCommand = new RelayCommand(ExecuteBanCommand, CanExecuteBanCommand);
            RuledPassCommand = new RelayCommand(ExecuteRulledCommand, CanExecuteBanCommand);
        }

        private bool CanExecuteBanCommand(object arg)
        {
            return true;
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
