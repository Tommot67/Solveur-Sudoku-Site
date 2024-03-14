/*
 * Rediged by Damien & Thomas TP2 IUT Robert Schuman
 * 
 */
 //Ensemble de librairie
using System;
using System.Collections.Generic;
using System.Threading;
using Sudoku; //dll , creer par NOUS

namespace Sudoku
{
    class resolveur
    {
        static void Main()
        {
            //inititlisation de la class du dll avec le nom du fichier pour la generation.
            //html_maker html = new html_maker("index(test).html");
            //appel de la procedure d'ecriture du début du fichier html
            //html.init_html();
            //inititlisation de la class du dll avec le nom du fichier pour la lecture de grille.
            csv_reader csv = new csv_reader("sudoku1-9*9.csv");
            //txt_reader txt = new txt_reader("simple.txt");
            try
            {
                //appel procedure de lecture du fichier contenant la grille
                var sudoku = csv.reader_file();
                //var sudoku = txt.reader_file();
                //appel de la procedure d'ecriture des tableaux dans le html avec le paramétre de la grille (base , partielle , complet)
                //html.entre_tab(sudoku, "base");
                //grille de test 
                var sudoku_test1 = new int[,]{
                {1, 0, 0, 0, 8, 0, 0, 0, 9},
                {0, 5, 0, 6, 0, 1, 0, 2, 0},
                {0, 0, 0, 5, 0, 3, 0, 0, 0},
                {0, 9, 6, 1, 0, 4, 8, 3, 0},
                {3, 0, 0, 0, 6, 0, 0, 0, 5},
                {0, 1, 5, 9, 0, 8, 4, 6, 0},
                {0, 0, 0, 7, 0, 5, 0, 0, 0},
                {0, 8, 0, 3, 0, 9, 0, 7, 0},
                {5, 0, 0, 0, 1, 0, 0, 0, 3}};
                var sudoku_test2 = new int[,]{
                {3, 0, 6, 5, 0, 8, 4, 0, 0},
                {5, 2, 0, 0, 0, 0, 0, 0, 0},
                {0, 8, 7, 0, 0, 0, 0, 3, 1},
                {0, 0, 3, 0, 1, 0, 0, 8, 0},
                {9, 0, 0, 8, 6, 3, 0, 0, 5},
                {0, 5, 0, 0, 9, 0, 6, 0, 0},
                {1, 3, 0, 0, 0, 0, 2, 5, 0},
                {0, 0, 0, 0, 0, 0, 0, 7, 4},
                {0, 0, 5, 2, 0, 6, 3, 0, 0}};

                affichetab(sudoku);
                Console.ReadKey();
                //appel procedure d'affichage de la grille (inititiale)
                //affichetab(sudoku);
                Console.WriteLine("brute force\n");
                affichetab(resolveurComplet2(sudoku));
                Console.WriteLine("backtracking\n");
                resolveurComplet(sudoku);
                affichetab(sudoku);
                //appel procedure d'affichage de la grille
                //affichetab(sudoku);
                //appel de la procedure d'ecriture des tableaux dans le html avec le paramétre de la grille (base , partielle , complet)
                //html.entre_tab(sudoku, "complet");
                //appel de la procedure d'ecriture de la fin du fichier html
                //html.close_html();
            }
            // affiche les erreurs
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadKey();

        }

        /*Procedure : afficheTab : affiche la grille de sudoku
        paramétre : 
          Xsudoku : int[,] (2D array) : grille de sudoku
         */
        public static void affichetab(int[,] Xsudoku)
        {
            int taille = Convert.ToInt32(Math.Sqrt(Xsudoku.GetLength(0)));
            //parcoure les lignes
            for (int i = 0; i < Xsudoku.GetLength(0); i++)
            {
                if(i%taille == 0 && i != 0)
                {
                    Console.WriteLine();
                }
                //parcoure les colonnes
                for (int j = 0; j < Xsudoku.GetLength(1); j++)
                {
                    if (j % taille == 0 && j != 0)
                    {
                        Console.Write("   ");
                    }
                    Console.Write(Xsudoku[i, j] + " ");
                }
                Console.Write("\n");
            }
            Console.Write('\n');
        }

