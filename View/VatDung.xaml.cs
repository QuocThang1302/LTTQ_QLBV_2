using System.Data;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using QuanLyBenhVien.Repositories;

namespace QuanLyBenhVien.View
{
    public partial class VatDung : UserControl
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
        public VatDung()
        {
            _userRepository = new UserRepository();
            InitializeComponent();
        searchControl.Tmp = "Nhập mã vật dụng hoặc tên vật dụng";
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
            ClearFields();
        }
        private void SearchControl_SearchButtonClicked(object sender, string searchText)
        {
            string maVatDung = searchText.Trim();

            if (string.IsNullOrEmpty(maVatDung))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
               
                return;
            }

            // Chuỗi kết nối tới cơ sở dữ liệu
            

            // Câu lệnh SQL để tìm kiếm thông tin vật dụng
            string query = "SELECT * FROM VatDung WHERE MaVatDung = @MaVatDung OR TenVatDung = @MaVatDung";

            try
            {
                using (SqlConnection connection = _userRepository.GetConnection())
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaVatDung", maVatDung);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        
                    }
                    else if (dataTable.Rows.Count == 1)
                    {
                        // Hiển thị thông tin lên các TextBox
                        DataRow row = dataTable.Rows[0];
                        tbMaVatDung.Text = row["MaVatDung"].ToString();
                        tbVatDung.Text = row["TenVatDung"].ToString();
                        tbMoTa.Text = row["MoTa"].ToString();
                        tbSoLuong.Text = row["SoLuong"].ToString();
                        tbGiaTien.Text = row["Gia"].ToString();
                        tbQuanLy.Text = row["MaQuanLy"].ToString();

                        // Xóa dữ liệu trong DataGrid nếu chỉ có một kết quả
                        dgDanhSachVatDung.ItemsSource = dataTable.DefaultView;
                    }
                    else
                    {
                        // Nếu có nhiều kết quả, chỉ hiển thị vào DataGrid
                        ClearFields(); // Xóa các TextBox
                        dgDanhSachVatDung.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFields()
        {
            tbMaVatDung.Text = "";
            tbVatDung.Text = "";
            tbMoTa.Text = "";
            tbSoLuong.Text = "";
            tbGiaTien.Text = "";
            tbQuanLy.Text = "";
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
            string query = "select MaVatDung, TenVatDung, MoTa, SoLuong, Gia, MaQuanLy from VatDung";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            ds = new DataSet();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter.Fill(ds, "tblVatDung");
            sqlCon.Close();

            dgDanhSachVatDung.ItemsSource = ds.Tables["tblVatDung"].DefaultView;
        }
        private int vitri = -1;
        private void ClearTextBoxes()
        {
            tbGiaTien.Clear();
            tbMaVatDung.Clear();
            tbSoLuong.Clear();
            tbMoTa.Clear();
            tbQuanLy.Clear();
            tbVatDung.Clear();
            
        }
        private void dgDanhSachVatDung_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRow = dgDanhSachVatDung.SelectedItem as DataRowView;

            if (selectedRow == null) return;

            // Lấy dữ liệu từ DataRowView
            DataRow dataRow = selectedRow.Row;

            tbMaVatDung.Text = dataRow["MaVatDung"].ToString();
            tbVatDung.Text = dataRow["TenVatDung"].ToString();
            tbMoTa.Text = dataRow["MoTa"].ToString();
            tbSoLuong.Text = dataRow["SoLuong"].ToString();
            tbGiaTien.Text = dataRow["Gia"].ToString();
            tbQuanLy.Text = dataRow["MaQuanLy"].ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Tạo một DataRow mới nhưng không thêm ngay vào DataTable
                DataRow dataRow = ds.Tables["tblVatDung"].NewRow();
                dataRow["MaVatDung"] = tbMaVatDung.Text.Trim();
                dataRow["TenVatDung"] = tbVatDung.Text.Trim();
                dataRow["MoTa"] = tbMoTa.Text.Trim();
                dataRow["SoLuong"] = tbSoLuong.Text.Trim();
                dataRow["Gia"] = tbGiaTien.Text.Trim();
                dataRow["MaQuanLy"] = tbQuanLy.Text.Trim();

                // Tạm thời thêm DataRow (chỉ khi không có lỗi xảy ra)
                ds.Tables["tblVatDung"].Rows.Add(dataRow);

                // Cập nhật dữ liệu vào cơ sở dữ liệu
                int kq = adapter.Update(ds.Tables["tblVatDung"]);
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
                if (ds.Tables["tblVatDung"].Rows.Count > 0 && ds.Tables["tblVatDung"].Rows[ds.Tables["tblVatDung"].Rows.Count - 1].RowState == DataRowState.Added)
                {
                    ds.Tables["tblVatDung"].Rows[ds.Tables["tblVatDung"].Rows.Count - 1].Delete();
                }

                // Kiểm tra lỗi SQL dựa trên mã lỗi
                switch (ex.Number)
                {
                    case 2627: // Lỗi vi phạm PRIMARY KEY
                        MessageBox.Show("Khóa chính đã tồn tại trong cơ sở dữ liệu! Không thể thêm dữ liệu trùng lặp.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;

                    case 547: // Lỗi vi phạm FOREIGN KEY
                        MessageBox.Show("Dữ liệu không hợp lệ! Mã quản lý không tồn tại trong hệ thống.", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Error);
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
                if (ds.Tables["tblVatDung"].Rows.Count > 0 && ds.Tables["tblVatDung"].Rows[ds.Tables["tblVatDung"].Rows.Count - 1].RowState == DataRowState.Added)
                {
                    ds.Tables["tblVatDung"].Rows[ds.Tables["tblVatDung"].Rows.Count - 1].Delete();
                }
            }

        }

        private void btnCapNhat_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = dgDanhSachVatDung.SelectedItem as DataRowView;

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
                dataRow["MaVatDung"] = tbMaVatDung.Text.Trim();
                dataRow["TenVatDung"] = tbVatDung.Text.Trim();
                dataRow["MoTa"] = tbMoTa.Text.Trim();
                dataRow["SoLuong"] = tbSoLuong.Text.Trim();
                dataRow["Gia"] = tbGiaTien.Text.Trim();
                dataRow["MaQuanLy"] = tbQuanLy.Text.Trim();

                // Cập nhật thay đổi vào cơ sở dữ liệu
                int kq = adapter.Update(ds.Tables["tblVatDung"]);

                if (kq > 0)
                {
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Cập nhật giao diện DataGrid
                    dgDanhSachVatDung.ItemsSource = null;
                    dgDanhSachVatDung.ItemsSource = ds.Tables["tblVatDung"].DefaultView;

                    // Giữ lại dòng đã chọn
                    dgDanhSachVatDung.SelectedItem = selectedRow;
                }
                else
                {
                    MessageBox.Show("Cập nhật dữ liệu thất bại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (SqlException sqlEx)
            {
                // Kiểm tra lỗi khóa chính
                if (sqlEx.Number == 2627 || sqlEx.Number == 2601) // Vi phạm khóa chính
                {
                    MessageBox.Show("Mã vật dụng đã tồn tại. Vui lòng nhập mã khác!", "Lỗi khóa chính", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                // Kiểm tra lỗi khóa ngoại
                else if (sqlEx.Number == 547) // Vi phạm khóa ngoại
                {
                    MessageBox.Show("Thông tin quản lý không hợp lệ. Vui lòng kiểm tra lại!", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show($"Lỗi SQL: {sqlEx.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                ClearTextBoxes(); // Xóa các trường sau khi xử lý
            }
            HienThiDanhSach();

        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            // Lấy hàng được chọn từ DataGrid
            var selectedRow = dgDanhSachVatDung.SelectedItem as DataRowView;

            if (selectedRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Hiển thị hộp thoại xác nhận xóa
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa vật dụng này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Lấy DataRow từ DataRowView
                    DataRow dataRow = selectedRow.Row;

                    // Xóa dòng khỏi DataTable
                    dataRow.Delete();

                    // Cập nhật thay đổi vào cơ sở dữ liệu
                    int kq = adapter.Update(ds.Tables["tblVatDung"]);

                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Cập nhật lại giao diện DataGrid
                        dgDanhSachVatDung.ItemsSource = null;
                        dgDanhSachVatDung.ItemsSource = ds.Tables["tblVatDung"].DefaultView;

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
                // Kiểm tra lỗi SQL, ví dụ vi phạm khóa ngoại hoặc khóa chính
                if (ex.Number == 547) // Lỗi vi phạm khóa ngoại (foreign key)
                {
                    MessageBox.Show("Không thể xóa vật dụng này vì dữ liệu bị ràng buộc với các bảng khác.", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ex.Number == 2627) // Lỗi vi phạm khóa chính (primary key)
                {
                    MessageBox.Show("Không thể xóa vật dụng này vì có dữ liệu trùng lặp trong hệ thống.", "Lỗi khóa chính", MessageBoxButton.OK, MessageBoxImage.Error);
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
