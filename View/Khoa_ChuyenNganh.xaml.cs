﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace QuanLyBenhVien.View
{
    /// <summary>
    /// Interaction logic for Khoa_ChuyenNganh.xaml
    /// </summary>
    public partial class Khoa_ChuyenNganh : UserControl
    {
        public Khoa_ChuyenNganh()
        {
            InitializeComponent();
            searchControl.Tmp = "Nhập mã khoa hoặc tên khoa";
            // Đăng ký sự kiện SearchButtonClicked
            searchControl.SearchButtonClicked += SearchControl_SearchButtonClicked;
        }

        private void SearchControl_SearchButtonClicked(object sender, string searchText)
        {
            // Lấy mã đơn thuốc từ tham số searchText
            string maKhoa = searchText.Trim();
            if (string.IsNullOrEmpty(maKhoa))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Chuỗi kết nối tới cơ sở dữ liệu
            string connectionString = "Data Source=QUOCTHANG\\SQLEXPRESS;Initial Catalog=BV;Integrated Security=True";

            // Câu lệnh SQL để tìm kiếm thông tin đơn thuốc và chi tiết đơn thuốc
            string queryKhoa = "SELECT * FROM Khoa WHERE MaKhoa = @MaKhoa OR TenKhoa = @MaKhoa";
            string queryChuyenNganh = "SELECT * FROM ChuyenNganh WHERE Khoa IN (SELECT MaKhoa From Khoa WHERE MaKhoa = @MaKhoa OR TenKhoa = @MaKhoa)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Hiển thị thông tin của Khoa
                    SqlDataAdapter adapterKhoa = new SqlDataAdapter(queryKhoa, connection);
                    adapterKhoa.SelectCommand.Parameters.AddWithValue("@MaKhoa", maKhoa);
                    DataTable dataTableKhoa = new DataTable();
                    adapterKhoa.Fill(dataTableKhoa);

                    // Gắn dữ liệu vào dgvKhoa
                    dgvKhoa.ItemsSource = dataTableKhoa.DefaultView;

                    // Kiểm tra nếu không tìm thấy đơn thuốc
                    if (dataTableKhoa.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Xóa dữ liệu của DataGridView nếu không có kết quả
                        dgvChuyenNganh.ItemsSource = null;
                        dgvKhoa.ItemsSource = null;
                        return;
                    }

                    // Hiển thị thông tin của ChuyenNganh
                    SqlDataAdapter adapterChuyenNganh = new SqlDataAdapter(queryChuyenNganh, connection);
                    adapterChuyenNganh.SelectCommand.Parameters.AddWithValue("@MaKhoa", maKhoa);
                    DataTable dataTableChuyenNganh = new DataTable();
                    adapterChuyenNganh.Fill(dataTableChuyenNganh);

                    // Gắn dữ liệu vào dgvChuyenNganh
                    dgvChuyenNganh.ItemsSource = dataTableChuyenNganh.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ChuyenNganh chuyenNganh = new ChuyenNganh();
           // chuyenNganh.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Khoa khoa = new Khoa();
           // khoa.Show();
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
        string strCon = @"Data Source=QUOCTHANG\SQLEXPRESS;Initial Catalog=BV;Integrated Security=True";
        SqlConnection sqlCon = null;
        SqlDataAdapter adapter = null;
        DataSet ds = null;
        SqlDataAdapter adapter1 = null;
        DataSet ds1 = null;
        private void HienThiDanhSach()
        {
            if (sqlCon == null)
            {
                sqlCon = new SqlConnection(strCon);
            }
            string query = "select MaKhoa, TenKhoa, TruongKhoa  From Khoa   join NhanVien   on Khoa.TruongKhoa = NhanVien.MaNhanVien";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            ds = new DataSet();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter.Fill(ds, "tblKhoa");
            sqlCon.Close();

            dgvKhoa.ItemsSource = ds.Tables["tblKhoa"].DefaultView;

            string query1 = "select MaChuyenNganh, TenChuyenNganh, Khoa from ChuyenNganh join Khoa on ChuyenNganh.Khoa = Khoa.MaKhoa";
            adapter1 = new SqlDataAdapter(query1, sqlCon);
            SqlCommandBuilder builder1 = new SqlCommandBuilder(adapter1);

            ds1 = new DataSet();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter1.Fill(ds1, "tblChuyenNganh");
            sqlCon.Close();

            dgvChuyenNganh.ItemsSource = ds1.Tables["tblChuyenNganh"].DefaultView;
        }
        private int vitri1 = -1;
        private int vitri2 = -1;

        private void btnXoa1_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn dòng nào chưa
            if (vitri1 == -1)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Xác nhận từ người dùng trước khi xóa
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa khoa này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Xóa dòng được chọn trong DataTable
                    DataRow dataRow = ds.Tables["tblKhoa"].Rows[vitri1];
                    dataRow.Delete();

                    // Cập nhật thay đổi vào cơ sở dữ liệu
                    int kq = adapter.Update(ds.Tables["tblKhoa"]);

                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Cập nhật giao diện DataGrid
                        dgvKhoa.ItemsSource = null;
                        dgvKhoa.ItemsSource = ds.Tables["tblKhoa"].DefaultView;
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

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void dgvKhoa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vitri1 = dgvKhoa.SelectedIndex;
            if (vitri1 == -1) return;
        }

        private void dgvChuyenNganh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vitri2 = dgvChuyenNganh.SelectedIndex;
            if (vitri2 == -1) return;
        }

        private void btnXoa2_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn dòng nào chưa
            if (vitri2 == -1)
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
                    DataRow dataRow = ds1.Tables["tblChuyenNganh"].Rows[vitri2];
                    dataRow.Delete();

                    // Cập nhật thay đổi vào cơ sở dữ liệu
                    int kq = adapter.Update(ds1.Tables["tblChuyenNganh"]);

                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Cập nhật giao diện DataGrid
                        dgvKhoa.ItemsSource = null;
                        dgvKhoa.ItemsSource = ds1.Tables["tblChuyenNganh"].DefaultView;
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
