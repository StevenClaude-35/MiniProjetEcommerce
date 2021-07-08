using System;
using System.Collections.Generic;
using System.Text;

namespace GestionCommercial.Entites
{
    public class ComptePrepaye
    {
        public double SoldePrepaye { get; set; }

        public double SoldeEnDollars
        {
            get
            {
                return this.SoldePrepaye * 1.2;
            }
        }
    }
}
