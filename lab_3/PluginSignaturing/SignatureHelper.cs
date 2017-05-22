using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace lab_3.PluginSignaturing
{
    public static class SignatureHelper
    {
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

        public static byte[] GetSignature(string pathOfPlugin, RSAParameters key)
        {
            byte[] fileHash = GetHash(pathOfPlugin);
            byte[] dateBytes = GetDate(pathOfPlugin);

            byte[] bytesForSignature = GetArrayConcat(fileHash, dateBytes);
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

        public static void WriteSignatureToFile(byte[] signature, string path)
        {
            //c using
            FileStream file = File.Open(path, FileMode.Create);
            StreamWriter outputFile = new StreamWriter(file);
            for (int i = 0; i < signature.Length; i++)
            {
                outputFile.WriteLine(signature[i]);
            }

            outputFile.Close();
            outputFile.Dispose();
        }


        public static void SavePublicKeyToFile(string path, RSACryptoServiceProvider algorithm) //save public key to verify signature
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
            const int size = 128;
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



        public static bool CheckIfValid(string pluginPath)
        {
            string pluginSignaturePath = Path.GetDirectoryName(pluginPath) + "\\" + Path.GetFileNameWithoutExtension(pluginPath) + ".signature";
            string pluginPublicKeyPath = Path.GetDirectoryName(pluginPath) + "\\" + Path.GetFileNameWithoutExtension(pluginPath) + ".key";
            if (File.Exists(pluginPublicKeyPath) && File.Exists(pluginSignaturePath))
            {
                byte[] fileHash = GetHash(pluginPath);
                byte[] dateBytes = GetDate(pluginPath);

                byte[] originalData = GetArrayConcat(fileHash, dateBytes); //original data

                byte[] signedData = ReadSignatureFromFile(pluginSignaturePath); //signed data

                string keyString = ReadPublicKey(pluginPublicKeyPath);

                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
                RSAalg.FromXmlString(keyString);

                return RSAalg.VerifyData(originalData, new SHA1CryptoServiceProvider(), signedData);
            }
            else
            {
                return false;
            }
        }




    }
}
