using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBenhVien.Model
{
    public class PhanCongModel
    {
        public string MaLichTruc {  get; set; }
        public string MaBacSi {  get; set; }
        public DateTime NgayTruc { get; set; }
        public string PhanCong { get; set; }
        public string TrangThai { get; set; }
    }
}
