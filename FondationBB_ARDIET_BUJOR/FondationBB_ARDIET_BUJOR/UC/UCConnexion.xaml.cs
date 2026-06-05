using FondationBB_ARDIET_BUJOR.Model;
using System.Windows;
using System.Windows.Controls;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class UCConnexion : UserControl
    {
        public event EventHandler<Employe>? LoginReussi;
        public UCConnexion()
        {
            InitializeComponent();
            this.Loaded += (s, e) => tbUserName.Focus();
        }

        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            bool connected = DataAccess.TryConnect(tbUserName.Text, pbMDP.Password);      
            if (connected)
            {
                List<Employe> lesEmployes = new Employe().FindAll();
                Employe emp = lesEmployes.FirstOrDefault(em => em.Login == tbUserName.Text);
                LoginReussi?.Invoke(this, emp);
            }
            else
            {
                MessageBox.Show("Login ou mot de passe incorrect", "Erreur de connexion", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}