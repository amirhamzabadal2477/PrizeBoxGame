using System.Collections.Generic;

public class EvilMorty : IMorty
{
    public string GetName() => "EvilMorty";

    public List<int> GetBoxesToKeep(int rickGuess, int portalBox, int numBoxes, FairRandomGenerator random)
    {
        // Keep any box that isn't Rick's guess or the portal box
        var boxes = new List<int>();
        for (int i = 0; i < numBoxes; i++)
            if (i != rickGuess && i != portalBox) boxes.Add(i);
        if (boxes.Count == 0) boxes.Add(rickGuess); // Rick always loses
        return boxes;
    }

    public double GetWinProbability(int numBoxes, bool switched) => 0.0;

    public string GetOpeningRemark() => "Rick, you seriously think you can win? Not today!";
    public string GetBoxSelectionRemark() => "I'm picking boxes so you lose, Rick!";
}