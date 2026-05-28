using System.Windows;
using System.Windows.Controls;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class UCConnexion : UserControl
    {
        public UCConnexion()
        {
            InitializeComponent();
        }

        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.ValiderConnexion();
            }
        }
    }
}