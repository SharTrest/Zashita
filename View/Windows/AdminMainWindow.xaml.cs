using Diplom.Client.Model;
using Diplom.Client.ViewModel.MainWindowViewModel;
using System.Windows;

namespace Diplom.Client.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        public AdminMainWindow(UserData user)
        {
            this.DataContext = new AdminViewModel(user);
            InitializeComponent();
        }
    }
}
