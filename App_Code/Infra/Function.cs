using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;


public class Function
{
    /// <summary>Computes the SHA-512 hash of the input string and returns it as a Base64 string.</summary>
    /// <param name="texto">Plain-text password.</param>
    /// <returns>Base64-encoded SHA-512 hash.</returns>
    public static string HashText(string texto)
    {
        HashAlgorithm hashAlgo = HashAlgorithm.Create("SHA-512");
        byte[] hash = hashAlgo.ComputeHash(Encoding.UTF8.GetBytes(texto));
        return Convert.ToBase64String(hash);
    }

    /// <summary>Returns current datetime formatted as dd/MM/yyyy HH:mm:ss for database storage.</summary>
    public static string Now()
    {
        return DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss");
    }
}
