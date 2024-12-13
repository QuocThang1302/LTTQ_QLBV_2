using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Separator = LiveCharts.Wpf.Separator;

namespace QuanLyBenhVien.View
{
    public partial class TrangChu : UserControl
    {
        public TrangChu()
        {
            InitializeComponent();

            piechart();
            cartesian();
            column();
        }

        public void piechart()
        {
            SeriesCollectionPie = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Paracetamol",
                    Values = new ChartValues<ObservableValue> {new ObservableValue(20)},
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Vitamin C",
                    Values = new ChartValues<ObservableValue> {new ObservableValue(15)},
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Amoxicillin",
                    Values = new ChartValues<ObservableValue> {new ObservableValue(10)},
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Ibuprofen",
                    Values = new ChartValues<ObservableValue> {new ObservableValue(5)},
                    DataLabels = true
                }
            };

            DataContext = this;
        }
        
        public void cartesian()
        {
            SeriesCollection = new SeriesCollection()
            {
                new LineSeries
                {
                   Title = "Lượt khám:",
                   Values = new ChartValues<double> { 12, 26, 21, 13, 9, 5, 15},
                   PointGeometry = DefaultGeometries.Circle,
                   PointGeometrySize = 10
                }
            };

            LabelsLine = ["2", "3", "4", "5", "6", "7", "CN"];
            DataContext = this;
        }

        public void column()
        {
            SeriesCollectionColumn = new SeriesCollection {
                new StackedColumnSeries
                {
                    Title = "Thuốc",
                    Values = new ChartValues<double> {6,4,7,5,8},
                    StackMode = StackMode.Values,
                    DataLabels = true
                },
                 new StackedColumnSeries
                 {
                     Title = "Dịch vụ",
                     Values = new ChartValues<double> {1,2,3,4,3},
                     StackMode = StackMode.Values,
                     DataLabels = true
                 }};

            AxisSeparator = new Separator
            {
                Step = 1, // Hiển thị nhãn mỗi bước là 1 (tức tháng 1, 2, 3, 4, 5)
                IsEnabled = true
            };
            Labels = new[] { "1", "2", "3", "4", "5" };
            Values = value => value.ToString("N0") + "M";
            DataContext = this;
        }
        
        public SeriesCollection SeriesCollectionPie { get; set; }
        public SeriesCollection SeriesCollection { get; set; } 
        public SeriesCollection SeriesCollectionColumn { get; set; }
        public string[] Labels { get; set; }
        public string[] LabelsLine { get; set; }
        public Func<double, string> Values { get; set; }
        public Separator AxisSeparator { get; set; }

        private void PieChart_DataClick(object sender, ChartPoint chartPoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartPoint.ChartView;
            foreach(PieSeries series in chart.Series)
            {
                series.PushOut = 0;
            }

            var selectedSeries = (PieSeries)chartPoint.SeriesView;
            selectedSeries.PushOut = 8;
        }
    }
}
