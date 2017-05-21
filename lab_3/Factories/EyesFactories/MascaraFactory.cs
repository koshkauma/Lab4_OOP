using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using lab_3.Classes;
using lab_3.Classes.ForEyes;

namespace lab_3.Factories.EyesFactories
{
    public class MascaraFactory: EyesFactory
    {

        const int effectIndex = 5;
        const int materialOfBrushIndex = 6;
        const int formOfBrushIndex = 7;

        public override object GetClassName()
        {
            Object obj = new Object();
            obj = "Тушь для ресниц";
            return obj;
        }

        public override List<Control> GetListControl(Size size, int leftCoord)
        {
            List<Control> resultList = base.GetListControl(size, leftCoord);

            resultList.Add(GetLabel("mascaraEffect", "Заявленный эффект", size, new Point(leftCoord, 260), 11));
            resultList.Add(GetTextBox("mascaraEffect", size, new Point(leftCoord, 280), 12, TextBoxString_KeyPress));

            List<string> values = EnumHelper<Mascara.MaterialOfBrush>.GetEnumValues();
            List<string> names = EnumHelper<Mascara.MaterialOfBrush>.GetAllDescriptions();
            resultList.Add(GetLabel("materialOfBrush", "Материал щеточки", size, new Point(leftCoord, 305), 13));
            resultList.Add(GetComboBox("materialOfBrush", size, new Point(leftCoord, 325), 14, names, values));

            values = EnumHelper<Mascara.FormOfBrush>.GetEnumValues();
            names = EnumHelper<Mascara.FormOfBrush>.GetAllDescriptions();
            resultList.Add(GetLabel("formOfBrush", "Форма щеточки", size, new Point(leftCoord, 355), 15));
            resultList.Add(GetComboBox("formOfBrush", size, new Point(leftCoord, 375), 16, names, values));

            return resultList;
        }


        public override void GetDataFromComponents(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            base.GetDataFromComponents(currentProduct, controls);
            Control[] controlList = GetComponentsForInput(controls);

            Mascara currentMascara = (Mascara)currentProduct;

            currentMascara.Effect = controlList[effectIndex].Text;

            ComboBox temp = (ComboBox)controlList[materialOfBrushIndex];
            currentMascara.BrushMaterial = (Mascara.MaterialOfBrush)Enum.Parse(typeof(Mascara.MaterialOfBrush), temp.SelectedValue.ToString());

            temp = (ComboBox)controlList[formOfBrushIndex];
            currentMascara.BrushForm = (Mascara.FormOfBrush)Enum.Parse(typeof(Mascara.FormOfBrush), temp.SelectedValue.ToString());

        }


        public override void LoadDataToComponets(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            base.LoadDataToComponets(currentProduct, controls);
            Control[] controlList = GetComponentsForInput(controls);
            Mascara currentMascara = (Mascara)currentProduct;
            controlList[effectIndex].Text = currentMascara.Effect;

            ComboBox temp = (ComboBox)controlList[materialOfBrushIndex];
            temp.SelectedValue = Enum.GetName(typeof(Mascara.MaterialOfBrush), currentMascara.BrushMaterial);

            temp = (ComboBox)controlList[formOfBrushIndex];
            temp.SelectedValue = Enum.GetName(typeof(Mascara.FormOfBrush), currentMascara.BrushForm);
        }



        public override CosmeticProduct GetSomeCosmeticProduct(int index)
        {
            return new Mascara(index);
        }
    }
}
