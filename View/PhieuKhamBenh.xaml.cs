using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using QuanLyBenhVien.Repositories;
using System.Windows.Input;

namespace QuanLyBenhVien.View
{
    public partial class PhieuKhamBenh : UserControl
    {
        private readonly RepositoryBase _userRepository;
        public PhieuKhamBenh()
        {
            _userRepository = new UserRepository();
            InitializeComponent();
            searchControl.Tmp = "Nhập mã phiếu khám hoặc mã bác sĩ";
            // Đăng ký sự kiện SearchButtonClicked
            searchControl.SearchButtonClicked += SearchControl_SearchButtonClicked;
            // Đăng ký sự kiện ClearButtonClicked cho nút X
            searchControl.ClearButtonClicked += SearchControl_ClearButtonClicked;

        }
        private void txtNgayKham_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            popupCalendarNgayKham.IsOpen = true; // Mở popup khi nhấn vào TextBox
            
        }

        private void calendarNgayKham_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (calendarNgayKham.SelectedDate.HasValue)
            {
                txtNgayKham.Text = calendarNgayKham.SelectedDate.Value.ToString("yyyy-MM-dd"); // Gán ngày với định dạng yyyy-MM-dd
                popupCalendarNgayKham.IsOpen = false; // Ẩn popup sau khi chọn ngày
            }
        }

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (popupCalendarNgayKham.IsOpen && !popupCalendarNgayKham.IsMouseOver)
            {
                popupCalendarNgayKham.IsOpen = false; // Ẩn popup khi nhấn bên ngoài
            }
        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Kiểm tra nếu lịch đang mở và người dùng nhấn phím bất kỳ
            if (popupCalendarNgayKham.IsOpen )
            {
                popupCalendarNgayKham.IsOpen = false; // Ẩn popup khi nhấn bên ngoài
            }
        }

        private void SearchControl_ClearButtonClicked(object sender, EventArgs e)
        {
            // Logic khi nút X được nhấn
            HienThiDanhSach();
            ClearFields();
        }
        private void SearchControl_SearchButtonClicked(object sender, string searchText)
        {

            // Lấy mã phiếu khám từ tham số searchText
            string maPhieuKham = searchText.Trim();

            if (string.IsNullOrEmpty(maPhieuKham))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                
                return;
            }

            // Câu lệnh SQL để tìm kiếm thông tin phiếu khám bệnh và chi tiết phiếu khám bệnh
            string query = @"
    SELECT 
        BN.Ten AS TEN_BENHNHAN, 
        NV.Ten AS TEN_BACSI, 
        MaPhieuKham, 
        PK.MaBenhNhan, 
        NgayKham, 
        LyDoKhamBenh, 
        KhamLamSang, 
        ChanDoan, 
        KetQuaKham, 
        DieuTri, 
        MaBacSi
    FROM 
        BenhNhan BN 
    JOIN 
        PhieuKhamBenh PK ON PK.MaBenhNhan = BN.MaBenhNhan
    JOIN 
        NhanVien NV ON NV.MaNhanVien = PK.MaBacSi
    WHERE 
        MaPhieuKham = @MaPhieuKham OR MaNhanVien = @MaPhieuKham" ;

            try
            {
                sqlCon = _userRepository.GetConnection();
                adapter = new SqlDataAdapter(query, sqlCon);
                adapter.SelectCommand.Parameters.AddWithValue("@MaPhieuKham", maPhieuKham);

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
                    txtMaPhieu.Text = row["MaPhieuKham"].ToString();
                    txtMaBenhNhan.Text = row["MaBenhNhan"].ToString();
                    txtBenhNhan.Text = row["TEN_BENHNHAN"].ToString();
                    txtBacSi.Text = row["TEN_BACSI"].ToString();
                    txtNgayKham.Text = Convert.ToDateTime(row["NgayKham"]).ToString("yyyy-MM-dd");
                    txtLyDoKham.Text = row["LyDoKhamBenh"].ToString();
                    txtKhamLamSan.Text = row["KhamLamSang"].ToString();
                    txtChuanDoan.Text = row["ChanDoan"].ToString();
                    txtKetQua.Text = row["KetQuaKham"].ToString(); // Dữ liệu ban đầu
                    txtDieuTri.Text = row["DieuTri"].ToString();
                    txtMaBacSi.Text = row["MaBacSi"].ToString();

                    // Hiển thị dữ liệu vào DataGrid
                    dgDanhSach.ItemsSource = dataTable.DefaultView;
                }
                else
                {
                    // Nếu có nhiều kết quả, chỉ hiển thị vào DataGrid
                    ClearFields(); // Xóa TextBox
                    dgDanhSach.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (sqlCon != null && sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
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


        private void ClearFields()
        {
            txtMaPhieu.Text = "";
            txtMaBenhNhan.Text = "";
            txtNgayKham.Text = "";
            txtLyDoKham.Text = "";
            txtKhamLamSan.Text = "";
            txtChuanDoan.Text = "";
            txtKetQua.Text = "";
            txtDieuTri.Text = "";
            txtMaBacSi.Text = "";
            txtBenhNhan.Text = "";
            txtBacSi.Text = "";
        }

        private void HienThiDanhSach()
        {
            if (sqlCon == null)
            {
                sqlCon = _userRepository.GetConnection();
            }

            // Câu lệnh SELECT cho bảng PhieuKhamBenh
            string selectQuery = @"
    SELECT 
        BN.Ten AS TEN_BENHNHAN, 
        NV.Ten AS TEN_BACSI, 
        MaPhieuKham, 
        PK.MaBenhNhan, 
        NgayKham, 
        LyDoKhamBenh, 
        KhamLamSang, 
        ChanDoan, 
        KetQuaKham, 
        DieuTri, 
        MaBacSi
    FROM 
        BenhNhan BN 
    JOIN 
        PhieuKhamBenh PK ON PK.MaBenhNhan = BN.MaBenhNhan
    JOIN 
        NhanVien NV ON NV.MaNhanVien = PK.MaBacSi";

            // Khởi tạo SqlDataAdapter
            adapter = new SqlDataAdapter(selectQuery, sqlCon);

            // Thêm lệnh INSERT
            adapter.InsertCommand = new SqlCommand(
                "INSERT INTO PhieuKhamBenh (MaPhieuKham, MaBenhNhan, NgayKham, LyDoKhamBenh, KhamLamSang, ChanDoan, KetQuaKham, DieuTri, MaBacSi) " +
                "VALUES (@MaPhieuKham, @MaBenhNhan, @NgayKham, @LyDoKhamBenh, @KhamLamSang, @ChanDoan, @KetQuaKham, @DieuTri, @MaBacSi)", sqlCon);
            adapter.InsertCommand.Parameters.Add("@MaPhieuKham", SqlDbType.NVarChar, 50, "MaPhieuKham");
            adapter.InsertCommand.Parameters.Add("@MaBenhNhan", SqlDbType.NVarChar, 50, "MaBenhNhan");
            adapter.InsertCommand.Parameters.Add("@NgayKham", SqlDbType.Date, 0, "NgayKham");
            adapter.InsertCommand.Parameters.Add("@LyDoKhamBenh", SqlDbType.NVarChar, 255, "LyDoKhamBenh");
            adapter.InsertCommand.Parameters.Add("@KhamLamSang", SqlDbType.NVarChar, 255, "KhamLamSang");
            adapter.InsertCommand.Parameters.Add("@ChanDoan", SqlDbType.NVarChar, 255, "ChanDoan");
            adapter.InsertCommand.Parameters.Add("@KetQuaKham", SqlDbType.NVarChar, 255, "KetQuaKham");
            adapter.InsertCommand.Parameters.Add("@DieuTri", SqlDbType.NVarChar, 255, "DieuTri");
            adapter.InsertCommand.Parameters.Add("@MaBacSi", SqlDbType.NVarChar, 50, "MaBacSi");

            // Thêm lệnh UPDATE
            adapter.UpdateCommand = new SqlCommand(
                "UPDATE PhieuKhamBenh " +
                "SET MaBenhNhan=@MaBenhNhan, NgayKham=@NgayKham, LyDoKhamBenh=@LyDoKhamBenh, KhamLamSang=@KhamLamSang, ChanDoan=@ChanDoan, " +
                "KetQuaKham=@KetQuaKham, DieuTri=@DieuTri, MaBacSi=@MaBacSi " +
                "WHERE MaPhieuKham=@MaPhieuKham", sqlCon);
            adapter.UpdateCommand.Parameters.Add("@MaPhieuKham", SqlDbType.NVarChar, 50, "MaPhieuKham");
            adapter.UpdateCommand.Parameters.Add("@MaBenhNhan", SqlDbType.NVarChar, 50, "MaBenhNhan");
            adapter.UpdateCommand.Parameters.Add("@NgayKham", SqlDbType.Date, 0, "NgayKham");
            adapter.UpdateCommand.Parameters.Add("@LyDoKhamBenh", SqlDbType.NVarChar, 255, "LyDoKhamBenh");
            adapter.UpdateCommand.Parameters.Add("@KhamLamSang", SqlDbType.NVarChar, 255, "KhamLamSang");
            adapter.UpdateCommand.Parameters.Add("@ChanDoan", SqlDbType.NVarChar, 255, "ChanDoan");
            adapter.UpdateCommand.Parameters.Add("@KetQuaKham", SqlDbType.NVarChar, 255, "KetQuaKham");
            adapter.UpdateCommand.Parameters.Add("@DieuTri", SqlDbType.NVarChar, 255, "DieuTri");
            adapter.UpdateCommand.Parameters.Add("@MaBacSi", SqlDbType.NVarChar, 50, "MaBacSi");

            // Thêm lệnh DELETE
            adapter.DeleteCommand = new SqlCommand(
                "DELETE FROM PhieuKhamBenh WHERE MaPhieuKham=@MaPhieuKham", sqlCon);
            adapter.DeleteCommand.Parameters.Add("@MaPhieuKham", SqlDbType.NVarChar, 50, "MaPhieuKham");

            ds = new DataSet();

            // Mở kết nối nếu chưa mở
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            // Đổ dữ liệu vào DataSet
            adapter.Fill(ds, "tblPhieuKhamBenh");

            // Đóng kết nối
            sqlCon.Close();

            // Gán nguồn dữ liệu cho DataGrid
            dgDanhSach.ItemsSource = ds.Tables["tblPhieuKhamBenh"].DefaultView;
        }


        private void dgDanhSach_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Lấy hàng được chọn từ DataGrid
            var selectedRow = dgDanhSach.SelectedItem as DataRowView;

            if (selectedRow == null) return;

            // Lấy dữ liệu từ DataRowView
            DataRow dataRow = selectedRow.Row;

            txtMaPhieu.Text = dataRow["MaPhieuKham"].ToString();
            txtMaBenhNhan.Text = dataRow["MaBenhNhan"].ToString();
            txtBenhNhan.Text = dataRow["TEN_BENHNHAN"].ToString();
            txtBacSi.Text = dataRow["TEN_BACSI"].ToString();
            txtNgayKham.Text = Convert.ToDateTime(dataRow["NgayKham"]).ToString("yyyy-MM-dd");
            txtLyDoKham.Text = dataRow["LyDoKhamBenh"].ToString();
            txtKhamLamSan.Text = dataRow["KhamLamSang"].ToString();
            txtChuanDoan.Text = dataRow["ChanDoan"].ToString();
            txtKetQua.Text = dataRow["KetQuaKham"].ToString();
            txtDieuTri.Text = dataRow["DieuTri"].ToString();
            txtMaBacSi.Text = dataRow["MaBacSi"].ToString();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Tạo một DataRow mới nhưng không thêm ngay vào DataTable
                DataRow dataRow = ds.Tables["tblPhieuKhamBenh"].NewRow();
                dataRow["MaPhieuKham"] = txtMaPhieu.Text.Trim();
                dataRow["TEN_BENHNHAN"] = txtBenhNhan.Text.Trim();
                dataRow["TEN_BACSI"] = txtBacSi.Text.Trim();
                dataRow["MaBenhNhan"] = txtBenhNhan.Text.Trim();

                // Xử lý định dạng ngày
                dataRow["NgayKham"] = DateTime.TryParse(txtNgayKham.Text.Trim(), out DateTime ngaySinh)
                    ? ngaySinh.ToString("yyyy-MM-dd")
                    : throw new FormatException("Invalid date format");

                dataRow["LyDoKhamBenh"] = txtLyDoKham.Text.Trim();
                dataRow["KhamLamSang"] = txtKhamLamSan.Text.Trim();
                dataRow["DieuTri"] = txtDieuTri.Text.Trim();
                dataRow["ChanDoan"] = txtChuanDoan.Text.Trim();
                dataRow["KetQuaKham"] = txtKetQua.Text.Trim();
                dataRow["MaBacSi"] = txtMaBacSi.Text.Trim();

                // Tạm thời thêm DataRow (chỉ khi không có lỗi xảy ra)
                ds.Tables["tblPhieuKhamBenh"].Rows.Add(dataRow);

                // Cập nhật dữ liệu vào cơ sở dữ liệu
                int kq = adapter.Update(ds.Tables["tblPhieuKhamBenh"]);
                if (kq > 0)
                {
                    MessageBox.Show("Thêm dữ liệu thành công!!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearFields();
                }
                else
                {
                    // Nếu không cập nhật được, xóa DataRow vừa thêm
                    ds.Tables["tblPhieuKhamBenh"].Rows.Remove(dataRow);
                    MessageBox.Show("Thêm dữ liệu thất bại!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (SqlException ex)
            {
                // Xóa DataRow vừa thêm nếu gặp lỗi SQL
                if (ds.Tables["tblPhieuKhamBenh"].Rows.Count > 0 && ds.Tables["tblPhieuKhamBenh"].Rows[ds.Tables["tblPhieuKhamBenh"].Rows.Count - 1].RowState == DataRowState.Added)
                {
                    ds.Tables["tblPhieuKhamBenh"].Rows[ds.Tables["tblPhieuKhamBenh"].Rows.Count - 1].Delete();
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
                if (ds.Tables["tblPhieuKhamBenh"].Rows.Count > 0 && ds.Tables["tblPhieuKhamBenh"].Rows[ds.Tables["tblPhieuKhamBenh"].Rows.Count - 1].RowState == DataRowState.Added)
                {
                    ds.Tables["tblPhieuKhamBenh"].Rows[ds.Tables["tblPhieuKhamBenh"].Rows.Count - 1].Delete();
                }
            }

        }

       // private int vitri = -1;
        private void btnCapNhat_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn dòng nào trong DataGrid chưa
            var selectedRow = dgDanhSach.SelectedItem as DataRowView;

            if (selectedRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để cập nhật!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Cập nhật thông tin vào cơ sở dữ liệu
                using (SqlConnection sqlCon = _userRepository.GetConnection())
                {
                    sqlCon.Open();

                    // Câu lệnh SQL cập nhật
                    string query = @"
                UPDATE PhieuKhamBenh 
                SET 
                    MaBenhNhan = @MaBenhNhan, 
                    NgayKham = @NgayKham, 
                    LyDoKhamBenh = @LyDoKhamBenh, 
                    KhamLamSang = @KhamLamSang, 
                    ChanDoan = @ChanDoan, 
                    KetQuaKham = @KetQuaKham, 
                    DieuTri = @DieuTri, 
                    MaBacSi = @MaBacSi
                WHERE MaPhieuKham = @MaPhieuKham";

                    SqlCommand cmd = new SqlCommand(query, sqlCon);
                    cmd.Parameters.AddWithValue("@MaPhieuKham", txtMaPhieu.Text.Trim());
                    cmd.Parameters.AddWithValue("@MaBenhNhan", txtMaBenhNhan.Text.Trim());

                    if (DateTime.TryParse(txtNgayKham.Text.Trim(), out DateTime ngayKham))
                        cmd.Parameters.AddWithValue("@NgayKham", ngayKham);
                    else
                        throw new FormatException("Ngày khám không đúng định dạng!");

                    cmd.Parameters.AddWithValue("@LyDoKhamBenh", txtLyDoKham.Text.Trim());
                    cmd.Parameters.AddWithValue("@KhamLamSang", txtKhamLamSan.Text.Trim());
                    cmd.Parameters.AddWithValue("@ChanDoan", txtChuanDoan.Text.Trim());
                    cmd.Parameters.AddWithValue("@KetQuaKham", txtKetQua.Text.Trim());
                    cmd.Parameters.AddWithValue("@DieuTri", txtDieuTri.Text.Trim());
                    cmd.Parameters.AddWithValue("@MaBacSi", txtMaBacSi.Text.Trim());

                    // Thực thi lệnh
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Đồng bộ lại dữ liệu trong DataGrid
                        RefreshDataGrid();
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show("Không có dòng nào được cập nhật!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
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
                    MessageBox.Show("Dữ liệu không hợp lệ! Mã bệnh nhân hoặc mã bác sĩ không tồn tại.", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Error);
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

        }
        private void RefreshDataGrid()
        {
            try
            {
                string query = @"
            SELECT 
                BN.Ten AS TEN_BENHNHAN, 
                NV.Ten AS TEN_BACSI, 
                MaPhieuKham, 
                PK.MaBenhNhan, 
                NgayKham, 
                LyDoKhamBenh, 
                KhamLamSang, 
                ChanDoan, 
                KetQuaKham, 
                DieuTri, 
                MaBacSi
            FROM 
                BenhNhan BN 
            JOIN 
                PhieuKhamBenh PK ON PK.MaBenhNhan = BN.MaBenhNhan
            JOIN 
                NhanVien NV ON NV.MaNhanVien = PK.MaBacSi";

                sqlCon = _userRepository.GetConnection();
                adapter = new SqlDataAdapter(query, sqlCon);
                ds = new DataSet();
                adapter.Fill(ds, "tblPhieuKhamBenh");

                // Cập nhật DataGrid
                dgDanhSach.ItemsSource = ds.Tables["tblPhieuKhamBenh"].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dòng được chọn từ DataGrid
            var selectedRow = dgDanhSach.SelectedItem as DataRowView;

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
                    int kq = adapter.Update(ds.Tables["tblPhieuKhamBenh"]);

                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Cập nhật lại giao diện DataGrid
                        dgDanhSach.ItemsSource = null;
                        dgDanhSach.ItemsSource = ds.Tables["tblPhieuKhamBenh"].DefaultView;

                        // Xóa dữ liệu trong TextBox
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
                    MessageBox.Show("Không thể xóa dữ liệu này vì dữ liệu bị ràng buộc với các bảng khác.", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ex.Number == 2627) // Lỗi vi phạm khóa chính (primary key)
                {
                    MessageBox.Show("Không thể xóa dữ liệu này vì có dữ liệu trùng lặp trong hệ thống.", "Lỗi khóa chính", MessageBoxButton.OK, MessageBoxImage.Error);
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
