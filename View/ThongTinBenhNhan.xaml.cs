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
            searchControl.Tmp = "Nhập mã bệnh nhân hoặc tên bệnh nhân";
            // Đăng ký sự kiện SearchButtonClicked
            searchControl.SearchButtonClicked += SearchControl_SearchButtonClicked;
            // Đăng ký sự kiện ClearButtonClicked cho nút X
            searchControl.ClearButtonClicked += SearchControl_ClearButtonClicked;

        }
        private void SearchControl_ClearButtonClicked(object sender, EventArgs e)
        {
            // Logic khi nút X được nhấn
            HienThiDanhSach();
        }
        private void SearchControl_SearchButtonClicked(object sender, string searchText)
        {
            string maBenhNhan = searchText.Trim();

            if (string.IsNullOrEmpty(maBenhNhan))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
               
                return;
            }

            // Chuỗi kết nối tới cơ sở dữ liệu
            string connectionString = "Data Source=LAPTOP-702RPVLR;Initial Catalog=BV;Integrated Security=True";

            // Câu lệnh SQL để tìm kiếm thông tin bệnh nhân
            string query = "SELECT * FROM BenhNhan WHERE MaBenhNhan=@MaBenhNhan OR Ten=@MaBenhNhan";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaBenhNhan", maBenhNhan);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        
                    }
                    else if (dataTable.Rows.Count == 1)
                    {
                        // Hiển thị thông tin lên TextBox
                        DataRow row = dataTable.Rows[0];
                        txtMaBenhNhan.Text = row["MaBenhNhan"].ToString();
                        txtHo.Text = row["Ho"].ToString();
                        txtTen.Text = row["Ten"].ToString();
                        txtNgaySinh.Text = Convert.ToDateTime(row["NgaySinh"]).ToString("yyyy-MM-dd");
                        txtGioiTinh.Text = row["GioiTinh"].ToString();
                        txtCCCD.Text = row["CCCD"].ToString();
                        txtNgheNghiep.Text = row["NgheNghiep"].ToString();
                        txtDiaChi.Text = row["DiaChi"].ToString();
                        txtSDT.Text = row["SDT"].ToString();
                        txtEmail.Text = row["Email"].ToString();
                        txtKhoa.Text = row["MaKhoa"].ToString();

                        // Hiển thị dữ liệu vào DataGrid
                        dgDanhSachBenhNhan.ItemsSource = dataTable.DefaultView;
                    }
                    else
                    {
                        // Nếu có nhiều kết quả, chỉ hiển thị vào DataGrid
                        CLearTextBoxes(); // Xóa các TextBox
                        dgDanhSachBenhNhan.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void CLearTextBoxes()
        {
            txtCCCD.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtGioiTinh.Clear();
            txtHo.Clear();
            txtKhoa.Clear();
            txtMaBenhNhan.Clear();
            txtNgheNghiep.Clear();
            txtSDT.Clear();
            txtTen.Clear();
            txtNgaySinh.Clear();
        }
        string strCon = @"Data Source=LAPTOP-702RPVLR;Initial Catalog=BV;Integrated Security=True";
        SqlConnection sqlCon = null;
        SqlDataAdapter adapter = null;
        DataSet ds = null;
        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            DataRow dataRow = ds.Tables["tblBenhNhan"].NewRow();
            dataRow["MaBenhNhan"] = txtMaBenhNhan.Text.Trim();
            dataRow["Ho"] = txtHo.Text.Trim();
            dataRow["Ten"] = txtTen.Text.Trim();
            dataRow["NgaySinh"] = Convert.ToDateTime(dataRow["NgaySinh"]).ToString("yyyy-MM-dd");
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
                CLearTextBoxes();
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
                        CLearTextBoxes();
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
                dataRow["NgaySinh"] = Convert.ToDateTime(dataRow["NgaySinh"]).ToString("yyyy-MM-dd");
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
                    CLearTextBoxes();

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
