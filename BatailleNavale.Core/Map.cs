namespace Core;

public class Map
{
    private int nbLignes;
    private int nbColonnes;
    protected int[,] map;
    protected List<JsonDecoder.Bateaux> bateauxPlaced;

    public Map(int nbLignes, int nbColonnes)
    {
        this.nbLignes = nbLignes;
        this.nbColonnes = nbColonnes;
        map = new int[nbLignes, nbColonnes];
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
        for (int lignes = 0; lignes <= nbLignes; lignes++)
        {
            for (int colonnes = 0; colonnes <= nbColonnes; colonnes++)
            {
                // Si on est en haut a gauche on créer un espace
                if (lignes == 0 && colonnes == 0)
                {
                    Console.Write("  ");
                    continue;
                }

                // On met un espace entre chaque caractère
                // Sauf si c'est la première colonne
                if (colonnes > 0)
                {
                    Console.Write(" ");
                }
                else
                {
                    Console.Write(lignes < 10 ? lignes + " " : lignes + "");
                    continue;
                }

                if (lignes == 0)
                {
                    // On met un espace entre chaque lettre
                    // On affiche les lettres dans l'ordre
                    Console.Write((char)(lettre+(colonnes-1)));
                    if (colonnes == nbColonnes)
                    {
                        Console.WriteLine();
                    }
                }
                else
                {
                    if (colonnes == nbColonnes)
                    {
                        Console.WriteLine("-");
                    }
                    else
                    {
                        Console.Write("-");
                    }
                }
            }
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
        bateauxPlaced.Add(bateau);
    }
}