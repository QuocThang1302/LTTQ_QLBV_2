using System.Data;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using QuanLyBenhVien.Repositories;
using System.Windows.Input;

namespace QuanLyBenhVien.View
{
    public partial class ThongTinBenhNhan : UserControl
    {
        private readonly RepositoryBase _userRepository;
        public ThongTinBenhNhan()
        {
            _userRepository = new UserRepository();
            InitializeComponent();
            searchControl.Tmp = "Nhập mã bệnh nhân hoặc tên bệnh nhân";
            // Đăng ký sự kiện SearchButtonClicked
            searchControl.SearchButtonClicked += SearchControl_SearchButtonClicked;
            // Đăng ký sự kiện ClearButtonClicked cho nút X
            searchControl.ClearButtonClicked += SearchControl_ClearButtonClicked;

        }
        private void txtNgaySinh_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            popupCalendarNgaySinh.IsOpen = true; // Mở popup khi nhấn vào TextBox
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
                popupCalendarNgaySinh.IsOpen = false; // Ẩn popup khi nhấn bên ngoài
            }
        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            popupCalendarNgaySinh.IsOpen = false;
        }
        private void SearchControl_ClearButtonClicked(object sender, EventArgs e)
        {
            // Logic khi nút X được nhấn
            HienThiDanhSach();
            txtCCCD.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtGioiTinh.Clear();
            txtHo.Clear();
            txtKhoa.Clear();
            txtMaBenhNhan.Clear();
            txtNgheNghiep.Clear();
            txtSDT.Clear();
            txtTen.Clear();
            txtNgaySinh.Clear();
        }
        private void SearchControl_SearchButtonClicked(object sender, string searchText)
        {
            string maBenhNhan = searchText.Trim();

            if (string.IsNullOrEmpty(maBenhNhan))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string query = "SELECT * FROM BenhNhan WHERE MaBenhNhan = @MaBenhNhan OR Ten = @MaBenhNhan";

            try
            {
                if (sqlCon == null)
                    sqlCon = _userRepository.GetConnection();

                sqlCon.Open();

                SqlCommand command = new SqlCommand(query, sqlCon);
                command.Parameters.AddWithValue("@MaBenhNhan", maBenhNhan);

                adapter = new SqlDataAdapter(command);
                ds = new DataSet();
                adapter.Fill(ds, "tblBenhNhan");

                if (ds.Tables["tblBenhNhan"].Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu phù hợp!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    dgDanhSachBenhNhan.ItemsSource = null;
                    CLearTextBoxes();
                }
                else
                {
                    dgDanhSachBenhNhan.ItemsSource = ds.Tables["tblBenhNhan"].DefaultView;
                    if (ds.Tables["tblBenhNhan"].Rows.Count == 1)
                    {
                        DisplayData(ds.Tables["tblBenhNhan"].Rows[0]);
                    }
                    else
                    {
                        CLearTextBoxes();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                sqlCon.Close();
            }
        }
        private void DisplayData(DataRow row)
        {
            txtMaBenhNhan.Text = row["MaBenhNhan"]?.ToString() ?? string.Empty;
            txtHo.Text = row["Ho"]?.ToString() ?? string.Empty;
            txtTen.Text = row["Ten"]?.ToString() ?? string.Empty;
            txtNgaySinh.Text = row["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(row["NgaySinh"]).ToString("yyyy-MM-dd") : string.Empty;
            txtGioiTinh.Text = row["GioiTinh"]?.ToString() ?? string.Empty;
            txtNgheNghiep.Text = row["NgheNghiep"]?.ToString() ?? string.Empty;
            txtCCCD.Text = row["CCCD"]?.ToString() ?? string.Empty;
            txtSDT.Text = row["SDT"]?.ToString() ?? string.Empty;
            txtEmail.Text = row["Email"]?.ToString() ?? string.Empty;
            txtKhoa.Text = row["MaKhoa"]?.ToString() ?? string.Empty;
            txtDiaChi.Text = row["DiaChi"]?.ToString() ?? string.Empty;
        }
        private void CLearTextBoxes()
        {
            txtCCCD.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtGioiTinh.Clear();
            txtHo.Clear();
            txtKhoa.Clear();
            txtMaBenhNhan.Clear();
            txtNgheNghiep.Clear();
            txtSDT.Clear();
            txtTen.Clear();
            txtNgaySinh.Clear();
        }

        SqlConnection sqlCon = null;
        SqlDataAdapter adapter = null;
        DataSet ds = null;
        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Tạo một DataRow mới nhưng không thêm ngay vào DataTable
                DataRow dataRow = ds.Tables["tblBenhNhan"].NewRow();
                dataRow["MaBenhNhan"] = txtMaBenhNhan.Text.Trim();
                dataRow["Ho"] = txtHo.Text.Trim();
                dataRow["Ten"] = txtTen.Text.Trim();

                // Xử lý định dạng ngày sinh
                dataRow["NgaySinh"] = DateTime.TryParse(txtNgaySinh.Text.Trim(), out DateTime ngaySinh)
                    ? ngaySinh.ToString("yyyy-MM-dd")
                    : throw new FormatException("Invalid date format");

                dataRow["GioiTinh"] = txtGioiTinh.Text.Trim();
                dataRow["NgheNghiep"] = txtNgheNghiep.Text.Trim();
                dataRow["CCCD"] = txtCCCD.Text.Trim();
                dataRow["SDT"] = txtSDT.Text.Trim();
                dataRow["MaKhoa"] = txtKhoa.Text.Trim();
                dataRow["Email"] = txtEmail.Text.Trim();
                dataRow["DiaChi"] = txtDiaChi.Text.Trim();

                // Tạm thời thêm DataRow (chỉ khi không có lỗi xảy ra)
                ds.Tables["tblBenhNhan"].Rows.Add(dataRow);

                // Cập nhật dữ liệu vào cơ sở dữ liệu
                int kq = adapter.Update(ds.Tables["tblBenhNhan"]);
                if (kq > 0)
                {
                    MessageBox.Show("Thêm dữ liệu thành công!!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    CLearTextBoxes(); // Giả định là bạn có phương thức này để làm sạch các ô nhập liệu
                }
                else
                {
                    MessageBox.Show("Thêm dữ liệu thất bại!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (SqlException ex)
            {
                // Xóa DataRow vừa thêm nếu gặp lỗi SQL
                if (ds.Tables["tblBenhNhan"].Rows.Count > 0 && ds.Tables["tblBenhNhan"].Rows[ds.Tables["tblBenhNhan"].Rows.Count - 1].RowState == DataRowState.Added)
                {
                    ds.Tables["tblBenhNhan"].Rows[ds.Tables["tblBenhNhan"].Rows.Count - 1].Delete();
                }

                // Kiểm tra lỗi SQL dựa trên mã lỗi
                switch (ex.Number)
                {
                    case 2627: // Lỗi vi phạm PRIMARY KEY
                        MessageBox.Show("Khóa chính đã tồn tại trong cơ sở dữ liệu! Không thể thêm dữ liệu trùng lặp.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;

                    case 547: // Lỗi vi phạm FOREIGN KEY
                        MessageBox.Show("Dữ liệu không hợp lệ! Mã khoa không tồn tại trong hệ thống.", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Error);
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
                if (ds.Tables["tblBenhNhan"].Rows.Count > 0 && ds.Tables["tblBenhNhan"].Rows[ds.Tables["tblBenhNhan"].Rows.Count - 1].RowState == DataRowState.Added)
                {
                    ds.Tables["tblBenhNhan"].Rows[ds.Tables["tblBenhNhan"].Rows.Count - 1].Delete();
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
            string query = "  select MaBenhNhan, Ho, Ten, NgaySinh, GioiTinh, NgheNghiep, CCCD, SDT, MaKhoa, Email, DiaChi from BenhNhan";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            ds = new DataSet();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter.Fill(ds, "tblBenhNhan");
            sqlCon.Close();

            dgDanhSachBenhNhan.ItemsSource = ds.Tables["tblBenhNhan"].DefaultView;
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = dgDanhSachBenhNhan.SelectedItem as DataRowView;

            if (selectedRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa bệnh nhân này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Lấy DataRow từ DataRowView
                    DataRow dataRow = selectedRow.Row;

                    // Xóa hàng trong DataTable
                    dataRow.Delete();

                    // Cập nhật thay đổi vào cơ sở dữ liệu
                    int kq = adapter.Update(ds.Tables["tblBenhNhan"]);

                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Cập nhật giao diện DataGrid
                        dgDanhSachBenhNhan.ItemsSource = null;
                        dgDanhSachBenhNhan.ItemsSource = ds.Tables["tblBenhNhan"].DefaultView;

                        // Xóa dữ liệu trong TextBox
                        CLearTextBoxes();
                    }
                    else
                    {
                        MessageBox.Show("Xóa dữ liệu thất bại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (SqlException ex)
            {
                // Kiểm tra lỗi SQL (vi phạm khóa ngoại, khóa chính...)
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
            HienThiDanhSach();
        }

        private int vitri = -1;
        private void dgDanhSachBenhNhan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Lấy dòng dữ liệu được chọn
            var selectedRow = dgDanhSachBenhNhan.SelectedItem as DataRowView;

            if (selectedRow == null) return; // Nếu không có dòng nào được chọn, thoát

            // Lấy DataRow từ DataRowView
            DataRow dataRow = selectedRow.Row;

            // Hiển thị dữ liệu lên các TextBox
            txtMaBenhNhan.Text = dataRow["MaBenhNhan"]?.ToString() ?? string.Empty;
            txtHo.Text = dataRow["Ho"]?.ToString() ?? string.Empty;
            txtTen.Text = dataRow["Ten"]?.ToString() ?? string.Empty;
            txtNgaySinh.Text = Convert.ToDateTime(dataRow["NgaySinh"]).ToString("yyyy-MM-dd")?.ToString() ?? string.Empty;
            txtGioiTinh.Text = dataRow["GioiTinh"]?.ToString() ?? string.Empty;
            txtNgheNghiep.Text = dataRow["NgheNghiep"]?.ToString() ?? string.Empty;
            txtCCCD.Text = dataRow["CCCD"]?.ToString() ?? string.Empty;
            txtSDT.Text = dataRow["SDT"]?.ToString() ?? string.Empty;
            txtKhoa.Text = dataRow["MaKhoa"]?.ToString() ?? string.Empty;
            txtEmail.Text = dataRow["Email"]?.ToString() ?? string.Empty;
            txtDiaChi.Text = dataRow["DiaChi"]?.ToString() ?? string.Empty;
        }

        private void btnCapNhat_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = dgDanhSachBenhNhan.SelectedItem as DataRowView;
            if (selectedRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để cập nhật!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!ValidateInput())
                return;

            string updateQuery = @"UPDATE BenhNhan 
                           SET Ho = @Ho, Ten = @Ten, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, 
                               NgheNghiep = @NgheNghiep, CCCD = @CCCD, DiaChi = @DiaChi, 
                               SDT = @SDT, Email = @Email, MaKhoa = @MaKhoa
                           WHERE MaBenhNhan = @MaBenhNhan";

            try
            {
                if (sqlCon == null)
                    sqlCon = _userRepository.GetConnection();

                sqlCon.Open();

                SqlCommand command = new SqlCommand(updateQuery, sqlCon);
                command.Parameters.AddWithValue("@MaBenhNhan", txtMaBenhNhan.Text.Trim());
                command.Parameters.AddWithValue("@Ho", txtHo.Text.Trim());
                command.Parameters.AddWithValue("@Ten", txtTen.Text.Trim());
                command.Parameters.AddWithValue("@NgaySinh", DateTime.Parse(txtNgaySinh.Text.Trim()));
                command.Parameters.AddWithValue("@GioiTinh", txtGioiTinh.Text.Trim());
                command.Parameters.AddWithValue("@NgheNghiep", txtNgheNghiep.Text.Trim());
                command.Parameters.AddWithValue("@CCCD", txtCCCD.Text.Trim());
                command.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text.Trim());
                command.Parameters.AddWithValue("@SDT", txtSDT.Text.Trim());
                command.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                command.Parameters.AddWithValue("@MaKhoa", txtKhoa.Text.Trim());

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    HienThiDanhSach(); // Tải lại dữ liệu
                }
                else
                {
                    MessageBox.Show("Không có bản ghi nào được cập nhật!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                sqlCon.Close();
            }

        }
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaBenhNhan.Text) ||
                string.IsNullOrWhiteSpace(txtHo.Text) ||
                string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!DateTime.TryParse(txtNgaySinh.Text.Trim(), out _))
            {
                MessageBox.Show("Ngày sinh không hợp lệ. Vui lòng nhập đúng định dạng (yyyy-MM-dd).", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }
}
