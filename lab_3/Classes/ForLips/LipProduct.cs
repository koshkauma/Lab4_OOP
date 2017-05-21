using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace lab_3.Classes
{
    public abstract class LipProduct: CosmeticProduct
    {

        public LipProduct() { }
        public string Aromatizer { get; set; }

        public LipProduct(string ProductName,
            string Brand, PriceCategory PriceCategoryOfProduct,
            Color Color, string Aromatizer) : base(ProductName, Brand, PriceCategoryOfProduct, Color)
        {
            this.Aromatizer = Aromatizer;
            this.Color = Color;
        }


        public LipProduct(int classIndex): base(classIndex)
        { }


        public override void SerializeObject(StreamWriter outputFile, char separator)
        {
            base.SerializeObject(outputFile, separator);

            outputFile.Write(Aromatizer);
            outputFile.Write(separator);
        }


        public override void DeserializeObject(List<string> data)
        {
            base.DeserializeObject(data);
            try
            {
                Aromatizer = data[currentItemList];
                data.RemoveAt(currentItemList);
            }
            catch
            {
                throw new Exception();
            }
        }

    }
}
