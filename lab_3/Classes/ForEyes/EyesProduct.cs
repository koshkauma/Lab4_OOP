using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace lab_3.Classes.ForEyes
{
    public abstract class EyesProduct: CosmeticProduct
    {
       public bool IsWaterproof { get; set; }

       public EyesProduct(string productName, string brand, PriceCategory priceCategoryOfProduct, Color Color, bool isWaterpoof) : base(productName, brand, priceCategoryOfProduct, Color)
       {
            this.IsWaterproof = IsWaterproof;
       }


        public EyesProduct(int classIndex): base(classIndex)
        { }

        public override void SerializeObject(StreamWriter outputFile, char separator)
        {
            base.SerializeObject(outputFile, separator);

            outputFile.Write(IsWaterproof);
            outputFile.Write(separator);
        }

        public override void DeserializeObject(List<string> data)
        {
            base.DeserializeObject(data);
            try
            {
                IsWaterproof = bool.Parse(data[currentItemList]);
                data.RemoveAt(currentItemList);
            }
            catch
            {
                throw new Exception();
            }
        }

    }
}
