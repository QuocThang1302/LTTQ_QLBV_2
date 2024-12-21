using System.Data;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using QuanLyBenhVien.Repositories;

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

            // Câu lệnh SQL để tìm kiếm thông tin bệnh nhân
            string query = "SELECT * FROM BenhNhan WHERE MaBenhNhan=@MaBenhNhan OR Ten=@MaBenhNhan";

            try
            {
                using (SqlConnection connection = _userRepository.GetConnection())
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaBenhNhan", maBenhNhan);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        
                    }
                    else if (dataTable.Rows.Count == 1)
                    {
                        // Hiển thị thông tin lên TextBox
                        DataRow row = dataTable.Rows[0];
                        txtMaBenhNhan.Text = row["MaBenhNhan"].ToString();
                        txtHo.Text = row["Ho"].ToString();
                        txtTen.Text = row["Ten"].ToString();
                        txtNgaySinh.Text = Convert.ToDateTime(row["NgaySinh"]).ToString("yyyy-MM-dd");
                        txtGioiTinh.Text = row["GioiTinh"].ToString();
                        txtCCCD.Text = row["CCCD"].ToString();
                        txtNgheNghiep.Text = row["NgheNghiep"].ToString();
                        txtDiaChi.Text = row["DiaChi"].ToString();
                        txtSDT.Text = row["SDT"].ToString();
                        txtEmail.Text = row["Email"].ToString();
                        txtKhoa.Text = row["MaKhoa"].ToString();

                        // Hiển thị dữ liệu vào DataGrid
                        dgDanhSachBenhNhan.ItemsSource = dataTable.DefaultView;
                    }
                    else
                    {
                        // Nếu có nhiều kết quả, chỉ hiển thị vào DataGrid
                        CLearTextBoxes(); // Xóa các TextBox
                        dgDanhSachBenhNhan.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            if (vitri == -1)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa bệnh nhân này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                   
                    DataRow dataRow = ds.Tables["tblBenhNhan"].Rows[vitri];
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
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int vitri = -1;
        private void dgDanhSachBenhNhan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vitri = dgDanhSachBenhNhan.SelectedIndex;
            if (vitri == -1) return;

            DataRow dataRow = ds.Tables["tblBenhNhan"].Rows[vitri];

            txtMaBenhNhan.Text = dataRow["MaBenhNhan"].ToString();
            txtHo.Text = dataRow["Ho"].ToString();
            txtTen.Text = dataRow["Ten"].ToString();
            txtNgaySinh.Text = dataRow["NgaySinh"].ToString();
            txtGioiTinh.Text = dataRow["GioiTinh"].ToString();
            txtNgheNghiep.Text = dataRow["NgheNghiep"].ToString();
            txtCCCD.Text = dataRow["CCCD"].ToString();
            txtSDT.Text = dataRow["SDT"].ToString();
            txtKhoa.Text = dataRow["MaKhoa"].ToString();
            txtEmail.Text = dataRow["Email"].ToString();
            txtDiaChi.Text = dataRow["DiaChi"].ToString();
        }

        private void btnCapNhat_Click(object sender, RoutedEventArgs e)
        {
            if (vitri == -1)
            {
                MessageBox.Show("Vui lòng chọn một dòng để cập nhật!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Kiểm tra dữ liệu đầu vào trước
                if (!DateTime.TryParse(txtNgaySinh.Text.Trim(), out DateTime ngaySinh))
                    throw new FormatException("Định dạng ngày không hợp lệ.");

                // Lấy dòng dữ liệu được chọn trong DataSet
                DataRow dataRow = ds.Tables["tblBenhNhan"].Rows[vitri];

                dataRow.BeginEdit(); // Bắt đầu chỉnh sửa dữ liệu

                // Cập nhật dữ liệu từ các TextBox vào DataRow
                dataRow["MaBenhNhan"] = txtMaBenhNhan.Text.Trim();
                dataRow["Ho"] = txtHo.Text.Trim();
                dataRow["Ten"] = txtTen.Text.Trim();
                dataRow["NgaySinh"] = ngaySinh.ToString("yyyy-MM-dd");
                dataRow["GioiTinh"] = txtGioiTinh.Text.Trim();
                dataRow["NgheNghiep"] = txtNgheNghiep.Text.Trim();
                dataRow["CCCD"] = txtCCCD.Text.Trim();
                dataRow["SDT"] = txtSDT.Text.Trim();
                dataRow["MaKhoa"] = txtKhoa.Text.Trim();
                dataRow["Email"] = txtEmail.Text.Trim();
                dataRow["DiaChi"] = txtDiaChi.Text.Trim();

                dataRow.EndEdit(); // Kết thúc chỉnh sửa

                // Cập nhật thay đổi vào cơ sở dữ liệu
                int kq = adapter.Update(ds.Tables["tblBenhNhan"]);

                if (kq > 0)
                {
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Cập nhật giao diện DataGrid
                    dgDanhSachBenhNhan.ItemsSource = null;
                    dgDanhSachBenhNhan.ItemsSource = ds.Tables["tblBenhNhan"].DefaultView;

                    // Đặt lại vị trí dòng đã chọn
                    dgDanhSachBenhNhan.SelectedIndex = vitri;

                    CLearTextBoxes(); // Xóa các trường nhập liệu
                }
                else
                {
                    MessageBox.Show("Cập nhật dữ liệu thất bại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Định dạng ngày không hợp lệ: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                ds.Tables["tblBenhNhan"].RejectChanges(); // Hủy thay đổi trên DataRow
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Lỗi vi phạm PRIMARY KEY
                {
                    MessageBox.Show("Khóa chính đã tồn tại! Không thể cập nhật dữ liệu trùng lặp.", "Lỗi khóa chính", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ex.Number == 547) // Lỗi vi phạm FOREIGN KEY
                {
                    MessageBox.Show("Dữ liệu không hợp lệ! Mã khoa hoặc mã bệnh nhân không tồn tại trong hệ thống.", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show($"Lỗi SQL: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                ds.Tables["tblBenhNhan"].RejectChanges(); // Hủy thay đổi trên DataRow
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                ds.Tables["tblBenhNhan"].RejectChanges(); // Hủy thay đổi trên DataRow
            }
            finally
            {
                // Đảm bảo giao diện không bị thay đổi nếu có lỗi
                dgDanhSachBenhNhan.ItemsSource = ds.Tables["tblBenhNhan"].DefaultView;
            }

        }
    }
}
