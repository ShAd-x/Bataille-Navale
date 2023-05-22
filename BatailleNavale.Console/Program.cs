namespace Console;

using Core;

public class Program
{
    public static void Main(string[] args)
    {
        // Instance du core
        Core core = new Core();
        
        // On démarre la partie
        core.startGame();
    }
}