using QuanLyBenhVien.Model;
using QuanLyBenhVien.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyBenhVien.View
{
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
