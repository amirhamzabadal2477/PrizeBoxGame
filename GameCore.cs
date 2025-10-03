using System;
using System.Collections.Generic;

public class GameCore
{
    private int numBoxes;
    private IMorty morty;
    private GameStatistics stats;

    public GameCore(int numBoxes, IMorty morty)
    {
        this.numBoxes = numBoxes;
        this.morty = morty;
        this.stats = new GameStatistics();
    }

    public void PlayGame()
    {
        Console.WriteLine($"🎲 Starting Prize Box Game with {numBoxes} boxes...");
        Console.WriteLine($"🤖 Using Morty: {morty.GetName()}\n");

        bool playAgain = true;
        
        while (playAgain)
        {
            PlayRound();
            Console.Write("Morty: D-do you wanna play another round (y/n)?  \nRick: ");
            string response = Console.ReadLine()?.ToLower();
            playAgain = response == "y" || response == "yes";
            Console.WriteLine();
        }

        Console.WriteLine("Morty: Okay… uh, bye!\n");
        stats.DisplayStatistics(numBoxes, morty);
    }

    private void PlayRound()
    {
        var random1 = new FairRandomGenerator();
        var random2 = new FairRandomGenerator();

        Console.WriteLine($"Morty: {string.Format(morty.GetOpeningRemark(), numBoxes)}  ");
        
        int mortyValue1, rickValue1;
        string hmac1 = random1.GenerateHMAC(0, numBoxes, out mortyValue1);
        Console.WriteLine($"Morty: HMAC1={hmac1}");
        Console.Write($"Morty: Rick, enter your number (1 to {numBoxes}) so you don't whine later that I cheated, alright?  \nRick: ");
        
        while (!int.TryParse(Console.ReadLine(), out rickValue1) || rickValue1 < 1 || rickValue1 > numBoxes)
        {
            Console.Write($"Please enter a number between 1 and {numBoxes}: ");
        }
        int internalRickValue1 = rickValue1 - 1;

        int portalBox = random1.GetFinalValue(internalRickValue1, numBoxes);
        
        Console.Write($"Morty: Okay, okay, I hid the gun. What's your guess (1 to {numBoxes})?  \nRick: ");
        int rickGuess;
        while (!int.TryParse(Console.ReadLine(), out rickGuess) || rickGuess < 1 || rickGuess > numBoxes)
        {
            Console.Write($"Please enter a number between 1 and {numBoxes}: ");
        }
        int internalRickGuess = rickGuess - 1;

        Console.WriteLine($"Morty: {morty.GetBoxSelectionRemark()}");

        int mortyValue2, rickValue2;
        string hmac2 = random2.GenerateHMAC(0, numBoxes, out mortyValue2);
        Console.WriteLine($"Morty: HMAC2={hmac2}");
        Console.Write($"Morty: Rick, enter your number (1 to {numBoxes}) to select which box to remove, and, uh, don't say I didn't play fair, okay?  \nRick: ");
        
        while (!int.TryParse(Console.ReadLine(), out rickValue2) || rickValue2 < 1 || rickValue2 > numBoxes)
        {
            Console.Write($"Please enter a number between 1 and {numBoxes}: ");
        }
        int internalRickValue2 = rickValue2 - 1;

        var boxesToKeep = morty.GetBoxesToKeep(internalRickGuess, portalBox, numBoxes, random2);
        
        int otherBox = boxesToKeep[0] == internalRickGuess ? boxesToKeep[1] : boxesToKeep[0];
        Console.WriteLine($"Morty: I'm keeping box {rickGuess} and box {otherBox + 1}.");

        Console.Write($"Morty: You can switch to box {otherBox + 1}, or, you know, stick with box {rickGuess}.  \nRick: ");
        int switchChoice;
        while (!int.TryParse(Console.ReadLine(), out switchChoice) || (switchChoice != otherBox + 1 && switchChoice != rickGuess))
        {
            Console.Write($"Please enter {otherBox + 1} (switch) or {rickGuess} (stay): ");
        }

        bool switched = switchChoice == otherBox + 1;
        int finalChoice = switched ? otherBox : internalRickGuess;

        random1.RevealSecrets(out int revealedMorty1, out string key1);
        random2.RevealSecrets(out int revealedMorty2, out string key2);

        Console.WriteLine($"Morty: Aww man, my 1st random value is {revealedMorty1 + 1}.  ");
        Console.WriteLine($"Morty: KEY1={key1}");
        Console.WriteLine($"Morty: So the 1st fair number is ({rickValue1} + {revealedMorty1 + 1}) % {numBoxes} = {portalBox + 1}");
        Console.WriteLine($"Morty: Aww man, my 2nd random value is {revealedMorty2 + 1}.  ");
        Console.WriteLine($"Morty: KEY2={key2}");
        Console.WriteLine($"Morty: Uh, okay, the 2nd fair number is ({rickValue2} + {revealedMorty2 + 1}) % {numBoxes} = {(internalRickValue2 + revealedMorty2) % numBoxes + 1}");

        bool won = finalChoice == portalBox;
        Console.WriteLine($"Morty: Your portal gun is in the box {portalBox + 1}. ");
        
        if (won)
            Console.WriteLine("Morty: Aww man, you won, Rick. Now we gotta go on one of YOUR adventures!");
        else
            Console.WriteLine("Morty: Aww man, you lost, Rick. Now we gotta go on one of *my* adventures!");

        stats.RecordRound(switched, won);
    }
}