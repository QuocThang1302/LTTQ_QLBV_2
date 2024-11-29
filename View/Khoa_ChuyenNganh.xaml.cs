using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyBenhVien.View
{
    /// <summary>
    /// Interaction logic for Khoa_ChuyenNganh.xaml
    /// </summary>
    public partial class Khoa_ChuyenNganh : UserControl
    {
        public Khoa_ChuyenNganh()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {   
            ChuyenNganh chuyenNganh = new ChuyenNganh();
            chuyenNganh.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Khoa khoa = new Khoa();
            khoa.Show();
        }
    }
}
