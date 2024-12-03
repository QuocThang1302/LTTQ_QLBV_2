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
    /// Interaction logic for PhieuKhamBenh.xaml
    /// </summary>
    public partial class PhieuKhamBenh : UserControl
    {
        public PhieuKhamBenh()
        {
            InitializeComponent();
            searchControl.Tmp = "Nhập mã phiếu khám hoặc mã bác sĩ";
            // Đăng ký sự kiện SearchButtonClicked
            searchControl.SearchButtonClicked += SearchControl_SearchButtonClicked;
        }
        private void SearchControl_SearchButtonClicked(object sender, string searchText)
        {

            // Lấy mã phiếu khám từ tham số searchText
            string maPhieuKham = searchText.Trim();

            if (string.IsNullOrEmpty(maPhieuKham))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                dgvPhieuKham.ItemsSource = null; // Xóa dữ liệu trong DataGrid
                ClearFields();
                return;
            }

            string connectionString = "Data Source=LAPTOP-702RPVLR;Initial Catalog=BV;Integrated Security=True";

            // Câu lệnh SQL để tìm kiếm thông tin phiếu khám bệnh và chi tiết phiếu khám bệnh
            string query = @"
    SELECT 
        BN.Ten AS TEN_BENHNHAN, 
        NV.Ten AS TEN_BACSI, 
        MaPhieuKham, 
        PK.MaBenhNhan, 
        NgayKham, 
        LyDoKhamBenh, 
        KhamLamSang, 
        ChanDoan, 
        KetQuaKham, 
        DieuTri, 
        MaBacSi
    FROM 
        BenhNhan BN 
    JOIN 
        PhieuKhamBenh PK ON PK.MaBenhNhan = BN.MaBenhNhan
    JOIN 
        NhanVien NV ON NV.MaNhanVien = PK.MaBacSi
    WHERE 
        MaPhieuKham = @MaPhieuKham";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaPhieuKham", maPhieuKham);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearFields(); // Xóa TextBox
                        dgvPhieuKham.ItemsSource = null; // Xóa dữ liệu trong DataGrid
                    }
                    else if (dataTable.Rows.Count == 1)
                    {
                        // Hiển thị thông tin lên TextBox
                        DataRow row = dataTable.Rows[0];
                        TxB_MaPhieu.Text = row["MaPhieuKham"].ToString();
                        TxB_MaBenhNhan.Text = row["MaBenhNhan"].ToString();
                        TxB_BenhNhan.Text = row["TEN_BENHNHAN"].ToString();
                        TxB_BacSi.Text = row["TEN_BACSI"].ToString();
                        TxB_NgayKham.Text = Convert.ToDateTime(row["NgayKham"]).ToString("yyyy-MM-dd");
                        TxB_LyDoKham.Text = row["LyDoKhamBenh"].ToString();
                        TxB_KhamLamSan.Text = row["KhamLamSang"].ToString();
                        TxB_ChuanDoan.Text = row["ChanDoan"].ToString();
                        TxB_KetQua.Text = row["KetQuaKham"].ToString();
                        TxB_DieuTri.Text = row["DieuTri"].ToString();
                        TxB_MaBacSi.Text = row["MaBacSi"].ToString();

                        // Hiển thị dữ liệu vào DataGrid
                        dgvPhieuKham.ItemsSource = dataTable.DefaultView;
                    }
                    else
                    {
                        // Nếu có nhiều kết quả, chỉ hiển thị vào DataGrid
                        ClearFields(); // Xóa TextBox
                        dgvPhieuKham.ItemsSource = dataTable.DefaultView;
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
            TxB_MaPhieu.Text = "";
            TxB_MaBenhNhan.Text = "";
            TxB_NgayKham.Text = "";
            TxB_LyDoKham.Text = "";
            TxB_KhamLamSan.Text = "";
            TxB_ChuanDoan.Text = "";
            TxB_KetQua.Text = "";
            TxB_DieuTri.Text = "";
            TxB_MaBacSi.Text = "";
            TxB_BenhNhan.Text = "";
            TxB_BacSi.Text = "";
        }
    }
}
