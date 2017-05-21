using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using lab_3.Classes;

namespace lab_3.Factories
{
    public class Item
    {
        public string NameToShow { get; set; }
        public string Value { get; set; }
        public Item(string NameToShow, string Value)
        {
            this.NameToShow = NameToShow;
            this.Value = Value;
        }
    }

    public abstract class CosmeticFactory
    {
        const int backspaceCode = 8;

        const int nameIndex = 0;
        const int brandIndex = 1;
        const int priceCategoryIndex = 2;
        const int colorButtonIndex = 3;

        public abstract object GetClassName();

        public void TextBoxString_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != backspaceCode) && (e.KeyChar != ' ') && !Char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public void TextBoxDigits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != backspaceCode))
            {
                e.Handled = true;
            }
        }


        public TextBox GetTextBox(string name, Size size, Point location, int tabIndex,
            KeyPressEventHandler keyPressEvent)
        {
            TextBox textboxToCreate = new TextBox();
            textboxToCreate.Location = location;
            textboxToCreate.Name = name;
            textboxToCreate.TabIndex = tabIndex;
            textboxToCreate.KeyPress += keyPressEvent;
            return textboxToCreate;
        }

        public Label GetLabel(string name, string text, Size size, Point location, int tabIndex)
        {
            Label labelToCreate = new Label();
            labelToCreate.Location = location;
            labelToCreate.Name = name;
            labelToCreate.Text = text;
            labelToCreate.Size = size;
            labelToCreate.BackColor = Color.Transparent;
            labelToCreate.TabIndex = tabIndex;
            return labelToCreate;
        }
       
        public ComboBox GetComboBox(string name, Size size, Point location, int tabIndex, List<string> names, List<string> values)
        {
            ComboBox comboBoxToCreate = new ComboBox();
            comboBoxToCreate.Name = name;
            comboBoxToCreate.TabIndex = tabIndex;

            int len = values.Count;
            List<Item> dataSource = new List<Item>();
        
            for (int i = 0; i < len; i++)
            {
                dataSource.Add(new Item(names[i], values[i]));
            }
            
            comboBoxToCreate.DataSource = dataSource;
            comboBoxToCreate.DisplayMember = "NameToShow";
            comboBoxToCreate.ValueMember = "Value";

            comboBoxToCreate.Location = location;
            comboBoxToCreate.DropDownStyle = ComboBoxStyle.DropDownList;
            return comboBoxToCreate;
        }
        
        private void buttonColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Button buttonToChange = (Button)sender;
                buttonToChange.BackColor = colorDialog.Color;
           
            }
        }

        public Button GetPaletteButton(string name, Size size, Point location, int tabIndex, EventHandler buttonClick)
        {
            Button buttonToCreate = new Button();
            buttonToCreate.Name = name;
            buttonToCreate.Size = size;
            buttonToCreate.Location = location;
            buttonToCreate.TabIndex = tabIndex;
            buttonToCreate.Click += buttonClick;
            return buttonToCreate;
        }

        public CheckBox GetCheckBox(string name, Size size, Point location, int tabIndex)
        {
            CheckBox result = new CheckBox();
            result.Name = name;
            result.Size = size;
            result.Location = location;
            result.TabIndex = tabIndex;
            result.Checked = true;
            return result;
        }

        public virtual List<Control> GetListControl(Size size, int leftCoord)
        {
            List<Control> resultList = new List<Control>();

            resultList.Add(GetLabel("productName", "Название продукта", size, new Point(leftCoord, 10), 1));
            resultList.Add(GetTextBox("productName", size, new Point(leftCoord, 30), 2, TextBoxString_KeyPress));
     

            resultList.Add(GetLabel("brand", "Бренд", size, new Point(leftCoord, 60), 3));
            resultList.Add(GetTextBox("brand", size, new Point(leftCoord, 80), 4, TextBoxString_KeyPress));

            List<string> namesOfItems = EnumHelper<CosmeticProduct.PriceCategory>.GetAllDescriptions();
            List<string> values = EnumHelper<CosmeticProduct.PriceCategory>.GetEnumValues();
            resultList.Add(GetLabel("priceCategory", "Ценовая категория", size, new Point(leftCoord, 110), 5));
            resultList.Add(GetComboBox("priceCategory", size, new Point(leftCoord, 130), 6, namesOfItems, values));

            resultList.Add(GetLabel("color", "Цвет", size, new Point(leftCoord, 160), 7));
            resultList.Add(GetPaletteButton("colorButton", new Size(50, 30), new Point(leftCoord, 180), 8, buttonColor_Click));

            return resultList;
        }


        public abstract CosmeticProduct GetSomeCosmeticProduct(int index);

        public Control[] GetComponentsForInput(Control.ControlCollection controlList)
        {
            List<Control> result = new List<Control>();
            foreach (Control someControl in controlList)
            {
                if (!(someControl is Label))
                {
                    result.Add(someControl);
                }
            }
            return result.ToArray();
        }

        public virtual void GetDataFromComponents(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            Control[] controlList = GetComponentsForInput(controls);
            currentProduct.ProductName = controlList[nameIndex].Text;
            currentProduct.Brand = controlList[brandIndex].Text;
            ComboBox temp = (ComboBox)controlList[priceCategoryIndex];
            currentProduct.PriceCategoryOfProduct = (CosmeticProduct.PriceCategory)Enum.Parse(typeof(CosmeticProduct.PriceCategory), temp.SelectedValue.ToString());
            currentProduct.Color = controlList[colorButtonIndex].BackColor;
        }


        public virtual void LoadDataToComponets(CosmeticProduct currentProduct, Control.ControlCollection controls)
        {
            Control[] controlList = GetComponentsForInput(controls);
            controlList[nameIndex].Text = Convert.ToString(currentProduct.ProductName);
            controlList[brandIndex].Text = Convert.ToString(currentProduct.Brand);

            ComboBox temp = (ComboBox)controlList[priceCategoryIndex];
            temp.SelectedValue = Enum.GetName(typeof(CosmeticProduct.PriceCategory), currentProduct.PriceCategoryOfProduct);
          
            controlList[colorButtonIndex].BackColor = currentProduct.Color;
        }
        
        public virtual bool CheckTextBoxes(Control.ControlCollection controlList)
        {
            bool result = true;
            int i = 0;
            while (result && i < controlList.Count)
            {
                if (controlList[i] is TextBox)
                {
                    if (controlList[i].Text == "")
                    {
                        result = false;
                        break;
                    }
                }
                i++;
            }
            return result;
        }

    }
}
