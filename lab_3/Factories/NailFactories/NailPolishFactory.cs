using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using lab_3.Classes;
using lab_3.Classes.ForNails;
using System;


namespace lab_3.Factories.NailFactories
{
    public class NailPolishFactory: CosmeticFactory
    {
        const int durabilityIndex = 4;
        const int effectIndex = 5;

        public override object GetClassName()
        {
            Object obj = new Object();
            obj = "Лак для ногтей";
            return obj;
        }

        public override List<Control> GetListControl(Size size, int leftCoord)
        {
            List<Control> resultList = base.GetListControl(size, leftCoord);
         
            resultList.Add(GetLabel("durability", "Заявленное кол-во дней носки", size, new Point(leftCoord, 215), 9));
            resultList.Add(GetTextBox("durability", size, new Point(leftCoord, 235), 10, TextBoxDigits_KeyPress));

            List<string> values = EnumHelper<NailPolish.TypesOfEffects>.GetEnumValues();
            List<string> namesOfEffects = EnumHelper<NailPolish.TypesOfEffects>.GetAllDescriptions();
            resultList.Add(GetLabel("effect", "Специальный эффект лака", size, new Point(leftCoord, 260), 11));
            resultList.Add(GetComboBox("effect", size, new Point(leftCoord, 280), 12, namesOfEffects, values));

            return resultList;
        }


        public override void LoadDataToComponets(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            base.LoadDataToComponets(currentProduct, controls);
            Control[] controlList = GetComponentsForInput(controls);
            NailPolish currentNailPolish = (NailPolish)currentProduct;
            controlList[durabilityIndex].Text = Convert.ToString(currentNailPolish.Durability);

            ComboBox temp = (ComboBox)controlList[effectIndex];
            temp.SelectedValue = Enum.GetName(typeof(NailPolish.TypesOfEffects), currentNailPolish.SpecialEffect);
        }


        public override void GetDataFromComponents(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            base.GetDataFromComponents(currentProduct, controls);
            Control[] controlList = GetComponentsForInput(controls);
            NailPolish currentNailPolish = (NailPolish)currentProduct;
            try
            {
                currentNailPolish.Durability = Convert.ToInt32(controlList[durabilityIndex].Text);
            }
            catch
            {
                throw new Exception();
            }

            ComboBox temp = (ComboBox)controlList[effectIndex];
            currentNailPolish.SpecialEffect = (NailPolish.TypesOfEffects)Enum.Parse(typeof(NailPolish.TypesOfEffects), temp.SelectedValue.ToString());
        }


        public override CosmeticProduct GetSomeCosmeticProduct(int index)
        {
            return new NailPolish(index);
        }


    }
}
