
using System.Security.Cryptography;

var rsa = RSA.Create();

var publicKey = rsa.ExportRSAPublicKey();
var privateKey = rsa.ExportRSAPrivateKey();

Console.WriteLine($"Public key:\n{Convert.ToBase64String(publicKey)}");
Console.WriteLine($"Private key:\n{Convert.ToBase64String(privateKey)}");