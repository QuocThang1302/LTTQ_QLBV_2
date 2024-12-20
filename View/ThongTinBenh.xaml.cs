using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;
using System.Data;
using QuanLyBenhVien.Repositories;

namespace QuanLyBenhVien.View
{
    public partial class ThongTinBenh : UserControl
    {
        private readonly RepositoryBase _userRepository;
        public ThongTinBenh()
        {
            _userRepository = new UserRepository();
            InitializeComponent();
            searchControl.Tmp = "Nhập mã bệnh hoặc tên bệnh";
            // Đăng ký sự kiện SearchButtonClicked
            searchControl.SearchButtonClicked += SearchControl_SearchButtonClicked;
            // Đăng ký sự kiện ClearButtonClicked cho nút X
            searchControl.ClearButtonClicked += SearchControl_ClearButtonClicked;

        }
        private void SearchControl_ClearButtonClicked(object sender, EventArgs e)
        {
            // Logic khi nút X được nhấn
            HienThiDanhSach();
            ClearTextBoxes();
        }
        private void SearchControl_SearchButtonClicked(object sender, string searchText)
        {
            string maBenh = searchText.Trim();

            if (string.IsNullOrEmpty(maBenh))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                 // Xóa dữ liệu trong DataGrid
                return;
            }

            // Câu lệnh SQL để tìm kiếm thông tin bệnh
            string query = "SELECT * FROM Benh WHERE MaBenh=@MaBenh OR TenBenh=@MaBenh";

            try
            {
                using (SqlConnection connection = _userRepository.GetConnection())
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaBenh", maBenh);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                         // Xóa dữ liệu trong DataGrid
                    }
                    else if (dataTable.Rows.Count == 1)
                    {
                        // Hiển thị thông tin lên TextBox
                        DataRow row = dataTable.Rows[0];
                        tbMaBenh.Text = row["MaBenh"].ToString();
                        tbBenh.Text = row["TenBenh"].ToString();
                        tbMoTa.Text = row["MoTa"].ToString();
                        tbTrieuChung.Text = row["TrieuChung"].ToString();

                        // Hiển thị dữ liệu vào DataGrid
                        dgDanhSachBenh.ItemsSource = dataTable.DefaultView;
                    }
                    else
                    {
                        // Nếu có nhiều kết quả, chỉ hiển thị vào DataGrid
                        ClearTextBoxes(); // Xóa TextBox
                        dgDanhSachBenh.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
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
                sqlCon = _userRepository.GetConnection();
            }
            string query = "select MaBenh, TenBenh, MoTa, TrieuChung from Benh";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            ds = new DataSet();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter.Fill(ds, "tblBenh");
            sqlCon.Close();

            dgDanhSachBenh.ItemsSource = ds.Tables["tblBenh"].DefaultView;
        }
        private int vitri = -1;

        private void dgDanhSachBenh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vitri = dgDanhSachBenh.SelectedIndex;
            if (vitri == -1) return;

            DataRow dataRow = ds.Tables["tblBenh"].Rows[vitri];

            tbMaBenh.Text = dataRow["MaBenh"].ToString();
            tbBenh.Text = dataRow["TenBenh"].ToString();
            tbMoTa.Text = dataRow["MoTa"].ToString();
            tbTrieuChung.Text = dataRow["TrieuChung"].ToString();
            
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            DataRow dataRow = ds.Tables["tblBenh"].NewRow();
            dataRow["MaBenh"] = tbMaBenh.Text.Trim();
            dataRow["TenBenh"] = tbBenh.Text.Trim();
            dataRow["MoTa"] = tbMoTa.Text.Trim();
            dataRow["TrieuChung"] = tbTrieuChung.Text.Trim();
            

            ds.Tables["tblBenh"].Rows.Add(dataRow);

            int kq = adapter.Update(ds.Tables["tblBenh"]);
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

        private void ClearTextBoxes()
        {
            tbTrieuChung.Clear();
            tbMaBenh.Clear();
            tbMoTa.Clear();
            tbBenh.Clear();
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
                DataRow dataRow = ds.Tables["tblBenh"].Rows[vitri];

                // Cập nhật dữ liệu từ các TextBox vào DataRow

                dataRow["MaBenh"] = tbMaBenh.Text.Trim();
                dataRow["TenBenh"] = tbBenh.Text.Trim();
                dataRow["MoTa"] = tbMoTa.Text.Trim();
                dataRow["TrieuChung"] = tbTrieuChung.Text.Trim();

                // Cập nhật thay đổi vào cơ sở dữ liệu
                int kq = adapter.Update(ds.Tables["tblBenh"]);

                if (kq > 0)
                {
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Cập nhật giao diện DataGrid
                    dgDanhSachBenh.ItemsSource = null;
                    dgDanhSachBenh.ItemsSource = ds.Tables["tblBenh"].DefaultView;

                    // Đặt lại vị trí dòng đã chọn
                    dgDanhSachBenh.SelectedIndex = vitri;
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

                    DataRow dataRow = ds.Tables["tblBenh"].Rows[vitri];
                    dataRow.Delete();

                    // Cập nhật thay đổi vào cơ sở dữ liệu
                    int kq = adapter.Update(ds.Tables["tblBenh"]);

                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Cập nhật giao diện DataGrid
                        dgDanhSachBenh.ItemsSource = null;
                        dgDanhSachBenh.ItemsSource = ds.Tables["tblBenh"].DefaultView;

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
