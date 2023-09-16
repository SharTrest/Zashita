using Diplom.Client.Model;
using Diplom.Client.ViewModel.LoginWindowViewModel;
using Diplom.Client.ViewModel.MainWindowViewModel;
using System.Windows.Controls;
using Zashita.DAL.Context;

namespace Diplom.Client.View.LoginWindowUserControls
{
    /// <summary>
    /// Логика взаимодействия для RegisterUserControl.xaml
    /// </summary>
    public partial class RegisterUserControl : UserControl
    {
        public RegisterUserControl(UserData user, ZashitaDB db)
        {
            this.DataContext = new RegViewModel(user, db);
            InitializeComponent();    
        }


    }
}
