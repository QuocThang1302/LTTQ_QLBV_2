using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace QuanLyBenhVien.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;
        public RepositoryBase()
        {
            _connectionString = "Data Source=DESKTOP-U5DJ7HG\\SQLEXPRESS01;Initial Catalog=BV;Integrated Security=True;Trust Server Certificate=True";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
