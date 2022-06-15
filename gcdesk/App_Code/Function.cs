using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;


public class Function
{
    /// <summary>
    /// Método para criptografar senha com base no SHA512
    /// </summary>
    /// <param name="texto">Senha</param>
    /// <returns>Texto criptografado</returns>
    public static string HashText(string texto)
    {
        HashAlgorithm hashAlgo = HashAlgorithm.Create("SHA-512");
        byte[] hash = hashAlgo.ComputeHash(Encoding.UTF8.GetBytes(texto));
        return Convert.ToBase64String(hash);
    }
}
