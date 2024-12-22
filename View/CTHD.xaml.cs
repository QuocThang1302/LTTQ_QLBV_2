using System.Windows;
using System.Windows.Input;
using Microsoft.Data.SqlClient;
using System.Data;
using QuanLyBenhVien.Repositories;
using QuanLyBenhVien.Model;

namespace QuanLyBenhVien.View
{
    public partial class CTHD : Window
    {
        private readonly RepositoryBase _userRepository;
        public CTHD()
        {
            _userRepository = new UserRepository();
            InitializeComponent();
        }
        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtMaHoaDon.Text) || string.IsNullOrEmpty(txtMaDonThuoc.Text))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn và mã đơn thuốc!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string maDonThuoc = txtMaDonThuoc.Text.Trim();
            string maHoaDon = txtMaHoaDon.Text.Trim();
            string tenHoaDon = txtTenHoaDon.Text.Trim();
            string maBenhNhan = txtMaBenhNhan.Text.Trim();
            string maNhanVien = txtMaNhanVien.Text.Trim();
            string ngayLapHoaDon = txtNgayLap.Text.Trim();
            
            using (SqlConnection conn = _userRepository.GetConnection())
            {
                conn.Open();

                try
                {
                    // Kiểm tra HoaDon đã tồn tại chưa
                    string queryCheckHoaDon = "SELECT COUNT(*) FROM HoaDon WHERE MaHoaDon = @MaHoaDon";
                    SqlCommand cmdCheck = new SqlCommand(queryCheckHoaDon, conn);
                    cmdCheck.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    int count = (int)cmdCheck.ExecuteScalar();

                    if (count == 0) // Trường hợp 1: Thêm mới hóa đơn
                    {
                        if (string.IsNullOrEmpty(tenHoaDon) || string.IsNullOrEmpty(maBenhNhan) ||
                            string.IsNullOrEmpty(maNhanVien) || string.IsNullOrEmpty(ngayLapHoaDon))
                        {
                            MessageBox.Show("Vui lòng nhập đầy đủ thông tin cho hóa đơn mới!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        // Truy vấn tên bệnh nhân từ bảng BenhNhan
                        string getTenBenhNhanQuery = "SELECT Ho + ' ' + Ten AS TenBenhNhan FROM BenhNhan WHERE MaBenhNhan = @MaBenhNhan";
                        using (SqlConnection connBenhNhan = _userRepository.GetConnection())
                        {
                            connBenhNhan.Open();
                            using (SqlCommand getTenBenhNhanCmd = new SqlCommand(getTenBenhNhanQuery, connBenhNhan))
                            {
                                getTenBenhNhanCmd.Parameters.AddWithValue("@MaBenhNhan", txtMaBenhNhan.Text);
                                object benhNhanResult = getTenBenhNhanCmd.ExecuteScalar();
                                if (benhNhanResult != null)
                                {
                                    txtBenhNhan.Text = benhNhanResult.ToString();
                                }
                                else
                                {
                                    txtBenhNhan.Text = "Không tìm thấy!";
                                }
                            }
                        }

                        // Truy vấn tên bác sĩ từ bảng NhanVien
                        string getTenNhanVienQuery = "SELECT Ho + ' ' + Ten AS TenNhanVien FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
                        using (SqlConnection connNhanVien = _userRepository.GetConnection())
                        {
                            connNhanVien.Open();
                            using (SqlCommand getTenNhanVienCmd = new SqlCommand(getTenNhanVienQuery, connNhanVien))
                            {
                                getTenNhanVienCmd.Parameters.AddWithValue("@MaNhanVien", txtMaNhanVien.Text);
                                object nhanVienResult = getTenNhanVienCmd.ExecuteScalar();
                                if (nhanVienResult != null)
                                {
                                    txtNhanVien.Text = nhanVienResult.ToString();
                                }
                                else
                                {
                                    txtNhanVien.Text = "Không tìm thấy!";
                                }
                            }
                        }

                        string queryInsertHoaDon = @"INSERT INTO HoaDon (MaHoaDon, TenHoaDon, MaBenhNhan, MaNhanVien, NgayLapHoaDon, GiaTien, TrangThai)
                                             VALUES (@MaHoaDon, @TenHoaDon, @MaBenhNhan, @MaNhanVien, @NgayLapHoaDon, @GiaTien, @TrangThai)";
                        SqlCommand cmdInsert = new SqlCommand(queryInsertHoaDon, conn);
                        cmdInsert.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                        cmdInsert.Parameters.AddWithValue("@TenHoaDon", tenHoaDon);
                        cmdInsert.Parameters.AddWithValue("@MaBenhNhan", maBenhNhan);
                        cmdInsert.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                        cmdInsert.Parameters.AddWithValue("@NgayLapHoaDon", ngayLapHoaDon);
                        cmdInsert.Parameters.AddWithValue("@GiaTien", 0);
                        cmdInsert.Parameters.AddWithValue("@TrangThai", "Chưa thanh toán");

                        cmdInsert.ExecuteNonQuery();
                        MessageBox.Show("Thêm hóa đơn mới thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else // Trường hợp 2: Đã tồn tại
                    {
                        // Kiểm tra trạng thái của hóa đơn
                        string queryCheckTrangThai = @"SELECT TrangThai FROM HoaDon WHERE MaHoaDon = @MaHoaDon";
                        using (SqlCommand cmdCheckTrangThai = new SqlCommand(queryCheckTrangThai, conn))
                        {
                            cmdCheckTrangThai.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                            object trangThaiResult = cmdCheckTrangThai.ExecuteScalar(); // Lấy giá trị của cột TrangThai

                            if (trangThaiResult != null && trangThaiResult.ToString() == "Đã thanh toán")
                            {
                                MessageBox.Show("Hóa đơn đã được thanh toán, không thể thay đổi thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return; // Dừng xử lý nếu hóa đơn đã thanh toán
                            }
                        }
                        // Truy vấn thông tin hóa đơn đã tồn tại
                        string queryGetHoaDon = @"SELECT TenHoaDon, MaBenhNhan, MaNhanVien, NgayLapHoaDon 
                              FROM HoaDon 
                              WHERE MaHoaDon = @MaHoaDon";
                        using (SqlCommand cmdGetHoaDon = new SqlCommand(queryGetHoaDon, conn))
                        {
                            cmdGetHoaDon.Parameters.AddWithValue("@MaHoaDon", maHoaDon);

                            using (SqlDataReader reader = cmdGetHoaDon.ExecuteReader())
                            {
                                if (reader.Read()) // Đọc dữ liệu từ hóa đơn đã tồn tại
                                {
                                    // Điền thông tin vào các textBox
                                    txtTenHoaDon.Text = reader["TenHoaDon"].ToString();
                                    txtMaBenhNhan.Text = reader["MaBenhNhan"].ToString();
                                    txtMaNhanVien.Text = reader["MaNhanVien"].ToString();
                                    txtNgayLap.Text = Convert.ToDateTime(reader["NgayLapHoaDon"]).ToString("yyyy-MM-dd");
                                    // Truy vấn tên bệnh nhân từ bảng BenhNhan
                                    string getTenBenhNhanQuery = "SELECT Ho + ' ' + Ten AS TenBenhNhan FROM BenhNhan WHERE MaBenhNhan = @MaBenhNhan";
                                    using (SqlConnection connBenhNhan = _userRepository.GetConnection())
                                    {
                                        connBenhNhan.Open();
                                        using (SqlCommand getTenBenhNhanCmd = new SqlCommand(getTenBenhNhanQuery, connBenhNhan))
                                        {
                                            getTenBenhNhanCmd.Parameters.AddWithValue("@MaBenhNhan", txtMaBenhNhan.Text);
                                            object benhNhanResult = getTenBenhNhanCmd.ExecuteScalar();
                                            if (benhNhanResult != null)
                                            {
                                                txtBenhNhan.Text = benhNhanResult.ToString();
                                            }
                                            else
                                            {
                                                txtBenhNhan.Text = "Không tìm thấy!";
                                            }
                                        }
                                    }

                                    // Truy vấn tên bác sĩ từ bảng NhanVien
                                    string getTenNhanVienQuery = "SELECT Ho + ' ' + Ten AS TenNhanVien FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
                                    using (SqlConnection connNhanVien = _userRepository.GetConnection())
                                    {
                                        connNhanVien.Open();
                                        using (SqlCommand getTenNhanVienCmd = new SqlCommand(getTenNhanVienQuery, connNhanVien))
                                        {
                                            getTenNhanVienCmd.Parameters.AddWithValue("@MaNhanVien", txtMaNhanVien.Text);
                                            object nhanVienResult = getTenNhanVienCmd.ExecuteScalar();
                                            if (nhanVienResult != null)
                                            {
                                                txtNhanVien.Text = nhanVienResult.ToString();
                                            }
                                            else
                                            {
                                                txtNhanVien.Text = "Không tìm thấy!";
                                            }
                                        }
                                    }

                                    MessageBox.Show("Mã hóa đơn đã tồn tại. Thông tin đã được hiển thị.",
                                                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Không thể lấy thông tin hóa đơn đã tồn tại!",
                                                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                    }

                    // Cập nhật DonThuoc
                    string queryUpdateDonThuoc = "UPDATE DonThuoc SET MaHoaDon = @MaHoaDon WHERE MaDonThuoc = @MaDonThuoc AND MaHoaDon IS NULL";
                    SqlCommand cmdUpdate = new SqlCommand(queryUpdateDonThuoc, conn);
                    cmdUpdate.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    cmdUpdate.Parameters.AddWithValue("@MaDonThuoc", maDonThuoc);

                    int rowsAffected = cmdUpdate.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật đơn thuốc thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Hiển thị ngày lập đơn thuốc
                        string queryGetNgayLapDon = "SELECT NgayLapDon FROM DonThuoc WHERE MaDonThuoc = @MaDonThuoc";
                        SqlCommand cmdGetNgay = new SqlCommand(queryGetNgayLapDon, conn);
                        cmdGetNgay.Parameters.AddWithValue("@MaDonThuoc", maDonThuoc);
                        object result = cmdGetNgay.ExecuteScalar(); // Lấy giá trị NgayLapDon

                        // Kiểm tra nếu có giá trị trả về
                        if (result != null)
                        {
                            txtNgayLapDon.Text = result.ToString(); // Gán giá trị vào TextBox
                        }
                        else
                        {
                            txtNgayLapDon.Text = "Không có ngày lập đơn thuốc"; // Trường hợp không tìm thấy ngày lập đơn
                        }

                    }
                    else
                    {
                        MessageBox.Show("Đơn thuốc đã có mã hóa đơn hoặc không tồn tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        // Hiển thị ngày lập đơn thuốc
                        string queryGetNgayLapDon = "SELECT NgayLapDon FROM DonThuoc WHERE MaDonThuoc = @MaDonThuoc";
                        SqlCommand cmdGetNgay = new SqlCommand(queryGetNgayLapDon, conn);
                        cmdGetNgay.Parameters.AddWithValue("@MaDonThuoc", maDonThuoc);
                        object result = cmdGetNgay.ExecuteScalar(); // Lấy giá trị NgayLapDon

                        // Kiểm tra nếu có giá trị trả về
                        if (result != null)
                        {
                            txtNgayLapDon.Text = result.ToString(); // Gán giá trị vào TextBox
                        }
                        else
                        {
                            txtNgayLapDon.Text = "Không có ngày lập đơn thuốc"; // Trường hợp không tìm thấy ngày lập đơn
                        }
                    }

                    // Load dữ liệu vào DataGrid
                    string queryLoadData = @"SELECT Thuoc.TenThuoc, Thuoc.SoLuong, Thuoc.GiaTien AS DonGia, 
                                    (CTDonThuoc.SoLuong * Thuoc.GiaTien) AS ThanhTien
                                    FROM CTDonThuoc
                                    INNER JOIN Thuoc ON CTDonThuoc.MaThuoc = Thuoc.MaThuoc
                                    WHERE CTDonThuoc.MaDonThuoc = @MaDonThuoc";

                    SqlDataAdapter adapter = new SqlDataAdapter(queryLoadData, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@MaDonThuoc", maDonThuoc);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgThuoc.ItemsSource = dt.DefaultView;
                }
                catch (SqlException ex)
                {
                    // Xử lý lỗi khóa chính và khóa ngoại
                    if (ex.Number == 2627) // Lỗi khóa chính
                    {
                        MessageBox.Show("Lỗi: Dữ liệu đã tồn tại. Vui lòng kiểm tra lại mã hóa đơn hoặc đơn thuốc!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (ex.Number == 547) // Lỗi khóa ngoại
                    {
                        MessageBox.Show("Lỗi: Vi phạm ràng buộc khóa ngoại. Vui lòng kiểm tra dữ liệu liên quan!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Lỗi SQL: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }



        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            string maDonThuoc = txtMaDonThuoc.Text.Trim();

            // Kiểm tra textbox trống
            if (string.IsNullOrEmpty(maDonThuoc))
            {
                MessageBox.Show("Vui lòng nhập Mã đơn thuốc để xóa!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = _userRepository.GetConnection())
                {
                    conn.Open();


                    // 1. Lấy MaHoaDon hiện tại của DonThuoc
                    string getMaHoaDonQuery = "SELECT MaHoaDon FROM DonThuoc WHERE MaDonThuoc = @MaDonThuoc";
                    string maHoaDon = null;

                    using (SqlCommand getCmd = new SqlCommand(getMaHoaDonQuery, conn))
                    {
                        getCmd.Parameters.AddWithValue("@MaDonThuoc", maDonThuoc);
                        var result = getCmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            maHoaDon = result.ToString();
                            // Kiểm tra trạng thái của hóa đơn
                            string queryCheckTrangThai = @"SELECT TrangThai FROM HoaDon WHERE MaHoaDon = @MaHoaDon";
                            using (SqlCommand cmdCheckTrangThai = new SqlCommand(queryCheckTrangThai, conn))
                            {
                                cmdCheckTrangThai.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                                object trangThaiResult = cmdCheckTrangThai.ExecuteScalar(); // Lấy giá trị của cột TrangThai

                                if (trangThaiResult != null && trangThaiResult.ToString() == "Đã thanh toán")
                                {
                                    MessageBox.Show("Hóa đơn của đơn thuốc đã được thanh toán, không thể thay đổi thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    return; // Dừng xử lý nếu hóa đơn đã thanh toán
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Không tìm thấy đơn thuốc với mã \"{maDonThuoc}\"!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }

                    // 2. Hiển thị hộp thoại xác nhận
                    MessageBoxResult confirmResult = MessageBox.Show(
                        $"Bạn có chắc muốn xóa đơn thuốc \"{maDonThuoc}\" khỏi hóa đơn \"{maHoaDon}\"?",
                        "Xác nhận xóa",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (confirmResult == MessageBoxResult.Yes)
                    {
                        // 3. Cập nhật MaHoaDon thành NULL
                        string updateQuery = "UPDATE DonThuoc SET MaHoaDon = NULL WHERE MaDonThuoc = @MaDonThuoc";
                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@MaDonThuoc", maDonThuoc);
                            int rowsAffected = updateCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show($"Đơn thuốc \"{maDonThuoc}\" đã được xóa khỏi hóa đơn \"{maHoaDon}\" thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show($"Có lỗi khi xóa đơn thuốc \"{maDonThuoc}\"!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnThem2_Click(object sender, RoutedEventArgs e)
        {
            
            string tenHoaDon = txtTenHoaDon.Text.Trim();
            string maBenhNhan = txtMaBenhNhan.Text.Trim();
            string maNhanVien = txtMaNhanVien.Text.Trim();
            string ngayLapHoaDon = txtNgayLap.Text.Trim();
            if (string.IsNullOrEmpty(txtMaHoaDon.Text) ||
                string.IsNullOrEmpty(txtMaVatDung.Text) ||
                string.IsNullOrEmpty(txtSoLuong2.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã hóa đơn, Mã vật dụng và Số lượng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string maHoaDon = txtMaHoaDon.Text.Trim();
            string maVatDung = txtMaVatDung.Text.Trim();
            int soLuongNhap;

            if (!int.TryParse(txtSoLuong2.Text.Trim(), out soLuongNhap) || soLuongNhap <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên dương!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            using (SqlConnection conn = _userRepository.GetConnection())
            {
                conn.Open();
                

                try
                {
                    // Kiểm tra tồn tại Mã hóa đơn
                    string queryCheckHoaDon = "SELECT COUNT(*) FROM HoaDon WHERE MaHoaDon = @MaHoaDon";
                    SqlCommand cmdCheckHoaDon = new SqlCommand(queryCheckHoaDon, conn);
                    cmdCheckHoaDon.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    int countHoaDon = (int)cmdCheckHoaDon.ExecuteScalar();

                    if (countHoaDon == 0)
                    {
                        if (string.IsNullOrEmpty(tenHoaDon) || string.IsNullOrEmpty(maBenhNhan) ||
                            string.IsNullOrEmpty(maNhanVien) || string.IsNullOrEmpty(ngayLapHoaDon))
                        {
                            MessageBox.Show("Vui lòng nhập đầy đủ thông tin cho hóa đơn mới!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        // Truy vấn tên bệnh nhân từ bảng BenhNhan
                        string getTenBenhNhanQuery = "SELECT Ho + ' ' + Ten AS TenBenhNhan FROM BenhNhan WHERE MaBenhNhan = @MaBenhNhan";
                        using (SqlConnection connBenhNhan = _userRepository.GetConnection())
                        {
                            connBenhNhan.Open();
                            using (SqlCommand getTenBenhNhanCmd = new SqlCommand(getTenBenhNhanQuery, connBenhNhan))
                            {
                                getTenBenhNhanCmd.Parameters.AddWithValue("@MaBenhNhan", txtMaBenhNhan.Text);
                                object benhNhanResult = getTenBenhNhanCmd.ExecuteScalar();
                                if (benhNhanResult != null)
                                {
                                    txtBenhNhan.Text = benhNhanResult.ToString();
                                }
                                else
                                {
                                    txtBenhNhan.Text = "Không tìm thấy!";
                                }
                            }
                        }

                        // Truy vấn tên bác sĩ từ bảng NhanVien
                        string getTenNhanVienQuery = "SELECT Ho + ' ' + Ten AS TenNhanVien FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
                        using (SqlConnection connNhanVien = _userRepository.GetConnection())
                        {
                            connNhanVien.Open();
                            using (SqlCommand getTenNhanVienCmd = new SqlCommand(getTenNhanVienQuery, connNhanVien))
                            {
                                getTenNhanVienCmd.Parameters.AddWithValue("@MaNhanVien", txtMaNhanVien.Text);
                                object nhanVienResult = getTenNhanVienCmd.ExecuteScalar();
                                if (nhanVienResult != null)
                                {
                                    txtNhanVien.Text = nhanVienResult.ToString();
                                }
                                else
                                {
                                    txtNhanVien.Text = "Không tìm thấy!";
                                }
                            }
                        }

                        string queryInsertHoaDon = @"INSERT INTO HoaDon (MaHoaDon, TenHoaDon, MaBenhNhan, MaNhanVien, NgayLapHoaDon)
                                             VALUES (@MaHoaDon, @TenHoaDon, @MaBenhNhan, @MaNhanVien, @NgayLapHoaDon, GiaTien, TrangThai)";
                        SqlCommand cmdInsert2 = new SqlCommand(queryInsertHoaDon, conn);
                        cmdInsert2.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                        cmdInsert2.Parameters.AddWithValue("@TenHoaDon", tenHoaDon);
                        cmdInsert2.Parameters.AddWithValue("@MaBenhNhan", maBenhNhan);
                        cmdInsert2.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                        cmdInsert2.Parameters.AddWithValue("@NgayLapHoaDon", ngayLapHoaDon);
                        cmdInsert2.Parameters.AddWithValue("@GiaTien", 0);
                        cmdInsert2.Parameters.AddWithValue("@TrangThai", "Chưa thanh toán");

                        cmdInsert2.ExecuteNonQuery();
                        MessageBox.Show("Thêm hóa đơn mới thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        // Kiểm tra trạng thái của hóa đơn
                        string queryCheckTrangThai = @"SELECT TrangThai FROM HoaDon WHERE MaHoaDon = @MaHoaDon";
                        using (SqlCommand cmdCheckTrangThai = new SqlCommand(queryCheckTrangThai, conn))
                        {
                            cmdCheckTrangThai.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                            object trangThaiResult = cmdCheckTrangThai.ExecuteScalar(); // Lấy giá trị của cột TrangThai

                            if (trangThaiResult != null && trangThaiResult.ToString() == "Đã thanh toán")
                            {
                                MessageBox.Show("Hóa đơn đã được thanh toán, không thể thay đổi thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return; // Dừng xử lý nếu hóa đơn đã thanh toán
                            }
                        }
                        // Truy vấn thông tin hóa đơn đã tồn tại
                        string queryGetHoaDon = @"SELECT TenHoaDon, MaBenhNhan, MaNhanVien, NgayLapHoaDon 
                              FROM HoaDon 
                              WHERE MaHoaDon = @MaHoaDon";
                        using (SqlCommand cmdGetHoaDon = new SqlCommand(queryGetHoaDon, conn))
                        {
                            cmdGetHoaDon.Parameters.AddWithValue("@MaHoaDon", maHoaDon);

                            using (SqlDataReader reader2 = cmdGetHoaDon.ExecuteReader())
                            {
                                if (reader2.Read()) // Đọc dữ liệu từ hóa đơn đã tồn tại
                                {
                                    // Điền thông tin vào các textBox
                                    txtTenHoaDon.Text = reader2["TenHoaDon"].ToString();
                                    txtMaBenhNhan.Text = reader2["MaBenhNhan"].ToString();
                                    txtMaNhanVien.Text = reader2["MaNhanVien"].ToString();
                                    txtNgayLap.Text = Convert.ToDateTime(reader2["NgayLapHoaDon"]).ToString("yyyy-MM-dd");
                                    // Truy vấn tên bệnh nhân từ bảng BenhNhan
                                    string getTenBenhNhanQuery = "SELECT Ho + ' ' + Ten AS TenBenhNhan FROM BenhNhan WHERE MaBenhNhan = @MaBenhNhan";
                                    using (SqlConnection connBenhNhan = _userRepository.GetConnection())
                                    {
                                        connBenhNhan.Open();
                                        using (SqlCommand getTenBenhNhanCmd = new SqlCommand(getTenBenhNhanQuery, connBenhNhan))
                                        {
                                            getTenBenhNhanCmd.Parameters.AddWithValue("@MaBenhNhan", txtMaBenhNhan.Text);
                                            object benhNhanResult = getTenBenhNhanCmd.ExecuteScalar();
                                            if (benhNhanResult != null)
                                            {
                                                txtBenhNhan.Text = benhNhanResult.ToString();
                                            }
                                            else
                                            {
                                                txtBenhNhan.Text = "Không tìm thấy!";
                                            }
                                        }
                                    }

                                    // Truy vấn tên bác sĩ từ bảng NhanVien
                                    string getTenNhanVienQuery = "SELECT Ho + ' ' + Ten AS TenNhanVien FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
                                    using (SqlConnection connNhanVien = _userRepository.GetConnection())
                                    {
                                        connNhanVien.Open();
                                        using (SqlCommand getTenNhanVienCmd = new SqlCommand(getTenNhanVienQuery, connNhanVien))
                                        {
                                            getTenNhanVienCmd.Parameters.AddWithValue("@MaNhanVien", txtMaNhanVien.Text);
                                            object nhanVienResult = getTenNhanVienCmd.ExecuteScalar();
                                            if (nhanVienResult != null)
                                            {
                                                txtNhanVien.Text = nhanVienResult.ToString();
                                            }
                                            else
                                            {
                                                txtNhanVien.Text = "Không tìm thấy!";
                                            }
                                        }
                                    }

                                    MessageBox.Show("Mã hóa đơn đã tồn tại. Thông tin đã được hiển thị.",
                                                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Không thể lấy thông tin hóa đơn đã tồn tại!",
                                                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                    }
                    // Lấy thông tin VatDung
                    string queryGetVatDung = "SELECT Gia, SoLuong FROM VatDung WHERE MaVatDung = @MaVatDung";
                    SqlCommand cmdGetVatDung = new SqlCommand(queryGetVatDung, conn);
                    cmdGetVatDung.Parameters.AddWithValue("@MaVatDung", maVatDung);
                    SqlDataReader reader = cmdGetVatDung.ExecuteReader();
                    if (!reader.Read())
                    {
                        MessageBox.Show("Mã vật dụng không tồn tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        reader.Close();
                        
                        return;
                    }

                    decimal gia = Convert.ToDecimal(reader["Gia"]);
                    int soLuongTonKho = Convert.ToInt32(reader["SoLuong"]);
                    reader.Close();

                    if (soLuongNhap > soLuongTonKho)
                    {
                        MessageBox.Show("Số lượng vật dụng trong kho không đủ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        
                        return;
                    }

                    decimal thanhTien = gia * soLuongNhap;
                    // Thêm vào bảng CTHDVatDung
                    string queryInsertCTHD = @"INSERT INTO CTHDVatDung (MaHoaDon, MaVatDung, SoLuong, ThanhTien) 
                                       VALUES (@MaHoaDon, @MaVatDung, @SoLuong, @ThanhTien)";
                    SqlCommand cmdInsert = new SqlCommand(queryInsertCTHD, conn);
                    cmdInsert.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    cmdInsert.Parameters.AddWithValue("@MaVatDung", maVatDung);
                    cmdInsert.Parameters.AddWithValue("@SoLuong", soLuongNhap);
                    cmdInsert.Parameters.AddWithValue("@ThanhTien", thanhTien);
                    cmdInsert.ExecuteNonQuery();

                    // Cập nhật số lượng trong kho
                    string queryUpdateVatDung = @"UPDATE VatDung SET SoLuong = SoLuong - @SoLuongNhap 
                                          WHERE MaVatDung = @MaVatDung";
                    SqlCommand cmdUpdateVatDung = new SqlCommand(queryUpdateVatDung, conn);
                    cmdUpdateVatDung.Parameters.AddWithValue("@SoLuongNhap", soLuongNhap);
                    cmdUpdateVatDung.Parameters.AddWithValue("@MaVatDung", maVatDung);
                    cmdUpdateVatDung.ExecuteNonQuery();

                    

                    MessageBox.Show("Thêm vật dụng vào hóa đơn thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Load dữ liệu vào DataGrid
                    string queryLoadData = @"
    SELECT VatDung.TenVatDung, VatDung.SoLuong, VatDung.Gia AS DonGia, 
           (VatDung.SoLuong * VatDung.Gia) AS ThanhTien
    FROM VatDung
    WHERE VatDung.MaVatDung = @MaVatDung";

                    SqlDataAdapter adapter = new SqlDataAdapter(queryLoadData, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@MaVatDung", maVatDung);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Thiết lập lại ItemsSource của DataGrid
                    dgVatDung.ItemsSource = dt.DefaultView;

                }
                catch (SqlException ex)
                {
                    
                    if (ex.Number == 2627) // Lỗi vi phạm khóa chính
                    {
                        MessageBox.Show("Lỗi khóa chính: Bản ghi đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (ex.Number == 547) // Lỗi khóa ngoại
                    {
                        MessageBox.Show("Lỗi khóa ngoại: Dữ liệu tham chiếu không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Lỗi SQL: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnXoa2_Click(object sender, RoutedEventArgs e)
        {
            string maVatDung = txtMaVatDung.Text.Trim();
            string maHoaDon = txtMaHoaDon.Text.Trim();

            if (string.IsNullOrEmpty(maVatDung) || string.IsNullOrEmpty(maHoaDon))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin Mã Vật Dụng và Mã Hóa Đơn.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Kết nối đến cơ sở dữ liệu
                using (SqlConnection connection = _userRepository.GetConnection())
                {
                    connection.Open();
                    // Kiểm tra trạng thái của hóa đơn
                    string queryCheckTrangThai = @"SELECT TrangThai FROM HoaDon WHERE MaHoaDon = @MaHoaDon";
                    using (SqlCommand cmdCheckTrangThai = new SqlCommand(queryCheckTrangThai, connection))
                    {
                        cmdCheckTrangThai.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                        object trangThaiResult = cmdCheckTrangThai.ExecuteScalar(); // Lấy giá trị của cột TrangThai

                        if (trangThaiResult != null && trangThaiResult.ToString() == "Đã thanh toán")
                        {
                            MessageBox.Show("Hóa đơn đã được thanh toán, không thể thay đổi thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return; // Dừng xử lý nếu hóa đơn đã thanh toán
                        }
                    }

                    // Kiểm tra xem có bản ghi nào phù hợp không
                    string checkQuery = "SELECT COUNT(*) FROM CTHDVatDung WHERE MaHoaDon = @MaHoaDon AND MaVatDung = @MaVatDung";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                        checkCommand.Parameters.AddWithValue("@MaVatDung", maVatDung);

                        int count = (int)checkCommand.ExecuteScalar();

                        if (count == 0)
                        {
                            MessageBox.Show("Không tìm thấy thông tin phù hợp để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }

                    // Thực hiện xóa
                    string deleteQuery = "DELETE FROM CTHDVatDung WHERE MaHoaDon = @MaHoaDon AND MaVatDung = @MaVatDung";

                    using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                        deleteCommand.Parameters.AddWithValue("@MaVatDung", maVatDung);

                        int rowsAffected = deleteCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Xóa không thành công. Vui lòng thử lại.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnThanhToan_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button Clicked!");
        }

        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
