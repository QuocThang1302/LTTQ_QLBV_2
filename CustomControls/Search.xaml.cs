using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyBenhVien.CustomControls
{
    public partial class Search : UserControl
    {
        // Delegate cho sự kiện tìm kiếm
        public delegate void SearchEventHandler(object sender, string searchText);

        // Sự kiện được kích hoạt khi nhấn nút Tìm kiếm
        public event EventHandler<string> SearchButtonClicked;

        public Search()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TmpProperty =
        DependencyProperty.Register(
            nameof(Tmp),
            typeof(string),
            typeof(Search),
            new PropertyMetadata(string.Empty, OnTmpChanged));

        public string Tmp
        {
            get { return (string)GetValue(TmpProperty); }
            set { SetValue(TmpProperty, value); }
        }

        private static void OnTmpChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as Search;
            if (control != null)
            {
                control.txtTmp.Text = e.NewValue?.ToString();
            }
        }



        // Xử lý sự kiện nút "Tìm kiếm" được nhấn
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            // Lấy nội dung từ TextBox (tbInput)
            string searchText = tbInput.Text.Trim();

            // Bắn sự kiện SearchButtonClicked
            SearchButtonClicked?.Invoke(this, searchText);
        }

        private void tbInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbInput.Text))
                txtTmp.Visibility = Visibility.Visible;
            else txtTmp.Visibility = Visibility.Hidden;
        }


    }
}
