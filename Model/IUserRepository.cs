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
    }
    public interface IUserRepositoryExtended : IUserRepository
    {
        UserModel GetNhanVienByID(int userID);
    }
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        void Add(UserModel userModel);
        void Edit(UserModel userModel);
        void Remove(int id);
        UserModel GetByID(int id);
        UserModel GetByID(string? name);
        public NhanVien GetNhanVienByID(string userID)
        {
            string connectionString = "Data Source=DESKTOP-U5DJ7HG\\SQLEXPRESS01;Initial Catalog=BV;Integrated Security=True";
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "SELECT N.MaNhanVien, N.Ho, N.Ten, N.MaChuyenNganh, N.Email, \r\n                   R.TenRole AS ChucVu, N.GioiTinh, N.CCCD, N.SDT, N.DiaChi, N.NgaySinh\r\n            FROM NhanVien N\r\n            INNER JOIN Role R ON N.RoleID = R.RoleID\r\n            WHERE N.MaNhanVien = @UserID";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new NhanVien
                        {
                            MaNhanVien = reader["MaNhanVien"].ToString(),
                            Ho = reader["Ho"].ToString(),
                            Ten = reader["Ten"].ToString(),
                            ChuyenNganh = reader["MaChuyenNganh"].ToString(),
                            Email = reader["Email"].ToString(),
                            ChucVu = reader["ChucVu"].ToString(),
                            GioiTinh = reader["GioiTinh"].ToString(),
                            CCCD = reader["CCCD"].ToString(),
                            SoDienThoai = reader["SDT"].ToString(),
                            DiaChi = reader["DiaChi"].ToString(),
                            NgaySinh = DateTime.Parse(reader["NgaySinh"].ToString())
                        };
                    }
                }
            }
            
            return new NhanVien();
        }
    }
}
