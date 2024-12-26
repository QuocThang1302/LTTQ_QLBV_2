using QuanLyBenhVien.Model;
using QuanLyBenhVien.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyBenhVien.View
{
    public partial class PhanCong : Window
    {
        public Action OnDataAdded;
        public PhanCong()
        {   
            InitializeComponent();
            PhanCongViewModel viewModel = new PhanCongViewModel();
            DataContext = viewModel;
        }
        private void TxB_NgayTruc_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            popupCalendarNgayTruc.IsOpen = true; // Mở popup khi nhấn vào TextBox
            e.Handled = true; // Ngăn sự kiện lan sang Window_PreviewMouseDown
        }

        private void calendarNgayTruc_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (calendarNgayTruc.SelectedDate.HasValue)
            {
                // Gán giá trị vào Binding Property (NgayTruc)
                var selectedDate = calendarNgayTruc.SelectedDate.Value.ToString("yyyy-MM-dd");
                TxB_NgayTruc.Text = selectedDate;

                popupCalendarNgayTruc.IsOpen = false; // Ẩn popup sau khi chọn ngày
            }
        }

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (popupCalendarNgayTruc.IsOpen && !popupCalendarNgayTruc.IsMouseOver)
            {
                popupCalendarNgayTruc.IsOpen = false; // Ẩn popup nếu nhấn ra ngoài
            }
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
                TxB_NgayTruc.Text = selectedItem.NgayTruc.ToString("yyyy-MM-dd");
                TxB_PhanCong.Text = selectedItem.PhanCong;
                TxB_TrangThai.Text = selectedItem.TrangThai; 
            }
        }
    }
}
