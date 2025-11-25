using System;
using System.Windows;
using System.Windows.Controls;

namespace patrimonio_digital
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ReplaceTheme(string path)
        {
            var rd = new ResourceDictionary() { Source = new Uri(path, UriKind.Relative) };
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(rd);
        }

        private void ThemeToggle_Checked(object sender, RoutedEventArgs e)
        {
            ReplaceTheme("Themes/Light.xaml");
        }

        private void ThemeToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            ReplaceTheme("Themes/Dark.xaml");
        }
    }
}
