namespace Core;

public class Map
{
    private int nbLignes;
    private int nbColonnes;
    public char[,] map;

    public Map(int nbLignes, int nbColonnes)
    {
        this.nbLignes = nbLignes;
        this.nbColonnes = nbColonnes;
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
     * return void
     */
    public void displayMap()
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

                if (colonnes == 0)
                {
                    Console.Write(lignes + 1 >= 10 ? lignes + 1 + " " : lignes + 1 + "  ");
                }

                Console.Write(map[lignes, colonnes] + " ");
            }

            Console.WriteLine();
        }
    }

    public bool IsAvailable(int x, int y)
    {
        return map[x, y] == '-';
    }
    
    public bool IsHit(int x, int y)
    {
        return !IsAvailable(x, y);
    }
}