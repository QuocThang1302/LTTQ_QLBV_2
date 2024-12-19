using QuanLyBenhVien.Model;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System.Windows.Controls;

namespace QuanLyBenhVien.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public NhanVien GetNhanVienByID(string userID)
        {
            
            using (var connection = GetConnection())
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

        public bool AuthenticateUserAndCheckDoctor(NetworkCredential credential)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
            select RoleID 
            from NhanVien 
            where MaNhanVien = @userID and MatKhau = @password";
                command.Parameters.Add("@userID", System.Data.SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar).Value = credential.Password;

                var roleIdObj = command.ExecuteScalar();
                if (roleIdObj != null)
                {
                    string roleId = roleIdObj.ToString();
                    return roleId.Equals("R02", StringComparison.OrdinalIgnoreCase); // True nếu là Doctor, False nếu là Admin
                }
            }

            return false; // Không tìm thấy hoặc thông tin không chính xác
        }

        public bool PhanQuyen {  get; set; }
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from NhanVien where MaNhanVien=@userID and MatKhau=@password";
                command.Parameters.Add("@userID", System.Data.SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar).Value = credential.Password;
                validUser = command.ExecuteScalar() == null ? false : true;
            }
            return validUser;
        }

        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public UserModel GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetByID(string? name)
        {
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from NhanVien where MaNhanVien=@userID";
                command.Parameters.Add("@userID", System.Data.SqlDbType.NVarChar).Value = name;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = reader["MaNhanVien"].ToString(),
                            Password = string.Empty,
                            FirstName = reader["Ten"].ToString(),
                            LastName = reader["Ho"].ToString(),
                            Email = reader["Email"].ToString()
                        };
                    }
                }
            }
            return user;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
