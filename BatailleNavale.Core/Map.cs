namespace Core;

public class Map
{
    public int NbLignes;
    public int NbColonnes;
    private char[,] _map;
    public List<JsonDecoder.Bateaux> BateauxPlaced;

    public Map(int nbLignes, int nbColonnes)
    {
        this.NbLignes = nbLignes;
        this.NbColonnes = nbColonnes;
        this._map = new char[nbLignes, nbColonnes];
        this.BateauxPlaced = new List<JsonDecoder.Bateaux>();
        FillMapWithEmptyChar();
    }

    /**
     * Rempli la carte au début de partie avec les cases vides
     */
    private void FillMapWithEmptyChar()
    {
        _map = new char[NbLignes, NbColonnes];
        for (int lignes = 0; lignes < NbLignes; lignes++)
        {
            for (int colonnes = 0; colonnes < NbColonnes; colonnes++)
            {
                _map[lignes, colonnes] = '-';
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
        for (int lignes = 0; lignes < NbLignes; lignes++)
        {
            for (int colonnes = 0; colonnes < NbColonnes; colonnes++)
            {
                // On génère la ligne du haut
                if (lignes == 0 && colonnes == 0)
                {
                    for (int i = 0; i <= NbColonnes; i++)
                    {
                        if (i == 0)
                        {
                            Console.Write("   ");
                        }

                        if (i != NbColonnes)
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

                if (_map[lignes, colonnes] == 'X')
                {
                    if (joueur == 2)
                    {
                        Console.Write("- ");
                    } 
                    else 
                    {
                        Console.Write(_map[lignes, colonnes] + " ");
                    }
                }
                else if (_map[lignes, colonnes] == 'O')
                {
                    if (joueur == 1)
                    {
                        Console.Write("- ");
                    }
                    else 
                    {
                        Console.Write(_map[lignes, colonnes] + " ");
                    }
                }
                else
                {
                    Console.Write(_map[lignes, colonnes] + " ");
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
        return _map[x, y] == '-';
    }
    
    public bool IsPlayerBoat(int x, int y, int joueur)
    {
        return _map[x, y] == (joueur == 1 ? 'X' : 'O');
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
        BateauxPlaced.Add(bateau);
        for (int i = 0; i < bateau.taille; i++)
        {
            _map[vertical, horizontal] = (joueur == 1 ? 'X' : 'O');
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
        BateauxPlaced.Remove(bateau);
        foreach (var coord in bateau.Coordonnees)
        {
            _map[coord.Item1, coord.Item2] = '-';
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
        foreach (var bateau in BateauxPlaced)
        {
            // Si le bateau appartient au joueur 1 ou au joueur 2
            // On met à jour les booléens pour savoir si les joueurs ont encore des bateaux
            if (_map[bateau.Coordonnees[0].Item1, bateau.Coordonnees[0].Item2] == 'X')
            {
                player1IsAlive = true;
            }
            else if (_map[bateau.Coordonnees[0].Item1, bateau.Coordonnees[0].Item2] == 'O')
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