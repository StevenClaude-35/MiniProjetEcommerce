using GestionCommercial.Entites.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionCommercial.Entites
{
    public class Client
    {
        public string Nom{ get; set; }
        public string Prenom { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Email { get; set; }
        public Civilites Civilite { get; set; }
        public Nationnalites Nationnalite { get; set; }

        public ComptePrepaye ComptePrepaye { get; set; }

        public string NomComplet
        {
            get
            {
                return this.Nom + " " + this.Prenom;
            }
        }

        public string resume
        {
            get
            {
                string le_resume=$"{this.NomComplet}[" +
                    $"{((this.ComptePrepaye==null)?0:this.ComptePrepaye.SoldePrepaye)} euros,"
                    +$"{this.Civilite.ToString()},"
                    +$"{this.Nationnalite.ToString()},"
                    +$"{ this.DateNaissance.ToShortDateString()},"
                    +$"{ this.Email}]";

                return le_resume;
            }
        }

        public override string ToString()
        {
            return this.resume;
        }
    }
}
