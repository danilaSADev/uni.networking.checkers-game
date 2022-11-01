using System.Security.Cryptography;
using System.Text;

namespace LAB2_Checkers;

public static class IdentifierGenerator
{
    public static string GenerateIdentifier(string nickname, string salt = "")
    {
        var text = nickname + DateTime.Now;
        
        if (String.IsNullOrEmpty(text))
        {
            return String.Empty;
        }
    
        // Uses SHA256 to create the hash
        using (var sha = new SHA256Managed())
        {
            // Convert the string to a byte array first, to be processed
            byte[] textBytes = Encoding.UTF8.GetBytes(text + salt);
            byte[] hashBytes = sha.ComputeHash(textBytes);
        
            // Convert back to a string, removing the '-' that BitConverter adds
            string hash = BitConverter
                .ToString(hashBytes)
                .Replace("-", String.Empty);

            return hash;
        }
    }
}