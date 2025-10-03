using System.Collections.Generic;

public interface IMorty
{
    string GetName();
    List<int> GetBoxesToKeep(int rickGuess, int portalBox, int numBoxes, FairRandomGenerator random);
    double GetWinProbability(int numBoxes, bool switched);
    string GetOpeningRemark();
    string GetBoxSelectionRemark();
}