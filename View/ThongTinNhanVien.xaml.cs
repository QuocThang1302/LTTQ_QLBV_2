using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace QuanLyBenhVien.View
{
    /// <summary>
    /// Interaction logic for ThongTinNhanVien.xaml
    /// </summary>
    public partial class ThongTinNhanVien : UserControl
    {
        public ThongTinNhanVien()
        {
            InitializeComponent();
            searchControl.Tmp = "Nhập mã nhân viên hoặc tên nhân viên";
            // Đăng ký sự kiện SearchButtonClicked
            searchControl.SearchButtonClicked += SearchControl_SearchButtonClicked;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button Clicked!");
        }
        private void SearchControl_SearchButtonClicked(object sender, string searchText)
        {


            string maNhanVien = searchText.Trim();

            if (string.IsNullOrEmpty(maNhanVien))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                dgvNhanVien.ItemsSource = null; // Xóa dữ liệu trong DataGrid
                ClearFields();
                return;
            }

            // Chuỗi kết nối tới cơ sở dữ liệu
            string connectionString = "Data Source=LAPTOP-702RPVLR;Initial Catalog=BV;Integrated Security=True";

            // Câu lệnh SQL để tìm kiếm thông tin nhân viên
            string query = "SELECT * FROM NhanVien WHERE MaNhanVien = @MaNhanVien OR Ten = @MaNhanVien";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.Add("@MaNhanVien", SqlDbType.VarChar).Value = maNhanVien;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearFields(); // Xóa các TextBox
                        dgvNhanVien.ItemsSource = null; // Xóa dữ liệu trong DataGrid
                    }
                    else if (dataTable.Rows.Count == 1)
                    {
                        // Hiển thị thông tin lên các TextBox
                        DataRow row = dataTable.Rows[0];
                        TxB_MaNhanVien.Text = row["MaNhanVien"].ToString();
                        string ho = row["Ho"].ToString();
                        string ten = row["Ten"].ToString();
                        TxB_HoTen.Text = ho + " " + ten; // Hiển thị họ và tên đầy đủ
                        TxB_Ten.Text = ten;
                        TxB_ChuyenNganh.Text = row["MaChuyenNganh"].ToString();
                        TxB_ChucVu.Text = row["LoaiNhanVien"].ToString();
                        TxB_NgaySinh.Text = Convert.ToDateTime(row["NgaySinh"]).ToString("yyyy-MM-dd");
                        TxB_GioiTinh.Text = row["GioiTinh"].ToString();
                        TxB_CCCD.Text = row["CCCD"].ToString();
                        TxB_DiaChi.Text = row["DiaChi"].ToString();
                        TxB_SDT.Text = row["SDT"].ToString();
                        TxB_Email.Text = row["Email"].ToString();

                        // Xóa dữ liệu trong DataGrid nếu chỉ có một kết quả
                        dgvNhanVien.ItemsSource = dataTable.DefaultView;
                    }
                    else
                    {
                        // Nếu có nhiều kết quả, chỉ hiển thị vào DataGrid
                        ClearFields(); // Xóa các TextBox
                        dgvNhanVien.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFields()
        {
            TxB_MaNhanVien.Text = "";
            TxB_HoTen.Text = "";
            TxB_Ten.Text = "";
            TxB_ChuyenNganh.Text = "";
            TxB_ChucVu.Text = "";
            TxB_NgaySinh.Text = "";
            TxB_GioiTinh.Text = "";
            TxB_DiaChi.Text = "";
            TxB_SDT.Text = "";
            TxB_Email.Text = "";
            TxB_CCCD.Text = "";
        }
    }
}



