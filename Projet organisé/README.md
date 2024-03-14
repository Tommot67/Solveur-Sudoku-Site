
# Dll explication
***

## Html

1. Initialise avec le nom du fichier html. **Default path : ./**

    * Commande :
    ```c#
    html_maker [name] =  new htmk_maker("[file name]");
    ```

2. Ecriture du début du fichier depuis le fichier template.txt.<br>Variable de coupure : "alt=\"grilles\"" , il faut le modifier dans le code du dll (**doit être unique dans le template**).

    * Commande :
    ```c#
    [name].init_html();
    ```

3. Intégration des grilles, mise en paramètre et format (base , partielle , complet).

     * Commande :
     ```c#
     [name].entre_tab([array] , [format]);
     ```

4. Ecriture de la fin du fichier depuis le fichier template.txt.

     * Commande :
     ```c#
     [name].close_html();
    ```
***
## Csv

**Format nombre seulement**

1. Initialise avec le nom du fichier csv. **Default path : ./**

    * Commande :
    ```c#
    csv_reader [name] =  new csv_reader("[file name]");
    ```

2. Lecture du fichier csv, retourne la grille

    * Commande :
    ```c#
    [name].reader_file();
    ```
***
## Txt

**Format nombre seulement**

1. Initialise avec le nom du fichier txt. **Default path : ./**

    * Commande :
    ```c#
    txt_reader [name] =  new txt_reader("[file name]");
    ```

2. Lecture du fichier txt, retourne la grille

    * Commande :
    ```c#
    [name].reader_file();
    ```
***
## Instruction de build

* Commande build dll:
    ```sh
    mcs -target:library sudoku_dll.cs
    ```
* Commande build programme principal
    ```sh
    mcs -r:sudoku_dll.dll [name of programme]
    ```
***
## Aide ou tips

Il est préférable de mettre l'ensemble des fichiers qui sont nécessaire au sein d'un même dossier.
Il vous sera plus facile d'executer les commandes de compilation (build).
