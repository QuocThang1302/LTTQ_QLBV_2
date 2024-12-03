﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

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
            searchControl.Tmp = "Nhập mã nhân viên hoặc tên nhân viên";
            // Đăng ký sự kiện SearchButtonClicked
            searchControl.SearchButtonClicked += SearchControl_SearchButtonClicked;
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
        private void SearchControl_SearchButtonClicked(object sender, string searchText)
        {


            string maNhanVien = searchText.Trim();

            if (string.IsNullOrEmpty(maNhanVien))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                dgvNhanVien.ItemsSource = null; // Xóa dữ liệu trong DataGrid
                ClearFields();
                return;
            }

            // Chuỗi kết nối tới cơ sở dữ liệu
            string connectionString = "Data Source=LAPTOP-702RPVLR;Initial Catalog=BV;Integrated Security=True";

            // Câu lệnh SQL để tìm kiếm thông tin nhân viên
            string query = "SELECT * FROM NhanVien WHERE MaNhanVien = @MaNhanVien OR Ten = @MaNhanVien";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
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
                        ClearFields(); // Xóa các TextBox
                        dgvNhanVien.ItemsSource = null; // Xóa dữ liệu trong DataGrid
                    }
                    else if (dataTable.Rows.Count == 1)
                    {
                        // Hiển thị thông tin lên các TextBox
                        DataRow row = dataTable.Rows[0];
                        TxB_MaNhanVien.Text = row["MaNhanVien"].ToString();
                        string ho = row["Ho"].ToString();
                        string ten = row["Ten"].ToString();
                        TxB_HoTen.Text = ho + " " + ten; // Hiển thị họ và tên đầy đủ
                        TxB_Ten.Text = ten;
                        TxB_ChuyenNganh.Text = row["MaChuyenNganh"].ToString();
                        TxB_ChucVu.Text = row["LoaiNhanVien"].ToString();
                        TxB_NgaySinh.Text = Convert.ToDateTime(row["NgaySinh"]).ToString("yyyy-MM-dd");
                        TxB_GioiTinh.Text = row["GioiTinh"].ToString();
                        TxB_CCCD.Text = row["CCCD"].ToString();
                        TxB_DiaChi.Text = row["DiaChi"].ToString();
                        TxB_SDT.Text = row["SDT"].ToString();
                        TxB_Email.Text = row["Email"].ToString();

                        // Xóa dữ liệu trong DataGrid nếu chỉ có một kết quả
                        dgvNhanVien.ItemsSource = dataTable.DefaultView;
                    }
                    else
                    {
                        // Nếu có nhiều kết quả, chỉ hiển thị vào DataGrid
                        ClearFields(); // Xóa các TextBox
                        dgvNhanVien.ItemsSource = dataTable.DefaultView;
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
            TxB_MaNhanVien.Text = "";
            TxB_HoTen.Text = "";
            TxB_Ten.Text = "";
            TxB_ChuyenNganh.Text = "";
            TxB_ChucVu.Text = "";
            TxB_NgaySinh.Text = "";
            TxB_GioiTinh.Text = "";
            TxB_DiaChi.Text = "";
            TxB_SDT.Text = "";
            TxB_Email.Text = "";
            TxB_CCCD.Text = "";
        }
    }
}



