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
    /// Interaction logic for DonThuoc.xaml
    /// </summary>
    public partial class DonThuoc : UserControl
    {
        public DonThuoc()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DonThuoc_CTDT ctdt = new DonThuoc_CTDT();
            ctdt.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CTHD hd = new CTHD();
            hd.Show();
        }
    }
}
