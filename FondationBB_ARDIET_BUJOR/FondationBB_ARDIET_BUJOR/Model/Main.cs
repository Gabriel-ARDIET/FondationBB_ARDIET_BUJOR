using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public class Main
    {
        private ObservableCollection<Employe> employes;
        public Main() 
        {
            this.Employes = new ObservableCollection<Employe>(new Employe().FindAll());
        }

        public ObservableCollection<Employe> Employes
        {
            get
            {
                return this.employes;
            }

            set
            {
                this.employes = value;
            }
        }
    }
}
