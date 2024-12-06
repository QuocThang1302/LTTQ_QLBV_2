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
    /// Interaction logic for Khoa.xaml
    /// </summary>
    public partial class Khoa : Window
    {
        public Khoa()
        {
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

            // Kết nối đến cơ sở dữ liệu và thêm thông tin
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=QUOCTHANG\\SQLEXPRESS;Initial Catalog=BV;Integrated Security=True"))
                {
                    conn.Open();

                    string query = "INSERT INTO Khoa (MaKhoa, TenKhoa, TruongKhoa) VALUES (@MaKhoa, @TenKhoa, @TruongKhoa)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKhoa", maKhoa);
                        cmd.Parameters.AddWithValue("@TenKhoa", tenKhoa);
                        cmd.Parameters.AddWithValue("@TruongKhoa", truongKhoa);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Thêm thông tin thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Thêm thông tin thất bại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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
