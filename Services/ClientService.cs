using GestionCommercial.Entites;
using GestionCommercial.Entites.Enums;
using GestionCommercial.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace GestionCommercial.Services
{
    public class ClientService
    {
        private List<Client> ClientCollection;

        public ClientService()
        {
            //Chargement des clients enregister
            this.ClientCollection = this.Ouvrir();
            if (this.ClientCollection == null)
            {
                ClientCollection = new List<Client>();

                ClientCollection.Add(new Client()
                {
                    Nom = "Madani",
                    Prenom = "Ali",
                    Civilite = Civilites.Homme,
                    ComptePrepaye = new ComptePrepaye() { SoldePrepaye = 100 },
                    DateNaissance = new DateTime(2000, 2, 1),
                    Email = "Madani.Ali@yahoo.fr",
                    Nationnalite = Nationnalites.Marocaine
                });
                ClientCollection.Add(new Client()
                {
                    Nom = "chami",
                    Prenom = "Moad",
                    Civilite = Civilites.Homme,
                    ComptePrepaye = new ComptePrepaye() { SoldePrepaye = 20 },
                    DateNaissance = new DateTime(2000, 1, 1),
                    Email = "Chami.Moad@yahoo.fr",
                    Nationnalite = Nationnalites.Belge
                });
                ClientCollection.Add(new Client()
                {
                    Nom = "Madani",
                    Prenom = "Mouna",
                    Civilite = Civilites.Femme,
                    ComptePrepaye = new ComptePrepaye() { SoldePrepaye = 40 },
                    DateNaissance = new DateTime(2000, 2, 1),
                    Email = "Madani.mouna@yahoo.fr",
                    Nationnalite = Nationnalites.Français
                });
            }
           
        }

        public void Ajouter(Client client)
        {
            //verification du nom du Client

            if (!this.VerificationNom(client.Nom))
            {
                string message = "Le nom du client n'est pas coorect";
                throw new AjouterClientException(message);
            }

            //verification de l'age est superieur à 18 

            if (!this.VerificationAge(client.DateNaissance))
            {
                string message = "Le client est mineur, "+
                    "son age est inferieur à 18 ans";
                throw new AjouterClientException(message);
            }

            //Verification Email
            if (!VerificationEmail(client.Email))
            {
                string message = "Email non valid";
                throw new AjouterClientException(message);
            }
            this.ClientCollection.Add(client);
        }

        public List<Client>  Ouvrir()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Client>));
            string fileName = "Client.xml";
            if (File.Exists(fileName))
            {
                StreamReader streamReader = new StreamReader("Clients.xml");

                List<Client> liste_Clients = (List<Client>)xmlSerializer.Deserialize(streamReader);
                streamReader.Close();

                return liste_Clients;
            }
            else
                return null;
        }

        public void Enregistrer()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Client>));
            StreamWriter streamWriter = new StreamWriter("Client.xml");
            xmlSerializer.Serialize(streamWriter, this.ClientCollection);
            streamWriter.Close();
        }

        public  void Trier()
        {
            this.ClientCollection.Sort();
        }

        public void Supprimer(string nomClient)
        {
            var clients = this.Rechercher(nomClient);
            if(clients!=null && clients.Count > 0)
            {
                this.ClientCollection.Remove(clients.First());
            }
        }

        public List<Client> Rechercher(string nomClient)
        {
            return this.ClientCollection
                .Where(c => c.Nom.ToLower().Contains(nomClient.ToLower())).ToList();
        }

        #region Verification

        private bool VerificationEmail(string email)
        {
            Regex RegexEmail = new Regex("^(\\w+([-_.]\\w+)*)[@](((outlook.)|(gmail.)|(yahoo.))(([a-z]{2})|([a-z]{3})))$");
            if (RegexEmail.IsMatch(email))
                return true;
            else
                return false;
        }

        private bool VerificationAge(DateTime dateNaissance)
        {
            TimeSpan difference = DateTime.Now - dateNaissance;
            float Age = (int)difference.TotalDays / 365;

            if (Age >= 18) return true;
            return false;
        }

        private bool VerificationNom(string nom)
        {
            Regex NameRegex = new Regex("^[a-zA-Z]+\\w+$");

            if (NameRegex.IsMatch(nom))
            {
                return true;
            }
            else
                return false;
        }


        #endregion


        public List<Client> TousClient()
        {
            return ClientCollection;
        }
    }

    
}
