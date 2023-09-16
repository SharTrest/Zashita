using System.Windows;
using System.Windows.Controls;

namespace Diplom.Client.Styles
{
    /// <summary>
    /// Логика взаимодействия для BindableLoginBox.xaml
    /// </summary>
    /// 


    public partial class BindableLoginBox : UserControl
    {
        public static readonly DependencyProperty UsernameProperty = DependencyProperty.Register("Username", typeof(string), typeof(BindableLoginBox));
        public string Username
        {
            get
            {
                return (string)GetValue(UsernameProperty);
            }
            set
            {
                SetValue(UsernameProperty, value);
            }
        }

        public BindableLoginBox()
        {
            InitializeComponent();
            txtUser.TextChanged += OnLoginChanged;
        }

        private void OnLoginChanged(object sender, RoutedEventArgs e)
        {
            Username = txtUser.Text;
        }
    }
}
