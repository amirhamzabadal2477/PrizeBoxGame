using System;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: dotnet run -- <number_of_boxes> <MortyType>");
            Console.WriteLine("Example: dotnet run -- 3 ClassicMorty");
            return;
        }

        if (!int.TryParse(args[0], out int numBoxes) || numBoxes < 3)
        {
            Console.WriteLine("Number of boxes must be an integer >= 3");
            return;
        }

        string mortyType = args[1];
        IMorty morty = MortyLoader.LoadMorty(mortyType);
        
        if (morty == null)
        {
            return;
        }

        var game = new GameCore(numBoxes, morty);
        game.PlayGame();
    }
}