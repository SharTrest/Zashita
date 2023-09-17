using Diplom.Client.Model;
using Diplom.Client.ViewModel.MainWindowViewModel;
using System.Windows.Controls;
using Zashita.DAL.Context;

namespace Diplom.Client.View.LoginWindowUserControls
{
    /// <summary>
    /// Логика взаимодействия для RemakePassUserControl.xaml
    /// </summary>
    public partial class RemakePassUserControl : UserControl
    {
        public RemakePassUserControl(UserData user, ZashitaDB db)
        {
            this.DataContext = new RemakePassViewModel(user, db);
            InitializeComponent();
        }


    }
}
