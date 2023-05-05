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
 //       askPlayersName();

        // Données brutes
        String json = getDatas();
        // Données décodées
        JsonDecoder.JsonDatas myjson = JsonSerializer.Deserialize<JsonDecoder.JsonDatas>(json);

        // La map créee
        Map map = new Map(myjson.nbLignes, myjson.nbColonnes);
        
        foreach (JsonDecoder.Bateaux bateau in myjson.bateaux)
        {
            //Console.WriteLine(bateau.nom + " : " + bateau.taille);
            //map.addBateau(bateau);
        }

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

        Console.WriteLine("Les noms des deux joueurs sont {0} et {1}.", player1Name, player2Name);
    }
}