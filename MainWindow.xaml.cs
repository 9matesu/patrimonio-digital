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
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("MainWindow carregada");//
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //MessageBox.Show("MainWindow fechando");//
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
