/*
 * Rediged by Damien & Thomas TP2 IUT Robert Schuman
 * 
 * csc -r:Programme_html.dll .\Programme.cs "pour compiler"
 *
 */
 //Ensemble de librairie
using System;
using System.Collections.Generic;
using Sudoku; //dll , creer par NOUS
public struct coord{
	public int x;
	public int y;
}

namespace Sudoku
{
    class resolveur
    {
        static void Main()
        {
            //inititlisation de la class du dll avec le nom du fichier pour la generation.
            html_maker html = new html_maker("index.html");
            //appel de la procedure d'ecriture du début du fichier html
            html.init_html();
            //inititlisation de la class du dll avec le nom du fichier pour la lecture de grille.
            ////csv_reader csv = new csv_reader("sudoku1.csv");
			txt_reader txt =  new txt_reader("simple.txt");
            try
            {
                //appel procedure de lecture du fichier contenant la grille
                ////var sudoku = csv.reader_file();
				var sudoku = txt.reader_file();
                //appel de la procedure d'ecriture des tableaux dans le html avec le paramétre de la grille (base , partielle , complet)
                html.entre_tab(sudoku, "base");
                //grille de test 
                var sudoku_test1 = new int[,]{
				{4, 0, 0, 0, 9, 1, 0, 0, 0},
				{0, 7, 0, 0, 8, 0, 0, 9, 0},
				{0, 0, 0, 0, 0, 0, 2, 0, 0},
				{3, 0, 6, 0, 7, 0, 0, 5, 0},
				{0, 8, 2, 0, 0, 6, 0, 4, 0},
				{5, 0, 0, 0, 0, 0, 1, 0, 0},
				{8, 1, 0, 3, 0, 0, 5, 0, 2},
				{0, 0, 0, 8, 0, 0, 0, 0, 1},
				{6, 0, 0, 0, 0, 4, 0, 0, 8}};

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
				soluceFinder(sudoku);
                //appel de la procedure d'ecriture des tableaux dans le html avec le paramétre de la grille (base , partielle , complet)
                html.entre_tab(sudoku, "partielle");
				soluceFinderComplet(sudoku);
				html.entre_tab(sudoku, "complet");
                //appel de la procedure d'ecriture de la fin du fichier html
                html.close_html();
				Console.ForegroundColor = ConsoleColor.Green;
				affichetab(sudoku);
				Console.ReadKey();
            }
            // affiche les erreurs
            catch (Exception e)
            {
                Console.WriteLine(e);
				Console.ReadKey();
            }
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
		/* 
		fonct : bool : resPartielle
			Vérifie que la grille est complété à moitié
		parametres : 
			int[,] : Xsudoku : tableau du sudoku initial
		*/
		public static bool resPartielle(int [,] Xsudoku,int Xlimite){
			bool estPartielle = false;
			int stop_partielle = Xlimite/2;
			int compteur=0;

			for(int i = 0; i<Xsudoku.GetLength(0);i++){
				for(int j = 0; j<Xsudoku.GetLength(0);j++){
					if(Xsudoku[i,j]==0){ 			// verifie que la case est résolue
						compteur++;                 // compte le nombre de case résolue
					}
				}
			}
			if(compteur<=stop_partielle){ // renvoie vrai si la grille est résolue à moitié 
				estPartielle=true;
			}
			return estPartielle;
		}
		/*
        Procedure : soluceFinder : résout la grille de sudoku à moitié avec plusieurs techniques "humaines"
        paramétre : 
          Xsudoku : int[,] (2D array) : grille de sudoku
		*/
		public static void soluceFinder(int[,] Xsudoku){
			int taille = Xsudoku.GetLength(0);
			Dictionary<coord,List<int>> dicoSoluce = new Dictionary<coord,List<int>>();
			List<int> possibleSoluce = new List<int>();
			coord temp; // coordonnee temporaire
			int compteur = 0;
			int limit_init = nbZero(Xsudoku);
			while(resPartielle(Xsudoku,limit_init)!=true){ // tant que la grille n'est pas completé à moitié
				compteur++;
				for(int i = 0; i < taille && resPartielle(Xsudoku,limit_init)!=true; i++){ // parcours les lignes
					for (int j = 0; j < taille && resPartielle(Xsudoku,limit_init)!=true; j++){ // parcours les colonnes
						
						if (Xsudoku[i, j] == 0){ // si il n'y a rien
							possibleSoluce = new List<int>();
							temp = new coord();
							temp.x = i;
							temp.y = j;
							for (int k = 1; k <= taille; k++) // test si on peut placer un chiffre
							{
								//verifie les conditions de complétion du sudoku (appel des fonctions)
								if (resLigne(Xsudoku, taille, i, j, k) == false && resCol(Xsudoku, taille, i, j, k) == false && resSousTab(Xsudoku, taille, i, j, k) == false)
								{
									possibleSoluce.Add(k);
									
									
								}
							}
							if(possibleSoluce.Count==1){ // si il n'y a qu'une solution d'après l'algo précédent: place le chiffre
								Xsudoku[i, j] = possibleSoluce[0];
								dicoSoluce.Remove(temp);
								compteur=0;
							}
							else if(dicoSoluce.ContainsKey(temp)!=true){ // si il n'y a pas de solution dans le dico pour cette coordonnee
								dicoSoluce.Add(temp,possibleSoluce); // ajoute dans le dico
							}
							else{		// si solution existe dans le dico
								dicoSoluce[temp]=possibleSoluce;  // ecrase avec la nouvelle liste de solution
							}	
						}
					}
				}
				
				if(compteur>1){                         // si la solution unique échoue plus d'une fois
					placepossible(dicoSoluce,Xsudoku);  // essaye de débloquer le programme avec la méthode des fourmis
					compteur=0;
				}
				
			}
		}
				/*
        Procedure : soluceFinderComplet : résout la grille de sudoku avec plusieurs techniques "humaines"
        paramétre : 
          Xsudoku : int[,] (2D array) : grille de sudoku
		*/
		public static void soluceFinderComplet(int[,] Xsudoku){
			int taille = Xsudoku.GetLength(0);
			Dictionary<coord,List<int>> dicoSoluce = new Dictionary<coord,List<int>>();
			List<int> possibleSoluce = new List<int>();
			coord temp; // coordonnee temporaire
			int compteur = 0;
			while(test(Xsudoku)!=true){ // tant que la grille n'est pas completé
				compteur++;
				for(int i = 0; i < taille && test(Xsudoku)!=true; i++){ // parcours les lignes
					for (int j = 0; j < taille && test(Xsudoku)!=true; j++){ // parcours les colonnes
						
						if (Xsudoku[i, j] == 0){ // si il n'y a rien
							possibleSoluce = new List<int>();
							temp = new coord();
							temp.x = i;
							temp.y = j;
							for (int k = 1; k <= taille; k++) // test si on peut placer un chiffre
							{
								//verifie les conditions de complétion du sudoku (appel des fonctions)
								if (resLigne(Xsudoku, taille, i, j, k) == false && resCol(Xsudoku, taille, i, j, k) == false && resSousTab(Xsudoku, taille, i, j, k) == false)
								{
									possibleSoluce.Add(k);
									
									
								}
							}
							if(possibleSoluce.Count==1){ // si il n'y a qu'une solution d'après l'algo précédent: place le chiffre
								Xsudoku[i, j] = possibleSoluce[0];
								dicoSoluce.Remove(temp);
								compteur=0;
							}
							else if(dicoSoluce.ContainsKey(temp)!=true){ // si il n'y a pas de solution dans le dico pour cette coordonnee
								dicoSoluce.Add(temp,possibleSoluce); // ajoute dans le dico
							}
							else{		// si solution existe dans le dico
								dicoSoluce[temp]=possibleSoluce;  // ecrase avec la nouvelle liste de solution
							}	
						}
					}
				}
				
				if(compteur>1){                         // si la solution unique échoue plus d'une fois
					placepossible(dicoSoluce,Xsudoku);  // essaye de débloquer le programme avec la méthode des fourmis
					compteur=0;
				}
				
			}
		}
		/* 
		procedure : placepossible : place un nombre à une coordonnee ou une solution n'est pas commune au reste du sous tableau
        paramétre : 
			Xdico : Dictionary<coord,List<int>> : dictionnaire des solutions possible pour chaque case du sudoku
			Xsudoku : int[,] (2D array) : grille de sudoku que l'on veut résoudre
		*/
		public static void placepossible(Dictionary<coord,List<int>> Xdico, int[,] Xsudoku){
			coord temp;
			coord temp_place;
			int taille_sous = (int)(Math.Sqrt(Xsudoku.GetLength(0)));
			List<int> listeDouble = new List<int>();
			int compteur=0;
			int placable;
			bool placer = false;
			temp_place = new coord();
			foreach(KeyValuePair<coord,List<int>> kvp in Xdico){ // parcour le dictionnaire de solution
				listeDouble.Clear(); // parcours le sous tableau contenant la coordonnee
				 for (int i = (taille_sous*(kvp.Key.x/taille_sous)); i < (taille_sous*(kvp.Key.x/taille_sous+1)) && placer != true; i++){
					for (int j = (taille_sous*(kvp.Key.y/taille_sous)); j < (taille_sous*(kvp.Key.y/taille_sous+1)) && placer != true; j++){
						temp = new coord(); // crée une nouvelle coordonnée correspondant à la clé utilisé
						temp.x=i;
						temp.y=j;
						if(Xdico.ContainsKey(temp)){ 
							foreach(int soluce in Xdico[temp]){ // ajoute l'ensemble des solutions dans une liste propre au sous tableau
								listeDouble.Add(soluce);
								}
						}
					}
				}
				foreach(int soluce in listeDouble){ // test pour chaque nombre de la liste du sous tableau
					compteur=0;
					foreach(int doublon in listeDouble){ // determine si il y a un doublon dans la liste des solution
						if(soluce == doublon){
							compteur++;
						}
					}
					if(compteur==1 && placer != true){ // si le nombre actuel n'a pas de doublon
						placable=soluce;			// reparcours le sous tableau
						for (int i = (taille_sous*(kvp.Key.x/taille_sous)); i < (taille_sous*(kvp.Key.x/taille_sous+1)) && placer != true; i++){    
							for (int j = (taille_sous*(kvp.Key.y/taille_sous)); j < (taille_sous*(kvp.Key.y/taille_sous+1)) && placer != true; j++){
								temp = new coord(); // nouvelle coordonnée correspondant à la clé
								temp.x=i;
								temp.y=j;
								if(Xdico.ContainsKey(temp)){
									for(int solution = 0; solution < Xdico[temp].Count && placer != true;solution++){ // parcours la liste des solutions
										if(Xdico[temp][solution]==placable){ // si la solution correspond à la solution placable
											Xsudoku[temp.x,temp.y]=Xdico[temp][solution]; // il la place
											//Xdico.Remove(temp);
											placer = true; // utilisé hors de la boucle foreach
											temp_place.x=i; // utilisé hors de la boucle foreach
											temp_place.y=j;
										}
									}
								}

							}
						}
					}
				}
				
			}
			if(placer == true){ // si on a placer un nombre, supprime de la liste des solutions
				Xdico.Remove(temp_place);
			}
		}
		/*
		fonct ; resLigne : vérifie si le nombre n'est pas présent dans la ligne
		parametres : 
          Xsudoku : int[,] (2D array) : grille de sudoku
          ligne : int : ligne dans la quel le nombre sera mis
          colonne : int : colonne dans la quel le nombre sera mis
          Xc : int : le nombre que l'on veut placer  
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
        public static bool resCol(int[,] Xsudoku, int taille, int ligne, int colonne, int Xc)
        {
            bool placer = false;
            //parcoure la colonne
            for (int i = 0; i < taille; i++)
            {
                if (Xsudoku[ligne, i] != 0 && Xsudoku[ligne, i] == Xc)
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
            int taille_sous = (int)(Math.Sqrt(taille));
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
		/*
		fonct : int[,] : arrayCopy
			effectue une copie d'un tableau
		parametres : 
			input : int[,] : tableau que l'on veut copier
		retour : result : tableau copié
			
		*/
        public static int[,] arrayCopy(int[,] input)
        {
            int[,] result = new int[input.GetLength(0), input.GetLength(1)]; //Create a result array that is the same length as the input array
            for (int x = 0; x < input.GetLength(0); x++) //Iterate through the horizontal rows of the two dimensional array
            {
                for (int y = 0; y < input.GetLength(1); y++) //Iterate throught the vertical rows, to add more dimensions add another for loop for z
                {
                    result[x, y] = input[x, y]; //Change result x,y to input x,y
                }
            }
            return result;
        }
		
		/* 
		fonct : bool : test
			test si la grille est résolut ou non
		parametres : 
			Xsudoku : int[,] : tableau du sudoku initial
		retour : estJUste : renvoie si la grille est résolut
		*/
		public static bool test(int [,] Xsudoku){
			bool estJuste = true;
			for(int i = 0; i<Xsudoku.GetLength(0) & estJuste!=false;i++){
				for(int j = 0; j<Xsudoku.GetLength(0) & estJuste!=false;j++){
					if(Xsudoku[i,j]==0){ 			// la présence de 0 indique que la fiche n'est pas résolut
						estJuste=false;
					}
				}
			}
			return estJuste;
		}
		/*
		fonct : int : nbZero
			determine le nombre de zero contenue dans la grille
		parametres :
			Xsudoku : int[,] : tableau du sudoku
		retour : nbZer : renvoie le nombre de zero
		*/
		public static int nbZero(int [,] Xsudoku){
			int nbZer = 0;
			for(int i = 0; i<Xsudoku.GetLength(0);i++){
				for(int j = 0; j<Xsudoku.GetLength(0);j++){
					if(Xsudoku[i,j]==0){ 			// la présence de 0 indique que la fiche n'est pas résolut
						nbZer++;
					}
				}
			}
			return nbZer;
		}
		
    }

}
