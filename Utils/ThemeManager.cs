using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace patrimonio_digital.Utils
{
   public static class ThemeManager
    {
        public static void ApplyTheme(string themePath)
        {
            var newTheme = new ResourceDictionary()
            {
                Source = new Uri(themePath, UriKind.Relative)
            };

            // remove o antigo
            Application.Current.Resources.MergedDictionaries.Clear();

            // aplica o novo
            Application.Current.Resources.MergedDictionaries.Add(newTheme);
        }
    }
}
