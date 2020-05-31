using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts
{
    public class Par_Tenedores
    {

        private Tenedor t1;
        private Tenedor t2;


        public Par_Tenedores(Tenedor T1, Tenedor T2)
        {
            this.t1 = T1;
            this.t2 = T2;
        }

        public Tenedor tenedor_A
        {
            get { return t1; }
            set { t1 = value; }
        }

        public Tenedor tenedor_B
        {
            get { return t2; }
            set { t1 = value; }
        }

    }
}
