using Diplom.Client.Model;
using Diplom.Client.ViewModel.MainWindowViewModel;
using System.Windows;
using Zashita.DAL.Context;

namespace Diplom.Client.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        public AdminMainWindow(UserData user, ZashitaDB db)
        {
            this.DataContext = new AdminViewModel(user, db);
            InitializeComponent();
        }
    }
}
