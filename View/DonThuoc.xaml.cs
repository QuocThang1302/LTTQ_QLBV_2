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
    /// Interaction logic for DonThuoc.xaml
    /// </summary>
    public partial class DonThuoc : UserControl
    {
        public DonThuoc()
        {
            InitializeComponent();
            searchControl.Tmp = "Nhập mã đơn thuốc hoặc mã bác sĩ";
            // Đăng ký sự kiện SearchButtonClicked
            searchControl.SearchButtonClicked += SearchControl_SearchButtonClicked;
            // Đăng ký sự kiện ClearButtonClicked cho nút X
            searchControl.ClearButtonClicked += SearchControl_ClearButtonClicked;

        }
        private void SearchControl_ClearButtonClicked(object sender, EventArgs e)
        {
            // Logic khi nút X được nhấn
            //HienThiDanhSach();
        }

        private void SearchControl_SearchButtonClicked(object sender, string searchText)
        {
            // Lấy mã đơn thuốc từ tham số searchText
            string maDonThuoc = searchText.Trim();
            if (string.IsNullOrEmpty(maDonThuoc))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Chuỗi kết nối tới cơ sở dữ liệu
            string connectionString = "Data Source=LAPTOP-702RPVLR;Initial Catalog=BV;Integrated Security=True";

            // Câu lệnh SQL để tìm kiếm thông tin đơn thuốc và chi tiết đơn thuốc
            string queryDonThuoc = "SELECT * FROM DonThuoc WHERE MaDonThuoc = @MaDonThuoc OR MaBacSi=@MaDonThuoc";
            string queryCTDonThuoc = "SELECT * FROM CTDonThuoc JOIN Thuoc ON CTDonThuoc.MaThuoc = Thuoc.MaThuoc\r\nWHERE MaDonThuoc IN (SELECT MaDonThuoc FROM DonThuoc WHERE MaBacSi = @MaDonThuoc OR MaDonThuoc = @MaDonThuoc)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Hiển thị thông tin của DonThuoc
                    SqlDataAdapter adapterDonThuoc = new SqlDataAdapter(queryDonThuoc, connection);
                    adapterDonThuoc.SelectCommand.Parameters.AddWithValue("@MaDonThuoc", maDonThuoc);
                    DataTable dataTableDonThuoc = new DataTable();
                    adapterDonThuoc.Fill(dataTableDonThuoc);

                    // Gắn dữ liệu vào dgvDonThuoc
                    dgvDonThuoc.ItemsSource = dataTableDonThuoc.DefaultView;

                    // Kiểm tra nếu không tìm thấy đơn thuốc
                    if (dataTableDonThuoc.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    }

                    // Hiển thị thông tin của CTDonThuoc
                    SqlDataAdapter adapterCTDonThuoc = new SqlDataAdapter(queryCTDonThuoc, connection);
                    adapterCTDonThuoc.SelectCommand.Parameters.AddWithValue("@MaDonThuoc", maDonThuoc);
                    DataTable dataTableCTDonThuoc = new DataTable();
                    adapterCTDonThuoc.Fill(dataTableCTDonThuoc);

                    // Gắn dữ liệu vào dgvCTDonThuoc
                    dgvCTDonThuoc.ItemsSource = dataTableCTDonThuoc.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DonThuoc_CTDT ctdt = new DonThuoc_CTDT();
            ctdt.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CTHD hd = new CTHD();
            hd.Show();
        }
    }
}
