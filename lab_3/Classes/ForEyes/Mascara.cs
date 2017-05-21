using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.IO;

namespace lab_3.Classes.ForEyes
{
    public class Mascara: EyesProduct
    {
       public string Effect { get; set; }
       public enum MaterialOfBrush
       {
            [Description("Силиконовая")]
            silicone,
            [Description("Пластиковая")]
            plastic
       }
       public MaterialOfBrush BrushMaterial { get; set; }
       public enum FormOfBrush
       {
            [Description("Прямая")]
            straight,
            [Description("Овальная")]
            oval,
            [Description("Конусообразная")]
            coneShaped,
            [Description("С изгибом")]
            curved,
            [Description("Шарообразная")]
            ballShaped
       }

       public FormOfBrush BrushForm { get; set; }


       public Mascara(string productName, string brand, PriceCategory priceCategoryOfProduct, Color Color, bool IsWaterpoof, string Effect, MaterialOfBrush BrushMaterial,
           FormOfBrush BrushForm) : base(productName, brand, priceCategoryOfProduct, Color, IsWaterpoof)
       {
            this.IsWaterproof = IsWaterproof;
            this.BrushForm = BrushForm;
            this.BrushMaterial = BrushMaterial;
       }


        public Mascara(int classIndex): base(classIndex)
        { }


        public override void SerializeObject(StreamWriter outputFile, char separator)
        {
            base.SerializeObject(outputFile, separator);

            outputFile.Write(Effect);
            outputFile.Write(separator);
            outputFile.Write(BrushMaterial);
            outputFile.Write(separator);
            outputFile.Write(BrushForm);
            outputFile.Write(separator);
        }


        public override void DeserializeObject(List<string> data)
        {
            base.DeserializeObject(data);
            try
            {
                Effect = data[currentItemList];
                data.RemoveAt(currentItemList);
                BrushMaterial = (MaterialOfBrush)Enum.Parse(typeof(MaterialOfBrush), data[currentItemList]);
                data.RemoveAt(currentItemList);
                BrushForm = (FormOfBrush)Enum.Parse(typeof(FormOfBrush), data[currentItemList]);
                data.RemoveAt(currentItemList);
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
