using System.Windows;
using System.Windows.Input;
using Microsoft.Data.SqlClient;
using System.Data;
using QuanLyBenhVien.Repositories;

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
            // Kiểm tra mã hóa đơn có trống không
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

                        string queryInsertHoaDon = @"INSERT INTO HoaDon (MaHoaDon, TenHoaDon, MaBenhNhan, MaNhanVien, NgayLapHoaDon)
                                             VALUES (@MaHoaDon, @TenHoaDon, @MaBenhNhan, @MaNhanVien, @NgayLapHoaDon, GiaTien, TrangThai)";
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
            MessageBox.Show("Button Clicked!");
        }

        private void btnThem2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button Clicked!");
        }

        private void btnXoa2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button Clicked!");
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
