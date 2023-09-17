using Diplom.Client.Model;
using Diplom.Client.ViewModel.MainWindowViewModel;
using System.Windows;
using Zashita.DAL.Context;

namespace Diplom.Client.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для UserMainWindow.xaml
    /// </summary>
    public partial class UserMainWindow : Window
    {
        public UserMainWindow(UserData user, ZashitaDB db)
        {
            this.DataContext = new UserViewModel(user, db);
            InitializeComponent();
        }
    }
}
