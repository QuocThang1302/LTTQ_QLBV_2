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

            using (SqlConnection connection = _userRepository.GetConnection())
            {
                try
                {
                    connection.Open();

                    // Hiển thị thông tin của
                    SqlDataAdapter adapterCongViec = new SqlDataAdapter(queryCongViec, connection);
                    adapterCongViec.SelectCommand.Parameters.AddWithValue("@MaCongViec", maCongViec);
                    DataTable dataTableCongViec = new DataTable();
                    adapterCongViec.Fill(dataTableCongViec);

                    // Gắn dữ liệu vào 
                    dgvCongViec.ItemsSource = dataTableCongViec.DefaultView;

                    // Kiểm tra nếu không tìm thấy 
                    if (dataTableCongViec.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Xóa dữ liệu của DataGridView nếu không có kết quả
                        
                        return;
                    }

                    // Hiển thị thông tin của PhanCong
                    SqlDataAdapter adapterPhanCong = new SqlDataAdapter(queryPhanCong, connection);
                    adapterPhanCong.SelectCommand.Parameters.AddWithValue("@MaCongViec", maCongViec);
                    DataTable dataTablePhanCong = new DataTable();
                    adapterPhanCong.Fill(dataTablePhanCong);

                    // Gắn dữ liệu vào dgvPhanCong
                    dgvPhanCong.ItemsSource = dataTablePhanCong.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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

            string query1 = "select MaLichTruc, MaBacSi, NgayTruc, PhanCong, TrangThai from LichTruc join NhanVien on LichTruc.MaBacSi = NhanVien.MaNhanVien";
            adapter1 = new SqlDataAdapter(query1, sqlCon);
            SqlCommandBuilder builder1 = new SqlCommandBuilder(adapter1);

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
            if (vitri == -1)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Xác nhận từ người dùng trước khi xóa
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Xóa dòng được chọn trong DataTable
                    DataRow dataRow = ds1.Tables["tblLichTruc"].Rows[vitri];
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
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private int vitri = -1;
        private void dgvPhanCong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vitri = dgvPhanCong.SelectedIndex;
            if (vitri == -1) return;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PhanCong phanCong = new PhanCong();
            phanCong.Show();
        }
    }
}
