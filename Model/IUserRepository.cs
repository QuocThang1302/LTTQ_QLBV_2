using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace QuanLyBenhVien.Model
{
    public class NhanVien
    {
        public string MaNhanVien { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string ChuyenNganh { get; set; }
        public string Email { get; set; }
        public string ChucVu { get; set; }
        public string GioiTinh { get; set; }
        public string CCCD { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string MatKhau {  get; set; }
    }

    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        void Add(UserModel userModel);
        void Edit(UserModel userModel);
        void Remove(int id);
        UserModel GetByID(int id);
        UserModel GetByID(string? name);
        NhanVien GetNhanVienByID(string userID);
        
    }
    
}
