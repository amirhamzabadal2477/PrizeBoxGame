using System.Collections.Generic;

public class ClassicMorty : IMorty
{
    public string GetName() => "ClassicMorty";
    
    public List<int> GetBoxesToKeep(int rickGuess, int portalBox, int numBoxes, FairRandomGenerator random)
    {
        var boxesToKeep = new List<int> { rickGuess };
        
        if (rickGuess == portalBox)
        {
            int randomValue;
            string hmac = random.GenerateHMAC(0, numBoxes - 1, out randomValue);
            
            for (int i = 0; i < numBoxes; i++)
            {
                if (i != rickGuess)
                {
                    boxesToKeep.Add(i);
                    break;
                }
            }
        }
        else
        {
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
    
    public string GetOpeningRemark() => "Oh geez, Rick, I'm gonna hide your portal gun in one of the {0} boxes, okay?";
    
    public string GetBoxSelectionRemark() => "Let's, uh, generate another value now, I mean, to select which box to remove from the game.";
}