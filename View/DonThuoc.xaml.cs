using QuanLyBenhVien.Repositories;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace QuanLyBenhVien.View
{
    public partial class DonThuoc : UserControl
    {
        private readonly RepositoryBase _userRepository;
        public DonThuoc()
        {
            _userRepository = new UserRepository();
            InitializeComponent();
            searchControl.Tmp = "Nhập mã đơn thuốc hoặc mã bác sĩ";
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
            // Lấy mã đơn thuốc từ tham số searchText
            string maDonThuoc = searchText.Trim();
            if (string.IsNullOrEmpty(maDonThuoc))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Câu lệnh SQL để tìm kiếm thông tin đơn thuốc và chi tiết đơn thuốc
            string queryDonThuoc = "SELECT * FROM DonThuoc WHERE MaDonThuoc = @MaDonThuoc OR MaBacSi=@MaDonThuoc";
            string queryCTDonThuoc = "SELECT * FROM CTDonThuoc JOIN Thuoc ON CTDonThuoc.MaThuoc = Thuoc.MaThuoc\r\nWHERE MaDonThuoc IN (SELECT MaDonThuoc FROM DonThuoc WHERE MaBacSi = @MaDonThuoc OR MaDonThuoc = @MaDonThuoc)";

            using (SqlConnection connection = _userRepository.GetConnection())
            {
                try
                {
                    connection.Open();

                    // Hiển thị thông tin của DonThuoc
                    SqlDataAdapter adapterDonThuoc = new SqlDataAdapter(queryDonThuoc, connection);
                    adapterDonThuoc.SelectCommand.Parameters.AddWithValue("@MaDonThuoc", maDonThuoc);
                    DataTable dataTableDonThuoc = new DataTable();
                    adapterDonThuoc.Fill(dataTableDonThuoc);

                    // Gắn dữ liệu vào dgvDonThuoc
                    dgvDonThuoc.ItemsSource = dataTableDonThuoc.DefaultView;

                    // Kiểm tra nếu không tìm thấy đơn thuốc
                    if (dataTableDonThuoc.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    }

                    // Hiển thị thông tin của CTDonThuoc
                    SqlDataAdapter adapterCTDonThuoc = new SqlDataAdapter(queryCTDonThuoc, connection);
                    adapterCTDonThuoc.SelectCommand.Parameters.AddWithValue("@MaDonThuoc", maDonThuoc);
                    DataTable dataTableCTDonThuoc = new DataTable();
                    adapterCTDonThuoc.Fill(dataTableCTDonThuoc);

                    // Gắn dữ liệu vào dgvCTDonThuoc
                    dgvCTDonThuoc.ItemsSource = dataTableCTDonThuoc.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DonThuoc_CTDT ctdt = new DonThuoc_CTDT();
            ctdt.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CTHD hd = new CTHD();
            hd.Show();
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
        
        SqlConnection sqlCon = null;
        SqlDataAdapter adapter = null;
        DataSet ds = null;
        SqlDataAdapter adapter1 = null;
        DataSet ds1 = null;
        private void HienThiDanhSach()
        {
            if (sqlCon == null)
            {
                sqlCon = _userRepository.GetConnection();
            }
            string query = "SELECT \r\n    MaDonThuoc, \r\n    DonThuoc.MaBenhNhan, \r\n    MaBacSi, \r\n    NgayLapDon, \r\n    DonThuoc.MaHoaDon\r\nFROM DonThuoc \r\nJOIN BenhNhan ON DonThuoc.MaBenhNhan = BenhNhan.MaBenhNhan \r\nJOIN NhanVien NV ON DonThuoc.MaBacSi = NV.MaNhanVien \r\nJOIN HoaDon HD ON DonThuoc.MaHoaDon = HD.MaHoaDon;";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            ds = new DataSet();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter.Fill(ds, "tblDonThuoc");
            sqlCon.Close();

            dgvDonThuoc.ItemsSource = ds.Tables["tblDonThuoc"].DefaultView;

            string query1 = " Select TenThuoc, CTDonThuoc.SoLuong, CTDonThuoc.GiaTien, HuongDanSuDung from CTDonThuoc join Thuoc on CTDonThuoc.MaThuoc = Thuoc.MaThuoc";
            adapter1 = new SqlDataAdapter(query1, sqlCon);
            SqlCommandBuilder builder1 = new SqlCommandBuilder(adapter1);

            ds1 = new DataSet();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter1.Fill(ds1, "tblChiTiet");
            sqlCon.Close();

            dgvCTDonThuoc.ItemsSource = ds1.Tables["tblChiTiet"].DefaultView;
        }
        private int vitri = -1;
        private void dgvCTDonThuoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vitri = dgvCTDonThuoc.SelectedIndex;
            if (vitri == -1) return;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn dòng nào chưa
            if (vitri == -1)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Xác nhận từ người dùng trước khi xóa
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Xóa dòng được chọn trong DataTable
                    DataRow dataRow = ds1.Tables["tblChiTiet"].Rows[vitri];
                    dataRow.Delete();

                    // Cập nhật thay đổi vào cơ sở dữ liệu
                    int kq = adapter.Update(ds1.Tables["tblChiTiet"]);

                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Cập nhật giao diện DataGrid
                        dgvCTDonThuoc.ItemsSource = null;
                        dgvCTDonThuoc.ItemsSource = ds1.Tables["tblChiTiet"].DefaultView;
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
