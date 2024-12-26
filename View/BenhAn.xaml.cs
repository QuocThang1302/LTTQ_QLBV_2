using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;
using System.Data;
using QuanLyBenhVien.Repositories;
using System.Windows.Input;

namespace QuanLyBenhVien.View
{
    /// <summary>
    /// Interaction logic for BenhAn.xaml
    /// </summary>
    public partial class BenhAn : UserControl
    {
        private readonly RepositoryBase _userRepository;
        public BenhAn()
        {
            _userRepository = new UserRepository();
            InitializeComponent();
            searchControl.Tmp = "Nhập mã bệnh án hoặc mã bệnh nhân";
            // Đăng ký sự kiện SearchButtonClicked
            searchControl.SearchButtonClicked += SearchControl_SearchButtonClicked;
            // Đăng ký sự kiện ClearButtonClicked cho nút X
            searchControl.ClearButtonClicked += SearchControl_ClearButtonClicked;
        }
        private void SearchControl_ClearButtonClicked(object sender, EventArgs e)
        {
            // Logic khi nút X được nhấn
            ClearFields();
            HienThiDanhSach();
        }
        private void txtNgayTaoLap_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            popupCalendar.IsOpen = true;
        }
        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (popupCalendar.IsOpen && !popupCalendar.IsMouseOver && !calendar.IsMouseOver)
            {
                popupCalendar.IsOpen = false;
            }
        }
        
        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (calendar.SelectedDate.HasValue)
            {
                txtNgayTaoLap.Text = calendar.SelectedDate.Value.ToString("yyyy-MM-dd"); // Định dạng ngày tháng
                popupCalendar.IsOpen = false;
            }
        }
        private void SearchControl_SearchButtonClicked(object sender, string searchText)
        {

            string maBenhAn = searchText.Trim();

            if (string.IsNullOrEmpty(maBenhAn))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu trước khi tìm kiếm!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                
                return;
            }

           
            string query = "SELECT * FROM BenhAn JOIN BenhNhan ON BenhAn.MaBenhNhan=BenhNhan.MaBenhNhan WHERE MaBenhAn=@MaBenhAn OR BenhNhan.MaBenhNhan=@MaBenhAn";

            try
            {
                // Mở kết nối SQL
                sqlCon = _userRepository.GetConnection();
                sqlCon.Open();

                // Tạo adapter và thiết lập truy vấn
                adapter = new SqlDataAdapter(query, sqlCon);
                adapter.SelectCommand.Parameters.AddWithValue("@MaBenhAn", maBenhAn);

                // Gán kết quả vào DataSet
                ds = new DataSet();
                adapter.Fill(ds);

                DataTable dataTable = ds.Tables[0];

                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (dataTable.Rows.Count == 1)
                {
                    // Hiển thị thông tin lên TextBox
                    DataRow row = dataTable.Rows[0];
                    txtMaBenhAn.Text = row["MaBenhAn"].ToString();
                    txtMaBenhNhan.Text = row["MaBenhNhan"].ToString();
                    txtBenhNhan.Text = row["Ten"].ToString();
                    txtTinhTrang.Text = row["TinhTrang"].ToString();
                    txtNgayTaoLap.Text = Convert.ToDateTime(row["NgayTaoLap"]).ToString("yyyy-MM-dd");
                    txtBenh.Text = row["Benh"].ToString();
                    txtHuongDieuTri.Text = row["DieuTri"].ToString();

                    // Hiển thị kết quả vào DataGrid
                    dgDanhSachBenhAn.ItemsSource = dataTable.DefaultView;
                }
                else
                {
                    // Nếu có nhiều kết quả, chỉ hiển thị vào DataGrid
                    ClearFields();
                    dgDanhSachBenhAn.ItemsSource = dataTable.DefaultView;
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
            txtMaBenhAn.Text = "";
            txtMaBenhNhan.Text = "";
            txtBenh.Text = "";
            txtTinhTrang.Text = "";
            txtNgayTaoLap.Text = "";
            txtBenh.Text = "";
            txtHuongDieuTri.Text = "";
            txtBenhNhan.Clear();
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

            string selectQuery = "SELECT MaBenhAn, BenhAn.MaBenhNhan, Ten, NgayTaoLap, TinhTrang, Benh, DieuTri " +
                                 "FROM BenhAn " +
                                 "JOIN BenhNhan ON BenhAn.MaBenhNhan = BenhNhan.MaBenhNhan";

            adapter = new SqlDataAdapter(selectQuery, sqlCon);

            // Thêm lệnh INSERT
            adapter.InsertCommand = new SqlCommand(
                "INSERT INTO BenhAn (MaBenhAn, MaBenhNhan, NgayTaoLap, Benh, TinhTrang, DieuTri) " +
                "VALUES (@MaBenhAn, @MaBenhNhan, @NgayTaoLap, @Benh, @TinhTrang, @DieuTri)", sqlCon);
            adapter.InsertCommand.Parameters.Add("@MaBenhAn", SqlDbType.NVarChar, 50, "MaBenhAn");
            adapter.InsertCommand.Parameters.Add("@MaBenhNhan", SqlDbType.NVarChar, 50, "MaBenhNhan");
            adapter.InsertCommand.Parameters.Add("@NgayTaoLap", SqlDbType.Date, 0, "NgayTaoLap");
            adapter.InsertCommand.Parameters.Add("@Benh", SqlDbType.NVarChar, 100, "Benh");
            adapter.InsertCommand.Parameters.Add("@TinhTrang", SqlDbType.NVarChar, 100, "TinhTrang");
            adapter.InsertCommand.Parameters.Add("@DieuTri", SqlDbType.NVarChar, 100, "DieuTri");

            // Thêm lệnh UPDATE
            adapter.UpdateCommand = new SqlCommand(
                "UPDATE BenhAn " +
                "SET MaBenhNhan=@MaBenhNhan, NgayTaoLap=@NgayTaoLap, Benh=@Benh, TinhTrang=@TinhTrang, DieuTri=@DieuTri " +
                "WHERE MaBenhAn=@MaBenhAn", sqlCon);
            adapter.UpdateCommand.Parameters.Add("@MaBenhAn", SqlDbType.NVarChar, 50, "MaBenhAn");
            adapter.UpdateCommand.Parameters.Add("@MaBenhNhan", SqlDbType.NVarChar, 50, "MaBenhNhan");
            adapter.UpdateCommand.Parameters.Add("@NgayTaoLap", SqlDbType.Date, 0, "NgayTaoLap");
            adapter.UpdateCommand.Parameters.Add("@Benh", SqlDbType.NVarChar, 100, "Benh");
            adapter.UpdateCommand.Parameters.Add("@TinhTrang", SqlDbType.NVarChar, 100, "TinhTrang");
            adapter.UpdateCommand.Parameters.Add("@DieuTri", SqlDbType.NVarChar, 100, "DieuTri");

            // Thêm lệnh DELETE
            adapter.DeleteCommand = new SqlCommand(
                "DELETE FROM BenhAn WHERE MaBenhAn=@MaBenhAn", sqlCon);
            adapter.DeleteCommand.Parameters.Add("@MaBenhAn", SqlDbType.NVarChar, 50, "MaBenhAn");

            ds = new DataSet();

            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            adapter.Fill(ds, "tblBenhAn");
            sqlCon.Close();

            dgDanhSachBenhAn.ItemsSource = ds.Tables["tblBenhAn"].DefaultView;
        }
        private int vitri = -1;
        private void dgDanhSachBenhAn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Lấy hàng được chọn từ DataGrid
            var selectedRow = dgDanhSachBenhAn.SelectedItem as DataRowView;

            if (selectedRow == null) return;

            // Lấy dữ liệu từ DataRowView
            DataRow dataRow = selectedRow.Row;

            // Gán giá trị vào các TextBox
            txtBenhNhan.Text = dataRow["Ten"].ToString();
            txtMaBenhAn.Text = dataRow["MaBenhAn"].ToString();
            txtMaBenhNhan.Text = dataRow["MaBenhNhan"].ToString();
            txtNgayTaoLap.Text = Convert.ToDateTime(dataRow["NgayTaoLap"]).ToString("yyyy-MM-dd");
            txtBenh.Text = dataRow["Benh"].ToString();
            txtTinhTrang.Text = dataRow["TinhTrang"].ToString();
            txtHuongDieuTri.Text = dataRow["Dieutri"].ToString();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Tạo một DataRow mới nhưng không thêm ngay vào DataTable
                DataRow dataRow = ds.Tables["tblBenhAn"].NewRow();
                dataRow["MaBenhAn"] = txtMaBenhAn.Text.Trim();
                dataRow["Ten"] = txtBenhNhan.Text.Trim();
                dataRow["MaBenhNhan"] = txtMaBenhNhan.Text.Trim();

                // Xử lý định dạng ngày tạo lập
                dataRow["NgayTaoLap"] = DateTime.TryParse(txtNgayTaoLap.Text.Trim(), out DateTime ngayTaoLap)
                    ? ngayTaoLap.ToString("yyyy-MM-dd")
                    : throw new FormatException("Invalid date format");

                dataRow["Benh"] = txtBenh.Text.Trim();
                dataRow["TinhTrang"] = txtTinhTrang.Text.Trim();
                dataRow["DieuTri"] = txtHuongDieuTri.Text.Trim();

                // Tạm thời thêm DataRow (chỉ khi không có lỗi xảy ra)
                ds.Tables["tblBenhAn"].Rows.Add(dataRow);

                // Cập nhật dữ liệu vào cơ sở dữ liệu
                int kq = adapter.Update(ds.Tables["tblBenhAn"]);
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
                if (ds.Tables["tblBenhAn"].Rows.Count > 0 && ds.Tables["tblBenhAn"].Rows[ds.Tables["tblBenhAn"].Rows.Count - 1].RowState == DataRowState.Added)
                {
                    ds.Tables["tblBenhAn"].Rows[ds.Tables["tblBenhAn"].Rows.Count - 1].Delete();
                }

                // Kiểm tra lỗi SQL dựa trên mã lỗi
                switch (ex.Number)
                {
                    case 2627: // Lỗi vi phạm PRIMARY KEY
                        MessageBox.Show("Khóa chính đã tồn tại trong cơ sở dữ liệu! Không thể thêm dữ liệu trùng lặp.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;

                    case 547: // Lỗi vi phạm FOREIGN KEY
                        MessageBox.Show("Dữ liệu không hợp lệ! Mã bệnh nhân không tồn tại trong hệ thống.", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Error);
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
                if (ds.Tables["tblBenhAn"].Rows.Count > 0 && ds.Tables["tblBenhAn"].Rows[ds.Tables["tblBenhAn"].Rows.Count - 1].RowState == DataRowState.Added)
                {
                    ds.Tables["tblBenhAn"].Rows[ds.Tables["tblBenhAn"].Rows.Count - 1].Delete();
                }
            }

        }

        private void btnCapNhat_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dòng được chọn từ DataGrid
            var selectedRow = dgDanhSachBenhAn.SelectedItem as DataRowView;

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
                dataRow["MaBenhAn"] = txtMaBenhAn.Text.Trim();
                dataRow["Ten"] = txtBenhNhan.Text.Trim();
                dataRow["MaBenhNhan"] = txtMaBenhNhan.Text.Trim();

                if (DateTime.TryParse(txtNgayTaoLap.Text.Trim(), out DateTime ngayTaoLap))
                {
                    dataRow["NgayTaoLap"] = ngayTaoLap.ToString("yyyy-MM-dd");
                }
                else
                {
                    throw new FormatException("Định dạng ngày không hợp lệ! Vui lòng nhập đúng định dạng.");
                }

                dataRow["Benh"] = txtBenh.Text.Trim();
                dataRow["TinhTrang"] = txtTinhTrang.Text.Trim();
                dataRow["DieuTri"] = txtHuongDieuTri.Text.Trim();

                // Cập nhật thay đổi vào cơ sở dữ liệu
                int kq = adapter.Update(ds.Tables["tblBenhAn"]);

                if (kq > 0)
                {
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Cập nhật giao diện DataGrid
                    dgDanhSachBenhAn.ItemsSource = null;
                    dgDanhSachBenhAn.ItemsSource = ds.Tables["tblBenhAn"].DefaultView;

                    // Giữ lại dòng đã chọn
                    dgDanhSachBenhAn.SelectedItem = selectedRow;
                    HienThiDanhSach();
                }
                else
                {
                    MessageBox.Show("Cập nhật dữ liệu thất bại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (SqlException ex)
            {
                // Xử lý lỗi SQL
                if (ex.Number == 2627) // Lỗi vi phạm PRIMARY KEY
                {
                    MessageBox.Show("Khóa chính đã tồn tại! Không thể cập nhật dữ liệu trùng lặp.", "Lỗi khóa chính", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ex.Number == 547) // Lỗi vi phạm FOREIGN KEY
                {
                    MessageBox.Show("Dữ liệu không hợp lệ! Mã bệnh nhân không tồn tại trong hệ thống.", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show($"Lỗi SQL: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Lỗi định dạng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi không xác định: {ex.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                ClearFields(); // Xóa các trường sau khi xử lý
            }
            HienThiDanhSach();
        }
        private void ClearTextBoxes()
        {
            txtMaBenhAn.Clear();
            txtMaBenhNhan.Clear();
            txtNgayTaoLap.Clear();
            txtBenh.Clear();
            txtTinhTrang.Clear();
            txtHuongDieuTri.Clear();
            txtBenhNhan.Clear();
        }
        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            // Lấy hàng được chọn từ DataGrid
            var selectedRow = dgDanhSachBenhAn.SelectedItem as DataRowView;
            if (selectedRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa bệnh án này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Lấy DataRow từ DataRowView
                    DataRow dataRow = selectedRow.Row;

                    // Xóa hàng trong DataTable
                    dataRow.Delete();

                    // Cập nhật thay đổi vào cơ sở dữ liệu
                    int kq = adapter.Update(ds.Tables["tblBenhAn"]);

                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Cập nhật giao diện DataGrid
                        dgDanhSachBenhAn.ItemsSource = null;
                        dgDanhSachBenhAn.ItemsSource = ds.Tables["tblBenhAn"].DefaultView;

                        // Xóa dữ liệu trong TextBox
                        ClearTextBoxes();
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
                    MessageBox.Show("Không thể xóa bệnh án này vì dữ liệu bị ràng buộc với các bảng khác.", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ex.Number == 2627) // Lỗi vi phạm khóa chính (primary key)
                {
                    MessageBox.Show("Không thể xóa bệnh án này vì có dữ liệu trùng lặp trong hệ thống.", "Lỗi khóa chính", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}
