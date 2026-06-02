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
        //Exemple du cours :
        /*public partial class ConnexionUC : UserControl
        {
            public event EventHandler LoginReussi;
            public ConnexionUC()
            { InitializeComponent(); }

            private void BtnConnexion_Click(object sender, RoutedEventArgs e)
            {
                if (TxtLogin.Text == "admin" && TxtPassword.Password == "1234")
                    LoginReussi?.Invoke(this, EventArgs.Empty);
                else
                    MessageBox.Show("Identifiants incorrects.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }*/

    }
}