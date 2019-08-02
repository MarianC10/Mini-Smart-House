using System;

public class AES
{
    public byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
    {
        byte[] encrypted;
        // Create a new AesManaged.    
        using (System.Security.Cryptography.AesManaged aes = new System.Security.Cryptography.AesManaged())
        {
            // Create encryptor    
            System.Security.Cryptography.ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
            // Create MemoryStream    
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                // to encrypt    
                using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, encryptor, System.Security.Cryptography.CryptoStreamMode.Write))
                {
                    // Create StreamWriter and write data to a stream    
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(cs))
                        sw.Write(plainText);
                    encrypted = ms.ToArray();
                }
            }
        }
        // Return encrypted data    
        return encrypted;
    }

    public string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
    {
        string plaintext = null;
        // Create AesManaged    
        using (System.Security.Cryptography.AesManaged aes = new System.Security.Cryptography.AesManaged())
        {
            // Create a decryptor    
            System.Security.Cryptography.ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
            // Create the streams used for decryption.    
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(cipherText))
            {
                // Create crypto stream    
                using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, decryptor, System.Security.Cryptography.CryptoStreamMode.Read))
                {
                    // Read crypto stream    
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(cs))
                        plaintext = reader.ReadToEnd();
                }
            }
        }
        return plaintext;
    }

    public byte[] generateIV()
    {
        using (System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
        {
            byte[] nonce = new byte[16];
            rng.GetBytes(nonce);
            return nonce;
        }
    }
}
