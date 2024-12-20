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

        public TrangChuViewModel()
        {
            _userRepository = new UserRepository();

            piechart();
            cartesian();
            column();
        }

        public void piechart()
        {
            SeriesCollectionPie = new SeriesCollection();
            string[] colorpie = new[] { "#4DA3D4", "#E5B7EA", "#BA94E0", "#A3D8F4" };
            int index = 0;

            using (SqlConnection conn = _userRepository.GetConnection())
            {
                string query = "SELECT TenThuoc, CTDonThuoc.SoLuong FROM Thuoc, CTDonThuoc WHERE Thuoc.MaThuoc = CTDonThuoc.MaThuoc";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SeriesCollectionPie.Add(new PieSeries
                    {
                        Title = reader["TenThuoc"].ToString(),
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
                   //Values = new ChartValues<double> { 12, 26, 21, 13, 9, 5, 15},
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
                    Values = new ChartValues<double> {6,4,7,5,8},
                    StackMode = StackMode.Values,
                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DBBDE2")),
                    DataLabels = true
                },
                 new StackedColumnSeries
                 {
                     Title = "Dịch vụ",
                     Values = new ChartValues<double> {1,2,3,4,3},
                     StackMode = StackMode.Values,
                     Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#78C7D9")),
                     DataLabels = true
                 }};

            Labels = new[] { "1", "2", "3", "4", "5" };
            Values = value => value.ToString("N0") + "M";
        }
    }
}

