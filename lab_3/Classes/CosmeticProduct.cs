using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.ComponentModel;

namespace lab_3.Classes
{

   
    public class CosmeticProduct
    {
        public CosmeticProduct () {}

        public const int currentItemList = 0;
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public enum PriceCategory
        {
           [Description("Масс-маркет")]
           massMarket,
           [Description("Люкс")]
           lux,
           [Description("Профессиональная")]
           professional
        };
        public PriceCategory PriceCategoryOfProduct { get; set; }
        public Color Color { get; set; }

        private int classIndex;
        public int ClassIndex
        {
            get
            {
                return classIndex;
            }
        }

        public CosmeticProduct(int classIndex)
        {
            this.classIndex = classIndex;
        }

        public CosmeticProduct(string productName, string brand, PriceCategory priceCategoryOfProduct, Color Color)
        {
            this.ProductName = productName;
            this.Brand = brand;
            this.PriceCategoryOfProduct = priceCategoryOfProduct;
            this.Color = Color;
          
        }

        public virtual void SerializeObject(StreamWriter outputFile, char separator)
        {
            outputFile.Write(ClassIndex);
            outputFile.Write(separator);
            outputFile.Write(ProductName);
            outputFile.Write(separator);
            outputFile.Write(Brand);
            outputFile.Write(separator);
            outputFile.Write(PriceCategoryOfProduct);
            outputFile.Write(separator);
            int rgbColor = Color.ToArgb();
            outputFile.Write(rgbColor);
            outputFile.Write(separator);
        }


        public virtual void DeserializeObject(List<string> data)
        {

                ProductName = data[currentItemList];
                data.RemoveAt(currentItemList);
                Brand = data[currentItemList];
                data.RemoveAt(currentItemList);

                PriceCategoryOfProduct = (PriceCategory)Enum.Parse(typeof(PriceCategory), data[currentItemList]);
                data.RemoveAt(currentItemList);

                int rgbColor = Convert.ToInt32(data[currentItemList]);
                Color = Color.FromArgb(rgbColor);
                data.RemoveAt(currentItemList);

        }
    }
    
}
