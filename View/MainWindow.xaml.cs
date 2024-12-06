using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace QuanLyBenhVien
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        private void panelControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void panelControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else this.WindowState = WindowState.Maximized;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnTrangChu_Click(object sender, RoutedEventArgs e)
        {
            exNhanVien.IsExpanded = false;
            exDieuTri.IsExpanded = false;
            exThongTin.IsExpanded = false;
            btnNhanVien.IsChecked = false;
            btnDieuTri.IsChecked = false;
            btnThongTin.IsChecked = false;
            btnTTNhanVien.IsChecked = false;
            btnLichHenKham.IsChecked = false;
            btnLichTruc.IsChecked = false;
            btnKhoa.IsChecked = false;
            btnBenhAn.IsChecked = false;
            btnKhamBenh.IsChecked = false;
            btnDonThuoc.IsChecked = false;
            btnBenh.IsChecked = false;
            btnThuoc.IsChecked = false;
            btnVatDung.IsChecked = false;
            btnHoaDon.IsChecked = false;
        }

        private void btnTTNhanVien_Click(object sender, RoutedEventArgs e)
        {
            exDieuTri.IsExpanded = false;
            exThongTin.IsExpanded = false;
            btnDieuTri.IsChecked=false;
            btnThongTin.IsChecked=false;
            btnTrangChu.IsChecked = false;
            btnBenhNhan.IsChecked = false;
            btnBenhAn.IsChecked = false;
            btnKhamBenh.IsChecked = false;
            btnDonThuoc.IsChecked = false;
            btnBenh.IsChecked = false;
            btnThuoc.IsChecked = false;
            btnVatDung.IsChecked = false;
            btnHoaDon.IsChecked = false;

        }

        private void btnLichTruc_Click(object sender, RoutedEventArgs e)
        {
            exDieuTri.IsExpanded = false;
            exThongTin.IsExpanded = false;
            btnDieuTri.IsChecked = false;
            btnThongTin.IsChecked = false;
            btnTrangChu.IsChecked = false;
            btnBenhNhan.IsChecked = false;
            btnBenhAn.IsChecked = false;
            btnKhamBenh.IsChecked = false;
            btnDonThuoc.IsChecked = false;
            btnBenh.IsChecked = false;
            btnThuoc.IsChecked = false;
            btnVatDung.IsChecked = false;
            btnHoaDon.IsChecked = false;
        }

        private void btnLichHenKham_Click(object sender, RoutedEventArgs e)
        {
            exDieuTri.IsExpanded = false;
            exThongTin.IsExpanded = false;
            btnDieuTri.IsChecked = false;
            btnThongTin.IsChecked = false;
            btnTrangChu.IsChecked = false;
            btnBenhNhan.IsChecked = false;
            btnBenhAn.IsChecked = false;
            btnKhamBenh.IsChecked = false;
            btnDonThuoc.IsChecked = false;
            btnBenh.IsChecked = false;
            btnThuoc.IsChecked = false;
            btnVatDung.IsChecked = false;
            btnHoaDon.IsChecked = false;
        }

        private void btnKhoa_Click(object sender, RoutedEventArgs e)
        {
            exDieuTri.IsExpanded = false;
            exThongTin.IsExpanded = false;
            btnDieuTri.IsChecked = false;
            btnThongTin.IsChecked = false;
            btnTrangChu.IsChecked = false;
            btnBenhNhan.IsChecked = false;
            btnBenhAn.IsChecked = false;
            btnKhamBenh.IsChecked = false;
            btnDonThuoc.IsChecked = false;
            btnBenh.IsChecked = false;
            btnThuoc.IsChecked = false;
            btnVatDung.IsChecked = false;
            btnHoaDon.IsChecked = false;
        }

        private void btnBenhAn_Click(object sender, RoutedEventArgs e)
        {
            exNhanVien.IsExpanded = false;
            exThongTin.IsExpanded = false;
            btnNhanVien.IsChecked = false;
            btnThongTin.IsChecked = false;
            btnTrangChu.IsChecked=false;
            btnBenhNhan.IsChecked=false;
            btnTTNhanVien.IsChecked = false;
            btnLichHenKham.IsChecked = false;
            btnLichTruc.IsChecked = false;
            btnKhoa.IsChecked = false;
            btnBenh.IsChecked = false;
            btnThuoc.IsChecked = false;
            btnVatDung.IsChecked = false;
            btnHoaDon.IsChecked = false;
        }

        private void btnKhamBenh_Click(object sender, RoutedEventArgs e)
        {
            exNhanVien.IsExpanded = false;
            exThongTin.IsExpanded = false;
            btnNhanVien.IsChecked = false;
            btnThongTin.IsChecked = false;
            btnTrangChu.IsChecked = false;
            btnBenhNhan.IsChecked = false;
            btnTTNhanVien.IsChecked = false;
            btnLichHenKham.IsChecked = false;
            btnLichTruc.IsChecked = false;
            btnKhoa.IsChecked = false;
            btnBenh.IsChecked = false;
            btnThuoc.IsChecked = false;
            btnVatDung.IsChecked = false;
            btnHoaDon.IsChecked = false;
        }

        private void btnDonThuoc_Click(object sender, RoutedEventArgs e)
        {
            exNhanVien.IsExpanded = false;
            exThongTin.IsExpanded = false;
            btnNhanVien.IsChecked = false;
            btnThongTin.IsChecked = false;
            btnTrangChu.IsChecked = false;
            btnBenhNhan.IsChecked = false;
            btnTTNhanVien.IsChecked = false;
            btnLichHenKham.IsChecked = false;
            btnLichTruc.IsChecked = false;
            btnKhoa.IsChecked = false;
            btnBenh.IsChecked = false;
            btnThuoc.IsChecked = false;
            btnVatDung.IsChecked = false;
            btnHoaDon.IsChecked = false;
        }

        private void btnBenhNhan_Click(object sender, RoutedEventArgs e)
        {
            exNhanVien.IsExpanded = false;
            exDieuTri.IsExpanded = false;
            exThongTin.IsExpanded = false;
            btnNhanVien.IsChecked = false;
            btnDieuTri.IsChecked = false;
            btnThongTin.IsChecked = false;
            btnTTNhanVien.IsChecked = false;
            btnLichHenKham.IsChecked = false;
            btnLichTruc.IsChecked = false;
            btnKhoa.IsChecked = false;
            btnBenhAn.IsChecked = false;
            btnKhamBenh.IsChecked = false;
            btnDonThuoc.IsChecked = false;
            btnBenh.IsChecked = false;
            btnThuoc.IsChecked = false;
            btnVatDung.IsChecked = false;
            btnHoaDon.IsChecked = false;
        }

        private void btnBenh_Click(object sender, RoutedEventArgs e)
        {
            exNhanVien.IsExpanded = false;
            exDieuTri.IsExpanded = false;
            btnNhanVien.IsChecked = false;
            btnDieuTri.IsChecked = false;
            btnTrangChu.IsChecked = false;
            btnBenhNhan.IsChecked = false;
            btnTTNhanVien.IsChecked = false;
            btnLichHenKham.IsChecked = false;
            btnLichTruc.IsChecked = false;
            btnKhoa.IsChecked = false;
            btnBenhAn.IsChecked = false;
            btnKhamBenh.IsChecked = false;
            btnDonThuoc.IsChecked = false;
        }

        private void btnThuoc_Click(object sender, RoutedEventArgs e)
        {
            exNhanVien.IsExpanded = false;
            exDieuTri.IsExpanded = false;
            btnNhanVien.IsChecked = false;
            btnDieuTri.IsChecked = false;
            btnTrangChu.IsChecked = false;
            btnBenhNhan.IsChecked = false;
            btnTTNhanVien.IsChecked = false;
            btnLichHenKham.IsChecked = false;
            btnLichTruc.IsChecked = false;
            btnKhoa.IsChecked = false;
            btnBenhAn.IsChecked = false;
            btnKhamBenh.IsChecked = false;
            btnDonThuoc.IsChecked = false;
        }

        private void btnVatDung_Click(object sender, RoutedEventArgs e)
        {
            exNhanVien.IsExpanded = false;
            exDieuTri.IsExpanded = false;
            btnNhanVien.IsChecked = false;
            btnDieuTri.IsChecked = false;
            btnTrangChu.IsChecked = false;
            btnBenhNhan.IsChecked = false;
            btnTTNhanVien.IsChecked = false;
            btnLichHenKham.IsChecked = false;
            btnLichTruc.IsChecked = false;
            btnKhoa.IsChecked = false;
            btnBenhAn.IsChecked = false;
            btnKhamBenh.IsChecked = false;
            btnDonThuoc.IsChecked = false;
        }

        private void btnHoaDon_Click(object sender, RoutedEventArgs e)
        {
            exNhanVien.IsExpanded = false;
            exDieuTri.IsExpanded = false;
            btnNhanVien.IsChecked = false;
            btnDieuTri.IsChecked = false;
            btnTrangChu.IsChecked = false;
            btnBenhNhan.IsChecked = false;
            btnTTNhanVien.IsChecked = false;
            btnLichHenKham.IsChecked = false;
            btnLichTruc.IsChecked = false;
            btnKhoa.IsChecked = false;
            btnBenhAn.IsChecked = false;
            btnKhamBenh.IsChecked = false;
            btnDonThuoc.IsChecked = false;
        }

        private void btnNhanVien_Click(object sender, RoutedEventArgs e)
        {
            var expander = exNhanVien;

            if (expander != null)
            {
                // Đảo ngược trạng thái IsExpanded của Expander
                expander.IsExpanded = !expander.IsExpanded;
            }

            //exDieuTri.IsExpanded = false;
            //exThongTin.IsExpanded = false;
            //btnDieuTri.IsChecked = false;
            //btnThongTin.IsChecked = false;
            //btnTrangChu.IsChecked = false;
            //btnBenhNhan.IsChecked = false;
            //btnBenhAn.IsChecked = false;
            //btnKhamBenh.IsChecked = false;
            //btnDonThuoc.IsChecked = false;
            //btnBenh.IsChecked = false;
            //btnThuoc.IsChecked = false;
            //btnVatDung.IsChecked = false;
            //btnHoaDon.IsChecked = false;
        }

        private void btnDieuTri_Click(object sender, RoutedEventArgs e)
        {
            var expander = exDieuTri;

            if (expander != null)
            {
                // Đảo ngược trạng thái IsExpanded của Expander
                expander.IsExpanded = !expander.IsExpanded;
            }

            //exNhanVien.IsExpanded = false;
            //exThongTin.IsExpanded = false;
            //btnNhanVien.IsChecked = false;
            //btnThongTin.IsChecked = false;
            //btnTrangChu.IsChecked = false;
            //btnBenhNhan.IsChecked = false;
            //btnTTNhanVien.IsChecked = false;
            //btnLichHenKham.IsChecked = false;
            //btnLichTruc.IsChecked = false;
            //btnKhoa.IsChecked = false;
            //btnBenh.IsChecked = false;
            //btnThuoc.IsChecked = false;
            //btnVatDung.IsChecked = false;
            //btnHoaDon.IsChecked = false;
        }

        private void btnThongTin_Click(object sender, RoutedEventArgs e)
        {
            var expander = exThongTin;

            if (expander != null)
            {
                // Đảo ngược trạng thái IsExpanded của Expander
                expander.IsExpanded = !expander.IsExpanded;
            }

            //exNhanVien.IsExpanded = false;
            //exDieuTri.IsExpanded = false;
            //btnNhanVien.IsChecked = false;
            //btnDieuTri.IsChecked = false;
            //btnTrangChu.IsChecked = false;
            //btnBenhNhan.IsChecked = false;
            //btnTTNhanVien.IsChecked = false;
            //btnLichHenKham.IsChecked = false;
            //btnLichTruc.IsChecked = false;
            //btnKhoa.IsChecked = false;
            //btnBenhAn.IsChecked = false;
            //btnKhamBenh.IsChecked = false;
            //btnDonThuoc.IsChecked = false;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            exNhanVien.IsExpanded = true;
            exDieuTri.IsExpanded = false;
            exThongTin.IsExpanded = false;
            btnNhanVien.IsChecked = true;
            btnDieuTri.IsChecked = false;
            btnThongTin.IsChecked = false;
            btnTrangChu.IsChecked = false;
            btnBenhNhan.IsChecked = false;
            btnTTNhanVien.IsChecked = true;
            btnLichHenKham.IsChecked = false;
            btnLichTruc.IsChecked = false;
            btnKhoa.IsChecked = false;
            btnBenhAn.IsChecked = false;
            btnKhamBenh.IsChecked = false;
            btnDonThuoc.IsChecked = false;
            btnBenh.IsChecked = false;
            btnThuoc.IsChecked = false;
            btnVatDung.IsChecked = false;
            btnHoaDon.IsChecked = false;
        }

        private void MenuItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            MenuItem_Click(sender, e);
        }
    }
}