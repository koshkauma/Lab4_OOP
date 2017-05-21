using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Drawing;

namespace lab_3.Classes.ForLips
{
    public class LipGloss: LipProduct
    {
       public bool isShimmer { get; set; } //содержит ли блестки
       public enum GradeOfCoverage
       {
            [Description("Полное")]
            fullCoverage,
            [Description("Полупрозрачное")]
            semitransparent
       }; 
       public GradeOfCoverage Coverage { get; set; }

       public LipGloss(string ProductName, string Brand, PriceCategory PriceCategoryOfProduct, Color Color, string Aromatizer,
       GradeOfCoverage Coverage, bool isShimmer) : base(ProductName, Brand, PriceCategoryOfProduct, Color, Aromatizer)
       {
            this.isShimmer = isShimmer;
            this.Coverage = Coverage;
       }


        public LipGloss(int classIndex): base(classIndex)
        { }


        public override void SerializeObject(StreamWriter outputFile, char separator)
        {
            base.SerializeObject(outputFile, separator);
           
            outputFile.Write(Coverage);
            outputFile.Write(separator);

            outputFile.Write(isShimmer);
            outputFile.Write(separator);
        }



        public override void DeserializeObject(List<string> data)
        {
            base.DeserializeObject(data);
            try
            {
                Coverage = (GradeOfCoverage)Enum.Parse(typeof(GradeOfCoverage), data[currentItemList]);
                data.RemoveAt(currentItemList);
                isShimmer = bool.Parse(data[currentItemList]);
                data.RemoveAt(currentItemList);    
            }
            catch
            {
                throw new Exception();
            }
        }

    }
}
