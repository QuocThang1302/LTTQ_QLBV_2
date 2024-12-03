using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyBenhVien.View
{
    /// <summary>
    /// Interaction logic for Khoa_ChuyenNganh.xaml
    /// </summary>
    public partial class Khoa_ChuyenNganh : UserControl
    {
        public Khoa_ChuyenNganh()
        {
            InitializeComponent();
            searchControl.Tmp = "Nhập mã khoa hoặc tên khoa";
            // Đăng ký sự kiện SearchButtonClicked
            searchControl.SearchButtonClicked += SearchControl_SearchButtonClicked;
        }

        private void SearchControl_SearchButtonClicked(object sender, string searchText)
        {
            // Lấy mã đơn thuốc từ tham số searchText
            string maKhoa = searchText.Trim();
            if (string.IsNullOrEmpty(maKhoa))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Chuỗi kết nối tới cơ sở dữ liệu
            string connectionString = "Data Source=LAPTOP-702RPVLR;Initial Catalog=BV;Integrated Security=True";

            // Câu lệnh SQL để tìm kiếm thông tin đơn thuốc và chi tiết đơn thuốc
            string queryKhoa = "SELECT * FROM Khoa WHERE MaKhoa = @MaKhoa OR TenKhoa = @MaKhoa";
            string queryChuyenNganh = "SELECT * FROM ChuyenNganh WHERE Khoa IN (SELECT MaKhoa From Khoa WHERE MaKhoa = @MaKhoa OR TenKhoa = @MaKhoa)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Hiển thị thông tin của Khoa
                    SqlDataAdapter adapterKhoa = new SqlDataAdapter(queryKhoa, connection);
                    adapterKhoa.SelectCommand.Parameters.AddWithValue("@MaKhoa", maKhoa);
                    DataTable dataTableKhoa = new DataTable();
                    adapterKhoa.Fill(dataTableKhoa);

                    // Gắn dữ liệu vào dgvKhoa
                    dgvKhoa.ItemsSource = dataTableKhoa.DefaultView;

                    // Kiểm tra nếu không tìm thấy đơn thuốc
                    if (dataTableKhoa.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Xóa dữ liệu của DataGridView nếu không có kết quả
                        dgvChuyenNganh.ItemsSource = null;
                        dgvKhoa.ItemsSource = null;
                        return;
                    }

                    // Hiển thị thông tin của ChuyenNganh
                    SqlDataAdapter adapterChuyenNganh = new SqlDataAdapter(queryChuyenNganh, connection);
                    adapterChuyenNganh.SelectCommand.Parameters.AddWithValue("@MaKhoa", maKhoa);
                    DataTable dataTableChuyenNganh = new DataTable();
                    adapterChuyenNganh.Fill(dataTableChuyenNganh);

                    // Gắn dữ liệu vào dgvChuyenNganh
                    dgvChuyenNganh.ItemsSource = dataTableChuyenNganh.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChuyenNganh chuyenNganh = new ChuyenNganh();
            chuyenNganh.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Khoa khoa = new Khoa();
            khoa.Show();
        }
    }
}
