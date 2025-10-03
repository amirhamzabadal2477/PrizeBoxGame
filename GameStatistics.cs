using System;

public class GameStatistics
{
    private int roundsSwitched;
    private int roundsStayed;
    private int winsSwitched;
    private int winsStayed;

    public void RecordRound(bool switched, bool won)
    {
        if (switched)
        {
            roundsSwitched++;
            if (won) winsSwitched++;
        }
        else
        {
            roundsStayed++;
            if (won) winsStayed++;
        }
    }

    public void DisplayStatistics(int numBoxes, IMorty morty)
    {
        Console.WriteLine("                  GAME STATS ");
        Console.WriteLine("┌──────────────┬───────────────┬─────────────┐");
        Console.WriteLine("│ Game results │ Rick switched │ Rick stayed │");
        Console.WriteLine("├──────────────┼───────────────┼─────────────┤");
        Console.WriteLine($"│ Rounds       │ {roundsSwitched,13} │ {roundsStayed,11} │");
        Console.WriteLine("├──────────────┼───────────────┼─────────────┤");
        Console.WriteLine($"│ Wins         │ {winsSwitched,13} │ {winsStayed,11} │");
        Console.WriteLine("├──────────────┼───────────────┼─────────────┤");
        
        double estPswitched = roundsSwitched > 0 ? (double)winsSwitched / roundsSwitched : 0;
        double estPstayed = roundsStayed > 0 ? (double)winsStayed / roundsStayed : 0;
        
        Console.WriteLine($"│ P (estimate) │ {FormatProbability(estPswitched),13} │ {FormatProbability(estPstayed),11} │");
        Console.WriteLine("├──────────────┼───────────────┼─────────────┤");
        
        double exactPswitched = morty.GetWinProbability(numBoxes, true);
        double exactPstayed = morty.GetWinProbability(numBoxes, false);
        
        Console.WriteLine($"│ P (exact)    │ {exactPswitched,13:F3} │ {exactPstayed,11:F3} │");
        Console.WriteLine("└──────────────┴───────────────┴─────────────┘");
    }

    private string FormatProbability(double p)
    {
        return p == 0 ? "?" : p.ToString("F3");
    }
}