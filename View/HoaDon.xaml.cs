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
    /// Interaction logic for HoaDon.xaml
    /// </summary>
    public partial class HoaDon : UserControl
    {
        public HoaDon()
        {
            InitializeComponent();
            searchControl.Tmp = "Nhập số hóa đơn hoặc tên hóa đơn";
            // Đăng ký sự kiện SearchButtonClicked
            searchControl.SearchButtonClicked += SearchControl_SearchButtonClicked;
        }

        private void SearchControl_SearchButtonClicked(object sender, string searchText)
        {
            // Lấy mã đơn thuốc từ tham số searchText
            string maHoaDon = searchText.Trim();
            if (string.IsNullOrEmpty(maHoaDon))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                dgvHoaDon.ItemsSource = null;
                return;
            }

            // Chuỗi kết nối tới cơ sở dữ liệu
            string connectionString = "Data Source=LAPTOP-702RPVLR;Initial Catalog=BV;Integrated Security=True";

            // Câu lệnh SQL để tìm kiếm thông tin đơn thuốc và chi tiết đơn thuốc
            string queryHoaDon = "SELECT * FROM HoaDon WHERE MaHoaDon = @MaHoaDon OR TenHoaDon = @MaHoaDon";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Hiển thị thông tin của HoaDon
                    SqlDataAdapter adapterHoaDon = new SqlDataAdapter(queryHoaDon, connection);
                    adapterHoaDon.SelectCommand.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    DataTable dataTableHoaDon = new DataTable();
                    adapterHoaDon.Fill(dataTableHoaDon);

                    // Gắn dữ liệu vào dgvHoaDon
                    dgvHoaDon.ItemsSource = dataTableHoaDon.DefaultView;

                    // Kiểm tra nếu không tìm thấy đơn thuốc
                    if (dataTableHoaDon.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        dgvHoaDon.ItemsSource = null;
                        return;
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CTHD cTHD = new CTHD();
            cTHD.Show();
        }
    }
}
