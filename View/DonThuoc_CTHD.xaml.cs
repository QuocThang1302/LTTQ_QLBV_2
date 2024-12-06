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
using System.Data.SqlClient;

namespace QuanLyBenhVien.View
{
    /// <summary>
    /// Interaction logic for DonThuoc_CTHD.xaml
    /// </summary>
    public partial class DonThuoc_CTDT : Window
    {
        public DonThuoc_CTDT()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonThem_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin từ TextBox
            string maDonThuoc = TxB_MaDonThuoc.Text.Trim();
            string maBenhNhan = TxB_MaBenhNhan.Text.Trim();
            string maBacSi = TxB_MaBacSi.Text.Trim();
            string ngayLapDon = TxB_NgayLapDon.Text.Trim();

            string tenThuoc = TxB_TenThuoc.Text.Trim();
            string soLuong = TxB_SoLuong.Text.Trim();
            string huongDan = TxB_HuongDan.Text.Trim();

            // Kiểm tra lỗi nhập liệu
            if (string.IsNullOrEmpty(maDonThuoc) || string.IsNullOrEmpty(maBenhNhan) ||
                string.IsNullOrEmpty(maBacSi) || string.IsNullOrEmpty(ngayLapDon) ||
                string.IsNullOrEmpty(tenThuoc) || string.IsNullOrEmpty(soLuong) || string.IsNullOrEmpty(huongDan))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Kết nối cơ sở dữ liệu và thêm dữ liệu vào hai bảng
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=LAPTOP-702RPVLR;Initial Catalog=BV;Integrated Security=True"))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Thêm vào bảng DonThuoc
                            string queryDonThuoc = @"
                        INSERT INTO DonThuoc (MaDonThuoc, MaBenhNhan, MaBacSi, NgayLapDon)
                        VALUES (@MaDonThuoc, @MaBenhNhan, @MaBacSi, @NgayLapDon)";
                            using (SqlCommand cmdDonThuoc = new SqlCommand(queryDonThuoc, conn, transaction))
                            {
                                cmdDonThuoc.Parameters.AddWithValue("@MaDonThuoc", maDonThuoc);
                                cmdDonThuoc.Parameters.AddWithValue("@MaBenhNhan", maBenhNhan);
                                cmdDonThuoc.Parameters.AddWithValue("@MaBacSi", maBacSi);
                                cmdDonThuoc.Parameters.AddWithValue("@NgayLapDon", DateTime.Parse(ngayLapDon));

                                cmdDonThuoc.ExecuteNonQuery();
                            }

                            // Thêm vào bảng CTDonThuoc
                            string queryCTDonThuoc = @"
                        INSERT INTO CTDonThuoc (MaDonThuoc, TenThuoc, SoLuong, HuongDan)
                        VALUES (@MaDonThuoc, @TenThuoc, @SoLuong, @HuongDan)";
                            using (SqlCommand cmdCTDonThuoc = new SqlCommand(queryCTDonThuoc, conn, transaction))
                            {
                                cmdCTDonThuoc.Parameters.AddWithValue("@MaDonThuoc", maDonThuoc);
                                cmdCTDonThuoc.Parameters.AddWithValue("@TenThuoc", tenThuoc);
                                cmdCTDonThuoc.Parameters.AddWithValue("@SoLuong", int.Parse(soLuong));
                                cmdCTDonThuoc.Parameters.AddWithValue("@HuongDan", huongDan);

                                cmdCTDonThuoc.ExecuteNonQuery();
                            }

                            // Xác nhận giao dịch
                            transaction.Commit();
                            MessageBox.Show("Thêm dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception ex)
                        {
                            // Hoàn tác giao dịch nếu xảy ra lỗi
                            transaction.Rollback();
                            MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi kết nối: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
