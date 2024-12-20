using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using QuanLyBenhVien.Repositories;

namespace QuanLyBenhVien.View
{
    public partial class PhieuKhamBenh : UserControl
    {
        private readonly RepositoryBase _userRepository;
        public PhieuKhamBenh()
        {
            _userRepository = new UserRepository();
            InitializeComponent();
            searchControl.Tmp = "Nhập mã phiếu khám hoặc mã bác sĩ";
            // Đăng ký sự kiện SearchButtonClicked
            searchControl.SearchButtonClicked += SearchControl_SearchButtonClicked;
            // Đăng ký sự kiện ClearButtonClicked cho nút X
            searchControl.ClearButtonClicked += SearchControl_ClearButtonClicked;

        }
        private void SearchControl_ClearButtonClicked(object sender, EventArgs e)
        {
            // Logic khi nút X được nhấn
            HienThiDanhSach();
            ClearFields();
        }
        private void SearchControl_SearchButtonClicked(object sender, string searchText)
        {

            // Lấy mã phiếu khám từ tham số searchText
            string maPhieuKham = searchText.Trim();

            if (string.IsNullOrEmpty(maPhieuKham))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                
                return;
            }

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
                using (SqlConnection connection = _userRepository.GetConnection())
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
                sqlCon = _userRepository.GetConnection();
            }
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
        NhanVien NV ON NV.MaNhanVien = PK.MaBacSi";
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

        private void dgDanhSach_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vitri = dgDanhSach.SelectedIndex;
            if (vitri == -1) return;

            DataRow dataRow = ds.Tables["tblPhieuKhamBenh"].Rows[vitri];

            txtMaPhieu.Text = dataRow["MaPhieuKham"].ToString();
            txtMaBenhNhan.Text = dataRow["MaBenhNhan"].ToString();
            txtBenhNhan.Text = dataRow["TEN_BENHNHAN"].ToString();
            txtBacSi.Text = dataRow["TEN_BACSI"].ToString();
            txtNgayKham.Text = dataRow["NgayKham"].ToString();
            txtLyDoKham.Text = dataRow["LyDoKhamBenh"].ToString();
            txtKhamLamSan.Text = dataRow["KhamLamSang"].ToString();
            txtChuanDoan.Text = dataRow["ChanDoan"].ToString();
            txtKetQua.Text = dataRow["KetQuaKham"].ToString();
            txtDieuTri.Text = dataRow["DieuTri"].ToString();
            txtMaBacSi.Text = dataRow["MaBacSi"].ToString();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            DataRow dataRow = ds.Tables["tblPhieuKhamBenh"].NewRow();
            dataRow["MaPhieuKham"] = txtMaPhieu.Text.Trim();
            dataRow["TEN_BENHNHAN"] = txtBenhNhan.Text.Trim();

            dataRow["TEN_BACSI"] =txtBacSi.Text.Trim();
            dataRow["MaBenhNhan"] = txtBenhNhan.Text.Trim();
            dataRow["NgayKham"] = DateTime.TryParse(txtNgayKham.Text.Trim(), out DateTime ngaySinh) ? ngaySinh.ToString("yyyy-MM-dd") : throw new FormatException("Invalid date format");

            dataRow["LyDoKhamBenh"] = txtLyDoKham.Text.Trim();
            dataRow["KhamLamSang"] = txtKhamLamSan.Text.Trim();
            dataRow["DieuTri"] = txtDieuTri.Text.Trim();
            dataRow["ChanDoan"] = txtChuanDoan.Text.Trim();
            dataRow["KetQuaKham"] = txtKetQua.Text.Trim();
            dataRow["MaBacSi"] = txtMaBacSi.Text.Trim();


            ds.Tables["tblPhieuKhamBenh"].Rows.Add(dataRow);

            int kq = adapter.Update(ds.Tables["tblPhieuKhamBenh"]);
            if (kq > 0)
            {
                MessageBox.Show("Thêm dữ liệu thành công!!!");
                ClearFields();
            }
            else
            {
                MessageBox.Show("Thêm dữ liệu thất bại!!");
            }
            ClearFields();
        }

        private int vitri = -1;
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
                DataRow dataRow = ds.Tables["tblPhieuKhamBenh"].Rows[vitri];

                // Cập nhật dữ liệu từ các TextBox vào DataRow
                dataRow["MaPhieuKham"] = txtMaPhieu.Text.Trim();
                dataRow["TEN_BENHNHAN"] = txtBenhNhan.Text.Trim();

                dataRow["TEN_BACSI"] = txtBacSi.Text.Trim();
                dataRow["MaBenhNhan"] = txtBenhNhan.Text.Trim();
                dataRow["NgayKham"] = DateTime.TryParse(txtNgayKham.Text.Trim(), out DateTime ngaySinh) ? ngaySinh.ToString("yyyy-MM-dd") : throw new FormatException("Invalid date format");
                dataRow["LyDoKhamBenh"] = txtLyDoKham.Text.Trim();
                dataRow["KhamLamSang"] = txtKhamLamSan.Text.Trim();
                dataRow["DieuTri"] = txtDieuTri.Text.Trim();
                dataRow["ChanDoan"] = txtChuanDoan.Text.Trim();
                dataRow["KetQuaKham"] = txtKetQua.Text.Trim();
                dataRow["MaBacSi"] = txtMaBacSi.Text.Trim();


                // Cập nhật thay đổi vào cơ sở dữ liệu
                int kq = adapter.Update(ds.Tables["tblNhanVien"]);

                if (kq > 0)
                {
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Cập nhật giao diện DataGrid
                    dgDanhSach.ItemsSource = null;
                    dgDanhSach.ItemsSource = ds.Tables["tblNhanVien"].DefaultView;

                    // Đặt lại vị trí dòng đã chọn
                    dgDanhSach.SelectedIndex = vitri;
                    ClearFields();
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

                    DataRow dataRow = ds.Tables["tblPhieuKhamBenh"].Rows[vitri];
                    dataRow.Delete();

                    // Cập nhật thay đổi vào cơ sở dữ liệu
                    int kq = adapter.Update(ds.Tables["tblNhanVien"]);

                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Cập nhật giao diện DataGrid
                        dgDanhSach.ItemsSource = null;
                        dgDanhSach.ItemsSource = ds.Tables["tblNhanVien"].DefaultView;
                        ClearFields();
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
    }
}
