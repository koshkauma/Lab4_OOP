using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using lab_3.Classes.ForLips;
using lab_3.Classes;
namespace lab_3.Factories.LipFactories
{
    public class TintFactory: LipProductFactory
    {
        const int durabilityIndex = 5;
        const int applicationIndex = 6;

        public override object GetClassName()
        {
            Object obj = new Object();
            obj = "Тинт для губ";
            return obj;
        }


        public override List<Control> GetListControl(Size size, int leftCoord)
        {
            List<Control> resultList = base.GetListControl(size, leftCoord);
            
            resultList.Add(GetLabel("tintDurability", "Стойкость(час)", size, new Point(leftCoord, 260), 11));
            resultList.Add(GetTextBox("tintDurability", size, new Point(leftCoord, 280), 12, TextBoxDigits_KeyPress));

            resultList.Add(GetLabel("application", "Способ нанесения", size, new Point(leftCoord, 305), 11));
            resultList.Add(GetTextBox("application", size, new Point(leftCoord, 325), 12, TextBoxString_KeyPress));

            return resultList;
        }

        public override void LoadDataToComponets(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            base.LoadDataToComponets(currentProduct, controls);
            Control[] controlList = GetComponentsForInput(controls);
            Tint currentTint = (Tint)currentProduct;

            controlList[durabilityIndex].Text = currentTint.Durability.ToString();
            controlList[applicationIndex].Text = currentTint.ApplicationOfProduct;

        }


        public override void GetDataFromComponents(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            base.GetDataFromComponents(currentProduct, controls);
            Control[] controlList = GetComponentsForInput(controls);
            Tint currentTint = (Tint)currentProduct;
            try
            {
                currentTint.Durability = Convert.ToInt32(controlList[durabilityIndex].Text);
            }
            catch
            {
                throw new Exception();
            }
            currentTint.ApplicationOfProduct = controlList[applicationIndex].Text;
        }

        public override CosmeticProduct GetSomeCosmeticProduct(int index)
        {
            return new Tint(index);
        }
    }
}
