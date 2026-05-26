using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public class Date
    {
        private DateTime dateSoin;

        public Date()
        {
        }

        public Date(DateTime dateSoin)
        {
            this.DateSoin = dateSoin;
        }

        public DateTime DateSoin
        {
            get
            {
                return this.dateSoin;
            }

            set
            {
                this.dateSoin = value;
            }
        }
    }
}
