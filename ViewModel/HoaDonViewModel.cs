using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBenhVien.ViewModel
{
    class HoaDonViewModel : ViewModelBase
    {
        public string MaHoaDon { get; set; }
        public string TenHoaDon { get; set; }
        public string MaBenhNhan { get; set; }
        public string BenhNhan { get; set; }
        public string MaNhanVien { get; set; }
        public string NhanVien { get; set; }
        public string NgayLap { get; set; }
    }
}
