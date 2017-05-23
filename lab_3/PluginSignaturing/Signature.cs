using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace lab_3.PluginSignaturing
{
    public class Signature
    {
        public string PathForFile {get; set;}

        public Signature(string pathToFile)
        {
            this.PathForFile = pathToFile;
        }

        public virtual void SaveSignature()
        {

            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
            RSAParameters key = RSAalg.ExportParameters(true);

            byte[] signedData = SignatureHelper.GetSignature(PathForFile, key);

            SignatureHelper.WriteSignatureToFile(signedData, PathForFile);
            SignatureHelper.SavePublicKeyToFile(PathForFile, RSAalg);
        }

        public virtual bool CheckIfValid()
        {
            string pluginSignaturePath = Path.GetDirectoryName(PathForFile) + "\\" + Path.GetFileNameWithoutExtension(PathForFile) + ".signature";
            string pluginPublicKeyPath = Path.GetDirectoryName(PathForFile) + "\\" + Path.GetFileNameWithoutExtension(PathForFile) + ".key";
            if (File.Exists(pluginPublicKeyPath) && File.Exists(pluginSignaturePath))
            {
                try
                {

                    byte[] originalData = SignatureHelper.GetBytesForSignaturing(PathForFile); //original data

                    byte[] signedData = SignatureHelper.ReadSignatureFromFile(pluginSignaturePath); //signed data

                    string keyString = SignatureHelper.ReadPublicKey(pluginPublicKeyPath); //public key to verify signature

                    RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

                    RSAalg.FromXmlString(keyString);

                    return RSAalg.VerifyData(originalData, new SHA1CryptoServiceProvider(), signedData);
                }
                catch (System.FormatException)
                {
                    throw new Exception("Целостность публичного ключа нарушена!");
                }
                catch (Exception)
                {
                    throw new Exception("В процессе проверки подлинности произошла ошибка!");
                }
            }
            else
            {
                return false;
            }
        }

    }
}
