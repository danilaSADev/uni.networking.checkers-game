using System.Security.Cryptography;
using System.Text;

namespace CheckersServer.Common;

public static class IdentifierGenerator
{
    public static string GenerateIdentifier(string nickname, string salt = "")
    {
        var text = nickname + DateTime.Now;

        if (string.IsNullOrEmpty(text)) return string.Empty;

        // Uses SHA256 to create the hash
        using (var sha = new SHA256Managed())
        {
            // Convert the string to a byte array first, to be processed
            var textBytes = Encoding.UTF8.GetBytes(text + salt);
            var hashBytes = sha.ComputeHash(textBytes);

            // Convert back to a string, removing the '-' that BitConverter adds
            var hash = BitConverter
                .ToString(hashBytes)
                .Replace("-", string.Empty);

            return hash;
        }
    }
}