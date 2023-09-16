using System.Windows.Controls;
using System.Windows;

namespace Diplom.Client.Utilities
{
    internal class Btn : RadioButton
    {
        static Btn()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Btn), new FrameworkPropertyMetadata(typeof(Btn)));
        }
    }
}
