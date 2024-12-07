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
    /// Interaction logic for ChuyenNganh.xaml
    /// </summary>
    public partial class ChuyenNganh : Window
    {
        public ChuyenNganh()
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
            string maChuyenNganh = TxB_MaChuyenNganh.Text.Trim();
            string chuyenNganh = TxB_ChuyenNganh.Text.Trim();
            string khoa = TxB_Khoa.Text.Trim();

            // Kiểm tra lỗi
            if (string.IsNullOrEmpty(maChuyenNganh) || string.IsNullOrEmpty(chuyenNganh) || string.IsNullOrEmpty(khoa))
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

                    string query = "INSERT INTO ChuyenNganh (MaChuyenNganh, TenChuyenNganh, Khoa) VALUES (@MaChuyenNganh, @ChuyenNganh, @Khoa)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaChuyenNganh", maChuyenNganh);
                        cmd.Parameters.AddWithValue("@ChuyenNganh", chuyenNganh);
                        cmd.Parameters.AddWithValue("@Khoa", khoa);

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