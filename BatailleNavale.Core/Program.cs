using System.Text.Json;

namespace Core;

using Repo;

public class Core
{
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
        // Données brutes
        String json = getDatas();
        // Données décodées
        JsonDecoder.JsonDatas myjson = JsonSerializer.Deserialize<JsonDecoder.JsonDatas>(json);

        // La map créee
        Map map = new Map(myjson.nbLignes, myjson.nbColonnes);

        return map;
    }
}