namespace Console;

using Core;

public class Program
{
    public static void Main(string[] args)
    {
        // Instance du core
        Core core = new Core();
        
        // On démarre la partie
        System.Console.WriteLine(core.startGame());
    }

    /**
     * Affiche la carte sur la console
     */
    public void showMap(Map map)
    {

    }
}