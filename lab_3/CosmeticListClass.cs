using System.Text;
using lab_3.Factories;
using System.IO;
using System.ComponentModel;
using lab_3.Classes;
using System.Collections.Generic;
using System.Linq;
using System;

namespace lab_3
{
    public class CosmeticListClass
    {
        const int classIndexPosition = 0;
        const char separator = '|';

        public BindingList<CosmeticProduct> CosmeticList { get; set; }

        public CosmeticListClass()
        {
            CosmeticList = new BindingList<CosmeticProduct>();
        }

        public void SerializeItemsInList(string fileName)
        {
            StreamWriter outputFile = new StreamWriter(fileName, false, Encoding.Default);
            
            foreach (CosmeticProduct someProduct in CosmeticList)
            {
                someProduct.SerializeObject(outputFile, separator);
            }
            outputFile.Flush();
            outputFile.Close();
        }

        public void DeserializeItemsInList(string fileName, AllProductsFactory formEditorFactory)
        {
            StreamReader file = new StreamReader(fileName, Encoding.Default);
            string dataFromFile = file.ReadToEnd();
            file.Close();

            List<string> dataSeparated = dataFromFile.Split(separator).ToList();
            dataSeparated.RemoveAt(dataSeparated.Count - 1);

            while (dataSeparated.Count != 0)
            {
                try
                {
                    int classIndex = Convert.ToInt32(dataSeparated[classIndexPosition]);
                    dataSeparated.RemoveAt(classIndexPosition);
                    CosmeticProduct productToAdd = formEditorFactory.FactoryList[classIndex].GetSomeCosmeticProduct(classIndex);
                    productToAdd.DeserializeObject(dataSeparated);
                    CosmeticList.Add(productToAdd);
                }
                catch
                {
                    throw new Exception("Не удалось десериализовать!");
                }
            }
        }

    }
}
