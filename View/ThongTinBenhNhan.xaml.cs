using System;
using System.Collections.Generic;
using System.Data;
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
using System.Data.SqlClient;
using System.Windows.Shapes;

namespace QuanLyBenhVien.View
{
    /// <summary>
    /// Interaction logic for ThongTinBenhNhan.xaml
    /// </summary>
    public partial class ThongTinBenhNhan : UserControl
    {
        public ThongTinBenhNhan()
        {
            InitializeComponent();
        }

        string strCon = @"Data Source=DESKTOP-U5DJ7HG\SQLEXPRESS01;Initial Catalog=BV;Integrated Security=True";
        SqlConnection sqlCon = null;
        SqlDataAdapter adapter = null;
        DataSet ds = null;
        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            DataRow dataRow = ds.Tables["tblBenhNhan"].NewRow();
            dataRow["MaBenhNhan"] = txtMaBenhNhan.Text.Trim();
            dataRow["Ho"] = txtHo.Text.Trim();
            dataRow["Ten"] = txtTen.Text.Trim();
            dataRow["NgaySinh"] = DBNull.Value;
            dataRow["GioiTinh"] = txtGioiTinh.Text.Trim();
            dataRow["NgheNghiep"] = txtNgheNghiep.Text.Trim();
            dataRow["CCCD"] = txtCCCD.Text.Trim();
            dataRow["SDT"] = txtSDT.Text.Trim();
            dataRow["MaKhoa"] = txtKhoa.Text.Trim();
            dataRow["Email"] = txtEmail.Text.Trim();
            dataRow["DiaChi"] = txtDiaChi.Text.Trim();
            ds.Tables["tblBenhNhan"].Rows.Add(dataRow);

            int kq = adapter.Update(ds.Tables["tblBenhNhan"]);
            if (kq > 0)
            {
                MessageBox.Show("Thêm dữ liệu thành công!!!");
            }
            else
            {
                MessageBox.Show("Thêm dữ liệu thất bại!!");
            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            HienThiDanhSach();
        }

        private void HienThiDanhSach()
        {
            if (sqlCon == null)
            {
                sqlCon = new SqlConnection(strCon);
            }
            string query = "  select MaBenhNhan, Ho, Ten, NgaySinh, GioiTinh, NgheNghiep, CCCD, SDT, MaKhoa, Email, DiaChi from BenhNhan";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            ds = new DataSet();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter.Fill(ds, "tblBenhNhan");
            sqlCon.Close();

            dgDanhSachBenhNhan.ItemsSource = ds.Tables["tblBenhNhan"].DefaultView;
        }
    }
}
