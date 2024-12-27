using System.Data;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using QuanLyBenhVien.Repositories;

namespace QuanLyBenhVien.View
{
    public partial class HoaDon : UserControl
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
        
        public HoaDon()
        {
            _userRepository = new UserRepository();
            InitializeComponent();
            searchControl.Tmp = "Nhập số hóa đơn hoặc tên hóa đơn";
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
                
                btn_Them.Visibility = Visibility.Hidden;
                btn_Xoa.Visibility = Visibility.Hidden;
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
            string maHoaDon = searchText.Trim();
            if (string.IsNullOrEmpty(maHoaDon))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                dgvHoaDon.ItemsSource = null;
                return;
            }

            

            // Câu lệnh SQL để tìm kiếm thông tin đơn thuốc và chi tiết đơn thuốc
            string queryHoaDon = "SELECT * FROM HoaDon WHERE MaHoaDon = @MaHoaDon OR TenHoaDon = @MaHoaDon";


            try
            {
                // Mở kết nối SQL
                sqlCon = _userRepository.GetConnection();
                sqlCon.Open();

                // Sử dụng SqlDataAdapter để truy xuất dữ liệu
                adapter = new SqlDataAdapter(queryHoaDon, sqlCon);
                adapter.SelectCommand.Parameters.AddWithValue("@MaHoaDon", maHoaDon);

                // Đổ dữ liệu vào DataSet
                ds = new DataSet();
                adapter.Fill(ds, "HoaDon");
                DataTable dataTableHoaDon = ds.Tables["HoaDon"];

                // Gắn dữ liệu vào dgvHoaDon
                dgvHoaDon.ItemsSource = dataTableHoaDon.DefaultView;

                // Kiểm tra nếu không tìm thấy dữ liệu phù hợp
                if (dataTableHoaDon.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    dgvHoaDon.ItemsSource = null;
                }
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
            CTHD cTHD = new CTHD();
            cTHD.OnDataAdded = HienThiDanhSach;
            cTHD.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Lấy hàng được chọn từ DataGrid
            var selectedRow = dgvHoaDon.SelectedItem as DataRowView;

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

                    // Cập nhật thay đổi vào cơ sở dữ liệu
                    int kq = adapter.Update(ds.Tables["tblHoaDon"]);

                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Cập nhật giao diện DataGrid
                        dgvHoaDon.ItemsSource = null;
                        dgvHoaDon.ItemsSource = ds.Tables["tblHoaDon"].DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("Xóa dữ liệu thất bại! Không có thay đổi nào được thực hiện.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (SqlException ex)
            {
                // Kiểm tra lỗi SQL (vi phạm khóa ngoại, khóa chính...)
                if (ex.Number == 547) // Lỗi vi phạm khóa ngoại (foreign key)
                {
                    MessageBox.Show("Không thể xóa dòng này vì dữ liệu bị ràng buộc với các bảng khác.", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ex.Number == 2627) // Lỗi vi phạm khóa chính (primary key)
                {
                    MessageBox.Show("Không thể xóa dòng này vì có dữ liệu trùng lặp trong hệ thống.", "Lỗi khóa chính", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ex.Number == 4060) // Lỗi kết nối tới cơ sở dữ liệu
                {
                    MessageBox.Show("Lỗi kết nối tới cơ sở dữ liệu. Vui lòng kiểm tra kết nối và thử lại.", "Lỗi kết nối", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    // Lỗi SQL chung
                    MessageBox.Show($"Lỗi SQL: {ex.Message}", "Lỗi cơ sở dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                // Lỗi tổng quát (ví dụ: lỗi bất ngờ)
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            HienThiDanhSach();

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
        private void HienThiDanhSach()
        {
            if (sqlCon == null)
            {
                sqlCon = _userRepository.GetConnection();
            }
            string query = " select MaHoaDon, TenHoaDon, MaBenhNhan, MaNhanVien, NgayLapHoaDon, GiaTien, TrangThai from HoaDon";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            ds = new DataSet();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter.Fill(ds, "tblHoaDon");
            sqlCon.Close();

            dgvHoaDon.ItemsSource = ds.Tables["tblHoaDon"].DefaultView;
        }
     
        private void dgvHoaDon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRow = dgvHoaDon.SelectedItem as DataRowView;

            if (selectedRow == null) return;

            // Lấy dữ liệu từ DataRowView
            DataRow dataRow = selectedRow.Row;
        }

        
    }
}
