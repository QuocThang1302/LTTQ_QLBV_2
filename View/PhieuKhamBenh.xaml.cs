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
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;


namespace QuanLyBenhVien.View
{
    /// <summary>
    /// Interaction logic for PhieuKhamBenh.xaml
    /// </summary>
    public partial class PhieuKhamBenh : UserControl
    {
        public PhieuKhamBenh()
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
            string query = "SELECT \r\n    PhieuKhamBenh.MaPhieuKham, \r\n    PhieuKhamBenh.MaBenhNhan, \r\n    BenhNhan.Ten, \r\n\tNgayKham,\r\n\tLyDoKhamBenh,\r\n\tChanDoan,\r\n\tKhamLamSang,\r\n\tKetQuaKham,\r\n    PhieuKhamBenh.MaBacSi, \r\n    NhanVien.Ten,\r\n\tDieuTri\r\nFROM \r\n    PhieuKhamBenh\r\nJOIN \r\n    BenhNhan ON PhieuKhamBenh.MaBenhNhan = BenhNhan.MaBenhNhan\r\nJOIN \r\n    NhanVien ON PhieuKhamBenh.MaBacSi = NhanVien.MaNhanVien;";
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
    }
}
