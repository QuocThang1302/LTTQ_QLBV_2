using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using QuanLyBenhVien.ViewModel;

namespace QuanLyBenhVien.View
{
    public partial class TrangChu : UserControl
    {
        public TrangChu()
        {
            InitializeComponent();
            TrangChuViewModel trangChuViewModel = new TrangChuViewModel();
            DataContext = trangChuViewModel;
        }

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

        private void CartesianChart_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
