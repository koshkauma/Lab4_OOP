using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using lab_3.Factories;
using lab_3.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using lab_3.Helpers;
using lab_3.PluginSignaturing;
using System.Security.Cryptography;
using System.IO;

namespace lab_3
{
    public partial class serializeForm : Form
    {
        const int listDisplayMemberIndex = 0;
        CosmeticListClass list = new CosmeticListClass();

        public serializeForm()
        {
            InitializeComponent();
            foreach (CosmeticFactory factory in factoryFormEditor.FactoryList)
            {
                comboBoxItems.Items.Add(factory.GetClassName());
            }
            comboBoxItems.SelectedIndex = 0;
            panelAdd.Controls.Clear();
            panelAdd.Controls.AddRange(factoryFormEditor.FactoryList[comboBoxItems.SelectedIndex].GetListControl(new Size(200, 15), 10).ToArray());
            panelAdd.Tag = factoryFormEditor.FactoryList[comboBoxItems.SelectedIndex].GetSomeCosmeticProduct(comboBoxItems.SelectedIndex);
            listBoxOfProducts.DataSource = list.CosmeticList;
            listBoxOfProducts.DisplayMember = "ProductName";
        }


        private static AllProductsFactory factoryFormEditor = new AllProductsFactory();

        private void serializeForm_Load(object sender, EventArgs e)
        {

        }

        private void panelAdd_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBoxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelAdd.Controls.Clear();
            panelAdd.Controls.AddRange(factoryFormEditor.FactoryList[comboBoxItems.SelectedIndex].GetListControl(new Size(200, 20), 10).ToArray());
            panelAdd.Tag = factoryFormEditor.FactoryList[comboBoxItems.SelectedIndex].GetSomeCosmeticProduct(comboBoxItems.SelectedIndex);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            CosmeticProduct temp = (CosmeticProduct)panelAdd.Tag;
            if (factoryFormEditor.FactoryList[temp.ClassIndex].CheckTextBoxes(panelAdd.Controls))
            {
                try
                {
                    factoryFormEditor.FactoryList[temp.ClassIndex].GetDataFromComponents(temp, panelAdd.Controls);
                }
                catch
                {
                    MessageBox.Show("Введите корректные данные!");
                    return;
                }
                panelAdd.Tag = factoryFormEditor.FactoryList[comboBoxItems.SelectedIndex].GetSomeCosmeticProduct(comboBoxItems.SelectedIndex);
                list.CosmeticList.Add(temp);
            }
            else
            {

                MessageBox.Show("Заполните все поля!");
            }
        }

        private void listBoxOfProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            CosmeticProduct currentProduct = (CosmeticProduct)listBoxOfProducts.SelectedItem;
            if (currentProduct != null)
            {
                panelEdit.Controls.Clear();
                panelEdit.Controls.AddRange(factoryFormEditor.FactoryList[currentProduct.ClassIndex].GetListControl(new Size(200, 20), 10).ToArray());
                factoryFormEditor.FactoryList[currentProduct.ClassIndex].LoadDataToComponets(currentProduct, panelEdit.Controls);
                labelEdit.Text = comboBoxItems.Items[currentProduct.ClassIndex].ToString();
                panelEdit.Tag = currentProduct;
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            CosmeticProduct temp = (CosmeticProduct)panelEdit.Tag;
            try
            {
                factoryFormEditor.FactoryList[temp.ClassIndex].GetDataFromComponents(temp, panelEdit.Controls);

            }
            catch
            {
                MessageBox.Show("Данные некорректны! Пожалуйста, повторите ввод");
                return;
            }
            list.CosmeticList.ResetBindings();
        }


        private void сериализоватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxOfProducts.Items.Count == 0)
            {
                MessageBox.Show("В списке ничего нет!");
            }
            else
            {
                if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
                {
                    list.SerializeItemsInList(saveFileDialog.FileName);
                }

            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxOfProducts.SelectedItem != null)
            {
                if (MessageBox.Show("Вы уверены?", "Внимание!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CosmeticProduct temp = (CosmeticProduct)listBoxOfProducts.SelectedItem;
                    list.CosmeticList.Remove(temp);
                    panelEdit.Controls.Clear();
                    labelEdit.Text = "";
                }
            }
        }


        private void десToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                try
                {
                    list.DeserializeItemsInList(openFileDialog.FileName, factoryFormEditor);
                }
                catch (Exception exception)
                {
                    string message = exception.Message;
                    MessageBox.Show(message);
                }
            }
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void buttonClearList_Click(object sender, EventArgs e)
        {
            if (listBoxOfProducts.Items.Count != 0)
            {
                list.CosmeticList.Clear();
                panelEdit.Controls.Clear();
                labelEdit.Text = "";
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        public static List<Type> GetTypes<T>(Assembly assembly)
        {
            if (!typeof(T).IsInterface)
            {
                return null;
            }
            return assembly.GetTypes().Where(x => x.GetInterface(typeof(T).Name) != null).ToList();

        }

        private void buttonLoadPlugin_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Filter = "DLL files | *.dll"
            };

            if (dlg.ShowDialog() != DialogResult.Cancel)
            {
                string pluginPath = dlg.FileName;
                try
                {
                    if (SignatureHelper.CheckIfValid(pluginPath))
                    {

                        Assembly assembly = Assembly.LoadFrom(dlg.FileName);
                        List<Type> pluginTypes = GetTypes<IPlugin>(assembly);

                        if (pluginTypes.Count != 0)
                        {
                            int prevIndex = factoryFormEditor.FactoryList.Count();
                            int counter = 0;
                            for (int i = 0; i < pluginTypes.Count; i++)
                            {
                                IPlugin plugin = Activator.CreateInstance(pluginTypes[i]) as IPlugin;
                                factoryFormEditor.AddProduct(plugin.GetFormLoader());
                                counter++;
                            }

                            for (int i = prevIndex; i < factoryFormEditor.FactoryList.Count; i++)
                            {
                                comboBoxItems.Items.Add(factoryFormEditor.FactoryList[i].GetClassName());
                            }
                        }
                    }
                }
                catch (BadImageFormatException)
                {
                    MessageBox.Show("Ошибка с библиотечным файлом.");
                }

                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
       
            }
            else
            {
                MessageBox.Show("Подлинность НЕ установлена.");
            }

        }
   



     

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Для подписи плагина выберите dll-файл.");
        }

     

        private void buttonSignPlugin_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Filter = "DLL files | *.dll"
            };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
                RSAParameters key = RSAalg.ExportParameters(true);

                byte[] signedData = SignatureHelper.GetSignature(dlg.FileName, key);

                SaveFileDialog saveDlg = new SaveFileDialog();
                string signatureFilePath = Path.GetDirectoryName(dlg.FileName) + "\\" + Path.GetFileNameWithoutExtension(dlg.FileName) + ".signature";
                SignatureHelper.WriteSignatureToFile(signedData, signatureFilePath);
                SignatureHelper.SavePublicKeyToFile(dlg.FileName, RSAalg);
            }
        }

       
    }
    }



