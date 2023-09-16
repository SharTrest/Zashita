using Diplom.Client.Model;
using Diplom.Client.ViewModel.MainWindowViewModel;
using System.Windows.Controls;

namespace Diplom.Client.View.LoginWindowUserControls
{
    /// <summary>
    /// Логика взаимодействия для RemakePassUserControl.xaml
    /// </summary>
    public partial class RemakePassUserControl : UserControl
    {
        public RemakePassUserControl(UserData user)
        {
            this.DataContext = new RemakePassViewModel(user);
            InitializeComponent();
        }
    }
}
