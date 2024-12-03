using System;
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
    }
}
