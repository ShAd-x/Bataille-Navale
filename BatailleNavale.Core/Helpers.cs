namespace Core;

using Repo;

public class Helpers
{
    /**
     * Récupère les données JSON depuis l'API
     *
     * return string
     */
    internal static string GetDatas()
    {
        return new Repo().GetJsonDatas().Result;
    }
    
    /**
     * Récupère la position horizontale depuis la position saisie par le joueur
     *
     * @param string position
     * @return int
     */
    internal static int GetHorizontalPosition(string position)
    {
        char lettre = char.ToUpper(position[0]);
        return lettre - 65;
    }
    
    /**
     * Récupère la position verticale depuis la position saisie par le joueur
     *
     * @param string position
     * @return int
     */
    internal static int GetVerticalPosition(string position)
    {
        return int.Parse(position.Substring(1)) - 1;
    }
    
    /**
     * Retourne un string non vide depuis la console (avec un message passé en paramètre)
     *
     * @param string message
     * @return string
     */
    internal static string ReadNonEmptyString(string message)
    {
        string input;
        
        do
        {
            Console.Write(message);
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input));
        
        return input;
    }
}