using System.Text.Json;

namespace Core;

using Repo;

public class Core
{
    string player1Name, player2Name;

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
     * return Map
     */
    public Map startGame()
    {
        askPlayersName();

        // Données brutes
        String json = getDatas();
        // Données décodées
        JsonDecoder.JsonDatas myjson = JsonSerializer.Deserialize<JsonDecoder.JsonDatas>(json);

        // La map créee
        Map map = new Map(myjson.nbLignes, myjson.nbColonnes);
        
        placeBateau(myjson , map, 1);
        placeBateau(myjson , map, 2);

        
        map.displayMap();
        return map;
    }

    public void askPlayersName()
    {
        do
        {
            Console.Write("Entrez le nom du joueur 1 : ");
            player1Name = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(player1Name));

        do
        {
            Console.Write("Entrez le nom du joueur 2 : ");
            player2Name = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(player2Name));

        Console.WriteLine($"Les noms des deux joueurs sont {player1Name} et {player2Name}.");
    }

    /**
     * On place les bateau sur la carte
     */
    public void placeBateau(JsonDecoder.JsonDatas myjson, Map map, int joueur)
    {
        Console.WriteLine("On place les bateaux du joueur " + (joueur == 1 ? player1Name : player2Name));
        foreach (JsonDecoder.Bateaux bateau in myjson.bateaux)
        {
            map.displayMap();
            Console.WriteLine($"Bateau : {bateau.nom} de taille {bateau.taille}");
            bool placed = false;
            bool error = false;
            do
            { 
                Console.WriteLine($"Bateau : {bateau.nom} de taille {bateau.taille}");
                Console.Write("Donner la position du bateau à placer : ");
                string position = Console.ReadLine();
                Console.Write("Donner la direction (H ou V) du bateau à placer : ");
                string direction = Console.ReadLine();

                char lettre = position[0];
                lettre = char.ToUpper(lettre);
                int horizontal = (int)lettre - 65;
                // TODO: >= 10
                int vertical = int.Parse(position[1].ToString()) - 1;
                
                // if horizontal < 0 || horizontal > map.nbColonnes (erreur)
                // if horizontal + bateau.taille > myjson.nbColonnes (erreur ça dépasse)
                // if vertical + bateau.taille > myjson.nbLignes (erreur ça dépasse)

                if (direction == "H" || direction == "h")
                {
                    // On vérifie que les cases sont disponibles
                    for (int i = 0; i < bateau.taille; i++)
                    {
                        if (!map.IsAvailable(vertical, horizontal + i))
                        {
                            error = true;
                        }
                    }

                    if (error)
                    {
                        Console.WriteLine("Erreur, une des cases est déjà occupée");
                        continue;
                    }
                    
                    // On place le bateau
                    for (int i = 0; i < bateau.taille; i++)
                    {
                        map.map[vertical, horizontal + i] = 'X';
                        placed = true;
                    }
                }
                else if (direction == "V" || direction == "v")
                {
                    // On vérifie que les cases sont disponibles
                    for (int i = 0; i < bateau.taille; i++)
                    {
                        if (!map.IsAvailable(vertical + i, horizontal))
                        {
                            error = true;
                        }
                    }
                    
                    if (error)
                    {
                        Console.WriteLine("Erreur, une des cases est déjà occupée");
                        continue;
                    }

                    // On place le bateau
                    for (int i = 0; i < bateau.taille; i++)
                    {
                        map.map[vertical + i, horizontal] = 'O';
                        placed = true;
                    }
                }
                else
                {
                    Console.WriteLine("Erreur de direction");
                }
            } while (!placed);
        }
    }
}