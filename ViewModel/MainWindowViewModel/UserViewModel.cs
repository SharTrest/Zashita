using Diplom.Client.Model;
using Diplom.Client.View.LoginWindowUserControls;
using Diplom.Utilities;
using System.Windows;
using System.Windows.Input;

namespace Diplom.Client.ViewModel.MainWindowViewModel
{
    public class UserViewModel : ViewModelBase
    {
        private readonly string _username;
        private object _currentView;
        private RemakePassUserControl _rpv;
        private UserList _db;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }
        public string UserName => _username;

        public ICommand RemakePassCommand { get; }
        public ICommand ShowCreaterCommand { get; set; }

        private void Change(object obj) => CurrentView = _rpv;
        private void Show(object obj) => MessageBox.Show(_db.Creator, "Справка");

        public UserViewModel(UserData user, UserList db)
        {
            if (user.Password == "")
            {
                CurrentView = new RegisterUserControl(user, db);
            }
            ShowCreaterCommand = new RelayCommand(Show);
            RemakePassCommand = new RelayCommand(Change, CanChange);
            _rpv = new RemakePassUserControl(user, db);
            _username = user.UserName;
            _db = db;
        }

        private bool CanChange(object arg) => true;
    }
}
