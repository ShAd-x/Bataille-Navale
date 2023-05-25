namespace Core;

public class JsonDecoder
{
    /**
     * Décode chaque bateau en fonction de la taille et du nom
     */
    public class Bateaux
    {
        public List<(int, int)> Coordonnees { get; set; }
        public int Touches { get; set; }
        public int taille { get; set; }
        public string nom { get; set; }
        
        public Bateaux(int taille, string nom)
        {
            this.taille = taille;
            this.nom = nom;
            this.Touches = 0;
            this.Coordonnees = new List<(int, int)>();
        }
        
        /**
         * Ajoute les coordonnées du bateau
         *
         * @param int y
         * @param int x
         * @return void
         */
        public void AddCoord(int y, int x)
        {
            Coordonnees.Add((y, x));
        }

        /**
         * Vérifie si le bateau est touché et incrémente le nombre de touches
         *
         * @param int y
         * @param int x
         * @return bool
         */
        public bool IsABateau(int y, int x)
        {
            (int, int) coord = (y, x);
            if (Coordonnees.Contains(coord))
            {
                Touches += 1;
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
            // On compte le nombre de touches sur chaque bateau
            // donc si le nombre de touches est égal à la taille du bateau alors il est coulé
            return Touches == taille;
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