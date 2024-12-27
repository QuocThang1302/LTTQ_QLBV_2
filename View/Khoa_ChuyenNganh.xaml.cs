using QuanLyBenhVien.Repositories;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace QuanLyBenhVien.View
{
    public partial class Khoa_ChuyenNganh : UserControl
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

        public Khoa_ChuyenNganh()
        {
            _userRepository = new UserRepository();
            InitializeComponent();
            searchControl.Tmp = "Nhập mã khoa hoặc tên khoa";
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
                //btnCapNhat1.Visibility = Visibility.Hidden;
                btnThem1.Visibility = Visibility.Hidden;
                btnXoa1.Visibility = Visibility.Hidden;
                //    1btnCapNhat2.Visibility = Visibility.Hidden;
                btnThem2.Visibility = Visibility.Hidden;
                btnXoa2.Visibility = Visibility.Hidden;
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
            string maKhoa = searchText.Trim();
            if (string.IsNullOrEmpty(maKhoa))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Câu lệnh SQL để tìm kiếm thông tin đơn thuốc và chi tiết đơn thuốc
            string queryKhoa = "SELECT * FROM Khoa WHERE MaKhoa = @MaKhoa OR TenKhoa = @MaKhoa";
            string queryChuyenNganh = "SELECT * FROM ChuyenNganh WHERE Khoa IN (SELECT MaKhoa From Khoa WHERE MaKhoa = @MaKhoa OR TenKhoa = @MaKhoa)";

            using (SqlConnection connection = _userRepository.GetConnection())
            {
                try
                {
                    connection.Open();

                    // Hiển thị thông tin của Khoa
                    SqlDataAdapter adapterKhoa = new SqlDataAdapter(queryKhoa, connection);
                    adapterKhoa.SelectCommand.Parameters.AddWithValue("@MaKhoa", maKhoa);
                    DataTable dataTableKhoa = new DataTable();
                    adapterKhoa.Fill(dataTableKhoa);

                    // Gắn dữ liệu vào dgvKhoa
                    dgvKhoa.ItemsSource = dataTableKhoa.DefaultView;

                    // Kiểm tra nếu không tìm thấy đơn thuốc
                    if (dataTableKhoa.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Xóa dữ liệu của DataGridView nếu không có kết quả
                        dgvChuyenNganh.ItemsSource = null;
                        dgvKhoa.ItemsSource = null;
                        return;
                    }

                    // Hiển thị thông tin của ChuyenNganh
                    SqlDataAdapter adapterChuyenNganh = new SqlDataAdapter(queryChuyenNganh, connection);
                    adapterChuyenNganh.SelectCommand.Parameters.AddWithValue("@MaKhoa", maKhoa);
                    DataTable dataTableChuyenNganh = new DataTable();
                    adapterChuyenNganh.Fill(dataTableChuyenNganh);

                    // Gắn dữ liệu vào dgvChuyenNganh
                    dgvChuyenNganh.ItemsSource = dataTableChuyenNganh.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChuyenNganh chuyenNganh = new ChuyenNganh();
            chuyenNganh.OnDataAdded = HienThiDanhSach;
            chuyenNganh.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Khoa khoa = new Khoa();
            khoa.OnDataAdded = HienThiDanhSach;
            khoa.Show();
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
            string query = "select MaKhoa, TenKhoa, TruongKhoa  From Khoa";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            ds = new DataSet();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter.Fill(ds, "tblKhoa");
            sqlCon.Close();

            dgvKhoa.ItemsSource = ds.Tables["tblKhoa"].DefaultView;

            string query1 = "select MaChuyenNganh, TenChuyenNganh, Khoa from ChuyenNganh ";
            adapter1 = new SqlDataAdapter(query1, sqlCon);
            SqlCommandBuilder builder1 = new SqlCommandBuilder(adapter1);

            ds1 = new DataSet();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter1.Fill(ds1, "tblChuyenNganh");
            sqlCon.Close();

            dgvChuyenNganh.ItemsSource = ds1.Tables["tblChuyenNganh"].DefaultView;
        }
        
        private void btnXoa1_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow2 = dgvKhoa.SelectedItem as DataRowView;
            // Kiểm tra xem người dùng đã chọn dòng nào chưa
            if (selectedRow2 == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Xác nhận từ người dùng trước khi xóa
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa khoa này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Lấy DataRow từ DataRowView
                    DataRow dataRow = selectedRow2.Row;

                    // Đánh dấu hàng là Deleted
                    dataRow.Delete();

                    // Cập nhật thay đổi vào cơ sở dữ liệu
                    int kq = adapter.Update(ds.Tables["tblKhoa"]);

                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Cập nhật giao diện DataGrid
                        dgvKhoa.ItemsSource = null;
                        dgvKhoa.ItemsSource = ds.Tables["tblKhoa"].DefaultView;
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

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void dgvKhoa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRow1 = dgvKhoa.SelectedItem as DataRowView;

            if (selectedRow1 == null) return;

            // Lấy dữ liệu từ DataRowView
            DataRow dataRow1 = selectedRow1.Row;
        }

        private void dgvChuyenNganh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRow2 = dgvChuyenNganh.SelectedItem as DataRowView;

            if (selectedRow2 == null) return;

            // Lấy dữ liệu từ DataRowView
            DataRow dataRow2 = selectedRow2.Row;
        }

        private void btnXoa2_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow2 = dgvChuyenNganh.SelectedItem as DataRowView;
            // Kiểm tra xem người dùng đã chọn dòng nào chưa
            if (selectedRow2 == null)
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
                    // Lấy DataRow từ DataRowView
                    DataRow dataRow = selectedRow2.Row;

                    // Đánh dấu hàng là Deleted
                    dataRow.Delete();

                    // Cập nhật thay đổi vào cơ sở dữ liệu
                    int kq = adapter.Update(ds1.Tables["tblChuyenNganh"]);

                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Cập nhật giao diện DataGrid
                        dgvKhoa.ItemsSource = null;
                        dgvKhoa.ItemsSource = ds1.Tables["tblChuyenNganh"].DefaultView;
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

        
    }
}