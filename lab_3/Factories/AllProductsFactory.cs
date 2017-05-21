using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lab_3.Factories.NailFactories;
using lab_3.Factories.EyesFactories;
using lab_3.Factories.LipFactories;
using lab_3.Factories.FaceFactories;

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

        public void AddProduct(CosmeticFactory someFormLoader)
        {
            FactoryList.Add(someFormLoader);
        }
        
    }
}
