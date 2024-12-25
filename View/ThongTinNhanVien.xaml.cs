using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Data.SqlClient;
using QuanLyBenhVien.Repositories;

namespace QuanLyBenhVien.View
{
    public partial class ThongTinNhanVien : UserControl
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
        private void txtNgaySinh_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            popupCalendarNgaySinh.IsOpen = true; // Mở popup khi nhấn vào TextBox
            e.Handled = true; // Ngăn sự kiện lan sang Window_PreviewMouseDown
        }

        private void calendarNgaySinh_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (calendarNgaySinh.SelectedDate.HasValue)
            {
                txtNgaySinh.Text = calendarNgaySinh.SelectedDate.Value.ToString("yyyy-MM-dd"); // Gán ngày với định dạng yyyy-MM-dd
                popupCalendarNgaySinh.IsOpen = false; // Ẩn popup sau khi chọn ngày
            }
        }

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (popupCalendarNgaySinh.IsOpen && !popupCalendarNgaySinh.IsMouseOver)
            {
                popupCalendarNgaySinh.IsOpen = false; // Ẩn popup nếu nhấn ra ngoài
            }
        }


        public ThongTinNhanVien()
        {
            _userRepository = new UserRepository();
            InitializeComponent();
            searchControl.Tmp = "Nhập mã nhân viên hoặc tên nhân viên";
            // Đăng ký sự kiện SearchButtonClicked
            searchControl.SearchButtonClicked += SearchControl_SearchButtonClicked;
            // Đăng ký sự kiện ClearButtonClicked cho nút X
            searchControl.ClearButtonClicked += SearchControl_ClearButtonClicked;
            Role();

        }

        private void Role()
        {
            string roleID = GetRoleIDByUserID();
            if (roleID == "R02")
            {
                txtDiaChi.Width = 712;
                btnCapNhat.Visibility = Visibility.Collapsed;
                btnThem.Visibility = Visibility.Collapsed;
                btnXoa.Visibility = Visibility.Collapsed;
                btnMatKhau.Margin = new Thickness(52, 0, 0, 0);
            }
            if (roleID == "R01")
            {
                txtDiaChi.Width = 250;
                tbMatKhau.Visibility = Visibility.Visible;
                txtMatKhau.Visibility = Visibility.Visible;
            }
            return;
        }

        private void SearchControl_ClearButtonClicked(object sender, EventArgs e)
        {
            // Logic khi nút X được nhấn
            HienThiDanhSach();
            ClearFields();
            Role();
            btnMatKhau.Visibility = Visibility.Hidden;
        }

        SqlConnection sqlCon = null;
        SqlDataAdapter adapter = null;
        DataSet ds = null;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Tạo một DataRow mới nhưng không thêm ngay vào DataTable
                DataRow dataRow = ds.Tables["tblNhanVien"].NewRow();
                dataRow["MaNhanVien"] = txtMaNhanVien.Text.Trim();
                dataRow["Ho"] = txtHo.Text.Trim();
                dataRow["Ten"] = txtTen.Text.Trim();
                dataRow["LoaiNhanVien"] = txtChucVu.Text.Trim();
                dataRow["MaChuyenNganh"] = txtChuyenNganh.Text.Trim();
                dataRow["GioiTinh"] = txtGioiTinh.Text.Trim();
                dataRow["CCCD"] = txtCCCD.Text.Trim();
                dataRow["SDT"] = txtSDT.Text.Trim();

                // Xử lý định dạng ngày
                dataRow["NgaySinh"] = DateTime.TryParse(txtNgaySinh.Text.Trim(), out DateTime ngaySinh)
                    ? ngaySinh.ToString("yyyy-MM-dd")
                    : throw new FormatException("Invalid date format");

                dataRow["Email"] = txtEmail.Text.Trim();
                dataRow["DiaChi"] = txtDiaChi.Text.Trim();
                dataRow["MatKhau"] = txtMatKhau.Text.Trim();

                // Gán RoleID dựa trên LoaiNhanVien
                string loaiNhanVien = txtChucVu.Text.Trim().ToLower();
                // loaiNhanVien = loaiNhanVien.Replace(" ", ""); // Loại bỏ tất cả khoảng trắng
                //Debug.WriteLine("Chức vụ nhập vào: " + loaiNhanVien);

                if (loaiNhanVien.Contains("quản lý"))
                {
                    dataRow["RoleID"] = "R01"; // RoleID cho Nhân Viên
                }
                else if (loaiNhanVien.Contains("bác sĩ"))
                {
                    dataRow["RoleID"] = "R02"; // RoleID cho Bác Sĩ
                }
                else
                {
                    throw new Exception("Chức vụ không hợp lệ! Vui lòng nhập 'Quản lý' hoặc 'Bác sĩ'.");
                }

                // Tạm thời thêm DataRow (chỉ khi không có lỗi xảy ra)
                ds.Tables["tblNhanVien"].Rows.Add(dataRow);

                // Cập nhật dữ liệu vào cơ sở dữ liệu
                int kq = adapter.Update(ds.Tables["tblNhanVien"]);
                if (kq > 0)
                {
                    MessageBox.Show("Thêm dữ liệu thành công!!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearFields();
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                    adapter.UpdateCommand = builder.GetUpdateCommand();
                    adapter.InsertCommand = builder.GetInsertCommand();
                    adapter.DeleteCommand = builder.GetDeleteCommand();
                }
                else
                {
                    // Nếu không cập nhật được, xóa DataRow vừa thêm
                    ds.Tables["tblNhanVien"].Rows.Remove(dataRow);
                    MessageBox.Show("Thêm dữ liệu thất bại!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (SqlException ex)
            {
                // Xóa DataRow vừa thêm nếu gặp lỗi SQL
                if (ds.Tables["tblNhanVien"].Rows.Count > 0 && ds.Tables["tblNhanVien"].Rows[ds.Tables["tblNhanVien"].Rows.Count - 1].RowState == DataRowState.Added)
                {
                    ds.Tables["tblNhanVien"].Rows[ds.Tables["tblNhanVien"].Rows.Count - 1].Delete();
                }

                // Kiểm tra lỗi SQL dựa trên mã lỗi
                switch (ex.Number)
                {
                    case 2627: // Lỗi vi phạm PRIMARY KEY
                        MessageBox.Show("Khóa chính đã tồn tại trong cơ sở dữ liệu! Không thể thêm dữ liệu trùng lặp.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;

                    case 547: // Lỗi vi phạm FOREIGN KEY
                        MessageBox.Show("Dữ liệu không hợp lệ! Mã chuyên ngành không tồn tại trong hệ thống.", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;

                    default:
                        MessageBox.Show($"Lỗi SQL: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Định dạng ngày không hợp lệ: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi không xác định: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Nếu có lỗi và DataRow đã được thêm, xóa DataRow khỏi DataSet
                if (ds.Tables["tblNhanVien"].Rows.Count > 0 && ds.Tables["tblNhanVien"].Rows[ds.Tables["tblNhanVien"].Rows.Count - 1].RowState == DataRowState.Added)
                {
                    ds.Tables["tblNhanVien"].Rows[ds.Tables["tblNhanVien"].Rows.Count - 1].Delete();
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

        private void HienThiDanhSach()
        {
            if (sqlCon == null)
            {
                sqlCon = _userRepository.GetConnection();
            }
            string query = " select MaNhanVien, Ho, Ten, LoaiNhanVien, MaChuyenNganh, GioiTinh, CCCD, SDT, NgaySinh, Email, DiaChi, MatKhau, RoleID  from NhanVien";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            ds = new DataSet();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter.Fill(ds, "tblNhanVien");
            sqlCon.Close();

            dgDanhSachNhanVien.ItemsSource = ds.Tables["tblNhanVien"].DefaultView;
        }

        private int vitri = -1;
        private void dgDanhSachNhanVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Lấy hàng được chọn từ DataGrid
            var selectedRow = dgDanhSachNhanVien.SelectedItem as DataRowView;

            if (selectedRow == null) return;

            // Lấy dữ liệu từ DataRowView
            DataRow dataRow = selectedRow.Row;

            // Gán giá trị vào các TextBox, sử dụng toán tử null-coalescing (??) để tránh giá trị null
            txtMaNhanVien.Text = dataRow["MaNhanVien"]?.ToString() ?? string.Empty;
            txtHo.Text = dataRow["Ho"]?.ToString() ?? string.Empty;
            txtTen.Text = dataRow["Ten"]?.ToString() ?? string.Empty;
            txtChucVu.Text = dataRow["LoaiNhanVien"]?.ToString() ?? string.Empty;
            txtChuyenNganh.Text = dataRow["MaChuyenNganh"]?.ToString() ?? string.Empty;
            txtGioiTinh.Text = dataRow["GioiTinh"]?.ToString() ?? string.Empty;
            txtCCCD.Text = dataRow["CCCD"]?.ToString() ?? string.Empty;
            txtSDT.Text = dataRow["SDT"]?.ToString() ?? string.Empty;
            txtNgaySinh.Text = Convert.ToDateTime(dataRow["NgaySinh"]).ToString("yyyy-MM-dd")?.ToString() ?? string.Empty;
            txtEmail.Text = dataRow["Email"]?.ToString() ?? string.Empty;
            txtDiaChi.Text = dataRow["DiaChi"]?.ToString() ?? string.Empty;
            if (ID == txtMaNhanVien.Text)
            {
                txtDiaChi.Width = 250;
                tbMatKhau.Visibility = Visibility.Visible;
                txtMatKhau.Visibility = Visibility.Visible;
                btnMatKhau.Visibility = Visibility.Visible;
            }
            else
            {
                btnMatKhau.Visibility= Visibility.Hidden;
                tbMatKhau.Visibility = Visibility.Hidden;
                txtMatKhau.Visibility = Visibility.Hidden;
                Role();
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = dgDanhSachNhanVien.SelectedItem as DataRowView;
            if (selectedRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Lấy DataRow từ DataRowView
                    DataRow dataRow = selectedRow.Row;

                    // Xóa hàng trong DataTable
                    dataRow.Delete();

                    // Cập nhật thay đổi vào cơ sở dữ liệu
                    int kq = adapter.Update(ds.Tables["tblNhanVien"]);

                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Cập nhật giao diện DataGrid
                        dgDanhSachNhanVien.ItemsSource = null;
                        dgDanhSachNhanVien.ItemsSource = ds.Tables["tblNhanVien"].DefaultView;

                        // Xóa dữ liệu trong TextBox
                        ClearFields();
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
                    MessageBox.Show("Không thể xóa nhân viên này vì dữ liệu bị ràng buộc với các bảng khác.", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ex.Number == 2627) // Lỗi vi phạm khóa chính (primary key)
                {
                    MessageBox.Show("Không thể xóa nhân viên này vì có dữ liệu trùng lặp trong hệ thống.", "Lỗi khóa chính", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ex.Number == 4060) // Lỗi khi kết nối tới cơ sở dữ liệu
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

        }

        private void btnCapNhat_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn dòng nào trong DataGrid chưa
            var selectedRow = dgDanhSachNhanVien.SelectedItem as DataRowView;

            if (selectedRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để cập nhật!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Kiểm tra dữ liệu đầu vào trước
                if (!DateTime.TryParse(txtNgaySinh.Text.Trim(), out DateTime ngaySinh))
                    throw new FormatException("Định dạng ngày không hợp lệ.");

                // Lấy DataRow từ DataRowView đã chọn
                DataRow dataRow = selectedRow.Row;

                dataRow.BeginEdit(); // Bắt đầu chỉnh sửa dữ liệu

                // Cập nhật dữ liệu từ các TextBox vào DataRow
                dataRow["MaNhanVien"] = txtMaNhanVien.Text.Trim();
                dataRow["Ho"] = txtHo.Text.Trim();
                dataRow["Ten"] = txtTen.Text.Trim();
                dataRow["LoaiNhanVien"] = txtChucVu.Text.Trim();
                dataRow["MaChuyenNganh"] = txtChuyenNganh.Text.Trim();
                dataRow["GioiTinh"] = txtGioiTinh.Text.Trim();
                dataRow["CCCD"] = txtCCCD.Text.Trim();
                dataRow["SDT"] = txtSDT.Text.Trim();
                dataRow["NgaySinh"] = ngaySinh.ToString("yyyy-MM-dd");
                dataRow["Email"] = txtEmail.Text.Trim();
                dataRow["DiaChi"] = txtDiaChi.Text.Trim();

                dataRow.EndEdit(); // Kết thúc chỉnh sửa

                // Cập nhật thay đổi vào cơ sở dữ liệu
                int kq = adapter.Update(ds.Tables["tblNhanVien"]);

                if (kq > 0)
                {
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Cập nhật giao diện DataGrid
                    dgDanhSachNhanVien.ItemsSource = null;
                    dgDanhSachNhanVien.ItemsSource = ds.Tables["tblNhanVien"].DefaultView;

                    // Đặt lại vị trí dòng đã chọn
                    dgDanhSachNhanVien.SelectedItem = selectedRow;  // Giữ lại dòng đã chọn
                    ClearFields(); // Xóa các trường nhập liệu
                }
                else
                {
                    MessageBox.Show("Cập nhật dữ liệu thất bại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Lỗi định dạng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                ds.Tables["tblNhanVien"].RejectChanges(); // Hủy thay đổi
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Lỗi vi phạm PRIMARY KEY
                {
                    MessageBox.Show("Khóa chính đã tồn tại! Không thể cập nhật dữ liệu trùng lặp.", "Lỗi khóa chính", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ex.Number == 547) // Lỗi vi phạm FOREIGN KEY
                {
                    MessageBox.Show("Dữ liệu không hợp lệ! Mã chuyên ngành hoặc mã nhân viên không tồn tại trong hệ thống.", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show($"Lỗi SQL: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                ds.Tables["tblNhanVien"].RejectChanges(); // Hủy thay đổi
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                ds.Tables["tblNhanVien"].RejectChanges(); // Hủy thay đổi
            }
            finally
            {
                // Đảm bảo giao diện không bị thay đổi nếu có lỗi
                dgDanhSachNhanVien.ItemsSource = ds.Tables["tblNhanVien"].DefaultView;
            }



        }
        private void SearchControl_SearchButtonClicked(object sender, string searchText)
        {


            string maNhanVien = searchText.Trim();

            if (string.IsNullOrEmpty(maNhanVien))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Câu lệnh SQL để tìm kiếm thông tin nhân viên
            string query = "SELECT * FROM NhanVien WHERE MaNhanVien = @MaNhanVien OR Ten = @MaNhanVien";

            try
            {
                using (SqlConnection connection = _userRepository.GetConnection())
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.Add("@MaNhanVien", SqlDbType.VarChar).Value = maNhanVien;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                       // Xóa dữ liệu trong DataGrid
                    }
                    else if (dataTable.Rows.Count == 1)
                    {
                        // Hiển thị thông tin lên các TextBox
                        DataRow row = dataTable.Rows[0];
                        txtMaNhanVien.Text = row["MaNhanVien"].ToString();
                        string ho = row["Ho"].ToString();
                        string ten = row["Ten"].ToString();
                        txtHo.Text = ho + " " + ten; // Hiển thị họ và tên đầy đủ
                        txtTen.Text = ten;
                        txtChuyenNganh.Text = row["MaChuyenNganh"].ToString();
                        txtChucVu.Text = row["LoaiNhanVien"].ToString();
                        txtNgaySinh.Text = Convert.ToDateTime(row["NgaySinh"]).ToString("yyyy-MM-dd");
                        txtGioiTinh.Text = row["GioiTinh"].ToString();
                        txtCCCD.Text = row["CCCD"].ToString();
                        txtDiaChi.Text = row["DiaChi"].ToString();
                        txtSDT.Text = row["SDT"].ToString();
                        txtEmail.Text = row["Email"].ToString();

                        // Xóa dữ liệu trong DataGrid nếu chỉ có một kết quả
                        dgDanhSachNhanVien.ItemsSource = dataTable.DefaultView;
                    }
                    else
                    {
                        // Nếu có nhiều kết quả, chỉ hiển thị vào DataGrid
                        ClearFields(); // Xóa các TextBox
                        dgDanhSachNhanVien.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (ID == txtMaNhanVien.Text)
            {
                txtDiaChi.Width = 250;
                tbMatKhau.Visibility = Visibility.Visible;
                txtMatKhau.Visibility = Visibility.Visible;
                btnMatKhau.Visibility = Visibility.Visible;
            }
            else
            {
                btnMatKhau.Visibility = Visibility.Hidden;
                tbMatKhau.Visibility = Visibility.Hidden;
                txtMatKhau.Visibility = Visibility.Hidden;
                Role();
            }
        }

        private void ClearFields()
        {
            txtMaNhanVien.Text = "";
            txtHo.Text = "";
            txtTen.Text = "";
            txtChuyenNganh.Text = "";
            txtChucVu.Text = "";
            txtNgaySinh.Text = "";
            txtGioiTinh.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            txtCCCD.Text = "";
            txtMatKhau.Text = "";
        }

        private void btnMatKhau_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = _userRepository.GetConnection())
            {
                string query = "UPDATE NhanVien SET MatKhau = @MatKhau WHERE MaNhanVien = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@MatKhau", txtMatKhau.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật mật khẩu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}



