namespace Core;

public class Map
{
    public int nbLignes;
    protected int nbColonnes;
    protected int[,] map;
    
    public Map(int nbLignes, int nbColonnes)
    {
        this.nbLignes = nbLignes;
        this.nbColonnes = nbColonnes;
        map = new int[nbLignes, nbColonnes];
    }

    /**
     * Ajouter un bateau sur la carte
     *
     * <param name="bateaux"></param>
     * <param name="x"></param>
     * <param name="y"></param>
     * return void
     */
    public void addBateau(JsonDecoder.Bateaux bateaux, int x, int y)
    {
        
    }
}