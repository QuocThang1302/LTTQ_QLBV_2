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
using System.Diagnostics;

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
            try
            {
                HienThiDanhSach();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
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

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (vitri == -1)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa bệnh nhân này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                   
                    DataRow dataRow = ds.Tables["tblBenhNhan"].Rows[vitri];
                    dataRow.Delete();

                    // Cập nhật thay đổi vào cơ sở dữ liệu
                    int kq = adapter.Update(ds.Tables["tblBenhNhan"]);

                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Cập nhật giao diện DataGrid
                        dgDanhSachBenhNhan.ItemsSource = null;
                        dgDanhSachBenhNhan.ItemsSource = ds.Tables["tblBenhNhan"].DefaultView;

                        // Xóa dữ liệu trong TextBox
                        //ClearTextBoxes();
                    }
                    else
                    {
                        MessageBox.Show("Xóa dữ liệu thất bại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int vitri = -1;
        private void dgDanhSachBenhNhan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vitri = dgDanhSachBenhNhan.SelectedIndex;
            if (vitri == -1) return;

            DataRow dataRow = ds.Tables["tblBenhNhan"].Rows[vitri];

            txtMaBenhNhan.Text = dataRow["MaBenhNhan"].ToString();
            txtHo.Text = dataRow["Ho"].ToString();
            txtTen.Text = dataRow["Ten"].ToString();
            txtNgaySinh.Text = dataRow["NgaySinh"].ToString();
            txtGioiTinh.Text = dataRow["GioiTinh"].ToString();
            txtNgheNghiep.Text = dataRow["NgheNghiep"].ToString();
            txtCCCD.Text = dataRow["CCCD"].ToString();
            txtSDT.Text = dataRow["SDT"].ToString();
            txtKhoa.Text = dataRow["MaKhoa"].ToString();
            txtEmail.Text = dataRow["Email"].ToString();
            txtDiaChi.Text = dataRow["DiaChi"].ToString();
        }

        private void btnCapNhat_Click(object sender, RoutedEventArgs e)
        {
            if (vitri == -1)
            {
                MessageBox.Show("Vui lòng chọn một dòng để cập nhật!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Lấy dòng dữ liệu được chọn trong DataSet
                DataRow dataRow = ds.Tables["tblBenhNhan"].Rows[vitri];

                // Cập nhật dữ liệu từ các TextBox vào DataRow
                dataRow["MaBenhNhan"] = txtMaBenhNhan.Text.Trim();
                dataRow["Ho"] = txtHo.Text.Trim();
                dataRow["Ten"] = txtTen.Text.Trim();
                dataRow["NgaySinh"] = string.IsNullOrEmpty(txtNgaySinh.Text.Trim()) ? DBNull.Value : DateTime.Parse(txtNgaySinh.Text.Trim());
                dataRow["GioiTinh"] = txtGioiTinh.Text.Trim();
                dataRow["NgheNghiep"] = txtNgheNghiep.Text.Trim();
                dataRow["CCCD"] = txtCCCD.Text.Trim();
                dataRow["SDT"] = txtSDT.Text.Trim();
                dataRow["MaKhoa"] = txtKhoa.Text.Trim();
                dataRow["Email"] = txtEmail.Text.Trim();
                dataRow["DiaChi"] = txtDiaChi.Text.Trim();

                // Cập nhật thay đổi vào cơ sở dữ liệu
                int kq = adapter.Update(ds.Tables["tblBenhNhan"]);

                if (kq > 0)
                {
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Cập nhật giao diện DataGrid
                    dgDanhSachBenhNhan.ItemsSource = null;
                    dgDanhSachBenhNhan.ItemsSource = ds.Tables["tblBenhNhan"].DefaultView;

                    // Đặt lại vị trí dòng đã chọn
                    dgDanhSachBenhNhan.SelectedIndex = vitri;
                }
                else
                {
                    MessageBox.Show("Cập nhật dữ liệu thất bại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
