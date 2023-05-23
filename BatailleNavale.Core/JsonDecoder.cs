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
            this.coordonnees = new List<int[,]>();
        }
        
        public void AddCoord(int y, int x)
        {
            coordonnees.Add(new int[y, x]);
        }

        public bool IsABateau(int y, int x)
        {
            foreach (var coord in coordonnees)
            {
                int numRows = coord.GetLength(0);
                int numCols = coord.GetLength(1);
        
                if (y >= 0 && y < numRows && x >= 0 && x < numCols)
                {
                    if (coord[y, x] == 1)
                    {
                        return true;
                    }
                }
            }
    
            return false;
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