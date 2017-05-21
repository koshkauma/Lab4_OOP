using System;
using System.IO;

namespace lab_3.PluginSignaturing
{
    public class SignatureCreation
    {
        const char separator = '|';

        public byte[] Hash { get; set; }
        public DateTime Date { get; set; }

  
        public SignatureCreation (byte[] Hash, DateTime Date)
        {
            this.Hash = Hash;
            this.Date = Date;
        }

        public void CreateSignature(string pathOfFile)
        {
            FileStream file = File.Open(Path.GetDirectoryName(pathOfFile) + "\\" + Path.GetFileNameWithoutExtension(pathOfFile) + ".sign", FileMode.Create);
            StreamWriter outputFile = new StreamWriter(file);
            for (int i = 0; i < Hash.Length; i++)
            {
                outputFile.WriteLine(Hash[i]);
            }
            outputFile.Write(separator);
            outputFile.Write(Date);
            outputFile.Write(separator);
            outputFile.Dispose();
            outputFile.Close();
        }

        


    }


}
