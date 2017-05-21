using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using lab_3.Classes;
using lab_3.Classes.ForLips;


namespace lab_3.Factories.LipFactories
{
    public class LipstickFactory: LipProductFactory
    {
        const int packageIndex = 5;
        const int finishIndex = 6;

        public override object GetClassName()
        {
            Object obj = new Object();
            obj = "Помада для губ";
            return obj;
        }

        public override List<Control> GetListControl(Size size, int leftCoord)
        {
            List<Control> resultList = base.GetListControl(size, leftCoord);

            List<string> values = EnumHelper<Lipstick.TypeOfPackage>.GetEnumValues();
            List<string> names = EnumHelper<Lipstick.TypeOfPackage>.GetAllDescriptions();
            resultList.Add(GetLabel("package", "Формат", size, new Point(leftCoord, 260), 11));
            resultList.Add(GetComboBox("package", size, new Point(leftCoord, 280), 12, names, values));

            values = EnumHelper<Lipstick.TypeOfFinish>.GetEnumValues();
            names = EnumHelper<Lipstick.TypeOfFinish>.GetAllDescriptions();
            resultList.Add(GetLabel("finish", "Тип финиша", size, new Point(leftCoord, 305), 13));
            resultList.Add(GetComboBox("finish", size, new Point(leftCoord, 325), 14, names, values));

            return resultList;
        }



        public override void LoadDataToComponets(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            base.LoadDataToComponets(currentProduct, controls);
            Control[] controlList = GetComponentsForInput(controls);
            Lipstick currentLipstick = (Lipstick)currentProduct;

            ComboBox tempComboBox = (ComboBox)controlList[packageIndex];
            tempComboBox.SelectedValue = Enum.GetName(typeof(Lipstick.TypeOfPackage), currentLipstick.PackageType);

            tempComboBox = (ComboBox)controlList[finishIndex];
            tempComboBox.SelectedValue = Enum.GetName(typeof(Lipstick.TypeOfFinish), currentLipstick.Finish);

        }

        public override void GetDataFromComponents(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            base.GetDataFromComponents(currentProduct, controls);
            Control[] controlList = GetComponentsForInput(controls);
            Lipstick currentLipstick = (Lipstick)currentProduct;
            ComboBox temp = (ComboBox)controlList[packageIndex];
            currentLipstick.PackageType = (Lipstick.TypeOfPackage)Enum.Parse(typeof(Lipstick.TypeOfPackage), temp.SelectedValue.ToString());
            
            temp = (ComboBox)controlList[finishIndex];
            currentLipstick.Finish = (Lipstick.TypeOfFinish)Enum.Parse(typeof(Lipstick.TypeOfFinish), temp.SelectedValue.ToString());
        }

        public override CosmeticProduct GetSomeCosmeticProduct(int index)
        {
            return new Lipstick(index);
        }
    }
}
