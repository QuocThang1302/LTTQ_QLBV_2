using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace QuanLyBenhVien.View
{
    /// <summary>
    /// Interaction logic for CTHD.xaml
    /// </summary>
    public partial class CTHD : Window
    {
        public CTHD()
        {
            InitializeComponent();
        }
        string connectionString = "Data Source=LAPTOP-702RPVLR;Initial Catalog=BV;Integrated Security=True";
        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Kiểm tra nếu TextBox Mã Hóa Đơn trống
                    if (string.IsNullOrWhiteSpace(txtMaHoaDon.Text))
                    {
                        MessageBox.Show("Vui lòng nhập Mã Hóa Đơn!", "Lỗi");
                        return;
                    }

                    // Kiểm tra xem hóa đơn đã tồn tại hay chưa
                    string checkHoaDonQuery = "SELECT * FROM HoaDon WHERE MaHoaDon = @MaHoaDon";
                    SqlCommand checkHoaDonCmd = new SqlCommand(checkHoaDonQuery, conn);
                    checkHoaDonCmd.Parameters.AddWithValue("@MaHoaDon", txtMaHoaDon.Text);

                    using (SqlDataReader reader = checkHoaDonCmd.ExecuteReader())
                    {
                        if (reader.HasRows) // Nếu hóa đơn tồn tại
                        {
                            reader.Read(); // Lấy thông tin hóa đơn
                            txtTenHoaDon.Text = reader["TenHoaDon"].ToString();
                            txtMaBenhNhan.Text = reader["MaBenhNhan"].ToString();
                            txtMaNhanVien.Text = reader["MaNhanVien"].ToString();
                            txtNgayLap.Text = Convert.ToDateTime(reader["NgayLapHoaDon"]).ToString("yyyy-MM-dd");

                            // Truy vấn tên bệnh nhân từ bảng BenhNhan
                            string getTenBenhNhanQuery = "SELECT Ho + ' ' + Ten AS TenBenhNhan FROM BenhNhan WHERE MaBenhNhan = @MaBenhNhan";
                            using (SqlConnection connBenhNhan = new SqlConnection(connectionString))
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
                            using (SqlConnection connNhanVien = new SqlConnection(connectionString))
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
                        }
                        else
                        {
                            // Nếu hóa đơn chưa tồn tại, yêu cầu nhập thông tin đầy đủ
                            if (string.IsNullOrWhiteSpace(txtTenHoaDon.Text) ||
                                string.IsNullOrWhiteSpace(txtMaBenhNhan.Text) ||
                                string.IsNullOrWhiteSpace(txtMaNhanVien.Text) ||
                                string.IsNullOrWhiteSpace(txtNgayLap.Text) ||
                                string.IsNullOrWhiteSpace(txtMaHoaDon.Text)
                                )
                            {
                                MessageBox.Show("Hóa đơn không tồn tại. Vui lòng nhập đầy đủ thông tin để tạo hóa đơn mới!", "Lỗi");
                                return;
                            }

                            // Thêm hóa đơn mới
                            string insertHoaDonQuery = "INSERT INTO HoaDon (MaHoaDon, TenHoaDon, MaBenhNhan, MaNhanVien, NgayLapHoaDon, GiaTien, TrangThai) " +
                                                       "VALUES (@MaHoaDon, @TenHoaDon, @MaBenhNhan, @MaNhanVien, @NgayLapHoaDon, @GiaTien, @TrangThai)";
                            using (SqlCommand insertHoaDonCmd = new SqlCommand(insertHoaDonQuery, conn))
                            {
                                insertHoaDonCmd.Parameters.AddWithValue("@MaHoaDon", txtMaHoaDon.Text);
                                insertHoaDonCmd.Parameters.AddWithValue("@TenHoaDon", txtTenHoaDon.Text);
                                insertHoaDonCmd.Parameters.AddWithValue("@MaBenhNhan", txtMaBenhNhan.Text);
                                insertHoaDonCmd.Parameters.AddWithValue("@MaNhanVien", txtMaNhanVien.Text);
                                insertHoaDonCmd.Parameters.AddWithValue("@NgayLapHoaDon", DateTime.Parse(txtNgayLap.Text));
                                insertHoaDonCmd.Parameters.AddWithValue("@GiaTien", 0); // Ban đầu là 0
                                insertHoaDonCmd.Parameters.AddWithValue("@TrangThai", "Chưa thanh toán");

                                insertHoaDonCmd.ExecuteNonQuery();
                                MessageBox.Show("Hóa đơn mới đã được tạo!", "Thông báo");
                            }
                        }
                    }

                    // Truy vấn MaThuoc từ TenThuoc
                    string selectThuocQuery = "SELECT MaThuoc FROM Thuoc WHERE TenThuoc = @TenThuoc";
                    SqlCommand selectThuocCmd = new SqlCommand(selectThuocQuery, conn);
                    selectThuocCmd.Parameters.AddWithValue("@TenThuoc", txtTenThuoc.Text);

                    object result = selectThuocCmd.ExecuteScalar();
                    if (result == null)
                    {
                        MessageBox.Show("Không tìm thấy thông tin thuốc!", "Lỗi");
                        return;
                    }
                    string maThuoc = result.ToString();

                    // Thêm chi tiết đơn thuốc vào bảng CTDonThuoc
                    string insertCTDonThuocQuery = "INSERT INTO CTDonThuoc (MaDonThuoc, MaThuoc, SoLuong, GiaTien, HuongDanSuDung, MaHoaDon) " +
                                                   "VALUES (@MaDonThuoc, @MaThuoc, @SoLuong, @GiaTien, @HuongDanSuDung, @MaHoaDon)";
                    using (SqlCommand insertCTDonThuocCmd = new SqlCommand(insertCTDonThuocQuery, conn))
                    {
                        insertCTDonThuocCmd.Parameters.AddWithValue("@MaDonThuoc", Guid.NewGuid().ToString());
                        insertCTDonThuocCmd.Parameters.AddWithValue("@MaThuoc", maThuoc);
                        insertCTDonThuocCmd.Parameters.AddWithValue("@SoLuong", int.Parse(txtSoLuong.Text));
                        insertCTDonThuocCmd.Parameters.AddWithValue("@HuongDanSuDung", "Uống sau ăn");
                        insertCTDonThuocCmd.Parameters.AddWithValue("@MaHoaDon", txtMaHoaDon.Text);

                        insertCTDonThuocCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Thêm thông tin thành công!");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Dữ liệu nhập vào không hợp lệ. Vui lòng kiểm tra lại!", "Lỗi");
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627) // Lỗi vi phạm khóa chính
                    {
                        MessageBox.Show("Dữ liệu không hợp lệ. Vui lòng kiểm tra lại các thông tin liên quan!", "Lỗi");
                    }
                    else if (ex.Number == 547) // Lỗi vi phạm khóa ngoại
                    {
                        MessageBox.Show("Dữ liệu không hợp lệ. Vui lòng kiểm tra lại các thông tin liên quan!", "Lỗi");
                    }
                    else
                    {
                        MessageBox.Show($"Dữ liệu không hợp lệ. Vui lòng kiểm tra lại các thông tin liên quan!", "Lỗi");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi");
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();
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
