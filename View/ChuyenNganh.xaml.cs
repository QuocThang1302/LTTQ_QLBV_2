using System.Windows;
using System.Windows.Input;
using Microsoft.Data.SqlClient;
using QuanLyBenhVien.Repositories;

namespace QuanLyBenhVien.View
{
    public partial class ChuyenNganh : Window
    {
        private readonly RepositoryBase _userRepository;
        public ChuyenNganh()
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
        public Action OnDataAdded;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Lấy giá trị từ các TextBox
            string maChuyenNganh = TxB_MaChuyenNganh.Text.Trim();
            string tenChuyenNganh = TxB_ChuyenNganh.Text.Trim();
            string khoa = TxB_Khoa.Text.Trim();

            // Kiểm tra lỗi: Mã Chuyên Ngành không được để trống
            if (string.IsNullOrEmpty(maChuyenNganh))
            {
                MessageBox.Show("Vui lòng nhập Mã Chuyên Ngành!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = _userRepository.GetConnection())
                {
                    conn.Open();

                    // Kiểm tra xem MaChuyenNganh đã tồn tại chưa
                    string checkQuery = "SELECT COUNT(*) FROM ChuyenNganh WHERE MaChuyenNganh = @MaChuyenNganh";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MaChuyenNganh", maChuyenNganh);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0) // Nếu đã tồn tại
                        {
                            // Kiểm tra dữ liệu đầy đủ trước khi cập nhật
                            if (string.IsNullOrEmpty(tenChuyenNganh) || string.IsNullOrEmpty(khoa))
                            {
                                MessageBox.Show("Mã Chuyên Ngành đã tồn tại! Vui lòng nhập đầy đủ thông tin để cập nhật.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }

                            // Cập nhật thông tin Chuyên Ngành
                            string updateQuery = "UPDATE ChuyenNganh SET TenChuyenNganh = @TenChuyenNganh, Khoa = @Khoa WHERE MaChuyenNganh = @MaChuyenNganh";
                            using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@MaChuyenNganh", maChuyenNganh);
                                updateCmd.Parameters.AddWithValue("@TenChuyenNganh", tenChuyenNganh);
                                updateCmd.Parameters.AddWithValue("@Khoa", khoa);

                                int rowsAffected = updateCmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                    OnDataAdded?.Invoke();
                                }
                                else
                                {
                                    MessageBox.Show("Cập nhật thông tin thất bại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                        else // Nếu không tồn tại → Thêm mới
                        {
                            // Kiểm tra dữ liệu đầy đủ trước khi thêm mới
                            if (string.IsNullOrEmpty(tenChuyenNganh) || string.IsNullOrEmpty(khoa))
                            {
                                MessageBox.Show("Vui lòng điền đầy đủ thông tin để thêm mới!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                            string insertQuery = "INSERT INTO ChuyenNganh (MaChuyenNganh, TenChuyenNganh, Khoa) VALUES (@MaChuyenNganh, @TenChuyenNganh, @Khoa)";
                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@MaChuyenNganh", maChuyenNganh);
                                insertCmd.Parameters.AddWithValue("@TenChuyenNganh", tenChuyenNganh);
                                insertCmd.Parameters.AddWithValue("@Khoa", khoa);

                                int rowsAffected = insertCmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Thêm thông tin thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                    OnDataAdded?.Invoke();
                                }
                                else
                                {
                                    MessageBox.Show("Thêm thông tin thất bại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                
                if (sqlEx.Number == 547) // Vi phạm khóa ngoại
                {
                    MessageBox.Show("Dữ liệu nhập vào vi phạm ràng buộc khóa ngoại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show($"Lỗi SQL: {sqlEx.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}