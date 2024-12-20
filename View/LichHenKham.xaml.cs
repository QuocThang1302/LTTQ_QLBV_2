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
    /// Interaction logic for LichHenKham.xaml
    /// </summary>
    public partial class LichHenKham : UserControl
    {
        private string connectionString = "Data Source=LAPTOP-702RPVLR;Initial Catalog=BV;Integrated Security=True";
        public LichHenKham()
        {
            InitializeComponent();
            searchControl.Tmp = "Nhập mã lịch hẹn hoặc mã bác sĩ";
            // Đăng ký sự kiện SearchButtonClicked
            searchControl.SearchButtonClicked += SearchControl_SearchButtonClicked;
            // Đăng ký sự kiện ClearButtonClicked cho nút X
            searchControl.ClearButtonClicked += SearchControl_ClearButtonClicked;

        }
        private void SearchControl_ClearButtonClicked(object sender, EventArgs e)
        {
            // Logic khi nút X được nhấn
            //HienThiDanhSach();
            ClearFields();
        }
        private void SearchControl_SearchButtonClicked(object sender, string searchText)
        {


            string maLichHen = searchText.Trim();

            if (string.IsNullOrEmpty(maLichHen))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                
                return;
            }

            
            string query = "SELECT * FROM LichHenKham WHERE MaLichHen=@MaLichHen OR MaBacSi=@MaLichHen";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaLichHen", maLichHen);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu phù hợp ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        
                    }
                    else if (dataTable.Rows.Count == 1)
                    {
                        // Hiển thị thông tin lên TextBox
                        DataRow row = dataTable.Rows[0];
                        tbMaLichHen.Text = row["MaLichHen"].ToString();
                        tbMaBenhNhan.Text = row["MaBenhNhan"].ToString();
                        tbNgayHenKham.Text = Convert.ToDateTime(row["NgayHenKham"]).ToString("yyyy-MM-dd");
                        tbMaBacSi.Text = row["MaBacSi"].ToString();

                        // Hiển thị dữ liệu vào DataGrid
                        dgvLichHen.ItemsSource = dataTable.DefaultView;
                    }
                    else
                    {
                        // Nếu có nhiều kết quả, chỉ hiển thị vào DataGrid
                        ClearFields(); // Xóa TextBox
                        dgvLichHen.ItemsSource = dataTable.DefaultView;
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
            tbMaLichHen.Text = "";
            tbMaBenhNhan.Text = "";
            tbNgayHenKham.Text = "";
            tbMaBacSi.Text = "";
        }
    }
}

