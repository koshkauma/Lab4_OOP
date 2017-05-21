using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lab_3.Helpers;
using lab_3.Factories;
using lab_3.Classes.ForLips;
using lab_3.Factories.LipFactories;

namespace lab_3.CosmeticFactories
{
    public class TintPlugin: IPlugin
    {
        Tint someTint = new Tint();

        public string typeOfProduct = "Tint";
        public string parentType = "LipProduct";



        public CosmeticFactory GetFormLoader()
        {
            return new TintFactory();
        }

    }
}
