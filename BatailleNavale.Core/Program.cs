using System.Text.Json;

namespace Core;

using Repo;

public class Core
{
    string player1Name, player2Name;
    private bool IsFinished = false;

    /**
     * Récupère les données JSON depuis l'API
     *
     * return string
     */
    public string getDatas()
    {
        return new Repo().getJsonDatas().Result;
    }

    /**
     * Démarre la partie avec les informations fournies par l'api
     *
     * return void
     */
    public void StartGame()
    {
        AskPlayersName();

        // Données brutes
        String json = getDatas();
        // Données décodées
        JsonDecoder.JsonDatas myjson = JsonSerializer.Deserialize<JsonDecoder.JsonDatas>(json);

        // La map créee
        Map map = new Map(myjson.nbLignes, myjson.nbColonnes);
        
        PlaceBateau(myjson , map, 1);
        PlaceBateau(myjson , map, 2);

        do
        {
            
        } while (!IsFinished);
    }

    public void AskPlayersName()
    {
        do
        {
            Console.Write("Entrez le nom du joueur 1 (X) : ");
            player1Name = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(player1Name));

        do
        {
            Console.Write("Entrez le nom du joueur 2 (O) : ");
            player2Name = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(player2Name));

        Console.WriteLine($"Les noms des deux joueurs sont {player1Name} et {player2Name}.");
    }

    /**
     * On place les bateau sur la carte
     *
     * @param JsonDecoder.JsonDatas myjson
     * @param Map map
     * @param int joueur
     * @return void
     */
    public void PlaceBateau(JsonDecoder.JsonDatas myjson, Map map, int joueur)
    {
        Console.WriteLine("On place les bateaux du joueur " + (joueur == 1 ? player1Name : player2Name));
        foreach (JsonDecoder.Bateaux bateau in myjson.bateaux)
        {
            bool placed = false;
            do
            {
                bool error = false;
                // On affiche la carte à chaque boucle
                map.DisplayMap(joueur);

                Console.WriteLine($"Bateau : {bateau.nom} de taille {bateau.taille}");
                Console.Write("Donner la position du bateau à placer : ");
                string position = Console.ReadLine();
                Console.Write("Donner la direction (H ou V) du bateau à placer : ");
                string direction = Console.ReadLine();
                
                // On vérifie si les données sont saisies
                if (direction == null || position == null)
                {
                    Console.WriteLine("Erreur, veuillez recommencer");
                    continue;
                }

                char lettre = char.ToUpper(position[0]);
                int horizontal = lettre - 65;
                int vertical = int.Parse(position.Substring(1)) - 1;
                
                if (!IsValidPosition(horizontal, vertical, bateau.taille, direction, myjson))
                    continue;

                if (
                    direction.Equals("H", StringComparison.OrdinalIgnoreCase) ||
                    direction.Equals("V", StringComparison.OrdinalIgnoreCase)
                ) {
                    int orientation = direction.Equals("H", StringComparison.OrdinalIgnoreCase) ? 0 : 1;

                    if (IsOccupied(map, vertical, horizontal, bateau.taille, orientation))
                        continue;

                    PlaceBateau(map, vertical, horizontal, bateau, orientation, joueur);
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
    private void PlaceBateau(Map map, int vertical, int horizontal, JsonDecoder.Bateaux bateau, int direction, int joueur)
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