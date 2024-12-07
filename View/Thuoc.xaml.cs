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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Diagnostics;



namespace QuanLyBenhVien.View
{
    /// <summary>
    /// Interaction logic for Thuoc.xaml
    /// </summary>
    public partial class Thuoc : UserControl
    {
        public Thuoc()
        {
            InitializeComponent();
            searchControl.Tmp = "Nhập mã thuốc hoặc tên thuốc";
            // Đăng ký sự kiện SearchButtonClicked
            searchControl.SearchButtonClicked += SearchControl_SearchButtonClicked;
        }
        private void SearchControl_SearchButtonClicked(object sender, string searchText)
        {
            string maThuoc = searchText.Trim();

            if (string.IsNullOrEmpty(maThuoc))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                dgDanhSachThuoc.ItemsSource = null; // Xóa dữ liệu trong DataGrid
                ClearFields();
                return;
            }

            // Chuỗi kết nối tới cơ sở dữ liệu
            string connectionString = @"Data Source=QUOCTHANG\SQLEXPRESS;Initial Catalog=BV;Integrated Security=True";

            // Câu lệnh SQL để tìm kiếm thông tin thuốc
            string query = "SELECT * FROM Thuoc WHERE MaThuoc = @MaThuoc OR TenThuoc=@MaThuoc";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaThuoc", maThuoc);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearFields(); // Xóa các TextBox
                        dgDanhSachThuoc.ItemsSource = null; // Xóa dữ liệu trong DataGrid
                    }
                    else if (dataTable.Rows.Count == 1)
                    {
                        // Hiển thị thông tin lên các TextBox
                        DataRow row = dataTable.Rows[0];
                        tbMaThuoc.Text = row["MaThuoc"].ToString();
                        tbThuoc.Text = row["TenThuoc"].ToString();
                        tbCongDung.Text = row["CongDung"].ToString();
                        tbSoLuong.Text = row["SoLuong"].ToString();
                        tbGiaTien.Text = row["GiaTien"].ToString();
                        tbHSD.Text = Convert.ToDateTime(row["HanSuDung"]).ToString("yyyy-MM-dd");

                        // Xóa dữ liệu trong DataGrid nếu chỉ có một kết quả
                        dgDanhSachThuoc.ItemsSource = dataTable.DefaultView;
                    }
                    else
                    {
                        // Nếu có nhiều kết quả, chỉ hiển thị vào DataGrid
                        ClearFields(); // Xóa các TextBox
                        dgDanhSachThuoc.ItemsSource = dataTable.DefaultView;
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
            tbMaThuoc.Text = "";
            tbThuoc.Text = "";
            tbCongDung.Text = "";
            tbSoLuong.Text = "";
            tbGiaTien.Text = "";
            tbHSD.Text = "";
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

        private void HienThiDanhSach()
        {
            if (sqlCon == null)
            {
                sqlCon = new SqlConnection(strCon);
            }
            string query = "select MaThuoc, TenThuoc, CongDung, SoLuong, GiaTien, HanSuDung from Thuoc";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            ds = new DataSet();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter.Fill(ds, "tblThuoc");
            sqlCon.Close();

            dgDanhSachThuoc.ItemsSource = ds.Tables["tblThuoc"].DefaultView;
        }

        private int vitri = -1;

        private void dgDanhSachThuoc_Loaded(object sender, RoutedEventArgs e)
        {
            

        }

        private void dgDanhSachThuoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vitri = dgDanhSachThuoc.SelectedIndex;
            if (vitri == -1) return;

            DataRow dataRow = ds.Tables["tblThuoc"].Rows[vitri];

            tbMaThuoc.Text = dataRow["MaThuoc"].ToString();
            tbThuoc.Text = dataRow["TenThuoc"].ToString();
            tbCongDung.Text = dataRow["CongDung"].ToString();
            tbSoLuong.Text = dataRow["SoLuong"].ToString();
            tbGiaTien.Text = dataRow["GiaTien"].ToString();
            tbHSD.Text = dataRow["HanSuDung"].ToString();
        }
        private void ClearTextBoxes()
        {
            tbMaThuoc.Clear();
            tbSoLuong.Clear();
            tbThuoc.Clear();
            tbHSD.Clear();
            tbCongDung.Clear();
            tbGiaTien.Clear();
        }
        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            DataRow dataRow = ds.Tables["tblThuoc"].NewRow();
            dataRow["MaThuoc"] = tbMaThuoc.Text.Trim();
            dataRow["TenThuoc"] = tbThuoc.Text.Trim();
            dataRow["CongDung"] = tbCongDung.Text.Trim();
          
            dataRow["SoLuong"] = tbSoLuong.Text.Trim();
            dataRow["GiaTien"] = tbGiaTien.Text.Trim();
            dataRow["HanSuDung"] = tbHSD.Text.Trim();
           
            ds.Tables["tblThuoc"].Rows.Add(dataRow);

            int kq = adapter.Update(ds.Tables["tblThuoc"]);
            if (kq > 0)
            {
                MessageBox.Show("Thêm dữ liệu thành công!!!");
            }
            else
            {
                MessageBox.Show("Thêm dữ liệu thất bại!!");
            }
            ClearTextBoxes();
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
                DataRow dataRow = ds.Tables["tblThuoc"].Rows[vitri];

                // Cập nhật dữ liệu từ các TextBox vào DataRow
               
                dataRow["MaThuoc"] = tbMaThuoc.Text.Trim();
                dataRow["TenThuoc"] = tbThuoc.Text.Trim();
                dataRow["CongDung"] = tbCongDung.Text.Trim();

                dataRow["SoLuong"] = tbSoLuong.Text.Trim();
                dataRow["GiaTien"] = tbGiaTien.Text.Trim();
                dataRow["HanSuDung"] = tbHSD.Text.Trim();

                // Cập nhật thay đổi vào cơ sở dữ liệu
                int kq = adapter.Update(ds.Tables["tblThuoc"]);

                if (kq > 0)
                {
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Cập nhật giao diện DataGrid
                    dgDanhSachThuoc.ItemsSource = null;
                    dgDanhSachThuoc.ItemsSource = ds.Tables["tblThuoc"].DefaultView;

                    // Đặt lại vị trí dòng đã chọn
                    dgDanhSachThuoc.SelectedIndex = vitri;
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
            ClearTextBoxes();
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

                    DataRow dataRow = ds.Tables["tblThuoc"].Rows[vitri];
                    dataRow.Delete();

                    // Cập nhật thay đổi vào cơ sở dữ liệu
                    int kq = adapter.Update(ds.Tables["tblThuoc"]);

                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Cập nhật giao diện DataGrid
                        dgDanhSachThuoc.ItemsSource = null;
                        dgDanhSachThuoc.ItemsSource = ds.Tables["tblThuoc"].DefaultView;

                        // Xóa dữ liệu trong TextBox
                        ClearTextBoxes();
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

    }

}
