using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.IO;

namespace lab_3.Classes.ForFace
{
    public class Foundation: FaceProduct
    {
        public enum GradeOfCoverage
        {
            [Description("Плотное")]
            full,
            [Description("Ежедневное")]
            soft,
            [Description("Для съемок")]
            hdEffect
        }
        public GradeOfCoverage Coverage { get; set; }
        public bool isSPF { get; set; } 

        public Foundation(string ProductName, string Brand, PriceCategory PriceCategoryOfProduct, Color Color, SkinType TypeOfSkin, TypeOfFinish Finish, GradeOfCoverage Coverage, bool isSPF): 
            base(ProductName, Brand, PriceCategoryOfProduct, Color, TypeOfSkin, Finish)
        {
            this.isSPF = isSPF;
            this.Coverage = Coverage;
        }

        public Foundation(int classIndex): base(classIndex)
        { }


        public override void SerializeObject(StreamWriter outputFile, char separator)
        {
            base.SerializeObject(outputFile, separator);

            outputFile.Write(Coverage);
            outputFile.Write(separator);
            outputFile.Write(isSPF);
            outputFile.Write(separator);
        }


        public override void DeserializeObject(List<string> data)
        {
            base.DeserializeObject(data);
            try
            {
                Coverage = (GradeOfCoverage)Enum.Parse(typeof(GradeOfCoverage), data[currentItemList]);
                data.RemoveAt(currentItemList);
                isSPF = bool.Parse(data[currentItemList]);
                data.RemoveAt(currentItemList);
            }
            catch
            {
                throw new Exception();
            }
        }

    }
}
