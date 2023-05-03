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
        return new Repo().getJsonDatas();
    }

    /**
     * Démarre la partie avec les informations fournies par l'api
     */
    public Map startGame()
    {
        // Données brutes
        String json = getDatas();
        // Données décodées
        JsonDecoder.JsonDatas myjson = JsonSerializer.Deserialize<JsonDecoder.JsonDatas>(json);

        // La map créee
        Map map = createMap(myjson.nbLignes, myjson.nbColonnes);

        return map;
    }
    
    /**
     * Créer la map du jeu
     * 
     * <param name="nbLignes"></param>
     * <param name="nbColonnes"></param>
     * return Map
     */
    public Map createMap(int nbLignes, int nbColonnes)
    {
        return new Map(nbLignes, nbColonnes);
    }
}