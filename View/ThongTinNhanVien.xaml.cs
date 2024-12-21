﻿using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;
using QuanLyBenhVien.Repositories;

namespace QuanLyBenhVien.View
{
    public partial class ThongTinNhanVien : UserControl
    {
        private readonly RepositoryBase _userRepository;
        public ThongTinNhanVien()
        {
            _userRepository = new UserRepository();
            InitializeComponent();
            searchControl.Tmp = "Nhập mã nhân viên hoặc tên nhân viên";
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

        SqlConnection sqlCon = null;
        SqlDataAdapter adapter = null;
        DataSet ds = null;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Tạo một DataRow mới nhưng không thêm ngay vào DataTable
                DataRow dataRow = ds.Tables["tblNhanVien"].NewRow();
                dataRow["MaNhanVien"] = txtMaNhanVien.Text.Trim();
                dataRow["Ho"] = txtHo.Text.Trim();
                dataRow["Ten"] = txtTen.Text.Trim();
                dataRow["LoaiNhanVien"] = txtChucVu.Text.Trim();
                dataRow["MaChuyenNganh"] = txtChuyenNganh.Text.Trim();
                dataRow["GioiTinh"] = txtGioiTinh.Text.Trim();
                dataRow["CCCD"] = txtCCCD.Text.Trim();
                dataRow["SDT"] = txtSDT.Text.Trim();

                // Xử lý định dạng ngày
                dataRow["NgaySinh"] = DateTime.TryParse(txtNgaySinh.Text.Trim(), out DateTime ngaySinh)
                    ? ngaySinh.ToString("yyyy-MM-dd")
                    : throw new FormatException("Invalid date format");

                dataRow["Email"] = txtEmail.Text.Trim();
                dataRow["DiaChi"] = txtDiaChi.Text.Trim();

                // Tạm thời thêm DataRow (chỉ khi không có lỗi xảy ra)
                ds.Tables["tblNhanVien"].Rows.Add(dataRow);

                // Cập nhật dữ liệu vào cơ sở dữ liệu
                int kq = adapter.Update(ds.Tables["tblNhanVien"]);
                if (kq > 0)
                {
                    MessageBox.Show("Thêm dữ liệu thành công!!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearFields();
                }
                else
                {
                    // Nếu không cập nhật được, xóa DataRow vừa thêm
                    ds.Tables["tblNhanVien"].Rows.Remove(dataRow);
                    MessageBox.Show("Thêm dữ liệu thất bại!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (SqlException ex)
            {
                // Xóa DataRow vừa thêm nếu gặp lỗi SQL
                if (ds.Tables["tblNhanVien"].Rows.Count > 0 && ds.Tables["tblNhanVien"].Rows[ds.Tables["tblNhanVien"].Rows.Count - 1].RowState == DataRowState.Added)
                {
                    ds.Tables["tblNhanVien"].Rows[ds.Tables["tblNhanVien"].Rows.Count - 1].Delete();
                }

                // Kiểm tra lỗi SQL dựa trên mã lỗi
                switch (ex.Number)
                {
                    case 2627: // Lỗi vi phạm PRIMARY KEY
                        MessageBox.Show("Khóa chính đã tồn tại trong cơ sở dữ liệu! Không thể thêm dữ liệu trùng lặp.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;

                    case 547: // Lỗi vi phạm FOREIGN KEY
                        MessageBox.Show("Dữ liệu không hợp lệ! Mã chuyên ngành không tồn tại trong hệ thống.", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;

                    default:
                        MessageBox.Show($"Lỗi SQL: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Định dạng ngày không hợp lệ: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi không xác định: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Nếu có lỗi và DataRow đã được thêm, xóa DataRow khỏi DataSet
                if (ds.Tables["tblNhanVien"].Rows.Count > 0 && ds.Tables["tblNhanVien"].Rows[ds.Tables["tblNhanVien"].Rows.Count - 1].RowState == DataRowState.Added)
                {
                    ds.Tables["tblNhanVien"].Rows[ds.Tables["tblNhanVien"].Rows.Count - 1].Delete();
                }
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
                sqlCon = _userRepository.GetConnection();
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
                       ClearFields();
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
                dataRow["NgaySinh"] = DateTime.TryParse(txtNgaySinh.Text.Trim(), out DateTime ngaySinh) ? ngaySinh.ToString("yyyy-MM-dd") : throw new FormatException("Invalid date format");
                dataRow["Email"] = txtEmail.Text.Trim();
                dataRow["DiaChi"] = txtDiaChi.Text.Trim();

                // Cập nhật thay đổi vào cơ sở dữ liệu
                int kq = adapter.Update(ds.Tables["tblNhanVien"]);

                if (kq > 0)
                {
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Cập nhật giao diện DataGrid chỉ khi thành công
                    dgDanhSachNhanVien.ItemsSource = null; // Xóa nguồn dữ liệu cũ
                    dgDanhSachNhanVien.ItemsSource = ds.Tables["tblNhanVien"].DefaultView; // Đặt lại nguồn dữ liệu mới

                    // Đặt lại vị trí dòng đã chọn
                    dgDanhSachNhanVien.SelectedIndex = vitri;
                    ClearFields(); // Xóa các trường nhập liệu
                }
                else
                {
                    MessageBox.Show("Cập nhật dữ liệu thất bại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (SqlException ex)
            {
                // Xử lý lỗi SQL (khóa chính và khóa ngoại)
                if (ex.Number == 2627) // Lỗi vi phạm PRIMARY KEY
                {
                    MessageBox.Show("Khóa chính đã tồn tại! Không thể cập nhật dữ liệu trùng lặp.", "Lỗi khóa chính", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ex.Number == 547) // Lỗi vi phạm FOREIGN KEY
                {
                    MessageBox.Show("Dữ liệu không hợp lệ! Mã chuyên ngành hoặc mã nhân viên không tồn tại trong hệ thống.", "Lỗi khóa ngoại", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show($"Lỗi SQL: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // Nếu có lỗi, không cập nhật DataGrid, không thay đổi dữ liệu
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Định dạng ngày không hợp lệ: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            ClearFields(); // Làm sạch các trường nhập liệu sau khi xử lý


        }
        private void SearchControl_SearchButtonClicked(object sender, string searchText)
        {


            string maNhanVien = searchText.Trim();

            if (string.IsNullOrEmpty(maNhanVien))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
               
                return;
            }

            

            // Câu lệnh SQL để tìm kiếm thông tin nhân viên
            string query = "SELECT * FROM NhanVien WHERE MaNhanVien = @MaNhanVien OR Ten = @MaNhanVien";

            try
            {
                using (SqlConnection connection = _userRepository.GetConnection())
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.Add("@MaNhanVien", SqlDbType.VarChar).Value = maNhanVien;

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
                        // Hiển thị thông tin lên các TextBox
                        DataRow row = dataTable.Rows[0];
                        txtMaNhanVien.Text = row["MaNhanVien"].ToString();
                        string ho = row["Ho"].ToString();
                        string ten = row["Ten"].ToString();
                        txtHo.Text = ho + " " + ten; // Hiển thị họ và tên đầy đủ
                        txtTen.Text = ten;
                        txtChuyenNganh.Text = row["MaChuyenNganh"].ToString();
                        txtChucVu.Text = row["LoaiNhanVien"].ToString();
                        txtNgaySinh.Text = Convert.ToDateTime(row["NgaySinh"]).ToString("yyyy-MM-dd");
                        txtGioiTinh.Text = row["GioiTinh"].ToString();
                        txtCCCD.Text = row["CCCD"].ToString();
                        txtDiaChi.Text = row["DiaChi"].ToString();
                        txtSDT.Text = row["SDT"].ToString();
                        txtEmail.Text = row["Email"].ToString();

                        // Xóa dữ liệu trong DataGrid nếu chỉ có một kết quả
                        dgDanhSachNhanVien.ItemsSource = dataTable.DefaultView;
                    }
                    else
                    {
                        // Nếu có nhiều kết quả, chỉ hiển thị vào DataGrid
                        ClearFields(); // Xóa các TextBox
                        dgDanhSachNhanVien.ItemsSource = dataTable.DefaultView;
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
            txtMaNhanVien.Text = "";
            txtHo.Text = "";
            txtTen.Text = "";
            txtChuyenNganh.Text = "";
            txtChucVu.Text = "";
            txtNgaySinh.Text = "";
            txtGioiTinh.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            txtCCCD.Text = "";
        }
    }
}



