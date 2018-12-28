using System;
using System.Security.Cryptography;
using System.Text;

class MainClass {

    // https://www.codeproject.com/Articles/14150/Encrypt-and-Decrypt-Data-with-C
    public static string Encrypt(string Message, string Passphrase) {
        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        byte[] MessageArray = UTF8Encoding.UTF8.GetBytes(Message);
        byte[] PassphraseArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Passphrase));

        // Always release the resources and flush data of the Cryptographic service provide. Best Practice
        hashmd5.Clear();

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

        // set the secret key for the tripleDES algorithm
        tdes.Key = PassphraseArray;

        // mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)
        tdes.Mode = CipherMode.ECB;

        // padding mode(if any extra byte added)
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateEncryptor();

        // transform the specified region of bytes array to resultArray
        byte[] resultArray = cTransform.TransformFinalBlock(MessageArray, 0, MessageArray.Length);

        // Release resources held by TripleDes Encryptor
        tdes.Clear();

        // Return the encrypted data into unreadable string format
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    public static void Main (string[] args) {
        string Text = "LICENSED:CRACKED by @games195 2019 oops";
        Console.WriteLine("Restram4ME: " + Encrypt(Text, "m3u8 server url"));
        Console.WriteLine("Capture4ME: " + Encrypt(Text, "this capture device is no longer available"));
        Console.WriteLine("Cast4ME: " + Encrypt(Text, "invalid buffer directory"));
        Console.WriteLine("TSCapture: " + Encrypt(Text, "ts capture cryptkey"));
        Console.ReadKey();
    }
}
