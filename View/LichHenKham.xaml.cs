using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;
using System.Data;
using QuanLyBenhVien.Repositories;
using System.Diagnostics;

namespace QuanLyBenhVien.View
{
    public partial class LichHenKham : UserControl
    {
        private readonly RepositoryBase _userRepository;
        public LichHenKham()
        {
            _userRepository = new UserRepository();
            InitializeComponent();
            searchControl.Tmp = "Nhập mã lịch hẹn hoặc mã bác sĩ";
            // Đăng ký sự kiện SearchButtonClicked
            searchControl.SearchButtonClicked += SearchControl_SearchButtonClicked;
            // Đăng ký sự kiện ClearButtonClicked cho nút X
            searchControl.ClearButtonClicked += SearchControl_ClearButtonClicked;

        }
        private void SearchControl_ClearButtonClicked(object sender, EventArgs e)
        {
            // Logic khi nút X được nhấn
            HienThiDanhSach();
            ClearFields();
        }
        private void SearchControl_SearchButtonClicked(object sender, string searchText)
        {
            string maLichHen = searchText.Trim();

            if (string.IsNullOrEmpty(maLichHen))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                
                return;
            }
            
            string query = "SELECT * FROM LichHenKham WHERE MaLichHen=@MaLichHen OR MaBacSi=@MaLichHen";

            try
            {
                // Mở kết nối SQL
                sqlCon = _userRepository.GetConnection();
                sqlCon.Open();

                // Sử dụng SqlDataAdapter để truy xuất dữ liệu
                adapter = new SqlDataAdapter(query, sqlCon);
                adapter.SelectCommand.Parameters.AddWithValue("@MaLichHen", maLichHen);

                // Đổ dữ liệu vào DataSet
                ds = new DataSet();
                adapter.Fill(ds, "LichHenKham");
                DataTable dataTable = ds.Tables["LichHenKham"];

                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    dgvLichHen.ItemsSource = null;
                }
                else if (dataTable.Rows.Count == 1)
                {
                    // Hiển thị thông tin lên TextBox
                    DataRow row = dataTable.Rows[0];
                    tbMaLichHen.Text = row["MaLichHen"].ToString();
                    tbMaBenhNhan.Text = row["MaBenhNhan"].ToString();
                    tbNgayHenKham.Text = Convert.ToDateTime(row["NgayHenKham"]).ToString("yyyy-MM-dd");
                    tbMaBacSi.Text = row["MaBacSi"].ToString();

                    // Gắn dữ liệu vào DataGrid
                    dgvLichHen.ItemsSource = dataTable.DefaultView;
                }
                else
                {
                    // Nếu có nhiều kết quả, chỉ hiển thị vào DataGrid
                    ClearFields(); // Xóa TextBox
                    dgvLichHen.ItemsSource = dataTable.DefaultView;
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

        private void ClearFields()
        {
            tbMaLichHen.Text = "";
            tbMaBenhNhan.Text = "";
            tbNgayHenKham.Text = "";
            tbMaBacSi.Text = "";
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
            string query = @"select MaLichHen, MaBenhNhan, NgayHenKham, MaBacSi from LichHenKham";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            ds = new DataSet();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter.Fill(ds, "tblLichHenKham");
            sqlCon.Close();

            dgvLichHen.ItemsSource = ds.Tables["tblLichHenKham"].DefaultView;
        }

        

        private int vitri = -1;
        private void dgvLichHen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRow = dgvLichHen.SelectedItem as DataRowView;

            if (selectedRow == null) return;

            // Lấy dữ liệu từ DataRowView
            DataRow dataRow = selectedRow.Row;

            tbMaLichHen.Text = dataRow["MaLichHen"].ToString();
            tbMaBenhNhan.Text = dataRow["MaBenhNhan"].ToString();
            tbNgayHenKham.Text = dataRow["NgayHenKham"].ToString();
            tbMaBacSi.Text = dataRow["MaBacSi"].ToString();
            
        }

        private void btnCapNhat_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = dgvLichHen.SelectedItem as DataRowView;

