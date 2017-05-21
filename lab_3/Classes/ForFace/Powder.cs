using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.IO;

namespace lab_3.Classes.ForFace
{
    public class Powder: FaceProduct
    {
        public enum TypeOfPackage
        {
            [Description("Рассыпчатая")]
            loose,
            [Description("Компактная")]
            compact
        }
        public TypeOfPackage Package { get; set; }
        public bool isContainTalc { get; set; }

        public Powder(string ProductName, string Brand, PriceCategory PriceCategoryOfProduct, Color Color, SkinType TypeOfSkin, TypeOfFinish Finish, TypeOfPackage Package, bool isContainTalc) :
                base(ProductName, Brand, PriceCategoryOfProduct, Color, TypeOfSkin, Finish)
        {
            this.Package = Package;
            this.isContainTalc = isContainTalc;
        }


        public Powder(int classIndex): base(classIndex)
        { }


        public override void SerializeObject(StreamWriter outputFile, char separator)
        {
            base.SerializeObject(outputFile, separator);

            outputFile.Write(Package);
            outputFile.Write(separator);
            outputFile.Write(isContainTalc);
            outputFile.Write(separator);
        }


        public override void DeserializeObject(List<string> data)
        {
            base.DeserializeObject(data);
            try
            {
                Package = (TypeOfPackage)Enum.Parse(typeof(TypeOfPackage), data[currentItemList]);
                data.RemoveAt(currentItemList);
                isContainTalc = bool.Parse(data[currentItemList]);
                data.RemoveAt(currentItemList);
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
