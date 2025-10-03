using System;
using System.Security.Cryptography;
using System.Text;

public class FairRandomGenerator
{
    private string secretKey;
    private int mortyValue;
    private string hmacValue;

    public string GenerateHMAC(int min, int max, out int mortyValue)
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            byte[] keyBytes = new byte[32];
            rng.GetBytes(keyBytes);
            secretKey = Convert.ToHexString(keyBytes).ToLower();
        }

        using (var rng = RandomNumberGenerator.Create())
        {
            byte[] randomBytes = new byte[4];
            rng.GetBytes(randomBytes);
            mortyValue = Math.Abs(BitConverter.ToInt32(randomBytes, 0)) % (max - min) + min;
            this.mortyValue = mortyValue;
        }

        using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
        {
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(mortyValue.ToString()));
            hmacValue = Convert.ToHexString(hash).ToLower();
        }

        return hmacValue;
    }

    public int GetFinalValue(int rickValue, int range)
    {
        return (mortyValue + rickValue) % range;
    }
    
    public void RevealSecrets(out int revealedMortyValue, out string revealedKey)
    {
        revealedMortyValue = mortyValue;
        revealedKey = secretKey;
    }
}