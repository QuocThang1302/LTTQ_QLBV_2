using QuanLyBenhVien.Repositories;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace QuanLyBenhVien.View
{
    public partial class DonThuoc : UserControl
    {
        private readonly RepositoryBase _userRepository;
        string ID = Application.Current.Properties.Contains("UserID") ? Application.Current.Properties["UserID"] as string : null;
        public string GetRoleIDByUserID()
        {
            string roleID = null; // Biến để lưu RoleID

            // Câu lệnh SQL để lấy RoleID từ NhanVien theo MaNhanVien
            string query = "SELECT RoleID FROM NhanVien WHERE MaNhanVien = @userID";

            // Sử dụng SqlConnection để kết nối cơ sở dữ liệu
            using (SqlConnection connection = _userRepository.GetConnection())
            {
                try
                {
                    connection.Open(); // Mở kết nối
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Thêm tham số vào câu lệnh SQL
                        command.Parameters.AddWithValue("@userID", ID);

                        // Thực thi câu lệnh và lấy giá trị RoleID
                        var result = command.ExecuteScalar();

                        if (result != null)
                        {
                            roleID = result.ToString(); // Gán giá trị RoleID nếu tìm thấy
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu có
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return roleID; // Trả về RoleID hoặc null nếu không tìm thấy
        }
        public DonThuoc()
        {
            _userRepository = new UserRepository();
            InitializeComponent();
            searchControl.Tmp = "Nhập mã đơn thuốc hoặc mã bác sĩ";
            // Đăng ký sự kiện SearchButtonClicked
            searchControl.SearchButtonClicked += SearchControl_SearchButtonClicked;
            // Đăng ký sự kiện ClearButtonClicked cho nút X
            searchControl.ClearButtonClicked += SearchControl_ClearButtonClicked;
            BacSi();

        }

        private void BacSi()
        {
            string roleID = GetRoleIDByUserID();
            if (roleID == "R02")
            {
                btnXuatHoaDon.Visibility = Visibility.Hidden;
            }
            return;
        }

        private void SearchControl_ClearButtonClicked(object sender, EventArgs e)
        {
            // Logic khi nút X được nhấn
            HienThiDanhSach();
        }

        private void SearchControl_SearchButtonClicked(object sender, string searchText)
        {
            // Lấy mã đơn thuốc từ tham số searchText
            string maDonThuoc = searchText.Trim();
            if (string.IsNullOrEmpty(maDonThuoc))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Câu lệnh SQL để tìm kiếm thông tin đơn thuốc và chi tiết đơn thuốc
            string queryDonThuoc = "SELECT * FROM DonThuoc WHERE MaDonThuoc = @MaDonThuoc OR MaBacSi=@MaDonThuoc";
            string queryCTDonThuoc = "SELECT * FROM CTDonThuoc JOIN Thuoc ON CTDonThuoc.MaThuoc = Thuoc.MaThuoc\r\nWHERE MaDonThuoc IN (SELECT MaDonThuoc FROM DonThuoc WHERE MaBacSi = @MaDonThuoc OR MaDonThuoc = @MaDonThuoc)";

            try
            {
                // Mở kết nối SQL
                sqlCon = _userRepository.GetConnection();
                sqlCon.Open();

                // Hiển thị thông tin của DonThuoc
                adapter = new SqlDataAdapter(queryDonThuoc, sqlCon);
                adapter.SelectCommand.Parameters.AddWithValue("@MaDonThuoc", maDonThuoc);
                ds = new DataSet();
                adapter.Fill(ds, "DonThuoc");
                DataTable dataTableDonThuoc = ds.Tables["DonThuoc"];

                // Gắn dữ liệu vào dgvDonThuoc
                dgvDonThuoc.ItemsSource = dataTableDonThuoc.DefaultView;

                // Kiểm tra nếu không tìm thấy đơn thuốc
                if (dataTableDonThuoc.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // Hiển thị thông tin của CTDonThuoc
                adapter = new SqlDataAdapter(queryCTDonThuoc, sqlCon);
                adapter.SelectCommand.Parameters.AddWithValue("@MaDonThuoc", maDonThuoc);
                adapter.Fill(ds, "CTDonThuoc");
                DataTable dataTableCTDonThuoc = ds.Tables["CTDonThuoc"];

                // Gắn dữ liệu vào dgvCTDonThuoc
                dgvCTDonThuoc.ItemsSource = dataTableCTDonThuoc.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Đảm bảo đóng kết nối SQL
                if (sqlCon != null && sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                HienThiDanhSach();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
        }
        
        SqlConnection sqlCon = null;
        SqlDataAdapter adapter = null;
        DataSet ds = null;
        SqlDataAdapter adapter1 = null;
        DataSet ds1 = null;
        private void HienThiDanhSach()
        {
            if (sqlCon == null)
            {
                sqlCon = _userRepository.GetConnection();
            }
            string query = "SELECT \r\n    MaDonThuoc, \r\n    DonThuoc.MaBenhNhan, \r\n    MaBacSi, \r\n    NgayLapDon, \r\n    DonThuoc.MaHoaDon\r\nFROM DonThuoc \r\nJOIN BenhNhan ON DonThuoc.MaBenhNhan = BenhNhan.MaBenhNhan \r\nJOIN NhanVien NV ON DonThuoc.MaBacSi = NV.MaNhanVien \r\nJOIN HoaDon HD ON DonThuoc.MaHoaDon = HD.MaHoaDon;";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.DeleteCommand = new SqlCommand(
                "DELETE FROM CTDonThuoc WHERE MaDonThuoc=@MaDonThuoc", sqlCon);
            adapter.DeleteCommand.Parameters.Add("@MaDonThuoc", SqlDbType.NVarChar, 20, "MaDonThuoc");
            ds = new DataSet();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter.Fill(ds, "tblDonThuoc");
            sqlCon.Close();

            dgvDonThuoc.ItemsSource = ds.Tables["tblDonThuoc"].DefaultView;

            string query1 = " Select TenThuoc, CTDonThuoc.SoLuong, CTDonThuoc.GiaTien, HuongDanSuDung from CTDonThuoc join Thuoc on CTDonThuoc.MaThuoc = Thuoc.MaThuoc";
            adapter1 = new SqlDataAdapter(query1, sqlCon);
            SqlCommandBuilder builder1 = new SqlCommandBuilder(adapter1);

            ds1 = new DataSet();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter1.Fill(ds1, "tblChiTiet");
            sqlCon.Close();

            dgvCTDonThuoc.ItemsSource = ds1.Tables["tblChiTiet"].DefaultView;
        }
        private int vitri = -1;
        private void dgvCTDonThuoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRow = dgvCTDonThuoc.SelectedItem as DataRowView;

            if (selectedRow == null) return;

            // Lấy dữ liệu từ DataRowView
            DataRow dataRow = selectedRow.Row;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // Lấy hàng được chọn từ DataGrid
            var selectedRow = dgvCTDonThuoc.SelectedItem as DataRowView;

            // Kiểm tra xem người dùng đã chọn dòng nào chưa
            if (selectedRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Xác nhận từ người dùng trước khi xóa
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Lấy DataRow từ DataRowView
                    DataRow dataRow = selectedRow.Row;

                    // Xóa hàng trong DataTable
                    dataRow.Delete();

                    // Mở kết nối cơ sở dữ liệu
                    if (sqlCon.State != ConnectionState.Open)
                    {
                        sqlCon.Open();
                    }

                    //// Cấu hình SqlDataAdapter để xóa dữ liệu từ cơ sở dữ liệu
                    //SqlDataAdapter adapter = new SqlDataAdapter();
                    //adapter.DeleteCommand = new SqlCommand("DELETE FROM CTDonThuoc WHERE MaDonThuoc = @MaDonThuoc", sqlCon);

                    //// Thêm tham số cho câu lệnh DELETE
                    //adapter.DeleteCommand.Parameters.Add("@MaDonThuoc", SqlDbType.NVarChar, 20, "MaDonThuoc");

                    // Thực hiện cập nhật để xóa dữ liệu trong cơ sở dữ liệu
                    int kq = adapter.Update(ds1.Tables["tblChiTiet"]);

                    // Kiểm tra kết quả xóa
                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Cập nhật lại giao diện DataGrid
                        dgvCTDonThuoc.Items.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Xóa dữ liệu thất bại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (SqlException ex)
            {
                // Xử lý các lỗi SQL cụ thể
                if (ex.Number == 547) // Lỗi vi phạm khóa ngoại
                {
                    MessageBox.Show("Không thể xóa dòng này vì dữ liệu bị ràng buộc với các bảng khác.", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ex.Number == 2627) // Lỗi vi phạm khóa chính
                {
                    MessageBox.Show("Không thể xóa dòng này vì dữ liệu trùng lặp trong hệ thống.", "Lỗi khóa chính", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ex.Number == 4060) // Lỗi kết nối cơ sở dữ liệu
                {
                    MessageBox.Show("Lỗi kết nối tới cơ sở dữ liệu. Vui lòng kiểm tra kết nối và thử lại.", "Lỗi kết nối", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show($"Lỗi SQL: {ex.Message}", "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi tổng quát
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            HienThiDanhSach();
        }
    }
}
