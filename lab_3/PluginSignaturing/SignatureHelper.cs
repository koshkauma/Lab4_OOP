using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace lab_3.PluginSignaturing
{
    public static class SignatureHelper
    {
        const int size = 128;

        public static byte[] GetHash(string nameOfInputFile)
        {
            FileStream stream = File.OpenRead(nameOfInputFile);
            SHA256Managed shaM = new SHA256Managed();
            byte[] hash = shaM.ComputeHash(stream);
            return hash;
        }

        public static byte[] GetDate(string pathOfPlugun)
        {
            byte[] dateTime;
            dateTime = Encoding.UTF8.GetBytes(File.GetCreationTime(pathOfPlugun).ToString());
            return dateTime;
        }

        //array of bytes for signaturing 
        //depends on time and file data
        public static byte[] GetArrayConcat(byte[] fileHash, byte[] dateTimeBytes)
        {
            byte[] arrayConcat = new byte[fileHash.Length + dateTimeBytes.Length];
            for (int i = 0; i < fileHash.Length; i++)
            {
                arrayConcat[i] = fileHash[i];

            }

            int counter = 0;
            for (int i = fileHash.Length; i < arrayConcat.Length; i++)
            {
                arrayConcat[i] = dateTimeBytes[counter];
                counter++;
            }
            return arrayConcat;
        }


        public static byte[] GetBytesForSignaturing(string pathOfPlugin)
        {
            byte[] fileHash = GetHash(pathOfPlugin);
            byte[] dateBytes = GetDate(pathOfPlugin);

            byte[] result = GetArrayConcat(fileHash, dateBytes);
            return result;
        }

        //creating RSA signature
        public static byte[] GetSignature(string pathOfPlugin, RSAParameters key)
        {
            byte[] bytesForSignature = GetBytesForSignaturing(pathOfPlugin);
            try
            {
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
                RSAalg.ImportParameters(key);
                return RSAalg.SignData(bytesForSignature, new SHA1CryptoServiceProvider());
            }
            catch (CryptographicException)
            {
                throw new Exception("В процессе создания подписи возникла ошибка!");
            }

        }

        //save signature to file with ".signature" extension to home directory for choosed plugin

          
        public static void WriteSignatureToFile(byte[] signature, string pluginPath)
        {
            try
            {
                string signatureFilePath = Path.GetDirectoryName(pluginPath) + "\\" + Path.GetFileNameWithoutExtension(pluginPath) + ".signature";
                FileStream file = File.Open(signatureFilePath, FileMode.Create);

                using (StreamWriter outputFile = new StreamWriter(file))
                {
                    for (int i = 0; i < signature.Length; i++)
                    {
                        outputFile.WriteLine(signature[i]);
                    }
                }
            }
            catch
            {
                throw new Exception("В процессе сохранения подписи возникли ошибки!");
            }
           
        }

        //save public key to file with ".key" extension
        //public key will be used to verify signature

        public static void SavePublicKeyToFile(string path, RSACryptoServiceProvider algorithm) 
        {
            string publicKeyPath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + ".key";
            
            FileStream file = File.Open(publicKeyPath, FileMode.Create);

            StreamWriter outputFile = new StreamWriter(file);
            string publicKey = algorithm.ToXmlString(false); 
            outputFile.Write(publicKey);


            outputFile.Close();
            outputFile.Dispose();

        }

        
        public static byte[] ReadSignatureFromFile(string pluginSignaturePath)
        {
            byte[] result = new byte[size];

            try
            {
                string line;

                using (StreamReader reader = new StreamReader(pluginSignaturePath))
                {
                    for (int i = 0; i < size; i++)
                    {
                        line = reader.ReadLine();
                        result[i] = Convert.ToByte(line);
                    }

                }
            }
            catch
            {
                throw new Exception();
            }
            return result;
        }

        public static string ReadPublicKey(string keyPath)
        {
            string result;
            try
            {
                using (StreamReader reader = new StreamReader(keyPath))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch
            {
                throw new Exception();
            }
            return result;
        }



    }
}
