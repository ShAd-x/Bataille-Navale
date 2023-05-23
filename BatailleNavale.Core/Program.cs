using System.Text.Json;

namespace Core;

public class Core
{
    private string _player1Name = "Not defined";
    private string _player2Name = "Not defined";
    private bool _isFinished;

    public List<(int, int)> historiquePlayer1 = new List<(int, int)>();
    public List<(int, int)> historiquePlayer2 = new List<(int, int)>();
    
    /**
     * Démarre la partie avec les informations fournies par l'api
     *
     * return void
     */
    public void StartGame()
    {
        AskPlayersName();

        // Données brutes
        String rawJson = Helpers.GetDatas();
        // Données décodées
        JsonDecoder.JsonDatas decodedJson = JsonSerializer.Deserialize<JsonDecoder.JsonDatas>(rawJson) ?? throw new InvalidDataException();

        // La map créee
        Map map = new Map(decodedJson.nbLignes, decodedJson.nbColonnes);
        
        GameLoop(map, decodedJson);
    }

    /**
     * Lance la boucle de jeu
     *
     * @param Map map
     * @param JsonDecoder.JsonDatas json
     * @return void
     */
    public void GameLoop(Map map, JsonDecoder.JsonDatas json)
    {
        PlaceBateau(json , map, 1);
        //PlaceBateau(json , map, 2);
        //_isFinished = true;

        do
        {
            CheckIfHit(AskPositionToShoot(map, 1), map, 1);
        } while (!_isFinished);
    }
    
    /**
     * On demande la position à attaquer au joueur
     *
     * @param Map map
     * @param int joueur
     * @return string
     */
    public string AskPositionToShoot(Map map, int joueur)
    {
        bool error = false;
        string position;
        do
        {
            map.DisplayMap(joueur);
            position = Helpers.ReadNonEmptyString("Donner la position à attaquer : ");
            
            int horizontal = Helpers.GetHorizontalPosition(position);
            int vertical = Helpers.GetVerticalPosition(position);
            
            if (historiquePlayer1.Contains((vertical, horizontal)))
            {
                Console.WriteLine("Vous avez déjà attaqué cette position");
            }
            else
            {
                //historique.Add((vertical, horizontal));
                error = false;
            }
        } while (error);

        return position;
    }
    
    public void CheckIfHit(string position, Map map, int joueur)
    {
        int horizontal = Helpers.GetHorizontalPosition(position);
        int vertical = Helpers.GetVerticalPosition(position);
        
        map.bateauxPlaced.ForEach(bateau =>
        {
            bool hit = bateau.IsABateau(vertical, horizontal);
        });
    }

    /**
     * On place les bateau sur la carte avec les données saisies par les joueurs
     *
     * @param JsonDecoder.JsonDatas myjson
     * @param Map map
     * @param int joueur
     * @return void
     */
    public void PlaceBateau(JsonDecoder.JsonDatas myjson, Map map, int joueur)
    {
        Console.WriteLine("On place les bateaux du joueur " + (joueur == 1 ? _player1Name : _player2Name));
        foreach (JsonDecoder.Bateaux bateau in myjson.bateaux)
        {
            bool placed = false;
            do
            {
                // On affiche la carte à chaque boucle
                map.DisplayMap(joueur);

                Console.WriteLine($"Bateau : {bateau.nom} de taille {bateau.taille}");
                string position = Helpers.ReadNonEmptyString("Donner la position du bateau à placer : ");
                string direction = Helpers.ReadNonEmptyString("Donner la direction (H ou V) du bateau à placer : ");

                int horizontal = Helpers.GetHorizontalPosition(position);
                int vertical = Helpers.GetVerticalPosition(position);
                
                if (!IsValidPosition(horizontal, vertical, bateau.taille, direction, myjson))
                    continue;

                if (
                    direction.Equals("H", StringComparison.OrdinalIgnoreCase) ||
                    direction.Equals("V", StringComparison.OrdinalIgnoreCase)
                ) {
                    int orientation = direction.Equals("H", StringComparison.OrdinalIgnoreCase) ? 0 : 1;

                    if (IsOccupied(map, vertical, horizontal, bateau.taille, orientation))
                        continue;

                    PlaceBateauOnMap(map, vertical, horizontal, bateau, orientation, joueur);
                    placed = true;
                }
                else
                {
                    Console.WriteLine("Erreur de direction (H ou V)");
                }
            } while (!placed);
        }
    }
    
    /**
     * Demande les noms des joueurs
     *
     * return void
     */
    public void AskPlayersName()
    {
        _player1Name = Helpers.ReadNonEmptyString("Entrez le nom du joueur 1 (X) : ");
        _player2Name = Helpers.ReadNonEmptyString("Entrez le nom du joueur 2 (O) : ");
    
        Console.WriteLine($"Les noms des deux joueurs sont {_player1Name} et {_player2Name}.");
    }

    /**
     * Vérifie si la position est valide par rapport à la taille du bateau et la taille de la carte
     *
     * @param int horizontal
     * @param int vertical
     * @param int tailleBateau
     * @param JsonDecoder.JsonDatas myjson
     * @return bool
     */
    private bool IsValidPosition(int horizontal, int vertical, int tailleBateau, string direction, JsonDecoder.JsonDatas myjson)
    {
        if (horizontal < 0 || horizontal >= myjson.nbColonnes ||
            vertical < 0 || vertical >= myjson.nbLignes)
        {
            Console.WriteLine("Erreur, la position est en dehors de la carte");
            return false;
        }

        if (
            (
                direction.Equals("H", StringComparison.OrdinalIgnoreCase) &&
                horizontal + tailleBateau > myjson.nbColonnes
            ) || (
                direction.Equals("V", StringComparison.OrdinalIgnoreCase) &&
                vertical + tailleBateau > myjson.nbLignes
            )
        ) {
            Console.WriteLine("Erreur, le bateau dépasse de la carte");
            return false;
        }

        return true;
    }

    /**
     * Vérifie si la position est occupée par rapport à la direction du bateau et sa taille
     *
     * @param Map map
     * @param int vertical
     * @param int horizontal
     * @param int tailleBateau
     * @param int direction
     * @return bool
     */
    private bool IsOccupied(Map map, int vertical, int horizontal, int tailleBateau, int direction)
    {
        for (int i = 0; i < tailleBateau; i++)
        {
            if (!map.IsAvailable(vertical, horizontal))
            {
                Console.WriteLine("Erreur, une des cases est déjà occupée");
                return true;
            }
            
            // On incrémente la taille du bateau en fonction de la direction
            if (direction == 0)
                horizontal++;
            else
                vertical++;
        }

        return false;
    }

    /**
     * Place le bateau sur la carte
     *
     * @param Map map
     * @param int vertical
     * @param int horizontal
     * @param int tailleBateau
     * @param int direction
     * @param int joueur
     * @return void
     */
    private void PlaceBateauOnMap(Map map, int vertical, int horizontal, JsonDecoder.Bateaux bateau, int direction, int joueur)
    {
        map.AddBateau(bateau);
        for (int i = 0; i < bateau.taille; i++)
        {
            map.map[vertical, horizontal] = (joueur == 1 ? 'X' : 'O');
            bateau.AddCoord(vertical, horizontal);
            
            // On incrémente la taille du bateau en fonction de la direction
            if (direction == 0)
                horizontal++;
            else
                vertical++;
        }
    }
}