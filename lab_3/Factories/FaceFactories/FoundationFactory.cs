using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using lab_3.Classes;
using lab_3.Classes.ForFace;

namespace lab_3.Factories.FaceFactories
{
    public class FoundationFactory: FaceProductFactory
    {
        const int spfIndex = 6;
        const int coverageIndex = 7;

        public override object GetClassName()
        {
            Object obj = new Object();
            obj = "Тональный крем";
            return obj;
        }

        public override List<Control> GetListControl(Size size, int leftCoord)
        {
            List<Control> resultList = base.GetListControl(size, leftCoord);

            resultList.Add(GetLabel("spf", "Наличие spf-фактора", size, new Point(leftCoord, 310), 13));
            resultList.Add(GetCheckBox("spf", size, new Point(leftCoord, 330), 14));

            List<string> values = EnumHelper<Foundation.GradeOfCoverage>.GetEnumValues();
            List<string> names = EnumHelper<Foundation.GradeOfCoverage>.GetAllDescriptions();
            resultList.Add(GetLabel("coverageGrade", "Тип покрытия", size, new Point(leftCoord, 355), 15));
            resultList.Add(GetComboBox("coverageGrade", size, new Point(leftCoord, 375), 16, names, values));

            return resultList;
        }

        public override void LoadDataToComponets(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            base.LoadDataToComponets(currentProduct, controls);
            Control[] controlList = GetComponentsForInput(controls);
            Foundation currentFoundation = (Foundation)currentProduct;

            CheckBox temp = (CheckBox)controlList[spfIndex];
            temp.Checked = currentFoundation.isSPF;

            ComboBox tempComboBox = (ComboBox)controlList[coverageIndex];
            tempComboBox.SelectedValue = Enum.GetName(typeof(Foundation.GradeOfCoverage), currentFoundation.Coverage);
        }

        public override void GetDataFromComponents(CosmeticProduct currentProduct, Control.ControlCollection controls)
         {
             base.GetDataFromComponents(currentProduct, controls);
             Control[] controlList = GetComponentsForInput(controls);

             Foundation currentFoundation = (Foundation)currentProduct;

             CheckBox tempCheckBox = (CheckBox)controlList[spfIndex];
             currentFoundation.isSPF = tempCheckBox.Checked;

             ComboBox temp = (ComboBox)controlList[coverageIndex];
             currentFoundation.Coverage = (Foundation.GradeOfCoverage)Enum.Parse(typeof(Foundation.GradeOfCoverage), temp.SelectedValue.ToString());

         }


        public override CosmeticProduct GetSomeCosmeticProduct(int index)
        {
            return new Foundation(index);
        }
    }
}
