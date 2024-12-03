﻿using System;
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
    /// Interaction logic for VatDung.xaml
    /// </summary>
    public partial class VatDung : UserControl
    {
        public VatDung()
        {
            InitializeComponent();
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
            string query = "select MaVatDung, TenVatDung, MoTa, SoLuong, Gia, MaQuanLy from VatDung";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            ds = new DataSet();
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            adapter.Fill(ds, "tblVatDung");
            sqlCon.Close();

            dgDanhSachVatDung.ItemsSource = ds.Tables["tblVatDung"].DefaultView;
        }
        private int vitri = -1;
        private void ClearTextBoxes()
        {
            tbGiaTien.Clear();
            tbMaVatDung.Clear();
            tbSoLuong.Clear();
            tbMoTa.Clear();
            tbQuanLy.Clear();
            tbVatDung.Clear();
            
        }
        private void dgDanhSachVatDung_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vitri = dgDanhSachVatDung.SelectedIndex;
            if (vitri == -1) return;

            DataRow dataRow = ds.Tables["tblVatDung"].Rows[vitri];

            tbMaVatDung.Text = dataRow["MaVatDung"].ToString();
            tbVatDung.Text = dataRow["TenVatDung"].ToString();
            tbMoTa.Text = dataRow["MoTa"].ToString();
            tbSoLuong.Text = dataRow["SoLuong"].ToString();
            tbGiaTien.Text = dataRow["Gia"].ToString();
            tbQuanLy.Text = dataRow["MaQuanLy"].ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataRow dataRow = ds.Tables["tblVatDung"].NewRow();
            dataRow["MaVatDung"] = tbMaVatDung.Text.Trim();
            dataRow["TenVatDung"] = tbVatDung.Text.Trim();
            dataRow["MoTa"] = tbMoTa.Text.Trim();
            dataRow["SoLuong"] = tbSoLuong.Text.Trim();
            dataRow["Gia"] = tbGiaTien.Text.Trim();
            dataRow["MaQuanLy"] = tbQuanLy.Text.Trim();

            ds.Tables["tblVatDung"].Rows.Add(dataRow);

            int kq = adapter.Update(ds.Tables["tblVatDung"]);
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
                DataRow dataRow = ds.Tables["tblVatDung"].Rows[vitri];

                // Cập nhật dữ liệu từ các TextBox vào DataRow

                dataRow["MaVatDung"] = tbMaVatDung.Text.Trim();
                dataRow["TenVatDung"] = tbVatDung.Text.Trim();
                dataRow["MoTa"] = tbMoTa.Text.Trim();
                dataRow["SoLuong"] = tbSoLuong.Text.Trim();
                dataRow["Gia"] = tbGiaTien.Text.Trim();
                dataRow["MaQuanLy"] = tbQuanLy.Text.Trim();

                // Cập nhật thay đổi vào cơ sở dữ liệu
                int kq = adapter.Update(ds.Tables["tblVatDung"]);

                if (kq > 0)
                {
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Cập nhật giao diện DataGrid
                    dgDanhSachVatDung.ItemsSource = null;
                    dgDanhSachVatDung.ItemsSource = ds.Tables["tblVatDung"].DefaultView;

                    // Đặt lại vị trí dòng đã chọn
                    dgDanhSachVatDung.SelectedIndex = vitri;
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

                    DataRow dataRow = ds.Tables["tblVatDung"].Rows[vitri];
                    dataRow.Delete();

                    // Cập nhật thay đổi vào cơ sở dữ liệu
                    int kq = adapter.Update(ds.Tables["tblVatDung"]);

                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Cập nhật giao diện DataGrid
                        dgDanhSachVatDung.ItemsSource = null;
                        dgDanhSachVatDung.ItemsSource = ds.Tables["tblVatDung"].DefaultView;

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
