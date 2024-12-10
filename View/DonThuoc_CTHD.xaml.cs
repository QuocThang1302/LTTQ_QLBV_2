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
using System.Data;


namespace QuanLyBenhVien.View
{
    /// <summary>
    /// Interaction logic for DonThuoc_CTHD.xaml
    /// </summary>
    public partial class DonThuoc_CTDT : Window
    {
        private string connectionString = @"Server=LAPTOP-702RPVLR;Database=BV;Trusted_Connection=True;";
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
            string maDonThuoc = TxB_MaDonThuoc.Text.Trim();
            string tenThuoc = TxB_TenThuoc.Text.Trim();
            string huongDan = TxB_HuongDan.Text.Trim();
            int soLuong;

            // Kiểm tra các trường dữ liệu
            if (string.IsNullOrEmpty(maDonThuoc) || string.IsNullOrEmpty(tenThuoc) ||
                string.IsNullOrEmpty(huongDan) || string.IsNullOrEmpty(TxB_SoLuong.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ dữ liệu!");
                return;
            }

            // Kiểm tra tính hợp lệ của số lượng
            if (!int.TryParse(TxB_SoLuong.Text.Trim(), out soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng không hợp lệ!");
                return;
            }

            // Tìm thông tin thuốc từ tên thuốc
            string queryThuoc = "SELECT MaThuoc, GiaTien FROM Thuoc WHERE TenThuoc = @TenThuoc";
            DataTable dtThuoc = GetDataTable(queryThuoc, new SqlParameter("@TenThuoc", tenThuoc));

            if (dtThuoc.Rows.Count == 0)
            {
                MessageBox.Show("Thuốc không tồn tại!");
                return;
            }

            string maThuoc = dtThuoc.Rows[0]["MaThuoc"].ToString();
            decimal giaTien = Convert.ToDecimal(dtThuoc.Rows[0]["GiaTien"]);

            // Lấy MaBenhNhan từ Mã đơn thuốc
            string queryMaBenhNhan = "SELECT MaBenhNhan FROM DonThuoc WHERE MaDonThuoc = @MaDonThuoc";
            DataTable dtBenhNhan = GetDataTable(queryMaBenhNhan, new SqlParameter("@MaDonThuoc", maDonThuoc));

            if (dtBenhNhan.Rows.Count == 0)
            {
                MessageBox.Show("Mã đơn thuốc không hợp lệ!");
                return;
            }

            string maBenhNhan = dtBenhNhan.Rows[0]["MaBenhNhan"].ToString();

            // Tìm MaHoaDon từ MaBenhNhan
            string queryHoaDon = "SELECT MaHoaDon FROM HoaDon WHERE MaBenhNhan = @MaBenhNhan";
            DataTable dtHoaDon = GetDataTable(queryHoaDon, new SqlParameter("@MaBenhNhan", maBenhNhan));

            if (dtHoaDon.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy hóa đơn liên quan!");
                return;
            }

            string queryCheck = @"
    SELECT COUNT(*) 
    FROM CTDonThuoc 
    WHERE MaDonThuoc = @MaDonThuoc AND MaThuoc = @MaThuoc";

            int count = ExecuteCountQuery(queryCheck,
                new SqlParameter("@MaDonThuoc", maDonThuoc),
                new SqlParameter("@MaThuoc", maThuoc));

            if (count > 0)
            {
                MessageBox.Show("Chi tiết đơn thuốc này đã tồn tại!");
                return;
            }

            string maHoaDon = dtHoaDon.Rows[0]["MaHoaDon"].ToString();

            // Thêm dữ liệu vào bảng CTDonThuoc
            string queryInsert = @"
    INSERT INTO CTDonThuoc (MaDonThuoc, MaThuoc, SoLuong, GiaTien, HuongDanSuDung, MaHoaDon) 
    VALUES (@MaDonThuoc, @MaThuoc, @SoLuong, @GiaTien, @HuongDanSuDung, @MaHoaDon)";

            try
            {
                int result = ExecuteNonQuery(queryInsert,
                    new SqlParameter("@MaDonThuoc", maDonThuoc),
                    new SqlParameter("@MaThuoc", maThuoc),
                    new SqlParameter("@SoLuong", soLuong),
                    new SqlParameter("@GiaTien", giaTien),
                    new SqlParameter("@HuongDanSuDung", huongDan),
                    new SqlParameter("@MaHoaDon", maHoaDon));

                if (result > 0)
                {
                    MessageBox.Show("Thêm thành công!");
                }
                else
                {
                    MessageBox.Show("Thêm thất bại!");
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 2627) // Vi phạm khóa chính
                {
                    MessageBox.Show("Chi tiết đơn thuốc đã tồn tại (vi phạm khóa chính)!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (sqlEx.Number == 547) // Vi phạm khóa ngoại
                {
                    MessageBox.Show("Dữ liệu nhập vào vi phạm ràng buộc khóa ngoại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show($"Lỗi SQL: {sqlEx.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Hàm thực thi câu lệnh truy vấn trả về DataTable
        private DataTable GetDataTable(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        // Hàm thực thi câu lệnh truy vấn không trả về
        private int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        private int ExecuteCountQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetInt32(0); // Lấy giá trị COUNT(*) từ cột đầu tiên
                        }
                        return 0; // Nếu không có kết quả, trả về 0
                    }
                }
            }
        }

        private void ButtonXoa_Click(object sender, RoutedEventArgs e)
        {
            string maDonThuoc = TxB_MaDonThuoc.Text.Trim();
            string tenThuoc = TxB_TenThuoc.Text.Trim();
            if (string.IsNullOrEmpty(maDonThuoc) || string.IsNullOrEmpty(tenThuoc))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ dữ liệu!");
                return;
            }
            // Tìm mã thuốc từ tên thuốc
            string queryThuoc = "SELECT MaThuoc FROM Thuoc WHERE TenThuoc = @TenThuoc";
            DataTable dtThuoc = GetDataTable(queryThuoc, new SqlParameter("@TenThuoc", tenThuoc));

            if (dtThuoc.Rows.Count == 0)
            {
                MessageBox.Show("Thuốc không tồn tại!");
                return;
            }

            string maThuoc = dtThuoc.Rows[0]["MaThuoc"].ToString();

            // Xóa dữ liệu trong bảng CTDonThuoc
            string queryDelete = "DELETE FROM CTDonThuoc WHERE MaDonThuoc = @MaDonThuoc AND MaThuoc = @MaThuoc";
            int result = ExecuteNonQuery(queryDelete,
                new SqlParameter("@MaDonThuoc", maDonThuoc),
                new SqlParameter("@MaThuoc", maThuoc));

            if (result > 0)
            {
                MessageBox.Show("Xóa thành công!");
            }
            else
            {
                MessageBox.Show("Xóa thất bại!");
            }
        }
    }
}
