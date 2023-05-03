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
        for (int i = 0; i <= nbLignes; i++)
        {
            for (int j = 0; j <= nbColonnes; j++)
            {
                if (i == 0)
                {
                    // On met un espace entre chaque lettre
                    Console.Write(" ");
                    // On affiche les lettres dans l'ordre
                    Console.Write(j == 0 ? "" : (char)(lettre+(j-1)));
                    if (j == nbColonnes)
                    {
                        Console.WriteLine();
                    }
                }

                if (j == 0)
                {
                    Console.Write(i == 0 ? " " : i + "\n");
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