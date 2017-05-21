using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lab_3.Helpers;
using lab_3.Factories;

namespace lab_3.Helpers
{
    public interface IPlugin
    {
        CosmeticFactory GetFormLoader();
    }
}
