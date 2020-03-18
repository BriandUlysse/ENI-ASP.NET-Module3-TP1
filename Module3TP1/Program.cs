using ProjetLinq.BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3TP1
{
    class Program
    {

        private static List<Auteur> ListeAuteurs = new List<Auteur>();
        private static List<Livre> ListeLivres = new List<Livre>();

        static void Main(string[] args)
        {
            InitialiserDatas();
            Console.WriteLine(" - Liste des auteurs dont le nom commence par G :");
            IEnumerable<Auteur> listAuteurStartG = ListeAuteurs.Where(a => a.Nom.StartsWith("G"));
            foreach (Auteur auteur in listAuteurStartG)
            {
                Console.WriteLine(auteur.Nom);
            }


            Console.WriteLine("");
            Console.WriteLine(" - Auteur ayant le plus de livres :");
            Auteur auteurMoreBook = ListeLivres.GroupBy(l=>l.Auteur).OrderByDescending(a=>a.Count()).First().Key;
            Console.WriteLine(auteurMoreBook.Nom+" "+auteurMoreBook.Prenom);


            Console.WriteLine("");
            Console.WriteLine(" - Nombre moyen de pages par livre par auteur :");
            foreach (Auteur auteur in ListeAuteurs)
            {
                IEnumerable<Livre> listLIvreOfAUthor = ListeLivres.Where(l => l.Auteur == auteur);

                double nbPagesByAuthor;
                if (listLIvreOfAUthor.Count()>0)
                {
                    nbPagesByAuthor = listLIvreOfAUthor.Average(l => l.NbPages);
                }
                else
                {
                    nbPagesByAuthor = 0;
                }
                Console.WriteLine(auteur.Nom + " " + auteur.Prenom + " nombre de pages moyens : " + nbPagesByAuthor);
            }


            Console.WriteLine("");
            Console.WriteLine(" - Livre avec le plus de page :");
            Livre livreMaxPage = ListeLivres.OrderByDescending(l => l.NbPages).First();
            Console.WriteLine(livreMaxPage.Titre);


            Console.WriteLine("");
            Console.WriteLine(" - Combien ont gagné les auteurs en moyennes :");
            decimal avg = ListeAuteurs.Average(a => a.Factures.Sum(f=>f.Montant));
            Console.WriteLine(avg);


            Console.WriteLine("");
            Console.WriteLine(" - La listes des auteurs et leur livres :");
            IEnumerable bookByAuteur = ListeLivres.GroupBy(l => l.Auteur);
            foreach (Auteur auteur in ListeAuteurs)
            {
                Console.WriteLine(auteur.Nom);
                IEnumerable<Livre> livresOfAuthor = ListeLivres.Where(l => l.Auteur == auteur);
                printListLivres(livresOfAuthor);
            }


            Console.WriteLine("");
            Console.WriteLine(" - Liste des livres par ordre alphabétique :");
            IEnumerable<Livre> livreAlpha = ListeLivres.OrderBy(l => l.Titre).Distinct();
            printListLivres(livreAlpha);


            Console.WriteLine("");
            Console.WriteLine(" - Liste des livres dont le  nombre de page est supérieur à la moyenne :");
            double moyenne = ListeLivres.Average(l => l.NbPages);
            IEnumerable<Livre> livreMoreMoyenne = ListeLivres.Where(l => l.NbPages > moyenne);
            printListLivres(livreMoreMoyenne);


            Console.WriteLine("");
            Console.WriteLine(" - Auteur ayant écrit le moins de livre :");
            Auteur auteurLessBook = ListeAuteurs.OrderBy(a=> ListeLivres.Where(l => l.Auteur==a).Count()).First();
            Console.WriteLine(auteurLessBook.Nom + " " + auteurLessBook.Prenom);

            Console.ReadKey();
        }


        private static void InitialiserDatas()
        {
            ListeAuteurs.Add(new Auteur("GROUSSARD", "Thierry"));
            ListeAuteurs.Add(new Auteur("GABILLAUD", "Jérôme"));
            ListeAuteurs.Add(new Auteur("HUGON", "Jérôme"));
            ListeAuteurs.Add(new Auteur("ALESSANDRI", "Olivier"));
            ListeAuteurs.Add(new Auteur("de QUAJOUX", "Benoit"));
            ListeLivres.Add(new Livre(1, "C# 4", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 533));
            ListeLivres.Add(new Livre(2, "VB.NET", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 539));
            ListeLivres.Add(new Livre(3, "SQL Server 2008", "SQL, Transact SQL", ListeAuteurs.ElementAt(1), 311));
            ListeLivres.Add(new Livre(4, "ASP.NET 4.0 et C#", "Sous visual studio 2010", ListeAuteurs.ElementAt(3), 544));
            ListeLivres.Add(new Livre(5, "C# 4", "Développez des applications windows avec visual studio 2010", ListeAuteurs.ElementAt(2), 452));
            ListeLivres.Add(new Livre(6, "Java 7", "les fondamentaux du langage", ListeAuteurs.ElementAt(0), 416));
            ListeLivres.Add(new Livre(7, "SQL et Algèbre relationnelle", "Notions de base", ListeAuteurs.ElementAt(1), 216));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3500, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3200, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(1).addFacture(new Facture(4000, ListeAuteurs.ElementAt(1)));
            ListeAuteurs.ElementAt(2).addFacture(new Facture(4200, ListeAuteurs.ElementAt(2)));
            ListeAuteurs.ElementAt(3).addFacture(new Facture(3700, ListeAuteurs.ElementAt(3)));
        }

        private static void printListLivres(IEnumerable<Livre> livres)
        {
            foreach (Livre livre in livres)
            {
                Console.WriteLine(livre.Titre +" - " + livre.Synopsis);
            }
        }
    }
}
