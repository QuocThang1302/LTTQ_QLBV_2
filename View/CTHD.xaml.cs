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

namespace QuanLyBenhVien.View
{
    /// <summary>
    /// Interaction logic for CTHD.xaml
    /// </summary>
    public partial class CTHD : Window
    {
        public CTHD()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button Clicked!");
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button Clicked!");
        }

        private void btnThem2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button Clicked!");
        }

        private void btnXoa2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button Clicked!");
        }

        private void btnThanhToan_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button Clicked!");
        }

        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
