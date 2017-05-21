using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.IO;

namespace lab_3.Classes.ForLips
{
    public class Tint: LipProduct
    {
        public int Durability { get; set; } 
        public string ApplicationOfProduct { get; set; }

        public Tint(string ProductName, string Brand, PriceCategory PriceCategoryOfProduct, Color Color, string Aromatizer, int Durability, string ApplicationOfProduct)
        : base(ProductName, Brand, PriceCategoryOfProduct, Color, Aromatizer)
        {
            this.Durability = Durability;
            this.ApplicationOfProduct = ApplicationOfProduct;
        }

        public Tint(int classIndex): base(classIndex)
        { }

        public Tint() { }

        public override void SerializeObject(StreamWriter outputFile, char separator)
        {
            base.SerializeObject(outputFile, separator);

            outputFile.Write(Durability);
            outputFile.Write(separator);
            outputFile.Write(ApplicationOfProduct);
            outputFile.Write(separator);
        }

        public override void DeserializeObject(List<string> data)
        {
            base.DeserializeObject(data);
            try
            {
                Durability = Convert.ToInt32(data[currentItemList]);
                data.RemoveAt(currentItemList);
                ApplicationOfProduct = data[currentItemList];
                data.RemoveAt(currentItemList);
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
