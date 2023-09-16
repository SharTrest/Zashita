using Diplom.Client.Model;
using Diplom.Client.View.LoginWindowUserControls;
using Diplom.Utilities;
using MathCore.Values;
using System;
using System.Windows.Input;
using Zashita.DAL.Context;

namespace Diplom.Client.ViewModel.MainWindowViewModel
{
    public class UserViewModel : ViewModelBase
    {
        private readonly string _username;
        private object _currentView;
        private RemakePassUserControl _remakePassView;

        public object RemakePassView
        {
            get
            { 
                    if (_remakePassView.IsVisible)
                        _remakePassView.Visibility = System.Windows.Visibility.Hidden;
                    else
                        _remakePassView.Visibility = System.Windows.Visibility.Visible;
                    return _remakePassView;
            }
        }
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }
        public string UserName => _username;

        public ICommand RemakePassCommand { get; }


        private void Change(object obj) => CurrentView = RemakePassView;

        public UserViewModel(UserData user, ZashitaDB db)
        {
            RemakePassCommand = new RelayCommand(Change);
            _remakePassView = new RemakePassUserControl(user);
            _remakePassView.Visibility = System.Windows.Visibility.Visible;
            _username = user.UserName;
        }
    }
}
