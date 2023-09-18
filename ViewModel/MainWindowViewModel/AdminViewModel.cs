using Diplom.Client.Model;
using Diplom.Client.View.LoginWindowUserControls;
using Diplom.Client.View.MainWindowUserControls;
using Diplom.Utilities;
using System.Windows.Input;


namespace Diplom.Client.ViewModel.MainWindowViewModel
{
    public class AdminViewModel:ViewModelBase
    {
        private readonly string _username;
        private object _currentView;
        private UserData _user;
        private RemakePassUserControl _rpv;
        private UserListUserControl _showListView;
        private RemakePassUserControl _banUserByNameView;
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
        public string UserName => _username;

        public ICommand RemakePassCommand { get; }
        public ICommand ShowUsersListCommand { get; }   
        public ICommand BanUserByNameCommand { get; }

        private void Change(object obj) => CurrentView = _rpv;
        private void ShowList(object obj) => CurrentView = _showListView;
        private void Ban(object obj) => CurrentView = _banUserByNameView;


        public AdminViewModel(UserData user, UserList db)
        {
            if (user.Password == "") 
            {
                CurrentView = new RegisterUserControl(user, db);
            }
            _rpv = new RemakePassUserControl(user, db);
            _showListView = new UserListUserControl(db);
            RemakePassCommand = new RelayCommand(Change, CanChange);
            ShowUsersListCommand = new RelayCommand(ShowList, CanShowList);
            BanUserByNameCommand = new RelayCommand(Ban, CanBan);

            _username = user.UserName;
            _user = user;

        }

        private bool CanBan(object arg) => _user.Password != "";

        private bool CanShowList(object arg) => _user.Password != "";

        private bool CanChange(object arg) => _user.Password != "";
    }
}
