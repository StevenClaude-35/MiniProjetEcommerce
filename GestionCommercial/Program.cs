using GestionCommercial.Entites;
using GestionCommercial.Entites.Enums;
using GestionCommercial.Services;
using GestionCommercial.Services.Exceptions;
using System;
using System.Collections.Generic;

namespace GestionCommercial
{
    class Program
    {
        static ClientService clientService = new ClientService();

        static void Menu()
        {
            Console.WriteLine("\n\t-----Application de gestion des clients---\n");
            Console.WriteLine("1 - Afficher le clients---");
            Console.WriteLine("2 - Ajouter un client ");
            Console.WriteLine("3 - Supprimer un client");
            Console.WriteLine("4 - Rechercher un client");
            Console.WriteLine("5 - Enregistrer");
            Console.WriteLine("6 - Trier");
            Console.WriteLine("Q - Quitter");
            Console.Write("\t\n Donnez votre choix : ");
        }
        static void Main(string[] args)
        {
            string choixOperation;

            do
            {
                Menu();
                choixOperation = Console.ReadLine();
                switch (choixOperation)
                {
                    case "1":
                        AfficherTousClients(clientService.TousClient());
                        break;
                    case "2":
                        AjouterClient();
                        break;
                    case "3":
                        SupprimerClient();
                        break;
                    case "4":
                        RechercherClientParNom();
                        break;
                    case "5":
                        Enregistrement();
                        break;
                    case "6":
                        trier();
                        break;

                    default:
                        break;
                }


            } while (choixOperation !="Q");
           
            
        }

        private static void trier()
        {
            clientService.Trier();
        }

        private static void Enregistrement()
        {
            clientService.Enregistrer();
        }

        private static void SupprimerClient()
        {
            Console.WriteLine("Donner le nom du client à supprimer : ");
            string nomClient = Console.ReadLine();
            clientService.Supprimer(nomClient);
        }

        private static void RechercherClientParNom()
        {
            Console.Write("Donner le nom du client à chercher");
            string nomClient = Console.ReadLine();
            List<Client> clients = clientService.Rechercher(nomClient);
            AfficherTousClients(clients);
        }

        private static void AjouterClient()
        {
            Client client = new Client();
            Console.WriteLine("Saisie un nouveau Client");
            Console.Write("Nom :");
            client.Nom = Console.ReadLine();
            client.Prenom = Console.ReadLine();
            Console.Write("Civilites (1:Homme,2:Femme)");
            var CiviliteNombre = Console.ReadLine();
            client.Civilite = (Civilites)int.Parse(CiviliteNombre);

            Console.Write("Date de naissance : ");
            client.DateNaissance = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Nationnalité (1:Marocaine,2:Française ,3:Belge)");
            var NationaliteNombre = Console.ReadLine();
            client.Nationnalite = (Nationnalites)int.Parse(NationaliteNombre);

            Console.Write("Email: ");
            client.Email = Console.ReadLine();

            try
            {
                clientService.Ajouter(client);
            }
            catch (AjouterClientException Exception)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("\t Le client n'a pas été ajouter : " + Exception.Message);
                Console.ResetColor();
            }
            
        }

        private static void AfficherTousClients(List<Client> listClients)
        {
            Console.Clear();
            Console.WriteLine("\t ---Liste des Clients --- \n");
            foreach (var client in listClients)
            {
                Console.WriteLine(client);
            }
        }
    }
}
