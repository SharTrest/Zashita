using Diplom.Utilities;
using System.Windows.Input;
using Diplom.Client.View.LoginWindowUserControls;
using Diplom.Client.Model;
using Diplom.View.Windows;
using System.Windows;
using MathCore.WPF.Converters;

namespace Diplom.Client.ViewModel.LoginWindowViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        
        public LogViewModel LogViewModel { get; }
        private object _currentView;
        private LoginUserControl login;
        private RegisterUserControl register;
        private bool _isViewVisible = true;
        private UserList _db;

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

        public ICommand LogCommand { get; set; }
        public ICommand ShowCreaterCommand { get; set; }

        private void Log(object obj) => CurrentView = login;
        private void Show(object obj) => MessageBox.Show(_db.Creator, "Справка");


        public LoginViewModel(UserData user, LoginWindow wind, UserList dB)
        {
            _db = dB;
            register = new RegisterUserControl(user, dB);
            login = new LoginUserControl(user, wind, dB);
            IsViewVisible = user.Authed;
            LogCommand = new RelayCommand(Log);
            ShowCreaterCommand = new RelayCommand(Show);
            CurrentView = login;
        }
    }

}   