﻿using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace QuanLyBenhVien.View
{
    /// <summary>
    /// Interaction logic for ChuyenNganh.xaml
    /// </summary>
    public partial class ChuyenNganh : Window
    {
        public ChuyenNganh()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string maChuyenNganh = TxB_MaChuyenNganh.Text.Trim();
            string chuyenNganh = TxB_ChuyenNganh.Text.Trim();
            string khoa = TxB_Khoa.Text.Trim();

            if (string.IsNullOrEmpty(maChuyenNganh) || string.IsNullOrEmpty(chuyenNganh) || string.IsNullOrEmpty(khoa))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=LAPTOP-702RPVLR;Initial Catalog=BV;Integrated Security=True"))
                {
                    conn.Open();

                    string insertQuery = "INSERT INTO ChuyenNganh (MaChuyenNganh, TenChuyenNganh, Khoa) VALUES (@MaChuyenNganh, @ChuyenNganh, @Khoa)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@MaChuyenNganh", maChuyenNganh);
                        insertCmd.Parameters.AddWithValue("@ChuyenNganh", chuyenNganh);
                        insertCmd.Parameters.AddWithValue("@Khoa", khoa);

                        try
                        {
                            int rowsAffected = insertCmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Thêm thông tin thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show("Thêm thông tin thất bại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        catch (SqlException sqlEx)
                        {
                            if (sqlEx.Number == 2627) // Vi phạm khóa chính
                            {
                                MessageBox.Show("Mã chuyên ngành đã tồn tại (vi phạm khóa chính)!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            else if (sqlEx.Number == 547) // Vi phạm khóa ngoại
                            {
                                MessageBox.Show("Dữ liệu nhập vào vi phạm ràng buộc khóa ngoại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            else
                            {
                                MessageBox.Show($"Lỗi SQL: {sqlEx.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {sqlEx.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi không xác định: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}