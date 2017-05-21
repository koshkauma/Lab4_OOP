using System;
using System.IO;
using System.Linq;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using lab_3.Factories;
using lab_3.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using lab_3.Helpers;
using lab_3.PluginSignaturing;
using System.Security.Cryptography;
using System.IO;

namespace lab_3.PluginSignaturing
{
    public class SignatureCreation
    {
        int size = 32;
        const char separator = '|';

        public byte[] Hash { get; set; }
        public DateTime Date { get; set; }


        public SignatureCreation(byte[] Hash, DateTime Date)
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
            outputFile.WriteLine(Date);
            outputFile.Dispose();
            outputFile.Close();
        }


        public bool CheckValidation(string pathOfFile)
        {
            byte[] hashFromSignatureFile = new byte[size];
            DateTime dateFromSignatureFile;

            string pathToSignatureFile = Path.GetDirectoryName(pathOfFile) + "\\" + Path.GetFileNameWithoutExtension(pathOfFile) + ".sign";
           
            try
            {
                string line;

                using (StreamReader reader = new StreamReader(pathToSignatureFile))
                {
                    for (int i = 0; i < size; i++)
                    {
                        line = reader.ReadLine();
                        hashFromSignatureFile[i] = Convert.ToByte(line);
                    }

                    dateFromSignatureFile = DateTime.Parse(reader.ReadLine());

                    if ((String.Equals(dateFromSignatureFile.ToString(), Date.ToString())) && (Enumerable.SequenceEqual(Hash, hashFromSignatureFile)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }


    }
}
