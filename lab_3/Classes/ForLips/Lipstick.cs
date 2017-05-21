using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.IO;

namespace lab_3.Classes
{
    public class Lipstick: LipProduct
    {
        public enum TypeOfPackage
        {
            [Description("Баночка")]
            jar,
            [Description("Палетка")]
            palette,
            [Description("Стик")]
            stick,
            [Description("Кушон")]
            cushion
        }
        public TypeOfPackage PackageType { get; set; }
        public enum TypeOfFinish
        {
            [Description("Матовый")]
            matt,
            [Description("Оттеночный")]
            sheer,
            [Description("Металлик")]
            metallized,
            [Description("Кремовый")]
            cream,
            [Description("Сияющий (блестки)")]
            withShimmer
        }
        public TypeOfFinish Finish { get; set; }

        public Lipstick(string ProductName, string Brand, PriceCategory PriceCategoryOfProduct, Color Color, string Aromatizer,
        TypeOfPackage PackageType, TypeOfFinish Finish) : base(ProductName, Brand, PriceCategoryOfProduct, Color, Aromatizer)
        {
            this.PackageType = PackageType;
            this.Finish = Finish;
        }


        public Lipstick(int classIndex): base(classIndex)
        { }


        public override void SerializeObject(StreamWriter outputFile, char separator)
        {
            base.SerializeObject(outputFile, separator);

            outputFile.Write(PackageType);
            outputFile.Write(separator);
            outputFile.Write(Finish);
            outputFile.Write(separator);
        }

        public override void DeserializeObject(List<string> data)
        {
            base.DeserializeObject(data);
            try
            {
                PackageType = (TypeOfPackage)Enum.Parse(typeof(TypeOfPackage), data[currentItemList]);
                data.RemoveAt(currentItemList);
                Finish = (TypeOfFinish)Enum.Parse(typeof(TypeOfFinish), data[currentItemList]);
                data.RemoveAt(currentItemList);
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
