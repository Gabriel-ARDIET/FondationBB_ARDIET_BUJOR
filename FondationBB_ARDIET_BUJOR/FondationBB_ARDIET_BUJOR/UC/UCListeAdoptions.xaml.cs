using FondationBB_ARDIET_BUJOR.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class UCListeAdoptions : UserControl
    {
        private Data laData;
        public UCListeAdoptions()
        {
            InitializeComponent();

            laData = (Data)Application.Current.MainWindow.DataContext;
            this.DataContext = laData.LesAdoptions;
        }
        private void BtnSupprimer_Click(object sender, RoutedEventArgs e) { }
        private void BtnAjouter_Click(object sender, RoutedEventArgs e) { }
        private void BtnEditer_Click(object sender, RoutedEventArgs e) { }
    }
}
