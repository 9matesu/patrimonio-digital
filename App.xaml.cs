using System.Windows;
using patrimonio_digital.MVVM.Model;
using patrimonio_digital.MVVM.View; 

namespace patrimonio_digital
{
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var loginWindow = new Login();
            bool? result = loginWindow.ShowDialog();

            if (result == true)
            {
                try
                {

                    var mainWindow = new MainWindow();
                    MainWindow = mainWindow;
                    mainWindow.Show();
                    // MessageBox.Show("MainWindow abriu!"); //
                }
                catch (Exception ex)
                {
                   // MessageBox.Show("Erro ao abrir a MainWindow: " + ex.Message); //
                    Shutdown();
                }
            }
            else
            {
                MessageBox.Show("Login cancelado ou inválido. Aplicação será encerrada.");
                Shutdown();
            }
        }
    }
}