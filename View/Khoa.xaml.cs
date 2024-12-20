using System.Windows;
using System.Windows.Input;
using Microsoft.Data.SqlClient;
using QuanLyBenhVien.Repositories;

namespace QuanLyBenhVien.View
{
    public partial class Khoa : Window
    {
        private readonly RepositoryBase _userRepository;
        public Khoa()
        {
            _userRepository = new UserRepository();
            InitializeComponent();
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
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Lấy giá trị từ các TextBox
            string maKhoa = TxB_MaKhoa.Text.Trim();
            string tenKhoa = TxB_Khoa.Text.Trim();
            string truongKhoa = TxB_TruongKhoa.Text.Trim();

            // Kiểm tra lỗi
            if (string.IsNullOrEmpty(maKhoa) || string.IsNullOrEmpty(tenKhoa) || string.IsNullOrEmpty(truongKhoa))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Kết nối đến cơ sở dữ liệu và xử lý
            try
            {
                using (SqlConnection conn = _userRepository.GetConnection())
                {
                    conn.Open();

                    // Kiểm tra mã khoa đã tồn tại hay chưa
                    string checkQuery = "SELECT COUNT(*) FROM Khoa WHERE MaKhoa = @MaKhoa";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MaKhoa", maKhoa);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Mã khoa đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }

                    // Thêm thông tin mới vào bảng Khoa
                    string insertQuery = "INSERT INTO Khoa (MaKhoa, TenKhoa, TruongKhoa) VALUES (@MaKhoa, @TenKhoa, @TruongKhoa)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@MaKhoa", maKhoa);
                        insertCmd.Parameters.AddWithValue("@TenKhoa", tenKhoa);
                        insertCmd.Parameters.AddWithValue("@TruongKhoa", truongKhoa);

                        try
                        {
                            int rowsAffected = insertCmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Thêm thông tin thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show("Thêm thông tin thất bại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        catch (SqlException sqlEx)
                        {
                            if (sqlEx.Number == 2627) // Vi phạm khóa chính
                            {
                                MessageBox.Show("Mã khoa đã tồn tại (vi phạm khóa chính)!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            else if (sqlEx.Number == 547) // Vi phạm khóa ngoại
                            {
                                MessageBox.Show("Dữ liệu nhập vào vi phạm ràng buộc khóa ngoại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            else
                            {
                                MessageBox.Show($"Lỗi SQL: {sqlEx.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
