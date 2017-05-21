using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using lab_3.Classes;
using lab_3.Classes.ForEyes;

namespace lab_3.Factories.EyesFactories
{
    public class EyeshadowFactory: EyesFactory
    {
        const int eyeshadowTextureIndex = 5;
        const int packageTypeIndex = 6;

        public override object GetClassName()
        {
            Object obj = new Object();
            obj = "Тени для век";
            return obj;
        }

        public override List<Control> GetListControl(Size size, int leftCoord)
        {
            List<Control> resultList = base.GetListControl(size, leftCoord);

            List<string> values = EnumHelper<Eyeshadow.KindOfEyeshadow>.GetEnumValues();
            List<string> names = EnumHelper<Eyeshadow.KindOfEyeshadow>.GetAllDescriptions();
            resultList.Add(GetLabel("kindOfEyeshadow", "Тип теней", size, new Point(leftCoord, 260), 11));
            resultList.Add(GetComboBox("kindOfEyeshadow", size, new Point(leftCoord, 280), 12, names, values));

            values = EnumHelper<Eyeshadow.FormOfPackage>.GetEnumValues();
            names = EnumHelper<Eyeshadow.FormOfPackage>.GetAllDescriptions();
            resultList.Add(GetLabel("package", "Форма упаковки", size, new Point(leftCoord, 305), 13));
            resultList.Add(GetComboBox("package", size, new Point(leftCoord, 325), 14, names, values));

            return resultList;
        }

        public override void LoadDataToComponets(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            base.LoadDataToComponets(currentProduct, controls);
            Control[] controlList = GetComponentsForInput(controls);
            Eyeshadow currentEyeshadow = (Eyeshadow)currentProduct;

            ComboBox temp = (ComboBox)controlList[eyeshadowTextureIndex];
            temp.SelectedValue = Enum.GetName(typeof(Eyeshadow.KindOfEyeshadow), currentEyeshadow.EyeshadowTexture);

            temp = (ComboBox)controlList[packageTypeIndex];
            temp.SelectedValue = Enum.GetName(typeof(Eyeshadow.FormOfPackage), currentEyeshadow.PackageType);
        }




        public override void GetDataFromComponents(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            base.GetDataFromComponents(currentProduct, controls);
            Control[] controlList = GetComponentsForInput(controls);

            Eyeshadow currentEyeShadow = (Eyeshadow)currentProduct;

            ComboBox temp = (ComboBox)controlList[eyeshadowTextureIndex];
            currentEyeShadow.EyeshadowTexture = (Eyeshadow.KindOfEyeshadow)Enum.Parse(typeof(Eyeshadow.KindOfEyeshadow), temp.SelectedValue.ToString());

            temp = (ComboBox)controlList[packageTypeIndex];
            currentEyeShadow.PackageType = (Eyeshadow.FormOfPackage)Enum.Parse(typeof(Eyeshadow.FormOfPackage), temp.SelectedValue.ToString());

        }


        public override CosmeticProduct GetSomeCosmeticProduct(int index)
        {
            return new Eyeshadow(index);
        }


    }
}
