namespace Core;

public class Map
{
    private int nbLignes;
    private int nbColonnes;
    private char[,] map;
    protected List<JsonDecoder.Bateaux> bateauxPlaced;

    public Map(int nbLignes, int nbColonnes)
    {
        this.nbLignes = nbLignes;
        this.nbColonnes = nbColonnes;
        map = fillMap();
    }

    public char[,] fillMap()
    {
        char[,] map = new char[nbLignes, nbColonnes];
        for (int lignes = 0; lignes < nbLignes; lignes++)
        {
            for (int colonnes = 0; colonnes < nbColonnes; colonnes++)
            {
                map[lignes, colonnes] = '-';
            }
        }

        return map;
    }

    /**
     * Affiche la carte sur la console
     * Avec les bateaux placés
     *
     * return void
     */
    public void displayMap()
    {
        Console.WriteLine("Carte :");
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

                if (colonnes == 0)
                {
                    Console.Write(lignes+1 >= 10 ? lignes+1 + " " : lignes+1 + "  ");   
                }
                
                Console.Write(map[lignes, colonnes] + " ");
            }

            Console.WriteLine();
        }
    }
    
    /**
     * Ajouter un bateau sur la carte
     *
     * <param name="bateau"></param>
     * return void
     */
    public void addBateau(JsonDecoder.Bateaux bateau)
    {
        Console.WriteLine(bateau);
        bateauxPlaced.Add(bateau);
    }

    public bool IsAvailable(int x, int y)
    {
        return map[x, y] == '-';
    }
}