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
                var viewModel = DataContext as PhanCongViewModel;
                if (viewModel != null)
                {
                    viewModel.NgayTruc = calendarNgayTruc.SelectedDate.Value; // Cập nhật thuộc tính NgayTruc
                }

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
    }
}
