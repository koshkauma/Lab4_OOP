using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using lab_3.Classes;
using lab_3.Classes.ForFace;

namespace lab_3.Factories.FaceFactories
{
    public class PowderFactory: FaceProductFactory 
    {
        const int powderPackageTypeIndex = 6;
        const int talcIndex = 7;

        public override object GetClassName()
        {
            Object obj = new Object();
            obj = "Пудра";
            return obj;
        }

        public override List<Control> GetListControl(Size size, int leftCoord)
        {
            List<Control> resultList = base.GetListControl(size, leftCoord);

            List<string> values = EnumHelper<Powder.TypeOfPackage>.GetEnumValues();
            List<string> names = EnumHelper<Powder.TypeOfPackage>.GetAllDescriptions();
            resultList.Add(GetLabel("powderType", "Тип пудры", size, new Point(leftCoord, 310), 13));
            resultList.Add(GetComboBox("powderType", size, new Point(leftCoord, 330), 14, names, values));

            resultList.Add(GetLabel("talcContain", "Содержание талька", size, new Point(leftCoord, 355), 15));
            resultList.Add(GetCheckBox("talcContain", size, new Point(leftCoord, 375), 16));

            return resultList;
        }

        public override void LoadDataToComponets(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            base.LoadDataToComponets(currentProduct, controls);
            Control[] controlList = GetComponentsForInput(controls);
            Powder currentPowder = (Powder)currentProduct;

            ComboBox tempComboBox = (ComboBox)controlList[powderPackageTypeIndex];
            tempComboBox.SelectedValue = Enum.GetName(typeof(Powder.TypeOfPackage), currentPowder.Package);


            CheckBox temp = (CheckBox)controlList[talcIndex];
            temp.Checked = currentPowder.isContainTalc;
        }

        public override void GetDataFromComponents(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            base.GetDataFromComponents(currentProduct, controls);
            Control[] controlList = GetComponentsForInput(controls);
            Powder currentPowder = (Powder)currentProduct;
            ComboBox temp = (ComboBox)controlList[powderPackageTypeIndex];
            currentPowder.Package = (Powder.TypeOfPackage)Enum.Parse(typeof(Powder.TypeOfPackage), temp.SelectedValue.ToString());
            CheckBox tempCheckBox = (CheckBox)controlList[talcIndex];
            currentPowder.isContainTalc = tempCheckBox.Checked;
        }



        public override CosmeticProduct GetSomeCosmeticProduct(int index)
        {
            return new Powder(index);
        }
    }
}
