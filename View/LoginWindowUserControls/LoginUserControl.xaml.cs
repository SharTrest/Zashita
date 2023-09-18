using Diplom.Client.Model;
using Diplom.Client.ViewModel.LoginWindowViewModel;
using Diplom.View.Windows;
using System.Windows.Controls;

namespace Diplom.Client.View.LoginWindowUserControls
{
    /// <summary>
    /// Логика взаимодействия для LoginUserControl.xaml
    /// </summary>
    public partial class LoginUserControl : UserControl
    {
        public LoginUserControl(UserData user, LoginWindow wind, UserList db)
        {
            this.DataContext = new LogViewModel(user, wind, db);
            InitializeComponent();

        }
    }
}
