using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using lab_3.Classes;
using lab_3.Classes.ForFace;


namespace lab_3.Factories.FaceFactories
{
    public abstract class FaceProductFactory: CosmeticFactory
    {
        const int finishIndex = 4;
        const int skinTypeIndex = 5;
        
        public override List<Control> GetListControl(Size size, int leftCoord)
        {
            List<Control> resultList = base.GetListControl(size, leftCoord);


            List<string> values = EnumHelper<FaceProduct.TypeOfFinish>.GetEnumValues();
            List<string> names = EnumHelper<FaceProduct.TypeOfFinish>.GetAllDescriptions();
            resultList.Add(GetLabel("typeOfFinish", "Финиш", size, new Point(leftCoord, 215), 9));
            resultList.Add(GetComboBox("typeOfFinish", size, new Point(leftCoord, 235), 10, names, values));

            values = EnumHelper<FaceProduct.SkinType>.GetEnumValues();
            names = EnumHelper<FaceProduct.SkinType>.GetAllDescriptions();
            resultList.Add(GetLabel("skinType", "Тип кожи", size, new Point(leftCoord, 260), 11));
            resultList.Add(GetComboBox("skinType", size, new Point(leftCoord, 280), 12, names, values));


            return resultList;
        }

        public override void LoadDataToComponets(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            base.LoadDataToComponets(currentProduct, controls);
            Control[] controlList = GetComponentsForInput(controls);
            FaceProduct currentFaceProduct = (FaceProduct)currentProduct;

            ComboBox tempComboBox = (ComboBox)controlList[finishIndex];
            tempComboBox.SelectedValue = Enum.GetName(typeof(FaceProduct.TypeOfFinish), currentFaceProduct.Finish);

            tempComboBox = (ComboBox)controlList[skinTypeIndex];
            tempComboBox.SelectedValue = Enum.GetName(typeof(FaceProduct.SkinType), currentFaceProduct.Finish);
        }

        public override void GetDataFromComponents(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            base.GetDataFromComponents(currentProduct, controls);
            Control[] controlList = GetComponentsForInput(controls);

            FaceProduct currentFaceProduct = (FaceProduct)currentProduct;

            ComboBox temp = (ComboBox)controlList[finishIndex];
            currentFaceProduct.Finish = (FaceProduct.TypeOfFinish)Enum.Parse(typeof(FaceProduct.TypeOfFinish), temp.SelectedValue.ToString());

            temp = (ComboBox)controlList[skinTypeIndex];
            currentFaceProduct.TypeOfSkin = (FaceProduct.SkinType)Enum.Parse(typeof(FaceProduct.SkinType), temp.SelectedValue.ToString());

        }




    }

}
