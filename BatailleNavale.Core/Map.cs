namespace Core;

public class Map
{
    private int nbLignes;
    private int nbColonnes;
    public char[,] map;
    public List<JsonDecoder.Bateaux> bateauxPlaced;

    public Map(int nbLignes, int nbColonnes)
    {
        this.nbLignes = nbLignes;
        this.nbColonnes = nbColonnes;
        this.bateauxPlaced = new List<JsonDecoder.Bateaux>();
        FillMap();
    }

    /**
     * Rempli la carte au début de partie avec les bateaux et les cases vides
     */
    private void FillMap()
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
    public void DisplayMap(int joueur = 0)
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

    /**
     * Ajoute un bateau à la liste des bateaux placés
     *
     * @param JsonDecoder.Bateaux bateau
     * @return void
     */
    public void AddBateau(JsonDecoder.Bateaux bateau)
    {
        bateauxPlaced.Add(bateau);
    }
}