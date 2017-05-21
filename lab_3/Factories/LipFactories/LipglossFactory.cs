using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using lab_3.Classes;
using lab_3.Classes.ForLips;

namespace lab_3.Factories.LipFactories
{
    public class LipglossFactory: LipProductFactory
    {
        const int shimmerIndex = 5;
        const int coverageIndex = 6;

        public override object GetClassName()
        {
            Object obj = new Object();
            obj = "Блеск для губ";
            return obj;
        }

        public override List<Control> GetListControl(Size size, int leftCoord)
        {
            List<Control> resultList = base.GetListControl(size, leftCoord);

            List<string> values = EnumHelper<Lipstick.TypeOfPackage>.GetEnumValues();
            List<string> names = EnumHelper<Lipstick.TypeOfPackage>.GetAllDescriptions();
            resultList.Add(GetLabel("shimmer", "Содержание блесток", size, new Point(leftCoord, 260), 11));
            resultList.Add(GetCheckBox("shimmer", size, new Point(leftCoord, 280), 12));

            values = EnumHelper<LipGloss.GradeOfCoverage>.GetEnumValues();
            names = EnumHelper<LipGloss.GradeOfCoverage>.GetAllDescriptions();
            resultList.Add(GetLabel("coverage", "Степень покрытия", size, new Point(leftCoord, 305), 13));
            resultList.Add(GetComboBox("coverage", size, new Point(leftCoord, 325), 14, names, values));

            return resultList;
        }


        public override void GetDataFromComponents(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            base.GetDataFromComponents(currentProduct, controls);
            Control[] controlList = GetComponentsForInput(controls);
            LipGloss currentLipGloss = (LipGloss)currentProduct;
          
            CheckBox tempCheckBox = (CheckBox)controlList[shimmerIndex];
            currentLipGloss.isShimmer = tempCheckBox.Checked;

            ComboBox temp = (ComboBox)controlList[coverageIndex];
            currentLipGloss.Coverage = (LipGloss.GradeOfCoverage)Enum.Parse(typeof(LipGloss.GradeOfCoverage), temp.SelectedValue.ToString());
        }


        public override void LoadDataToComponets(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            base.LoadDataToComponets(currentProduct, controls);
            Control[] controlList = GetComponentsForInput(controls);
            LipGloss currentLipGloss = (LipGloss)currentProduct;

            CheckBox temp = (CheckBox)controlList[shimmerIndex];
            temp.Checked = currentLipGloss.isShimmer;

            ComboBox tempComboBox = (ComboBox)controlList[coverageIndex];
            tempComboBox.SelectedValue = Enum.GetName(typeof(LipGloss.GradeOfCoverage), currentLipGloss.Coverage);
        }



        public override CosmeticProduct GetSomeCosmeticProduct(int index)
        {
            return new LipGloss(index);
        }
    }
}
