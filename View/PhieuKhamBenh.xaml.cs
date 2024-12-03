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
using System.Diagnostics;

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
                dgDanhSach.ItemsSource = null; // Xóa dữ liệu trong DataGrid
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
                        dgDanhSach.ItemsSource = null; // Xóa dữ liệu trong DataGrid
                    }
                    else if (dataTable.Rows.Count == 1)
                    {
                        // Hiển thị thông tin lên TextBox
                        DataRow row = dataTable.Rows[0];
                        txtMaPhieu.Text = row["MaPhieuKham"].ToString();
                        txtMaBenhNhan.Text = row["MaBenhNhan"].ToString();
                        txtBenhNhan.Text = row["TEN_BENHNHAN"].ToString();
                        txtBacSi.Text = row["TEN_BACSI"].ToString();
                        txtNgayKham.Text = Convert.ToDateTime(row["NgayKham"]).ToString("yyyy-MM-dd");
                        txtLyDoKham.Text = row["LyDoKhamBenh"].ToString();
                        txtKhamLamSan.Text = row["KhamLamSang"].ToString();
                        txtChuanDoan.Text = row["ChanDoan"].ToString();
                        txtKetQua.Text = row["KetQuaKham"].ToString();
                        txtDieuTri.Text = row["DieuTri"].ToString();
                        txtMaBacSi.Text = row["MaBacSi"].ToString();

                        // Hiển thị dữ liệu vào DataGrid
                        dgDanhSach.ItemsSource = dataTable.DefaultView;
                    }
                    else
                    {
                        // Nếu có nhiều kết quả, chỉ hiển thị vào DataGrid
                        ClearFields(); // Xóa TextBox
                        dgDanhSach.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        string strCon = @"Data Source=QUOCTHANG\SQLEXPRESS;Initial Catalog=BV;Integrated Security=True";
        SqlConnection sqlCon = null;
        SqlDataAdapter adapter = null;
        DataSet ds = null;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                HienThiDanhSach();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
        }


        private void ClearFields()
        {
            txtMaPhieu.Text = "";
            txtMaBenhNhan.Text = "";
            txtNgayKham.Text = "";
            txtLyDoKham.Text = "";
            txtKhamLamSan.Text = "";
            txtChuanDoan.Text = "";
            txtKetQua.Text = "";
            txtDieuTri.Text = "";
            txtMaBacSi.Text = "";
            txtBenhNhan.Text = "";
            txtBacSi.Text = "";
        }

        private void HienThiDanhSach()
        {
            if (sqlCon == null)
            {
                sqlCon = new SqlConnection(strCon);
            }
            string query = "SELECT \r\n    PhieuKhamBenh.MaPhieuKham, \r\n    PhieuKhamBenh.MaBenhNhan, \r\n    BenhNhan.Ten, \r\n\tNgayKham,\r\n\tLyDoKhamBenh,\r\n\tChanDoan,\r\n\tKhamLamSang,\r\n\tKetQuaKham,\r\n    PhieuKhamBenh.MaBacSi, \r\n    NhanVien.Ten,\r\n\tDieuTri\r\nFROM \r\n    PhieuKhamBenh\r\nJOIN \r\n    BenhNhan ON PhieuKhamBenh.MaBenhNhan = BenhNhan.MaBenhNhan\r\nJOIN \r\n    NhanVien ON PhieuKhamBenh.MaBacSi = NhanVien.MaNhanVien;";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            ds = new DataSet();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter.Fill(ds, "tblPhieuKhamBenh");
            sqlCon.Close();

            dgDanhSach.ItemsSource = ds.Tables["tblPhieuKhamBenh"].DefaultView;
        }
    }
}
