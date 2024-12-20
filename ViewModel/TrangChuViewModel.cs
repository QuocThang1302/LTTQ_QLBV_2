using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts;
using QuanLyBenhVien.Repositories;
using System.Windows.Media;
using Microsoft.Data.SqlClient;
using System;

namespace QuanLyBenhVien.ViewModel
{
    class TrangChuViewModel : ViewModelBase
    {
        private readonly RepositoryBase _userRepository;

        public SeriesCollection SeriesCollectionPie { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection SeriesCollectionColumn { get; set; }
        public string[] Labels { get; set; }
        public string[] LabelsLine { get; set; }
        public Func<double, string> Values { get; set; }
        public int SLBS { get; set; }
        public int SLBN { get; set; }
        public int SLK { get; set; }
        public int SLCN { get; set; }

        public TrangChuViewModel()
        {
            _userRepository = new UserRepository();

            thongke();
            piechart();
            cartesian();
            column();
        }

        private void thongke()
        {
            using (SqlConnection conn = _userRepository.GetConnection())
            {
                string query = "SELECT COUNT(*) AS SoLuongBacSi FROM NhanVien WHERE RoleID = 'R02'";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SLBS = Convert.ToInt32(reader["SoLuongBacSi"]);
                }
                conn.Close();
            }
            using (SqlConnection conn = _userRepository.GetConnection())
            {
                string query = "SELECT COUNT(*) AS SoLuongBenhNhan FROM BenhNhan";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SLBN = Convert.ToInt32(reader["SoLuongBenhNhan"]);
                }
                conn.Close();
            }
            using (SqlConnection conn = _userRepository.GetConnection())
            {
                string query = "SELECT COUNT(*) AS SoLuongKhoa FROM Khoa";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SLK = Convert.ToInt32(reader["SoLuongKhoa"]);
                }
                conn.Close();
            }
            using (SqlConnection conn = _userRepository.GetConnection())
            {
                string query = "SELECT COUNT(*) AS SoLuongChuyenNganh FROM ChuyenNganh";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SLCN = Convert.ToInt32(reader["SoLuongChuyenNganh"]);
                }
                conn.Close();
            }
        }

        public void piechart()
        {
            SeriesCollectionPie = new SeriesCollection();
            string[] colorpie = new[] { "#4DA3D4", "#E5B7EA", "#BA94E0", "#A3D8F4" };
            int index = 0;

            using (SqlConnection conn = _userRepository.GetConnection())
            {
                string query = "SELECT COUNT(*) AS SoLuong, TenKhoa FROM Khoa, BenhNhan WHERE BenhNhan.MaKhoa = Khoa.MaKhoa\r\nGROUP BY TenKhoa";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SeriesCollectionPie.Add(new PieSeries
                    {
                        Title = reader["TenKhoa"].ToString(),
                        Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(reader["SoLuong"])) },
                        Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorpie[index])),
                        DataLabels = true
                    });
                    index++;
                }
            }
        }

        public void cartesian()
        {
            SeriesCollection = new SeriesCollection()
            {
                new LineSeries
                {
                   Title = "Lượt khám:",
                   PointGeometry = DefaultGeometries.Circle,
                   PointGeometrySize = 10
                }
            };

            LabelsLine = ["2", "3", "4", "5", "6", "7", "CN"];

            var data = new ChartValues<double> { };

            using (SqlConnection conn = _userRepository.GetConnection())
            {
                string query = "WITH DaysOfWeek AS (\r\n    SELECT 1 AS SoThu \r\n    UNION ALL SELECT 2 \r\n    UNION ALL SELECT 3 \r\n    UNION ALL SELECT 4\r\n    UNION ALL SELECT 5 \r\n    UNION ALL SELECT 6 \r\n    UNION ALL SELECT 7 \r\n)\r\nSELECT \r\n    ISNULL(COUNT(p.NgayKham), 0) AS SoLuong\r\nFROM \r\n    DaysOfWeek d\r\nLEFT JOIN \r\n    PhieuKhamBenh p\r\nON \r\n    DATEPART(WEEKDAY, p.NgayKham) = d.SoThu\r\nGROUP BY \r\n    d.SoThu\r\nORDER BY \r\n    d.SoThu;";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    data.Add(Convert.ToDouble(reader["SoLuong"]));
                }
            }

            SeriesCollection[0].Values = data;
        }

        public void column()
        {
            SeriesCollectionColumn = new SeriesCollection {
                new StackedColumnSeries
                {
                    Title = "Thuốc",
                    StackMode = StackMode.Values,
                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DBBDE2")),
                    DataLabels = true
                },
                 new StackedColumnSeries
                 {
                     Title = "Vật dụng",
                     StackMode = StackMode.Values,
                     Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#78C7D9")),
                     DataLabels = true
                 }       
            };

            Labels = new string[5];
            int index = 0;
            var dataVD = new ChartValues<double> { };
            var dataT = new ChartValues<double> { };

            using (SqlConnection conn = _userRepository.GetConnection())
            {
                string query = "WITH DoanhThuThuoc AS\r\n(\r\n    SELECT \r\n        FORMAT(HD.NgayLapHoaDon, 'MM/yyyy') AS Thang,\r\n        SUM(CT.GiaTien) AS TongDoanhThuThuoc\r\n    FROM \r\n        CTDonThuoc CT\r\n    JOIN \r\n        DonThuoc DT ON CT.MaDonThuoc = DT.MaDonThuoc\r\n\tJOIN\r\n\t\tHoaDon HD ON HD.MaHoaDon = DT.MaHoaDon\r\n    WHERE \r\n        HD.NgayLapHoaDon >= DATEADD(MONTH, -5, GETDATE()) -- Lọc 5 tháng gần đây\r\n    GROUP BY \r\n        FORMAT(HD.NgayLapHoaDon, 'MM/yyyy')\r\n),\r\nDoanhThuVatDung AS\r\n(\r\n    SELECT \r\n        FORMAT(HD.NgayLapHoaDon, 'MM/yyyy') AS Thang,\r\n        SUM(CT.ThanhTien) AS TongDoanhThuVatDung\r\n    FROM \r\n        CTHDVatDung CT\r\n    JOIN \r\n        HoaDon HD ON CT.MaHoaDon = HD.MaHoaDon\r\n    WHERE \r\n        HD.NgayLapHoaDon >= DATEADD(MONTH, -5, GETDATE()) -- Lọc 5 tháng gần đây\r\n    GROUP BY \r\n        FORMAT(HD.NgayLapHoaDon, 'MM/yyyy')\r\n)\r\nSELECT \r\n    ISNULL(TT.Thang, TV.Thang) AS Thang,\r\n    ISNULL(TT.TongDoanhThuThuoc, 0) AS DoanhThuThuoc,\r\n    ISNULL(TV.TongDoanhThuVatDung, 0) AS DoanhThuVatDung,\r\n    ISNULL(TT.TongDoanhThuThuoc, 0) + ISNULL(TV.TongDoanhThuVatDung, 0) AS TongDoanhThu\r\nFROM \r\n    DoanhThuThuoc TT\r\nFULL OUTER JOIN \r\n    DoanhThuVatDung TV ON TT.Thang = TV.Thang\r\nORDER BY \r\n    Thang;";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dataVD.Add(Convert.ToDouble(reader["DoanhThuVatDung"]));
                    dataT.Add(Convert.ToDouble(reader["DoanhThuThuoc"]));
                    Labels[index] = reader["Thang"].ToString();
                }
                index++;
            }

            SeriesCollectionColumn[0].Values = dataT;
            SeriesCollectionColumn[1].Values = dataVD;

            Values = value => value.ToString("N0") + "VNĐ";
        }
    }
}

