using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using lab_3.Classes;
using lab_3.Classes.ForLips;

namespace lab_3.Factories.LipFactories
{
    public class LipPencilFactory: LipProductFactory
    {
        const int typeOfPencilIndex = 5;
        const int textureTypeIndex = 6;

        public override object GetClassName()
        {
            Object obj = new Object();
            obj = "Карандаш для губ";
            return obj;
        }

        public override List<Control> GetListControl(Size size, int leftCoord)
        {
            List<Control> resultList = base.GetListControl(size, leftCoord);

            List<string> values = EnumHelper<LipPencil.TypeOfPencil>.GetEnumValues();
            List<string> names = EnumHelper<LipPencil.TypeOfPencil>.GetAllDescriptions();
            resultList.Add(GetLabel("typeOfPencil", "Устойство карандаша", size, new Point(leftCoord, 260), 11));
            resultList.Add(GetComboBox("typeOfPencil", size, new Point(leftCoord, 280), 12, names, values));

            values = EnumHelper<LipPencil.TypeOfTexture>.GetEnumValues();
            names = EnumHelper<LipPencil.TypeOfTexture>.GetAllDescriptions();
            resultList.Add(GetLabel("textureType", "Текстура", size, new Point(leftCoord, 305), 13));
            resultList.Add(GetComboBox("textureType", size, new Point(leftCoord, 325), 14, names, values));

            return resultList;
        }

        public override void LoadDataToComponets(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            base.LoadDataToComponets(currentProduct, controls);
            Control[] controlList = GetComponentsForInput(controls);
            LipPencil currentLipPencil = (LipPencil)currentProduct;

            ComboBox tempComboBox = (ComboBox)controlList[typeOfPencilIndex];
            tempComboBox.SelectedValue = Enum.GetName(typeof(LipPencil.TypeOfPencil), currentLipPencil.PencilDevice);

            tempComboBox = (ComboBox)controlList[textureTypeIndex];
            tempComboBox.SelectedValue = Enum.GetName(typeof(LipPencil.TypeOfTexture), currentLipPencil.PencilTexture);

        }


        public override void GetDataFromComponents(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            base.GetDataFromComponents(currentProduct, controls);
            Control[] controlList = GetComponentsForInput(controls);
            LipPencil currentLipPencil = (LipPencil)currentProduct;

            ComboBox temp = (ComboBox)controlList[typeOfPencilIndex];
            currentLipPencil.PencilDevice = (LipPencil.TypeOfPencil)Enum.Parse(typeof(LipPencil.TypeOfPencil), temp.SelectedValue.ToString());
            temp = (ComboBox)controlList[textureTypeIndex];
            currentLipPencil.PencilTexture = (LipPencil.TypeOfTexture)Enum.Parse(typeof(LipPencil.TypeOfTexture), temp.SelectedValue.ToString());
        }

        public override CosmeticProduct GetSomeCosmeticProduct(int index)
        {
            return new LipPencil(index);
        }
    }
}
