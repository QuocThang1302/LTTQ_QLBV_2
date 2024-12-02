using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
using System.Data.SqlClient;
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
        }
        string strCon = @"Data Source=QUOCTHANG\SQLEXPRESS;Initial Catalog=BV;Integrated Security=True";
        SqlConnection sqlCon = null;
        SqlDataAdapter adapter = null;
        DataSet ds = null;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataRow dataRow = ds.Tables["tblNhanVien"].NewRow();
            dataRow["MaNhanVien"] = txtMaNhanVien.Text.Trim();
            dataRow["Ho"] = txtHo.Text.Trim();
            dataRow["Ten"] = txtTen.Text.Trim();
            dataRow["LoaiNhanVien"] = txtChucVu.Text.Trim();
            dataRow["MaChuyenNganh"] = txtChuyenNganh.Text.Trim();
            dataRow["GioiTinh"] = txtGioiTinh.Text.Trim();
            dataRow["CCCD"] = txtCCCD.Text.Trim();
            dataRow["SDT"] = txtSDT.Text.Trim();
            dataRow["NgaySinh"] = DBNull.Value;
            dataRow["Email"] = txtEmail.Text.Trim();
            dataRow["DiaChi"] = txtDiaChi.Text.Trim();
            
            
            ds.Tables["tblNhanVien"].Rows.Add(dataRow);

            int kq = adapter.Update(ds.Tables["tblNhanVien"]);
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
            string query = " select MaNhanVien, Ho, Ten, LoaiNhanVien, MaChuyenNganh, GioiTinh, CCCD, SDT, NgaySinh, Email, DiaChi  from NhanVien";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            ds = new DataSet();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter.Fill(ds, "tblNhanVien");
            sqlCon.Close();

            dgDanhSachNhanVien.ItemsSource = ds.Tables["tblNhanVien"].DefaultView;
        }

        private int vitri = -1;
        private void dgDanhSachNhanVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vitri = dgDanhSachNhanVien.SelectedIndex;
            if (vitri == -1) return;

            DataRow dataRow = ds.Tables["tblNhanVien"].Rows[vitri];

            txtMaNhanVien.Text = dataRow["MaNhanVien"].ToString();
            txtHo.Text = dataRow["Ho"].ToString();
            txtTen.Text = dataRow["Ten"].ToString();
            txtChucVu.Text = dataRow["LoaiNhanVien"].ToString();
            txtChuyenNganh.Text = dataRow["MaChuyenNganh"].ToString();
            txtGioiTinh.Text = dataRow["GioiTinh"].ToString();
            txtCCCD.Text = dataRow["CCCD"].ToString();
            txtSDT.Text = dataRow["SDT"].ToString();
            txtNgaySinh.Text = dataRow["NgaySinh"].ToString();
            txtEmail.Text = dataRow["Email"].ToString();
            txtDiaChi.Text = dataRow["DiaChi"].ToString();
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

                    DataRow dataRow = ds.Tables["tblNhanVien"].Rows[vitri];
                    dataRow.Delete();

                    // Cập nhật thay đổi vào cơ sở dữ liệu
                    int kq = adapter.Update(ds.Tables["tblNhanVien"]);

                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Cập nhật giao diện DataGrid
                        dgDanhSachNhanVien.ItemsSource = null;
                        dgDanhSachNhanVien.ItemsSource = ds.Tables["tblNhanVien"].DefaultView;

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
                DataRow dataRow = ds.Tables["tblNhanVien"].Rows[vitri];

                // Cập nhật dữ liệu từ các TextBox vào DataRow
                dataRow["MaNhanVien"] = txtMaNhanVien.Text.Trim();
                dataRow["Ho"] = txtHo.Text.Trim();
                dataRow["Ten"] = txtTen.Text.Trim();
                dataRow["LoaiNhanVien"] = txtChucVu.Text.Trim();
                dataRow["MaChuyenNganh"] = txtChuyenNganh.Text.Trim();
                dataRow["GioiTinh"] = txtGioiTinh.Text.Trim();
                dataRow["CCCD"] = txtCCCD.Text.Trim();
                dataRow["SDT"] = txtSDT.Text.Trim();
                dataRow["NgaySinh"] = DBNull.Value;
                dataRow["Email"] = txtEmail.Text.Trim();
                dataRow["DiaChi"] = txtDiaChi.Text.Trim();
                

                // Cập nhật thay đổi vào cơ sở dữ liệu
                int kq = adapter.Update(ds.Tables["tblNhanVien"]);

                if (kq > 0)
                {
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Cập nhật giao diện DataGrid
                    dgDanhSachNhanVien.ItemsSource = null;
                    dgDanhSachNhanVien.ItemsSource = ds.Tables["tblNhanVien"].DefaultView;

                    // Đặt lại vị trí dòng đã chọn
                    dgDanhSachNhanVien.SelectedIndex = vitri;
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