        /*Fonction : resolveurComplet : complet la grille de sudoku 
        paramétre : 
          Xsudoku : int[,] (2D array) : grille de sudoku        
        retour:
          retour : int : retourne 5 (true) s'il est complet , retourne 15 (false) si la grille doit etre modifier dans le back-tracking , sinon il retourne 5 (true)
         */
        public static int resolveurComplet(int[,] Xsudoku)
        {
            int retour = 10;
            int taille = Xsudoku.GetLength(0);
            //parcoure les lignes
            for (int i = 0; i < taille && retour == 10 && retour != 15; i++)
            {
                //parcoure les colonnes
                for (int j = 0; j < taille && retour == 10 && retour != 15; j++)
                {
                    //verifie la non présence d'élément
                    if (Xsudoku[i, j] == 0)
                    {
                        //parcoure l'ensemble des nombres possible
                        for (int k = 1; k <= taille && retour == 10 && retour != 15; k++)
                        {
                            //verifie les conditions de complétion du sudoku (appel des fonctions)
                            if (resLigne(Xsudoku, taille, i, j, k) == false && resCol(Xsudoku, taille, i, j, k) == false && resSousTab(Xsudoku, taille, i, j, k) == false)
                            {
                                //ajoute le nombre dans la grille
                                Xsudoku[i, j] = k;
                                if (resolveurComplet(Xsudoku) == 5)
                                {
                                    retour = 5;
                                }
                                else
                                {
                                    Xsudoku[i, j] = 0;
                                }
                            }
                        }
                        if (retour != 5)
                        {
                            retour = 15;
                        }
                    }
                }
            }
            if (retour == 10)
            {
                retour = 5;
            }
            return retour;
        }

        /*Fonction : resolveurComplet : complet la grille de sudoku 
        paramétre : 
          Xsudoku : int[,] (2D array) : grille de sudoku        
        retour:
          retour : int : retourne 5 (true) s'il est complet , retourne 15 (false) si la grille doit etre modifier dans le back-tracking , sinon il retourne 5 (true)
         */
        public static int[,] resolveurComplet2(int[,] Xsudoku)
        {
            //clone la grille de base
            int [,] sudoku_comp = (int[,])Xsudoku.Clone();
            //détermine la taille de la grille
            int taille = Xsudoku.GetLength(0);
            //variable pour verification de placement
            bool placer = false;
            //parcoure les lignes
            for(int i = 0 ; i < taille ; i++)
            {
                //parcoure les colonnes
                for(int j = 0 ; j < taille ; j++)
                {
                    //verifie la non présence d'élément
                    if (Xsudoku[i, j] == 0)
                    {
                        //parcoure l'ensemble des nombres possible
                        for (int k = 1; k <= taille && placer == false ; k++)
                        {
                            //verifie les conditions de complétion du sudoku (appel des fonctions)
                            if (resLigne(sudoku_comp, taille, i, j, k) == false && resCol(sudoku_comp, taille, i, j, k) == false && resSousTab(sudoku_comp, taille, i, j, k) == false && sudoku_comp[i,j] < k)
                            {
                                //ajoute le nombre dans la grille
                                sudoku_comp[i, j] = k;
                                placer = true;
                                //Console.WriteLine("passe1");
                            }
                        }
                        //verifie si il y a aucun élément qui a été placer et si il est en bout de ligne à gauche
                        if (placer == false && j == 0)
                        {
                            //supprime l'élément de la case qui a été mit
                            sudoku_comp[i, j] = 0;
                            //passe à la ligne précédente
                            i--;
                            //passe en bout de ligne à droite
                            j = taille - 1;
                            //verifie et boucle si il y a déjà un élément de dans la grille de base
                            while(Xsudoku[i,j] != 0)
                            {
                                //verifie si il est en bout de ligne à gauche
                                if (j == 0)
                                {
                                    //passe à la ligne précédente
                                    i--;
                                    //passe en bout de ligne à droite
                                    j = taille - 1;
                                }
                                else
                                {
                                    //passe à la colone précédente
                                    j--;
                                }
                            }
                            //passe à la colone précédente pour éviter un probleme dans le for
                            j--;
                        }
                        else if (placer == false)
                        {
                            //supprime l'élément de la case qui a été mit
                            sudoku_comp[i, j] = 0;
                            //passe à la colone précédente
                            j--;
                            //verifie et boucle si il y a déjà un élément de dans la grille de base
                            while (Xsudoku[i, j] != 0)
                            {
                                //verifie si il est en bout de ligne à gauche
                                if (j == 0)
                                {
                                    //passe à la ligne précédente
                                    i--;
                                    //passe en bout de ligne à droite
                                    j = taille - 1;
                                }
                                else
                                {
                                    //passe à la colone précédente
                                    j--;
                                }
                            }
                            //passe à la colone précédente pour éviter un probleme dans le for
                            j--;
                        }
                        //réinialise la variable pour verification de placement
                        placer = false;
                    }
                }
            }
            return sudoku_comp;
        }

