using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using lab_3.Classes;
using lab_3.Classes.ForEyes;

namespace lab_3.Factories.EyesFactories
{
    public abstract class EyesFactory: CosmeticFactory
    {
        const int waterproofIndex = 4;

        public override List<Control> GetListControl(Size size, int leftCoord)
        {
            List<Control> resultList = base.GetListControl(size, leftCoord);

            resultList.Add(GetLabel("wateproof", "Наличие св-ва водойстойкости?", size, new Point(leftCoord, 215), 9));
            resultList.Add(GetCheckBox("waterproof", size, new Point(leftCoord, 235), 10));

            return resultList;
        }


        public override void LoadDataToComponets(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            base.LoadDataToComponets(currentProduct, controls);
            Control[] controlList = GetComponentsForInput(controls);
            EyesProduct currentEyeProduct = (EyesProduct)currentProduct;

            CheckBox temp = (CheckBox)controlList[waterproofIndex];
            temp.Checked = currentEyeProduct.IsWaterproof;

        }

        public override void GetDataFromComponents(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            base.GetDataFromComponents(currentProduct, controls);
            Control[] controlList = GetComponentsForInput(controls);
            EyesProduct currentEyesProduct = (EyesProduct)currentProduct;

            CheckBox tempCheckBox = (CheckBox)controlList[waterproofIndex];
            currentEyesProduct.IsWaterproof = tempCheckBox.Checked;

        }

       

    }
}
