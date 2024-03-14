**********
***Projet réaliser par Brunstein Damien et Rott Thomas***
**********

# Version 1 du résolveur de sudoku

## Introduction
Cette version du résolveur de sudoku utilise une méthode brute-force.
C'est-à-dire une méthode dite idiote, pas humaine.

## Fonctionnement de la méthode
Cette méthode parcours copie la grille initiale, puis la parccours et place le premier élément qui est possible dans la case.
Si il n'arrive pas à placer, il doit revenir dans la première case précédente et mettre le suivant élément possible. Si il ne peut pas non plus il va ainsi de suite à la case précédente. Le tout en testant si il peut le placer.

# Version 2 du résolveur de sudoku

## Introduction
Cette version du résolveur de sudoku est divisé en 2 version :
* **la version compléte** : permet de résoudre entièrement un sudoku de n'importe quelle taille
* **la version partielle** : permet de résoudre partiellement un sudoku de n'importe quelle taille

## Méthodes employé
Les méthodes employé pour la résolution sont les suivantes :
* **Solution simple**
* **Solution unique**

### Solution simple
Le résolveur va tout d'abord chercher à trouver une case n'ayant qu'une seule solution possible

### Solution unique
Si la méthode précédente échoue, le résolveur va regarder pour chaque sous tableau
dans le dictionnaire une case ayant une solution unique à celle-ci dans le sous tableau.
Si il en trouve une, il l'a place et retourne à la méthode simple

## Fonctionnement des méthodes

### Méthode simple
Pour chaque case vide (contenant un 0) le résolveur vérifie si il peut placer un nombre : si il le peux, il le place dans une liste.
À la fin de chaque passage par case le résolveur vérifie le nombre de solution, si il n'y en a qu'une :
* il la place (et supprimme la liste de solution associé à la case si celle ci existe),
* sinon : il ajoute la liste de solution au dictionnaire de solution (utiliser par la seconde méthode).


### Méthode unique
Cette méthode reprend le dictionnaire de solution et effectue un passage par sous tableau.
Dans chaque sous tableau, le résolveur vérifie si les cases sont déja résolue, si elles ne le sont pas:
* il ajoute les solutions des cases non résolue dans une liste temporaire
* il vérifie si il y a pas de doublon dans la liste, si il n'y a que des doublons :
    * il passe à autre chose,
    * sinon :
        * il effectue un second passage pour identifié la position de la solution unique, et la place à cette même position
        Le résolveur retourne alors à la méthode simple

# Version 3 du résolveur de sudoku

**Méthode backtracking utilisant la récursivité**
