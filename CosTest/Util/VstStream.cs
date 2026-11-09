using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security;
using System.Security.Cryptography;

namespace VstStream
{
    internal class VstStreamReader
    {
        private TextReader VstReader;

        internal VstStreamReader(string path)
        {
            if (path.ToLower().EndsWith(".vstini"))
            {
                // Password Key IV
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(@"vsstec-hl-2023#*");//32bit
                byte[] iv = UE.GetBytes(@"77954430");//16bit
                // Read File
                FileStream fs = new  FileStream(path,FileMode.Open);
                if(fs.Length > 0)
                {
                    byte[] bs = new byte[fs.Length];
                    fs.Read(bs, 0, (int)fs.Length);
                    // Decrypt
                    string plainText = Rijndael.DecryptStringFromBytes(bs, key, iv);
                    MemoryStream ms = new MemoryStream(UE.GetBytes(plainText));
                    this.VstReader = new StreamReader(ms, UE);
                }
                else
                {
                    MemoryStream ms = new MemoryStream();
                    this.VstReader = new StreamReader(ms, UE);
                }
            }
                
            else
            {
                this.VstReader = new StreamReader(path);
            }
                
        }

        internal string ReadLine()
        {
            return this.VstReader?.ReadLine();
        }

        internal void Close()
        {
            this.VstReader?.Close();
        }
        
    }

    internal class VstStreamWriter
    {
        private FileStream VstWriter;

        internal VstStreamWriter(string path)
        {
            this.VstWriter = new FileStream(path, FileMode.Create);
        }

        internal void Write(string value)
        {
            // Password Key IV
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] key = UE.GetBytes(@"vsstec-hl-2023#*");//32bit
            byte[] iv = UE.GetBytes(@"77954430");//16bit
            // Encrypt
            byte[] bs = Rijndael.EncryptStringToBytes(value,key,iv);
            // Write
            this.VstWriter?.Write(bs, 0, bs.Length);
        }

        internal void Close()
        {
            this.VstWriter?.Close();
        }

       
    }

    class Rijndael
    {
        internal static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        internal static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}
