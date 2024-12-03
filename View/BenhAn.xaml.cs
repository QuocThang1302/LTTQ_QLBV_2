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
using System.Collections;

namespace QuanLyBenhVien.View
{
    /// <summary>
    /// Interaction logic for BenhAn.xaml
    /// </summary>
    public partial class BenhAn : UserControl
    {
        public BenhAn()
        {
            InitializeComponent();
            searchControl.Tmp = "Nhập mã bệnh án hoặc mã bệnh nhân";
            // Đăng ký sự kiện SearchButtonClicked
            searchControl.SearchButtonClicked += SearchControl_SearchButtonClicked;
        }
        private void SearchControl_SearchButtonClicked(object sender, string searchText)
        {

            string maBenhAn = searchText.Trim();

            if (string.IsNullOrEmpty(maBenhAn))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu trước khi tìm kiếm!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                ClearFields();
                dgvBenhAn.ItemsSource = null;
                return;
            }

            string connectionString = "Data Source=LAPTOP-702RPVLR;Initial Catalog=BV;Integrated Security=True";
            string query = "SELECT * FROM BenhAn JOIN BenhNhan ON BenhAn.MaBenhNhan=BenhNhan.MaBenhNhan WHERE MaBenhAn=@MaBenhAn OR BenhNhan.MaBenhNhan=@MaBenhAn";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaBenhAn", maBenhAn);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearFields();
                        dgvBenhAn.ItemsSource = null;
                    }
                    else if (dataTable.Rows.Count == 1)
                    {
                        // Hiển thị thông tin lên TextBox
                        DataRow row = dataTable.Rows[0];
                        TxB_MaBenhAn.Text = row["MaBenhAn"].ToString();
                        TxB_MaBenhNhan.Text = row["MaBenhNhan"].ToString();
                        TxB_BenhNhan.Text = row["Ten"].ToString();
                        TxB_TinhTrang.Text = row["TinhTrang"].ToString();
                        TxB_NgayTaoLap.Text = Convert.ToDateTime(row["NgayTaoLap"]).ToString("yyyy-MM-dd");
                        TxB_Benh.Text = row["Benh"].ToString();
                        TxB_HuongDieuTri.Text = row["DieuTri"].ToString();

                        // Hiển thị kết quả vào DataGrid
                        dgvBenhAn.ItemsSource = dataTable.DefaultView;
                    }
                    else
                    {
                        // Nếu có nhiều kết quả, chỉ hiển thị vào DataGrid
                        ClearFields();
                        dgvBenhAn.ItemsSource = dataTable.DefaultView;
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
            TxB_MaBenhAn.Text = "";
            TxB_MaBenhNhan.Text = "";
            TxB_BenhNhan.Text = "";
            TxB_TinhTrang.Text = "";
            TxB_NgayTaoLap.Text = "";
            TxB_Benh.Text = "";
            TxB_HuongDieuTri.Text = "";
        }
    }
}
