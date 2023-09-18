using Diplom.Client.Model;
using Diplom.Client.View.LoginWindowUserControls;
using Diplom.Utilities;
using System.Windows.Input;

namespace Diplom.Client.ViewModel.MainWindowViewModel
{
    public class UserViewModel : ViewModelBase
    {
        private readonly string _username;
        private object _currentView;
        private RemakePassUserControl _rpv;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }
        public string UserName => _username;

        public ICommand RemakePassCommand { get; }


        private void Change(object obj) => CurrentView = _rpv;

        public UserViewModel(UserData user, UserList db)
        {
            if (user.Password == "")
            {
                CurrentView = new RegisterUserControl(user, db);
            }
            RemakePassCommand = new RelayCommand(Change, CanChange);
            _rpv = new RemakePassUserControl(user, db);
            _username = user.UserName;
        }

        private bool CanChange(object arg) => true;
    }
}
