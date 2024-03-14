/*
 * Rediged by Damien & Thomas TP2 IUT Robert Schuman
 * 
 */
//Ensemble de librairie
using System.IO;
using System;

namespace Sudoku
{
    public class html_maker
    {
        //variable pour le test de séparation du template html
        const string valeur_test = "alt=\"grilles\"";
        //nom du fichier pour l'ensemble de librairie , initilisation avec la processédure d'initilisation de la librairie
        string nom_fichier_html = "";
        //ligne de coupure
        int possition_coupure;
        //ensemble de lignes du template
        string[] tab_chaine = File.ReadAllLines("./template.txt");

        /*
         *  initialisateur du nom de fichier
        */
        public html_maker(string Xnom_fichier)
        {
            nom_fichier_html = $"./{Xnom_fichier}";
        }

        /*
         *  création du début de fichier html
        */
        public void init_html()
        {
            //string[] tab_chaine = File.ReadAllLines("./template.txt");
            if (File.Exists(nom_fichier_html))
            {
                //supression du fichier s'il existe
                File.Delete(nom_fichier_html);
            }

            for (possition_coupure = 0; tab_chaine[possition_coupure].Contains(valeur_test) == false; possition_coupure++)
            {
                //ecriture des lignes html de base (début)
                File.AppendAllText(nom_fichier_html, tab_chaine[possition_coupure] + '\n');
            }
            File.AppendAllText(nom_fichier_html, tab_chaine[possition_coupure] + '\n');
            possition_coupure++;
            File.AppendAllText(nom_fichier_html, tab_chaine[possition_coupure] + '\n');
        }

        /*
         *  création du tableau html depuis un tableau 2D c#
        */
        public void entre_tab(int[,] Xtab, string Xclass)
        {
            string id_html = "";
            //taille de la grille
            int longueur = Xtab.GetLength(0);
            //creation variable pour taille de grille dans la class du html / css
            if (longueur == 9)
            {
                id_html = "neuf";
            }
            else if (longueur == 16)
            {
                id_html = "seize";
            }
            //ecriture de ligne pour le tableau avec la class contenant le parametre pour le html / css
            File.AppendAllText(nom_fichier_html, $"<table class=\"{Xclass} {id_html}\">\n");
            //parcoure les lignes
            for (int i = 0; i < longueur; i++)
            {
                //ecriture du caratere pour la colonne dans html
                File.AppendAllText(nom_fichier_html, "<tr>\n");
                //parcoure les colonnes
                for (int j = 0; j < longueur; j++)
                {
                    //remplacement des 0 par le caractere vide
                    if (Xtab[i, j] == 0)
                    {
                        File.AppendAllText(nom_fichier_html, "<th></th>\n");
                    }
                    //ecriture du caratere pour la colonne dans html
                    else
                    {
                        File.AppendAllText(nom_fichier_html, $"<th>{Xtab[i, j]}</th>\n");
                    }
                }
                //ecriture du caratere pour la ligne dans html
                File.AppendAllText(nom_fichier_html, "</tr>\n");
            }
            //ecriture de ligne pour le tableau
            File.AppendAllText(nom_fichier_html, "</table>\n");
        }

        /*
         *  création de la fin du fichier html
        */
        public void close_html()
        {
            for (possition_coupure = possition_coupure + 1; possition_coupure < tab_chaine.Length; possition_coupure++)
            {
                //ecriture des lignes html de base (début)
                File.AppendAllText(nom_fichier_html, tab_chaine[possition_coupure] + '\n');
            }
        }
    }
    public class csv_reader
    {
        //nom du fichier pour l'ensemble de librairie , initilisation avec la processédure d'initilisation de la librairie
        string nom_fichier_csv = "";

        /*
         *  initialisateur du nom de fichier
        */
        public csv_reader(string Xnom_fichier)
        {
            nom_fichier_csv = $"./{Xnom_fichier}";
        }
        /*
         *  lecture des lignes du fichier csv et creation du tableau
         */
        public int[,] reader_file()
        {
            //lecture des lignes du fichier csv contenant la grille
            string[] tab_chaine = File.ReadAllLines(nom_fichier_csv);
            //determine la taille de la grille
            int longueur = (tab_chaine[0].Split(',')).Length /*+ 1*/;
            //declaration du tableau pour grille
            int[,] tab_csv = new int[longueur, longueur];
            //parcoure les lignes du csv et pour le tableau
            for (int i = 0; i < longueur; i++)
            {
                //variable intermediaire
                string[] split_tab = tab_chaine[i].Split(',');
                //parcoure les nombres et les colonnes du tableau
                for (int j = 0; j < split_tab.Length; j++)
                {
                    //ecriture dans la case du caractere avec une conversion en entier 
                    tab_csv[i, j] = int.Parse(split_tab[j]);
                }
            }
            //retour de la grille
            return tab_csv;
        }
    }
    public class txt_reader
    {
        //nom du fichier pour l'ensemble de librairie , initilisation avec la processédure d'initilisation de la librairie
        string nom_fichier_txt = "";

        /*
         *  initialisateur du nom de fichier
        */
        public txt_reader(string Xnom_fichier)
        {
            nom_fichier_txt = $"./{Xnom_fichier}";
        }

        public int[,] reader_file()
        {
            //lecture de ligne du fichier txt contenant la grille
            string[] tab_chaine = File.ReadAllLines(nom_fichier_txt);
            //decoupage en sous chaine a chaque espace
            string[] tab_sous_chaine = tab_chaine[0].Split(' ');
            //determine la taille en fonction du nombre d'élément présent dans le fichier 81 => 9*9 , 256 => 16*16
            int longueur = Convert.ToInt32(Math.Sqrt(tab_sous_chaine.Length));
            //declaration du tableau pour grille avec la taille
            int[,] tab_txt = new int[longueur, longueur];
            //parcoure les lignes du csv et pour le tableau
            for (int i = 0; i < longueur; i++)
            {
                //parcoure les nombres et les colonnes du tableau
                for (int j = 0; j < longueur; j++)
                {
                    //ecriture dans la case du caractere avec une conversion en entier 
                    tab_txt[i, j] = int.Parse(tab_sous_chaine[(i*longueur)+j]);
                }
            }
            //retour de la grille
            return tab_txt;
        }
    }
}
