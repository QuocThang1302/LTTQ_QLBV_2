using System.Windows;
using System.Windows.Input;
using Microsoft.Data.SqlClient;
using System.Data;
using QuanLyBenhVien.Repositories;
using System.Windows.Controls;


namespace QuanLyBenhVien.View
{
    public partial class DonThuoc_CTDT : Window
    {
        private readonly RepositoryBase _userRepository;
        public Action OnDataAdded;
        public DonThuoc_CTDT()
        {
            _userRepository = new UserRepository();
            InitializeComponent();
        }
        private void TxB_NgayLapDon_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            popupCalendarNgayLapDon.IsOpen = true; // Mở popup khi nhấn vào TextBox
            e.Handled = true; // Ngăn sự kiện lan sang Window_PreviewMouseDown
        }

        private void calendarNgayLapDon_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (calendarNgayLapDon.SelectedDate.HasValue)
            {
                TxB_NgayLapDon.Text = calendarNgayLapDon.SelectedDate.Value.ToString("yyyy-MM-dd");
                popupCalendarNgayLapDon.IsOpen = false; // Ẩn popup sau khi chọn ngày
            }
        }

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (popupCalendarNgayLapDon.IsOpen && !popupCalendarNgayLapDon.IsMouseOver)
            {
                popupCalendarNgayLapDon.IsOpen = false; // Ẩn popup nếu nhấn ra ngoài
            }
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OnDataAdded?.Invoke();
            Close();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra các ô không được trống
            if (string.IsNullOrWhiteSpace(TxB_MaDonThuoc.Text) || string.IsNullOrWhiteSpace(TxB_TenThuoc.Text) ||
                string.IsNullOrWhiteSpace(TxB_SoLuong.Text) || string.IsNullOrWhiteSpace(TxB_HuongDan.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ dữ liệu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            
            using (SqlConnection conn = _userRepository.GetConnection())
            {
                // Kiểm tra các ô không được trống
                if (string.IsNullOrWhiteSpace(TxB_MaDonThuoc.Text) || string.IsNullOrWhiteSpace(TxB_TenThuoc.Text) ||
                string.IsNullOrWhiteSpace(TxB_SoLuong.Text) || string.IsNullOrWhiteSpace(TxB_HuongDan.Text))
                {
                    MessageBox.Show("Mã đơn thuốc và Tên thuốc không được để trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                    try
                    {
                        conn.Open();
                        string maDonThuoc = TxB_MaDonThuoc.Text;
                        string maBenhNhan = TxB_MaBenhNhan.Text;
                        string maBacSi = TxB_MaBacSi.Text;
                        string ngayLapDon = TxB_NgayLapDon.Text;
                        string tenThuoc = TxB_TenThuoc.Text;
                        int soLuong = int.Parse(TxB_SoLuong.Text);
                        string huongDan = TxB_HuongDan.Text;

                        // Kiểm tra MaDonThuoc đã tồn tại hay chưa
                        string checkDonThuocQuery = "SELECT COUNT(*) FROM DonThuoc WHERE MaDonThuoc = @MaDonThuoc";
                        SqlCommand checkCmd = new SqlCommand(checkDonThuocQuery, conn);
                        checkCmd.Parameters.AddWithValue("@MaDonThuoc", maDonThuoc);
                        int donThuocExists = (int)checkCmd.ExecuteScalar();

                        if (donThuocExists == 0) // Trường hợp 1: Chưa tồn tại DonThuoc
                        {
                            // Kiểm tra đủ thông tin bắt buộc
                            if (string.IsNullOrWhiteSpace(maBenhNhan) || string.IsNullOrWhiteSpace(maBacSi) || string.IsNullOrWhiteSpace(ngayLapDon))
                            {
                                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                            // Thêm DonThuoc mới
                            string insertDonThuocQuery = @"INSERT INTO DonThuoc (MaDonThuoc, MaBenhNhan, MaBacSi, NgayLapDon, MaHoaDon) 
                                               VALUES (@MaDonThuoc, @MaBenhNhan, @MaBacSi, @NgayLapDon, @MaHoaDon)";
                            SqlCommand insertDonThuocCmd = new SqlCommand(insertDonThuocQuery, conn);
                            insertDonThuocCmd.Parameters.AddWithValue("@MaDonThuoc", maDonThuoc);
                            insertDonThuocCmd.Parameters.AddWithValue("@MaBenhNhan", maBenhNhan);
                            insertDonThuocCmd.Parameters.AddWithValue("@MaBacSi", maBacSi);
                            insertDonThuocCmd.Parameters.AddWithValue("@NgayLapDon", ngayLapDon);
                            insertDonThuocCmd.Parameters.AddWithValue("@MaHoaDon", DBNull.Value);
                            insertDonThuocCmd.ExecuteNonQuery();
                        }
                        else // Trường hợp 2: MaDonThuoc đã tồn tại
                        {
                            string updateFieldsQuery = @"SELECT MaBenhNhan, MaBacSi, NgayLapDon 
                                             FROM DonThuoc WHERE MaDonThuoc = @MaDonThuoc";
                            SqlCommand getFieldsCmd = new SqlCommand(updateFieldsQuery, conn);
                            getFieldsCmd.Parameters.AddWithValue("@MaDonThuoc", maDonThuoc);
                            SqlDataReader reader = getFieldsCmd.ExecuteReader();

                            if (reader.Read())
                            {
                                TxB_MaBenhNhan.Text = reader["MaBenhNhan"].ToString();
                                TxB_MaBacSi.Text = reader["MaBacSi"].ToString();
                                TxB_NgayLapDon.Text = reader["NgayLapDon"].ToString();
                            }
                            reader.Close();
                        }

                        // Xử lý chi tiết đơn thuốc (CTDonThuoc)
                        // Truy MaThuoc từ TenThuoc
                        string getMaThuocQuery = "SELECT MaThuoc, GiaTien, SoLuong FROM Thuoc WHERE TenThuoc = @TenThuoc";
                        SqlCommand getMaThuocCmd = new SqlCommand(getMaThuocQuery, conn);
                        getMaThuocCmd.Parameters.AddWithValue("@TenThuoc", tenThuoc);
                        SqlDataReader thuocReader = getMaThuocCmd.ExecuteReader();

                        if (thuocReader.Read())
                        {
                            string maThuoc = thuocReader["MaThuoc"].ToString();
                            decimal giaTienThuoc = (decimal)thuocReader["GiaTien"] ;
                            int soLuongTonKho = (int)thuocReader["SoLuong"];
                            thuocReader.Close();
                            
                        if (soLuong > soLuongTonKho)
                        {
                            MessageBox.Show($"Số lượng thuốc trong kho không đủ! Hiện tại còn {soLuongTonKho}.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        else
                        {
                            string updateSoLuongQuery = @"UPDATE Thuoc SET SoLuong = SoLuong - @SoLuong WHERE MaThuoc = @MaThuoc";
                            SqlCommand updateSoLuongCmd = new SqlCommand(updateSoLuongQuery, conn);
                            updateSoLuongCmd.Parameters.AddWithValue("@SoLuong", soLuong);
                            updateSoLuongCmd.Parameters.AddWithValue("@MaThuoc", maThuoc);
                            updateSoLuongCmd.ExecuteNonQuery();
                        }
                        // Tính giá tiền
                        decimal giaTien = giaTienThuoc * soLuong;

                            // Thêm chi tiết đơn thuốc
                            string insertCTDonThuocQuery = @"INSERT INTO CTDonThuoc (MaDonThuoc, MaThuoc, SoLuong, GiaTien, HuongDanSuDung) 
                                                VALUES (@MaDonThuoc, @MaThuoc, @SoLuong, @GiaTien, @HuongDan)";
                            SqlCommand insertCTCmd = new SqlCommand(insertCTDonThuocQuery, conn);
                            insertCTCmd.Parameters.AddWithValue("@MaDonThuoc", maDonThuoc);
                            insertCTCmd.Parameters.AddWithValue("@MaThuoc", maThuoc);
                            insertCTCmd.Parameters.AddWithValue("@SoLuong", soLuong);
                            insertCTCmd.Parameters.AddWithValue("@GiaTien", giaTien);
                            insertCTCmd.Parameters.AddWithValue("@HuongDan", huongDan);

                            insertCTCmd.ExecuteNonQuery();
                            MessageBox.Show("Thêm đơn thuốc thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thông tin thuốc!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Bắt lỗi khóa chính hoặc khóa ngoại
                        if (ex.Number == 2627) // Lỗi khóa chính
                            MessageBox.Show("Chi tiết đơn thuốc đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        else if (ex.Number == 547) // Lỗi khóa ngoại
                            MessageBox.Show("Dữ liệu không hợp lệ, vi phạm khóa ngoại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                            MessageBox.Show($"Lỗi SQL: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        conn.Close();
                    }
            }
        }


        // Hàm thực thi câu lệnh truy vấn trả về DataTable
        private DataTable GetDataTable(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = _userRepository.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        // Hàm thực thi câu lệnh truy vấn không trả về
        private int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = _userRepository.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        private int ExecuteCountQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = _userRepository.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetInt32(0); // Lấy giá trị COUNT(*) từ cột đầu tiên
                        }
                        return 0; // Nếu không có kết quả, trả về 0
                    }
                }
            }
        }

        private void ButtonXoa_Click(object sender, RoutedEventArgs e)
        {
            string maDonThuoc = TxB_MaDonThuoc.Text.Trim();
            string tenThuoc = TxB_TenThuoc.Text.Trim();
            if (string.IsNullOrEmpty(maDonThuoc) || string.IsNullOrEmpty(tenThuoc))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ dữ liệu!");
                return;
            }
            // Tìm mã thuốc từ tên thuốc
            string queryThuoc = "SELECT MaThuoc FROM Thuoc WHERE TenThuoc = @TenThuoc";
            DataTable dtThuoc = GetDataTable(queryThuoc, new SqlParameter("@TenThuoc", tenThuoc));

            if (dtThuoc.Rows.Count == 0)
            {
                MessageBox.Show("Thuốc không tồn tại!");
                return;
            }

            string maThuoc = dtThuoc.Rows[0]["MaThuoc"].ToString();

            // Xóa dữ liệu trong bảng CTDonThuoc
            string queryDelete = "DELETE FROM CTDonThuoc WHERE MaDonThuoc = @MaDonThuoc AND MaThuoc = @MaThuoc";
            int result = ExecuteNonQuery(queryDelete,
                new SqlParameter("@MaDonThuoc", maDonThuoc),
                new SqlParameter("@MaThuoc", maThuoc));

            if (result > 0)
            {
                MessageBox.Show("Xóa thành công!");
            }
            else
            {
                MessageBox.Show("Xóa thất bại!");
            }
        }
    }
}
