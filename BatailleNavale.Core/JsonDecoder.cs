namespace Core;

public class JsonDecoder
{
    /**
     * Décode chaque bateau en fonction de la taille et du nom
     */
    public class Bateaux
    {
        public List<int[,]> coordonnees { get; set; }
        public int taille { get; set; }
        public string nom { get; set; }
        
        public Bateaux(int taille, string nom)
        {
            this.taille = taille;
            this.nom = nom;
        }
    }

    /**
     * Déclaration des attributs qui sont nécéssaire pour les données JSON
     */
    public class JsonDatas
    {
        public int nbLignes { get; set; }
        public int nbColonnes { get; set; }
        public List<Bateaux> bateaux { get; set; }
        
        public JsonDatas(int nbLignes, int nbColonnes, List<Bateaux> bateaux)
        {
            this.nbLignes = nbLignes;
            this.nbColonnes = nbColonnes;
            this.bateaux = bateaux;
        }
    }
}