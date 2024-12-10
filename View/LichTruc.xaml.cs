using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
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
    /// Interaction logic for LichTruc.xaml
    /// </summary>
    public partial class LichTruc : UserControl
    {
        public LichTruc()
        {
            InitializeComponent();
            InitializeComponent();
            searchControl.Tmp = "Nhập mã công việc hoặc tên công việc";
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
            string maCongViec = searchText.Trim();
            if (string.IsNullOrEmpty(maCongViec))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Chuỗi kết nối tới cơ sở dữ liệu
            string connectionString = "Data Source=LAPTOP-702RPVLR;Initial Catalog=BV;Integrated Security=True";

            // Câu lệnh SQL để tìm kiếm thông tin đơn thuốc và chi tiết đơn thuốc
            string queryCongViec = "SELECT * FROM CongViec WHERE MaCongViec = @MaCongViec OR TenCongViec = @MaCongViec";
            string queryPhanCong = "SELECT * FROM LichTruc WHERE PhanCong IN (SELECT MaCongViec FROM CongViec WHERE MaCongViec = @MaCongViec OR TenCongViec = @MaCongViec)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Hiển thị thông tin của
                    SqlDataAdapter adapterCongViec = new SqlDataAdapter(queryCongViec, connection);
                    adapterCongViec.SelectCommand.Parameters.AddWithValue("@MaCongViec", maCongViec);
                    DataTable dataTableCongViec = new DataTable();
                    adapterCongViec.Fill(dataTableCongViec);

                    // Gắn dữ liệu vào 
                    dgvCongViec.ItemsSource = dataTableCongViec.DefaultView;

                    // Kiểm tra nếu không tìm thấy 
                    if (dataTableCongViec.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Xóa dữ liệu của DataGridView nếu không có kết quả
                        
                        return;
                    }

                    // Hiển thị thông tin của PhanCong
                    SqlDataAdapter adapterPhanCong = new SqlDataAdapter(queryPhanCong, connection);
                    adapterPhanCong.SelectCommand.Parameters.AddWithValue("@MaCongViec", maCongViec);
                    DataTable dataTablePhanCong = new DataTable();
                    adapterPhanCong.Fill(dataTablePhanCong);

                    // Gắn dữ liệu vào dgvPhanCong
                    dgvPhanCong.ItemsSource = dataTablePhanCong.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            PhanCong phanCong = new PhanCong();
            phanCong.Show();
        }
    }
}
