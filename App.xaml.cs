using System.Configuration;
using System.Data;
using System.Windows;
using QuanLyBenhVien.View;

namespace QuanLyBenhVien
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        var mainWindow = new MainWindow();
                        mainWindow.Show();

                        // Đảm bảo không gọi Close() ngay lập tức
                        loginView.Close();
                    });
                }
            };
        }
    }

}
