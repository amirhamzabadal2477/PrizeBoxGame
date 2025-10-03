using System.Collections.Generic;

public class LazyMorty : IMorty
{
    public string GetName() => "LazyMorty";
    
    public List<int> GetBoxesToKeep(int rickGuess, int portalBox, int numBoxes, FairRandomGenerator random)
    {
        var boxesToKeep = new List<int>();
        if (rickGuess == portalBox)
        {
            boxesToKeep.Add(rickGuess);
            boxesToKeep.Add((rickGuess + 1) % numBoxes);
        }
        else
        {
            boxesToKeep.Add(rickGuess);
            boxesToKeep.Add(portalBox);
        }
        return boxesToKeep;
    }
    
    public double GetWinProbability(int numBoxes, bool switched)
    {
        if (switched)
            return (numBoxes - 1.0) / numBoxes;
        else
            return 1.0 / numBoxes;
    }
    
    public string GetOpeningRemark() => "Uh, can we just get this over with? I'll hide it in one of {0} boxes...";
    
    public string GetBoxSelectionRemark() => "I'm just gonna remove the lowest boxes, because effort...";
}