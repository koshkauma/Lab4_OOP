using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lab_3.Factories;
using System.Windows.Forms;
using System.Reflection;

namespace lab_3.Helpers
{
    public static class LoadingOfPlugin
    {
        private static List<Type> GetTypes<T>(Assembly assembly)
        {
            if (!typeof(T).IsInterface)
            {
                return null;
            }
            return assembly.GetTypes().Where(x => x.GetInterface(typeof(T).Name) != null).ToList();

        }

        public static void ProccessLoadingOfPlugins(string pluginPath, AllProductsFactory factoryFormEditor, ComboBox comboBoxToUse)
        {
            Assembly assembly = Assembly.LoadFrom(pluginPath);
            List<Type> pluginTypes = GetTypes<IPlugin>(assembly);

            if (pluginTypes.Count != 0)
            {
                int prevIndex = factoryFormEditor.FactoryList.Count();
                for (int i = 0; i < pluginTypes.Count; i++)
                {

                    IPlugin plugin = Activator.CreateInstance(pluginTypes[i]) as IPlugin;
                    factoryFormEditor.AddProduct(plugin.GetFormLoader());
                }

                for (int i = prevIndex; i < factoryFormEditor.FactoryList.Count; i++)
                {
                    comboBoxToUse.Items.Add(factoryFormEditor.FactoryList[i].GetClassName());
                }
            }


        }
    }
}