        /*Fonction : resLigne : verifie si le nombre n'est pas encore présent dans la ligne
        paramétre : 
          Xsudoku : int[,] (2D array) : grille de sudoku
          ligne : int : ligne dans la quel le nombre sera mis
          colonne : int : colonne dans la quel le nombre sera mis
          Xc : int : le nombre que l'on veut placer          
        retour:
          placer : bool : retourne false s'il n'y a pas le nombre et true s'il est présent
         */
        public static bool resLigne(int[,] Xsudoku,int taille, int ligne, int colonne, int Xc)
        { 
            bool placer = false;
            //parcoure la ligne
            for (int i = 0; i < taille; i++)
            {
                if (Xsudoku[i, colonne] != 0 && Xsudoku[i, colonne] == Xc)
                {
                    placer = true;
                }
            }
            return placer;
        }

        /*Fonction : resCol : verifie si le nombre n'est pas encore présent dans la colonne
        paramétre : 
          Xsudoku : int[,] (2D array) : grille de sudoku
          ligne : int : ligne dans la quel le nombre sera mis
          colonne : int : colonne dans la quel le nombre sera mis
          Xc : int : le nombre que l'on veut placer          
        retour:
          placer : bool : retourne false s'il n'y a pas le nombre et true s'il est présent
         */
        public static bool resCol(int[,] Xsudoku, int taille, int ligne, int colonne, int k)
        {
            bool placer = false;
            //parcoure la colonne
            for (int i = 0; i < taille; i++)
            {
                if (Xsudoku[ligne, i] != 0 && Xsudoku[ligne, i] == k)
                {
                    placer = true;
                }
            }
            return placer;
        }

        /*Fonction : resSousTab : verifie si le nombre n'est pas encore présent dans le sous tableau
        paramétre : 
          Xsudoku : int[,] (2D array) : grille de sudoku
          ligne : int : ligne dans la quel le nombre sera mis
          colonne : int : colonne dans la quel le nombre sera mis
          Xc : int : le nombre que l'on veut placer          
        retour:
          placer : bool : retourne false s'il n'y a pas le nombre et true s'il est présent
         */
        public static bool resSousTab(int[,] Xsudoku, int taille ,int ligne, int colonne, int Xc)
        {
            //taille des sous tableaux
            int taille_sous = Convert.ToInt32(Math.Sqrt(taille));
            bool placer = false;
            //(taille_sous*(ligne/taille_sous)) : determine la case en haut à gauche du sous tableau
            //(taille_sous*(ligne/taille_sous+1)) : determine la case les extremitées du sous tableau en ligne ou en colonne
            for (int i = (taille_sous*(ligne/taille_sous)); i < (taille_sous*(ligne/taille_sous+1)) ; i++)
            {    
                for (int j = (taille_sous*(colonne/taille_sous)); j < (taille_sous*(colonne/taille_sous+1)) ; j++)
                {
                    if (Xsudoku[i, j] == Xc)
                    {
                        placer = true;
                    }
                
                }
            }
            return placer;
        }
    }
}
