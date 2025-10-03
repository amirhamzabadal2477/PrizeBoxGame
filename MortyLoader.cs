using System;

public static class MortyLoader
{
    public static IMorty LoadMorty(string mortyTypeName)
    {
        if (mortyTypeName.Equals("ClassicMorty", StringComparison.OrdinalIgnoreCase))
        {
            return new ClassicMorty();
        }
        else if (mortyTypeName.Equals("LazyMorty", StringComparison.OrdinalIgnoreCase))
        {
            return new LazyMorty();
        }
        
        Console.WriteLine($"Morty class '{mortyTypeName}' not found!");
        return null;
    }
}