using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using lab_3.Classes;
using lab_3.Classes.ForLips;

namespace lab_3.Factories.LipFactories
{
    public abstract class LipProductFactory: CosmeticFactory
    {
        const int aromatizerIndex = 4;

        public override List<Control> GetListControl(Size size, int leftCoord)
        {
            List<Control> resultList = base.GetListControl(size, leftCoord);

            resultList.Add(GetLabel("aromatizer", "Отдушка", size, new Point(leftCoord, 215), 9));
            resultList.Add(GetTextBox("aromatizer", size, new Point(leftCoord, 235), 10, TextBoxString_KeyPress));

            return resultList;
        }

        public override void LoadDataToComponets(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            base.LoadDataToComponets(currentProduct, controls);
            Control[] controlList = GetComponentsForInput(controls);
            LipProduct currentLipProduct = (LipProduct)currentProduct;

            controlList[aromatizerIndex].Text = Convert.ToString(currentLipProduct.Aromatizer);
        }

        public override void GetDataFromComponents(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            base.GetDataFromComponents(currentProduct, controls);
            Control[] controlList = GetComponentsForInput(controls);
            LipProduct currentLipProduct = (LipProduct)currentProduct;
            currentLipProduct.Aromatizer = (controlList[aromatizerIndex].Text);
        }


    }
}
