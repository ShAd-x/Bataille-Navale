namespace Core;

public class JsonDecoder
{
    /**
     * Décode chaque bateau en fonction de la taille et du nom
     */
    public class Bateaux
    {
        public List<(int, int)> coordonnees { get; set; }
        public int touches { get; set; }
        public int taille { get; set; }
        public string nom { get; set; }
        
        public Bateaux(int taille, string nom)
        {
            this.taille = taille;
            this.nom = nom;
            this.touches = 0;
            this.coordonnees = new List<(int, int)>();
        }
        
        public void AddCoord(int y, int x)
        {
            coordonnees.Add((y, x));
        }

        public bool IsABateau(int y, int x)
        {
            (int, int) coord = (y, x);
            if (coordonnees.Contains(coord))
            {
                Console.WriteLine("Touché ! " + nom + " / " + taille);
                touches += 1;
                return true;
            }
            return false;
        }

        /**
         * Vérifie si le bateau est coulé
         *
         * @return bool
         */
        public bool IsCoule()
        {
            return touches == taille;
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