using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;
using System.Data;
using QuanLyBenhVien.Repositories;

namespace QuanLyBenhVien.View
{
    /// <summary>
    /// Interaction logic for BenhAn.xaml
    /// </summary>
    public partial class BenhAn : UserControl
    {
        private readonly RepositoryBase _userRepository;
        public BenhAn()
        {
            _userRepository = new UserRepository();
            InitializeComponent();
            searchControl.Tmp = "Nhập mã bệnh án hoặc mã bệnh nhân";
            // Đăng ký sự kiện SearchButtonClicked
            searchControl.SearchButtonClicked += SearchControl_SearchButtonClicked;
            // Đăng ký sự kiện ClearButtonClicked cho nút X
            searchControl.ClearButtonClicked += SearchControl_ClearButtonClicked;

        }
        private void SearchControl_ClearButtonClicked(object sender, EventArgs e)
        {
            // Logic khi nút X được nhấn
            ClearFields();
            HienThiDanhSach();

        }
        private void SearchControl_SearchButtonClicked(object sender, string searchText)
        {

            string maBenhAn = searchText.Trim();

            if (string.IsNullOrEmpty(maBenhAn))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu trước khi tìm kiếm!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                
                return;
            }

           
            string query = "SELECT * FROM BenhAn JOIN BenhNhan ON BenhAn.MaBenhNhan=BenhNhan.MaBenhNhan WHERE MaBenhAn=@MaBenhAn OR BenhNhan.MaBenhNhan=@MaBenhAn";

            try
            {
                using (SqlConnection connection = _userRepository.GetConnection())
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
                      
                    }
                    else if (dataTable.Rows.Count == 1)
                    {
                        // Hiển thị thông tin lên TextBox
                        DataRow row = dataTable.Rows[0];
                        txtMaBenhAn.Text = row["MaBenhAn"].ToString();
                        txtMaBenhNhan.Text = row["MaBenhNhan"].ToString();
                        txtBenh.Text = row["Ten"].ToString();
                        txtTinhTrang.Text = row["TinhTrang"].ToString();
                        txtNgayTaoLap.Text = Convert.ToDateTime(row["NgayTaoLap"]).ToString("yyyy-MM-dd");
                        txtBenh.Text = row["Benh"].ToString();
                        txtHuongDieuTri.Text = row["DieuTri"].ToString();

                        // Hiển thị kết quả vào DataGrid
                        dgDanhSachBenhAn.ItemsSource = dataTable.DefaultView;
                    }
                    else
                    {
                        // Nếu có nhiều kết quả, chỉ hiển thị vào DataGrid
                        ClearFields();
                        dgDanhSachBenhAn.ItemsSource = dataTable.DefaultView;
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
            txtMaBenhAn.Text = "";
            txtMaBenhNhan.Text = "";
            txtBenh.Text = "";
            txtTinhTrang.Text = "";
            txtNgayTaoLap.Text = "";
            txtBenh.Text = "";
            txtHuongDieuTri.Text = "";
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
            string query = "  select MaBenhAn, BenhAn.MaBenhNhan, Ten,  NgayTaoLap, TinhTrang, Benh, DieuTri from BenhAn join BenhNhan on BenhAn.MaBenhNhan = BenhNhan.MaBenhNhan";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            ds = new DataSet();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter.Fill(ds, "tblBenhAn");
            sqlCon.Close();

            dgDanhSachBenhAn.ItemsSource = ds.Tables["tblBenhAn"].DefaultView;
        }
        private int vitri = -1;
        private void dgDanhSachBenhAn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vitri = dgDanhSachBenhAn.SelectedIndex;
            if (vitri == -1) return;

            DataRow dataRow = ds.Tables["tblBenhAn"].Rows[vitri];
            txtBenhNhan.Text = dataRow["Ten"].ToString();
            txtMaBenhAn.Text = dataRow["MaBenhAn"].ToString();
           
            txtMaBenhNhan.Text = dataRow["MaBenhNhan"].ToString();
            txtNgayTaoLap.Text = dataRow["NgayTaoLap"].ToString();
            txtBenh.Text = dataRow["Benh"].ToString();
            txtTinhTrang.Text = dataRow["TinhTrang"].ToString();
            txtHuongDieuTri.Text = dataRow["Dieutri"].ToString();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            DataRow dataRow = ds.Tables["tblBenhAn"].NewRow();
            dataRow["MaBenhAn"] = txtMaBenhAn.Text.Trim();
            dataRow["Ten"] = txtBenhNhan.Text.Trim();
            dataRow["MaBenhNhan"] = txtMaBenhNhan.Text.Trim();
            dataRow["NgayTaoLap"] = DateTime.TryParse(txtNgayTaoLap.Text.Trim(), out DateTime ngaySinh)
? ngaySinh.ToString("yyyy-MM-dd")
: throw new FormatException("Invalid date format");
            dataRow["Benh"] = txtBenh.Text.Trim();
            dataRow["TinhTrang"] = txtTinhTrang.Text.Trim();
            dataRow["DieuTri"] = txtHuongDieuTri.Text.Trim();


            ds.Tables["tblBenhAn"].Rows.Add(dataRow);

            int kq = adapter.Update(ds.Tables["tblBenhAn"]);
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
                DataRow dataRow = ds.Tables["tblBenhAn"].Rows[vitri];

                // Cập nhật dữ liệu từ các TextBox vào DataRow
                dataRow["MaBenhAn"] = txtMaBenhAn.Text.Trim();
                dataRow["Ten"] = txtBenhNhan.Text.Trim();
                dataRow["MaBenhNhan"] = txtMaBenhNhan.Text.Trim();
                dataRow["NgayTaoLap"] = DateTime.TryParse(txtNgayTaoLap.Text.Trim(), out DateTime ngaySinh)
? ngaySinh.ToString("yyyy-MM-dd")
: throw new FormatException("Invalid date format");
                dataRow["Benh"] = txtBenh.Text.Trim();
                dataRow["TinhTrang"] = txtTinhTrang.Text.Trim();
                dataRow["DieuTri"] = txtHuongDieuTri.Text.Trim();

                // Cập nhật thay đổi vào cơ sở dữ liệu
                int kq = adapter.Update(ds.Tables["tblBenhAn"]);

                if (kq > 0)
                {
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Cập nhật giao diện DataGrid
                    dgDanhSachBenhAn.ItemsSource = null;
                    dgDanhSachBenhAn.ItemsSource = ds.Tables["tblBenhAn"].DefaultView;

                    // Đặt lại vị trí dòng đã chọn
                    dgDanhSachBenhAn.SelectedIndex = vitri;
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
        private void ClearTextBoxes()
        {
            txtMaBenhAn.Clear();
            txtMaBenhNhan.Clear();
            txtNgayTaoLap.Clear();
            txtBenh.Clear();
            txtTinhTrang.Clear();
            txtHuongDieuTri.Clear();
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

                    DataRow dataRow = ds.Tables["tblBenhAn"].Rows[vitri];
                    dataRow.Delete();

                    // Cập nhật thay đổi vào cơ sở dữ liệu
                    int kq = adapter.Update(ds.Tables["tblBenhAn"]);

                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Cập nhật giao diện DataGrid
                        dgDanhSachBenhAn.ItemsSource = null;
                        dgDanhSachBenhAn.ItemsSource = ds.Tables["tblBenhAn"].DefaultView;

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
            ClearTextBoxes();
        }
    }
}
