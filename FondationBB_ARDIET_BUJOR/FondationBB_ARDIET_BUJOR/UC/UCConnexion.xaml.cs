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
        }

        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            DataAccess.TryConnect(tbUserName.Text, pbMDP.Password);
            List<Employe> lesEmployes = new Employe().FindAll();
            Employe emp = lesEmployes.FirstOrDefault(em => em.Login == tbUserName.Text);
            if (emp != null)
            {
                LoginReussi?.Invoke(this, emp);
            }
            else
            {
                MessageBox.Show("Login ou mot de passe incorrect", "Erreur de connexion", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}