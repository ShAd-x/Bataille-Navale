namespace Core;

public class Map
{
    public int nbLignes;
    public int nbColonnes;
    public char[,] map;
    public List<JsonDecoder.Bateaux> bateauxPlaced;

    public Map(int nbLignes, int nbColonnes)
    {
        this.nbLignes = nbLignes;
        this.nbColonnes = nbColonnes;
        this.map = new char[nbLignes, nbColonnes];
        this.bateauxPlaced = new List<JsonDecoder.Bateaux>();
        FillMapWithEmptyChar();
    }

    /**
     * Rempli la carte au début de partie avec les cases vides
     */
    private void FillMapWithEmptyChar()
    {
        map = new char[nbLignes, nbColonnes];
        for (int lignes = 0; lignes < nbLignes; lignes++)
        {
            for (int colonnes = 0; colonnes < nbColonnes; colonnes++)
            {
                map[lignes, colonnes] = '-';
            }
        }
    }

    /**
     * Affiche la carte sur la console
     * Avec les bateaux placés
     *
     * @param int joueur
     * @return void
     */
    public void DisplayMapWithBoat(int joueur = 0)
    {
        char lettre = (char)65; // A
        for (int lignes = 0; lignes < nbLignes; lignes++)
        {
            for (int colonnes = 0; colonnes < nbColonnes; colonnes++)
            {
                // On génère la ligne du haut
                if (lignes == 0 && colonnes == 0)
                {
                    for (int i = 0; i <= nbColonnes; i++)
                    {
                        if (i == 0)
                        {
                            Console.Write("   ");
                        }

                        if (i != nbColonnes)
                        {
                            Console.Write((char)(lettre + i) + " ");
                        }
                        else
                        {
                            Console.WriteLine("");
                        }
                    }
                }

                // On génère la colonne de gauche
                if (colonnes == 0)
                {
                    Console.Write(lignes + 1 >= 10 ? lignes + 1 + " " : lignes + 1 + "  ");
                }

                if (map[lignes, colonnes] == 'X')
                {
                    if (joueur == 2)
                    {
                        Console.Write("- ");
                    } 
                    else 
                    {
                        Console.Write(map[lignes, colonnes] + " ");
                    }
                }
                else if (map[lignes, colonnes] == 'O')
                {
                    if (joueur == 1)
                    {
                        Console.Write("- ");
                    }
                    else 
                    {
                        Console.Write(map[lignes, colonnes] + " ");
                    }
                }
                else
                {
                    Console.Write(map[lignes, colonnes] + " ");
                }
            }
            Console.WriteLine();
        }
    }

    /**
     * Vérifie si la position est disponible
     *
     * @param int x
     * @param int y
     * @return bool
     */
    public bool IsAvailable(int x, int y)
    {
        return map[x, y] == '-';
    }
    
    public bool IsPlayerBoat(int x, int y, int joueur)
    {
        return map[x, y] == (joueur == 1 ? 'X' : 'O');
    }

    /**
     * Ajoute un bateau à la liste des bateaux placés
     *
     * @param JsonDecoder.Bateaux bateau
     * @param int vertical
     * @param int horizontal
     * @param int direction
     * @param int joueur
     * @return void
     */
    public void AddBateauToMap(JsonDecoder.Bateaux bateau, int vertical, int horizontal, int direction, int joueur)
    {
        bateauxPlaced.Add(bateau);
        for (int i = 0; i < bateau.taille; i++)
        {
            map[vertical, horizontal] = (joueur == 1 ? 'X' : 'O');
            bateau.AddCoord(vertical, horizontal);
            
            // On incrémente la taille du bateau en fonction de la direction
            if (direction == 0)
                horizontal++;
            else
                vertical++;
        }
    }
    
    /**
     * Retire un bateau de la liste des bateaux placés et le retire de la carte
     *
     * @param JsonDecoder.Bateaux bateau
     * @return void
     */
    public void RemoveBateauFromMap(JsonDecoder.Bateaux bateau)
    {
        Console.WriteLine("Coulé !");
        bateauxPlaced.Remove(bateau);
        foreach (var coord in bateau.coordonnees)
        {
            map[coord.Item1, coord.Item2] = '-';
        }
    }

    /**
     * Retourne le gagnant de la partie
     *
     * @return int
     */
    public int GetWinner()
    {
        bool player1IsAlive = false;
        bool player2IsAlive = false;
        
        // Pour chaque bateau encore en vie
        foreach (var bateau in bateauxPlaced)
        {
            // Si le bateau appartient au joueur 1 ou au joueur 2
            // On met à jour les booléens pour savoir si les joueurs ont encore des bateaux
            if (map[bateau.coordonnees[0].Item1, bateau.coordonnees[0].Item2] == 'X')
            {
                player1IsAlive = true;
            }
            else if (map[bateau.coordonnees[0].Item1, bateau.coordonnees[0].Item2] == 'O')
            {
                player2IsAlive = true;
            }
        }
        
        // Retourne 0 si pas de gagnant (les deux joueurs sont encore en vie)
        // Retourne 1 si le joueur 1 a gagné
        // Retourne 2 si le joueur 2 a gagné
        return (player1IsAlive && player2IsAlive ? 0 : player1IsAlive ? 1 : 2);
    }
}