            if (selectedRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để cập nhật!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Lấy DataRow từ DataRowView
                DataRow dataRow = selectedRow.Row;

                // Cập nhật dữ liệu từ các TextBox vào DataRow
                dataRow["MaLichHen"] = tbMaLichHen.Text.Trim();
                dataRow["MaBenhNhan"] = tbMaBenhNhan.Text.Trim();
                dataRow["MaBacSi"] = tbMaBacSi.Text.Trim();

                // Kiểm tra và cập nhật ngày hẹn khám
                if (DateTime.TryParse(tbNgayHenKham.Text.Trim(), out DateTime ngayHenKham))
                {
                    dataRow["NgayHenKham"] = ngayHenKham.ToString("yyyy-MM-dd");
                }
                else
                {
                    throw new FormatException("Định dạng ngày không hợp lệ!");
                }

                // Cập nhật thay đổi vào cơ sở dữ liệu
                int kq = adapter.Update(ds.Tables["tblLichHenKham"]);

                if (kq > 0)
                {
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Cập nhật giao diện DataGrid
                    dgvLichHen.ItemsSource = null;
                    dgvLichHen.ItemsSource = ds.Tables["tblLichHenKham"].DefaultView;

                    // Giữ lại dòng đã chọn
                    dgvLichHen.SelectedItem = selectedRow;
                    ClearFields(); // Xóa các trường sau khi xử lý
                }
                else
                {
                    MessageBox.Show("Cập nhật dữ liệu thất bại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (SqlException ex)
            {
                // Xử lý lỗi SQL (khóa chính và khóa ngoại)
                if (ex.Number == 2627) // Lỗi vi phạm PRIMARY KEY
                {
                    MessageBox.Show("Khóa chính đã tồn tại! Không thể cập nhật dữ liệu trùng lặp.", "Lỗi khóa chính", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ex.Number == 547) // Lỗi vi phạm FOREIGN KEY
                {
                    MessageBox.Show("Dữ liệu không hợp lệ! Mã bệnh nhân hoặc mã bác sĩ không tồn tại trong hệ thống.", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show($"Lỗi SQL: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Định dạng ngày không hợp lệ: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            ClearFields(); // Đảm bảo xóa các trường sau khi hoàn thành


        }

        private void btnXoa_Click_1(object sender, RoutedEventArgs e)
        {
            // Lấy hàng được chọn từ DataGrid
            var selectedRow = dgvLichHen.SelectedItem as DataRowView;

            if (selectedRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Hiển thị hộp thoại xác nhận xóa
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Lấy DataRow từ DataRowView
                    DataRow dataRow = selectedRow.Row;

                    // Xóa dòng khỏi DataTable
                    dataRow.Delete();

                    // Cập nhật thay đổi vào cơ sở dữ liệu
                    int kq = adapter.Update(ds.Tables["tblLichHenKham"]);

                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Cập nhật lại giao diện DataGrid
                        dgvLichHen.ItemsSource = null;
                        dgvLichHen.ItemsSource = ds.Tables["tblLichHenKham"].DefaultView;

                        // Xóa dữ liệu trong các TextBox
                        ClearFields();
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
                    MessageBox.Show("Không thể xóa dòng này vì dữ liệu bị ràng buộc với các bảng khác.", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ex.Number == 2627) // Lỗi vi phạm khóa chính (primary key)
                {
                    MessageBox.Show("Không thể xóa dòng này vì có dữ liệu trùng lặp trong hệ thống.", "Lỗi khóa chính", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void btnDangKi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Tạo một DataRow mới nhưng không thêm ngay vào DataTable
                DataRow dataRow = ds.Tables["tblLichHenKham"].NewRow();
                dataRow["MaLichHen"] = tbMaLichHen.Text.Trim();
                dataRow["MaBenhNhan"] = tbMaBenhNhan.Text.Trim();
                dataRow["MaBacSi"] = tbMaBacSi.Text.Trim();

                // Xử lý định dạng ngày
                dataRow["NgayHenKham"] = DateTime.TryParse(tbNgayHenKham.Text.Trim(), out DateTime ngaySinh)
                    ? ngaySinh.ToString("yyyy-MM-dd")
                    : throw new FormatException("Invalid date format");

                // Tạm thời thêm DataRow (chỉ khi không có lỗi xảy ra)
                ds.Tables["tblLichHenKham"].Rows.Add(dataRow);

                // Cập nhật dữ liệu vào cơ sở dữ liệu
                int kq = adapter.Update(ds.Tables["tblLichHenKham"]);
                if (kq > 0)
                {
                    MessageBox.Show("Thêm dữ liệu thành công!!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearFields();
                }
                else
                {
                    // Nếu không cập nhật được, xóa DataRow vừa thêm
                    ds.Tables["tblLichHenKham"].Rows.Remove(dataRow);
                    MessageBox.Show("Thêm dữ liệu thất bại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (SqlException ex)
            {
                // Xóa DataRow vừa thêm nếu gặp lỗi SQL
                if (ds.Tables["tblLichHenKham"].Rows.Count > 0 && ds.Tables["tblLichHenKham"].Rows[ds.Tables["tblLichHenKham"].Rows.Count - 1].RowState == DataRowState.Added)
                {
                    ds.Tables["tblLichHenKham"].Rows[ds.Tables["tblLichHenKham"].Rows.Count - 1].Delete();
                }

                // Kiểm tra lỗi SQL dựa trên mã lỗi
                switch (ex.Number)
                {
                    case 2627: // Lỗi vi phạm PRIMARY KEY
                        MessageBox.Show("Khóa chính đã tồn tại trong cơ sở dữ liệu! Không thể thêm dữ liệu trùng lặp.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;

                    case 547: // Lỗi vi phạm FOREIGN KEY
                        MessageBox.Show("Dữ liệu không hợp lệ! Mã bệnh nhân hoặc mã bác sĩ không tồn tại trong hệ thống.", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Error);
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
                if (ds.Tables["tblLichHenKham"].Rows.Count > 0 && ds.Tables["tblLichHenKham"].Rows[ds.Tables["tblLichHenKham"].Rows.Count - 1].RowState == DataRowState.Added)
                {
                    ds.Tables["tblLichHenKham"].Rows[ds.Tables["tblLichHenKham"].Rows.Count - 1].Delete();
                }
            }


        }
    }
}

