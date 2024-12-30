using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;
using System.Data;
using QuanLyBenhVien.Repositories;

namespace QuanLyBenhVien.View
{
    public partial class ThongTinBenh : UserControl
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
        public ThongTinBenh()
        {
            _userRepository = new UserRepository();
            InitializeComponent();
            searchControl.Tmp = "Nhập mã bệnh hoặc tên bệnh";
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
                btnThem.Visibility = Visibility.Hidden;
                btnXoa.Visibility = Visibility.Hidden;
            }
            return;
        }

        private void SearchControl_ClearButtonClicked(object sender, EventArgs e)
        {
            // Logic khi nút X được nhấn
            HienThiDanhSach();
            ClearTextBoxes();
        }
        private void SearchControl_SearchButtonClicked(object sender, string searchText)
        {
            string maBenh = searchText.Trim();

            if (string.IsNullOrEmpty(maBenh))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string query = "SELECT * FROM Benh WHERE MaBenh=@MaBenh OR TenBenh=@MaBenh";

            try
            {
                if (sqlCon == null)
                    sqlCon = _userRepository.GetConnection();

                if (sqlCon.State != ConnectionState.Open)
                    sqlCon.Open();

                adapter = new SqlDataAdapter(query, sqlCon);
                adapter.SelectCommand.Parameters.AddWithValue("@MaBenh", maBenh);

                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                if (ds == null)
                    ds = new DataSet();
                else
                    ds.Clear(); // Xóa dữ liệu cũ

                adapter.Fill(ds, "tblBenh");

                if (ds.Tables["tblBenh"].Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if(ds.Tables["tblBenh"].Rows.Count == 1)
                {
                    dgDanhSachBenh.ItemsSource = ds.Tables["tblBenh"].DefaultView;

                    if (ds.Tables["tblBenh"].Rows.Count == 1)
                    {
                        DataRow row = ds.Tables["tblBenh"].Rows[0];
                        tbMaBenh.Text = row["MaBenh"].ToString();
                        tbBenh.Text = row["TenBenh"].ToString();
                        tbMoTa.Text = row["MoTa"].ToString();
                        tbTrieuChung.Text = row["TrieuChung"].ToString();
                    }
                }    
                else
                {
                    dgDanhSachBenh.ItemsSource = ds.Tables["tblBenh"].DefaultView;

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (sqlCon != null && sqlCon.State == ConnectionState.Open)
                    sqlCon.Close();
            }
        }
        
        SqlConnection sqlCon = null;
        SqlDataAdapter adapter = null;
        DataSet ds = null;

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

        private void HienThiDanhSach()
        {
            if (sqlCon == null)
            {
                sqlCon = _userRepository.GetConnection();
            }
            string query = "select MaBenh, TenBenh, MoTa, TrieuChung from Benh";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            ds = new DataSet();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter.Fill(ds, "tblBenh");
            sqlCon.Close();

            dgDanhSachBenh.ItemsSource = ds.Tables["tblBenh"].DefaultView;
        }
        private int vitri = -1;

        private void dgDanhSachBenh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRow = dgDanhSachBenh.SelectedItem as DataRowView;

            if (selectedRow == null) return;

            // Lấy dữ liệu từ DataRowView
            DataRow dataRow = selectedRow.Row;

            tbMaBenh.Text = dataRow["MaBenh"].ToString();
            tbBenh.Text = dataRow["TenBenh"].ToString();
            tbMoTa.Text = dataRow["MoTa"].ToString();
            tbTrieuChung.Text = dataRow["TrieuChung"].ToString();
            
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Tạo một DataRow mới nhưng không thêm ngay vào DataTable
                DataRow dataRow = ds.Tables["tblBenh"].NewRow();
                dataRow["MaBenh"] = tbMaBenh.Text.Trim();
                dataRow["TenBenh"] = tbBenh.Text.Trim();
                dataRow["MoTa"] = tbMoTa.Text.Trim();
                dataRow["TrieuChung"] = tbTrieuChung.Text.Trim();

                // Tạm thời thêm DataRow (chỉ khi không có lỗi xảy ra)
                ds.Tables["tblBenh"].Rows.Add(dataRow);

                // Cập nhật dữ liệu vào cơ sở dữ liệu
                int kq = adapter.Update(ds.Tables["tblBenh"]);
                if (kq > 0)
                {
                    MessageBox.Show("Thêm dữ liệu thành công!!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Thêm dữ liệu thất bại!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // Dọn sạch các ô nhập liệu sau khi thành công
                ClearTextBoxes();
            }
            catch (SqlException ex)
            {
                // Xóa DataRow vừa thêm nếu gặp lỗi SQL
                if (ds.Tables["tblBenh"].Rows.Count > 0 && ds.Tables["tblBenh"].Rows[ds.Tables["tblBenh"].Rows.Count - 1].RowState == DataRowState.Added)
                {
                    ds.Tables["tblBenh"].Rows[ds.Tables["tblBenh"].Rows.Count - 1].Delete();
                }

                // Kiểm tra lỗi SQL dựa trên mã lỗi
                switch (ex.Number)
                {
                    case 2627: // Lỗi vi phạm PRIMARY KEY
                        MessageBox.Show("Khóa chính đã tồn tại trong cơ sở dữ liệu! Không thể thêm dữ liệu trùng lặp.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;

                    case 547: // Lỗi vi phạm FOREIGN KEY
                        MessageBox.Show("Dữ liệu không hợp lệ! Mã bệnh không tồn tại trong hệ thống.", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;

                    default:
                        MessageBox.Show($"Lỗi SQL: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi không xác định: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Nếu có lỗi và DataRow đã được thêm, xóa DataRow khỏi DataSet
                if (ds.Tables["tblBenh"].Rows.Count > 0 && ds.Tables["tblBenh"].Rows[ds.Tables["tblBenh"].Rows.Count - 1].RowState == DataRowState.Added)
                {
                    ds.Tables["tblBenh"].Rows[ds.Tables["tblBenh"].Rows.Count - 1].Delete();
                }
            }

        }

        private void ClearTextBoxes()
        {
            tbTrieuChung.Clear();
            tbMaBenh.Clear();
            tbMoTa.Clear();
            tbBenh.Clear();
        }

        private void btnCapNhat_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = dgDanhSachBenh.SelectedItem as DataRowView;

            if (selectedRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để cập nhật!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (sqlCon == null)
                    sqlCon = _userRepository.GetConnection();

                if (sqlCon.State != ConnectionState.Open)
                    sqlCon.Open();

                // Cập nhật dữ liệu từ các TextBox vào DataRow
                DataRow dataRow = selectedRow.Row;
                dataRow["MaBenh"] = tbMaBenh.Text.Trim();
                dataRow["TenBenh"] = tbBenh.Text.Trim();
                dataRow["MoTa"] = tbMoTa.Text.Trim();
                dataRow["TrieuChung"] = tbTrieuChung.Text.Trim();

                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                int kq = adapter.Update(ds, "tblBenh");

                if (kq > 0)
                {
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Làm mới DataGrid
                    dgDanhSachBenh.ItemsSource = null;
                    dgDanhSachBenh.ItemsSource = ds.Tables["tblBenh"].DefaultView;
                }
                else
                {
                    MessageBox.Show("Cập nhật dữ liệu thất bại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Lỗi vi phạm PRIMARY KEY
                {
                    MessageBox.Show("Khóa chính đã tồn tại! Không thể cập nhật dữ liệu trùng lặp.", "Lỗi khóa chính", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ex.Number == 547) // Lỗi vi phạm FOREIGN KEY
                {
                    MessageBox.Show("Dữ liệu không hợp lệ! Mã bệnh không tồn tại trong hệ thống.", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show($"Lỗi SQL: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (sqlCon != null && sqlCon.State == ConnectionState.Open)
                    sqlCon.Close();
            }

            ClearTextBoxes(); // Xóa các trường sau khi xử lý

        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dòng được chọn từ DataGrid
            var selectedRow = dgDanhSachBenh.SelectedItem as DataRowView;

            if (selectedRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Hiển thị hộp thoại xác nhận xóa
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa bệnh nhân này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Lấy DataRow từ DataRowView
                    DataRow dataRow = selectedRow.Row;

                    // Xóa dòng khỏi DataTable
                    dataRow.Delete();

                    // Cập nhật thay đổi vào cơ sở dữ liệu
                    int kq = adapter.Update(ds.Tables["tblBenh"]);

                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Cập nhật lại giao diện DataGrid
                        dgDanhSachBenh.ItemsSource = null;
                        dgDanhSachBenh.ItemsSource = ds.Tables["tblBenh"].DefaultView;

                        // Xóa dữ liệu trong các TextBox
                        ClearTextBoxes();
                    }
                    else
                    {
                        MessageBox.Show("Xóa dữ liệu thất bại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (SqlException ex)
            {
                // Kiểm tra lỗi SQL, ví dụ vi phạm khóa ngoại hoặc khóa chính
                if (ex.Number == 547) // Lỗi vi phạm khóa ngoại (foreign key)
                {
                    MessageBox.Show("Không thể xóa bệnh nhân này vì dữ liệu bị ràng buộc với các bảng khác.", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ex.Number == 2627) // Lỗi vi phạm khóa chính (primary key)
                {
                    MessageBox.Show("Không thể xóa bệnh nhân này vì có dữ liệu trùng lặp trong hệ thống.", "Lỗi khóa chính", MessageBoxButton.OK, MessageBoxImage.Error);
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

        }
    }
}
