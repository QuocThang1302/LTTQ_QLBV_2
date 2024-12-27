using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using QuanLyBenhVien.Repositories;

namespace QuanLyBenhVien.View
{
    public partial class LichTruc : UserControl
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
        public LichTruc()
        {
            _userRepository = new UserRepository();
            InitializeComponent();
            searchControl.Tmp = "Nhập mã công việc hoặc tên công việc";
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
                btnCapNhat.Visibility = Visibility.Hidden;
                btnXoa.Visibility = Visibility.Hidden;
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
            string maCongViec = searchText.Trim();
            if (string.IsNullOrEmpty(maCongViec))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Câu lệnh SQL để tìm kiếm thông tin đơn thuốc và chi tiết đơn thuốc
            string queryCongViec = "SELECT * FROM CongViec WHERE MaCongViec = @MaCongViec OR TenCongViec = @MaCongViec";
            string queryPhanCong = "SELECT * FROM LichTruc WHERE PhanCong IN (SELECT MaCongViec FROM CongViec WHERE MaCongViec = @MaCongViec OR TenCongViec = @MaCongViec)";

            try
            {
                // Mở kết nối SQL
                sqlCon = _userRepository.GetConnection();
                sqlCon.Open();

                // Hiển thị thông tin của Công Việc
                adapter = new SqlDataAdapter(queryCongViec, sqlCon);
                adapter.SelectCommand.Parameters.AddWithValue("@MaCongViec", maCongViec);
                ds = new DataSet();
                adapter.Fill(ds, "CongViec");
                DataTable dataTableCongViec = ds.Tables["CongViec"];

                // Gắn dữ liệu vào dgvCongViec
                dgvCongViec.ItemsSource = dataTableCongViec.DefaultView;

                // Kiểm tra nếu không tìm thấy dữ liệu
                if (dataTableCongViec.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    dgvCongViec.ItemsSource = null;
                    dgvPhanCong.ItemsSource = null;
                    return;
                }

                // Hiển thị thông tin của Phân Công
                adapter = new SqlDataAdapter(queryPhanCong, sqlCon);
                adapter.SelectCommand.Parameters.AddWithValue("@MaCongViec", maCongViec);
                adapter.Fill(ds, "PhanCong");
                DataTable dataTablePhanCong = ds.Tables["PhanCong"];

                // Gắn dữ liệu vào dgvPhanCong
                dgvPhanCong.ItemsSource = dataTablePhanCong.DefaultView;
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
            string query = "  select MaCongViec, TenCongViec, MoTaCongViec, GhiChu from CongViec";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            ds = new DataSet();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter.Fill(ds, "tblCongViec");
            sqlCon.Close();

            dgvCongViec.ItemsSource = ds.Tables["tblCongViec"].DefaultView;

            // Kiểm tra nếu có giá trị MaDonThuoc từ dòng được chọn
            string maCongViec = null;
            if (dgvCongViec.SelectedItem is DataRowView selectedRow)
            {
                maCongViec = selectedRow.Row["MaCongViec"].ToString();
            }
            string query1 = "select MaLichTruc, MaBacSi, NgayTruc, PhanCong, TrangThai from LichTruc";
            if (!string.IsNullOrEmpty(maCongViec))
            {
                query1 += " WHERE LichTruc.PhanCong = @MaCongViec";
            }
            adapter1 = new SqlDataAdapter(query1, sqlCon);
            SqlCommandBuilder builder1 = new SqlCommandBuilder(adapter1);

            ds1 = new DataSet();
            if (!string.IsNullOrEmpty(maCongViec))
            {
                adapter1.SelectCommand.Parameters.AddWithValue("@PhanCong", maCongViec);
            }
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter1.Fill(ds1, "tblLichTruc");
            sqlCon.Close();

            dgvPhanCong.ItemsSource = ds1.Tables["tblLichTruc"].DefaultView;
        }
        private void dgvCongViec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRow = dgvCongViec.SelectedItem as DataRowView;

            if (selectedRow == null) return;

            // Lấy giá trị của MaDonThuoc
            string maCongViec = selectedRow.Row["MaCongViec"].ToString();

            // Cập nhật chỉ DataGrid CTDonThuoc
            HienThiPhanCong(maCongViec);
        }

        private void HienThiPhanCong(string maCongViec)
        {
            if (sqlCon == null)
            {
                sqlCon = _userRepository.GetConnection();
            }

            string query1 = "select MaLichTruc, MaBacSi, NgayTruc, PhanCong, TrangThai from LichTruc WHERE LichTruc.PhanCong = @MaCongViec";


            adapter1 = new SqlDataAdapter(query1, sqlCon);
            adapter1.SelectCommand.Parameters.AddWithValue("@MaCongViec", maCongViec);

            ds1 = new DataSet();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter1.Fill(ds1, "tblLichTruc");
            sqlCon.Close();

            dgvPhanCong.ItemsSource = ds1.Tables["tblLichTruc"].DefaultView;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Lấy hàng được chọn từ DataGrid
            var selectedRow = dgvPhanCong.SelectedItem as DataRowView;
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

                    // Đánh dấu hàng là Deleted
                    dataRow.Delete();

                    // Cập nhật thay đổi vào cơ sở dữ liệu
                    int kq = adapter.Update(ds1.Tables["tblLichTruc"]);

                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Cập nhật giao diện DataGrid
                        dgvPhanCong.ItemsSource = null;
                        dgvPhanCong.ItemsSource = ds1.Tables["tblLichTruc"].DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("Xóa dữ liệu thất bại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (SqlException ex)
            {
                // Xử lý lỗi liên quan đến khóa chính hoặc khóa ngoại
                if (ex.Number == 547) // Lỗi ràng buộc khóa ngoại
                {
                    MessageBox.Show("Không thể xóa dữ liệu vì đang được tham chiếu bởi bảng khác!", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ex.Number == 2627 || ex.Number == 2601) // Lỗi trùng lặp khóa chính hoặc khóa duy nhất
                {
                    MessageBox.Show("Dữ liệu đã tồn tại và vi phạm ràng buộc khóa chính hoặc khóa duy nhất!", "Lỗi khóa chính", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show($"Lỗi cơ sở dữ liệu: {ex.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi chung
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            HienThiDanhSach();
        }
      
        private void dgvPhanCong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRow = dgvPhanCong.SelectedItem as DataRowView;

            if (selectedRow == null) return;

            // Lấy dữ liệu từ DataRowView
            DataRow dataRow = selectedRow.Row;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PhanCong phanCong = new PhanCong();
            phanCong.Show();
        }

        
    }
}
