using System.Collections.Generic;
using lab_3.Factories.NailFactories;
using lab_3.Factories.EyesFactories;
using lab_3.Factories.LipFactories;
using lab_3.Factories.FaceFactories;
using System.Windows.Forms;

namespace lab_3.Factories
{
    public class AllProductsFactory
    {
        private List<CosmeticFactory> factoryList;
        public List<CosmeticFactory> FactoryList
        {
            get
            {
                return factoryList;
            }
            private set
            { }
        }


        public AllProductsFactory()
        {
            factoryList = new List<CosmeticFactory>();
            FactoryList.Add(new NailPolishFactory());
            FactoryList.Add(new MascaraFactory());
            FactoryList.Add(new EyeshadowFactory());
            FactoryList.Add(new LipstickFactory());
            FactoryList.Add(new LipglossFactory());
            FactoryList.Add(new LipPencilFactory());
            FactoryList.Add(new PowderFactory());
            FactoryList.Add(new FoundationFactory());
        }

        public bool CheckIfAlreadyInList(CosmeticFactory someFormLoader)
        {
            bool duplicateFound = false;
            int i = 0;
            while (!duplicateFound && i < factoryList.Count)
            {
                if (factoryList[i].GetType().ToString() == someFormLoader.GetType().ToString())
                {
                    duplicateFound = true;
                }
                i++;
            }
            return duplicateFound;
        }

        public void AddProduct(CosmeticFactory someFormLoader)
        {
            if (!CheckIfAlreadyInList(someFormLoader))
            {
                FactoryList.Add(someFormLoader);
            }
            else
            {
                MessageBox.Show(someFormLoader.GetClassName() + " - " + "Данный продукт уже существует!");
            }
        }
        
    }
}
