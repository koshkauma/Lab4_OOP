using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lab_3.Factories;

namespace lab_3.Helpers
{
    public class Loader
    {
       public CosmeticFactory LoaderForProduct { get; set; }

       public Loader(CosmeticFactory LoaderForProduct)
       {
            this.LoaderForProduct = LoaderForProduct;
       }
    }
}
