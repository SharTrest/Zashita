using Diplom.Client.Model;
using Diplom.Client.View.LoginWindowUserControls;
using Diplom.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Diplom.Client.ViewModel.MainWindowViewModel
{
    public class AdminViewModel:ViewModelBase
    {
        private readonly string _username;
        private object _currentView;
        private UserData _user;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }
        public string UserName => _username;

        public ICommand RemakePass { get; }
        public ICommand ShowUsersListCommand { get; }
        public ICommand BanUserByNameCommand { get; }

        private void Change(object obj) => CurrentView = new RemakePassUserControl(_user);
        //private void ShowList(object obj) => CurrentView = ShowListView;
        //private void Ban(object obj) => CurrentView = BanUserByNameView;


        public AdminViewModel(UserData user)
        {
            _username = user.UserName;
            _user = user;
            CurrentView = new RelayCommand(Change);
        }
    }
}
