using QuanLyBenhVien.Model;
using QuanLyBenhVien.ViewModel;
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
using System.Windows.Shapes;

namespace QuanLyBenhVien.View
{
    /// <summary>
    /// Interaction logic for PhanCong.xaml
    /// </summary>
    public partial class PhanCong : Window
    {
        public PhanCong()
        {   
            InitializeComponent();
            PhanCongViewModel viewModel = new PhanCongViewModel();
            DataContext = viewModel;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DSPhanCong.SelectedItem is PhanCongModel selectedItem)
            {
                TxB_MaLichTruc.Text = selectedItem.MaLichTruc;
                TxB_MaBacSi.Text = selectedItem.MaBacSi;
                TxB_NgayTruc.Text = selectedItem.NgayTruc.ToString("MM/dd/yyyy HH:mm:ss");
                TxB_PhanCong.Text = selectedItem.PhanCong;
                TxB_TrangThai.Text = selectedItem.TrangThai; 
            }
        }
    }
}
