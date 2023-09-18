using Diplom.Client.Model;
using Diplom.Client.ViewModel.MainWindowViewModel;
using System.Windows.Controls;

namespace Diplom.Client.View.MainWindowUserControls
{
    /// <summary>
    /// Логика взаимодействия для UserListUserControl.xaml
    /// </summary>
    public partial class UserListUserControl : UserControl
    {
        public UserListUserControl(UserList db)
        {
            InitializeComponent();
            this.DataContext = new UserListViewModel(db);
        }
    }
}
