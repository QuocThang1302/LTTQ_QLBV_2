﻿using Microsoft.Data.SqlClient;
using QuanLyBenhVien.Model;
using QuanLyBenhVien.Repositories;
using QuanLyBenhVien.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace QuanLyBenhVien.ViewModel
{
    public class PhanCongViewModel : ViewModelBase
    {
        private readonly RepositoryBase _userRepository;

        private string _MaLichTruc;
        private string _MaBacSi;
        private DateTime? _NgayTruc;
        private string _PhanCong;
        private string _TrangThai;
        private NhanVienModel _SelectedNV;
        private PhanCongModel _SelectedPC;
        public ObservableCollection<PhanCongModel> DSPhanCong {  get; set; }
        public ObservableCollection<NhanVienModel> DSNhanVien { get; set; }
        private ObservableCollection<PhanCongModel> _filteredDS;
        public ObservableCollection<PhanCongModel> FilteredDS
        {
            get => _filteredDS;
            set
            {
                _filteredDS = value;
                OnPropertyChanged(nameof(FilteredDS));
            }
        }
        public string MaLichTruc
        {
            get => _MaLichTruc;
            set
            {
                _MaLichTruc = value;
                OnPropertyChanged(nameof(MaLichTruc));
            }
        }

        public string MaBacSi
        {
            get => _MaBacSi;
            set
            {
                _MaBacSi = value;
                OnPropertyChanged(nameof(MaBacSi));
            }
        }

        public DateTime? NgayTruc
        {
            get => _NgayTruc;
            set
            {
                _NgayTruc = value;
                OnPropertyChanged(nameof(NgayTruc));
            }
        }

        public string PhanCong
        {
            get => _PhanCong;
            set
            {
                _PhanCong = value;
                OnPropertyChanged(nameof(PhanCong));
            }
        }

        public string TrangThai
        {
            get => _TrangThai;
            set
            {
                _TrangThai = value;
                OnPropertyChanged(nameof(TrangThai));
            }
        }

        public NhanVienModel SelectedNV
        {
            get => _SelectedNV;
            set
            {
                _SelectedNV = value;
                OnPropertyChanged(nameof(SelectedNV));
                UpdateFilteredDS();
            }
        }

        public PhanCongModel SelectedPC
        {
            get => _SelectedPC;
            set
            {
                _SelectedPC = value;
                OnPropertyChanged(nameof(SelectedPC));
                MaLichTruc = SelectedPC?.MaLichTruc;
                MaBacSi = SelectedPC?.MaBacSi;
                NgayTruc = SelectedPC?.NgayTruc;
                PhanCong = SelectedPC?.PhanCong;
                TrangThai = SelectedPC?.TrangThai;
            }
        }
        public ICommand DongYCommand { get; }

        public PhanCongViewModel()
        {
            _userRepository = new UserRepository();
            DSNhanVien = new ObservableCollection<NhanVienModel>();
            DSPhanCong = new ObservableCollection<PhanCongModel>();

            LoadDSNhanVien();
            LoadDSPhanCong();

            FilteredDS = new ObservableCollection<PhanCongModel>(DSPhanCong);
            UpdateFilteredDS();

            DongYCommand = new ViewModelCommand(ExcecuteDongYCommand);
        }

        private void UpdateFilteredDS()
        {
            if (SelectedNV != null)
            {
                FilteredDS = new ObservableCollection<PhanCongModel>(DSPhanCong.Where(a => a.MaBacSi == SelectedNV.MaNhanVien));
            }
            else
                FilteredDS = new ObservableCollection<PhanCongModel>(DSPhanCong);
        }

        private void ExecuteQuery(Action action)
        {
            try
            {
                action(); // Thực thi hành động truyền vào
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Vi phạm khóa chính
                {
                    MessageBox.Show("Lỗi: Khóa chính đã tồn tại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ex.Number == 547) // Vi phạm khóa ngoại
                {
                    MessageBox.Show("Lỗi: Không thể xóa hoặc thêm do ràng buộc khóa ngoại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show($"Lỗi SQL: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi không xác định: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadDSPhanCong()
        {
            using (SqlConnection conn = _userRepository.GetConnection())
            {
                string query = "SELECT MaBacSi, MaLichTruc, NgayTruc, PhanCong, TrangThai FROM LichTruc";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DSPhanCong.Add(new PhanCongModel
                    {
                        MaLichTruc = reader["MaLichTruc"].ToString(),
                        MaBacSi = reader["MaBacSi"].ToString(),
                        NgayTruc = reader.GetDateTime(reader.GetOrdinal("NgayTruc")),
                        PhanCong = reader["PhanCong"].ToString(),
                        TrangThai = reader["TrangThai"].ToString()
                    });
                }
            }
        }

        private void LoadDSNhanVien()
        {
            using (SqlConnection conn = _userRepository.GetConnection())
            {
                string query = "SELECT Ho + ' ' + Ten As HoTen, MaNhanVien, MaChuyenNganh FROM NhanVien";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DSNhanVien.Add(new NhanVienModel
                    {
                        MaNhanVien = reader["MaNhanVien"].ToString(),
                        HoTen = reader["HoTen"].ToString(),
                        ChuyenNganh = reader["MaChuyenNganh"].ToString()
                    });
                }
            }
        }

        private void ExcecuteDongYCommand(object obj)
        {
            var newPhanCong = new PhanCongModel
            {
                MaLichTruc = MaLichTruc,
                MaBacSi = MaBacSi,
                NgayTruc = NgayTruc ?? DateTime.MinValue,
                PhanCong = PhanCong,
                TrangThai = TrangThai
            };

            ExecuteQuery(() =>
            {
                using (SqlConnection conn = _userRepository.GetConnection())
                {
                    string CheckQuery = "SELECT COUNT(*) FROM LichTruc WHERE MaLichTruc=@MaLichTruc";
                    SqlCommand cmd = new SqlCommand(CheckQuery, conn);
                    cmd.Parameters.AddWithValue("@MaLichTruc", newPhanCong.MaLichTruc);

                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    conn.Close();

                    if (count > 0)
                    {
                        string updatedQuery = @"UPDATE LichTruc 
                                                SET MaBacSi = @MaBacSi, NgayTruc = @NgayTruc, PhanCong = @PhanCong, TrangThai = @TrangThai
                                                WHERE MaLichTruc = @MaLichTruc";
                        SqlCommand updatedCommand = new SqlCommand(updatedQuery, conn);
                        updatedCommand.Parameters.AddWithValue("@MaLichTruc", newPhanCong.MaLichTruc);
                        updatedCommand.Parameters.AddWithValue("@MaBacSi", newPhanCong.MaBacSi);
                        updatedCommand.Parameters.AddWithValue("@NgayTruc", newPhanCong.NgayTruc);
                        updatedCommand.Parameters.AddWithValue("@PhanCong", newPhanCong.PhanCong);
                        updatedCommand.Parameters.AddWithValue("@TrangThai", newPhanCong.TrangThai);

                        conn.Open();
                        updatedCommand.ExecuteNonQuery();
                        MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    else
                    {
                        string query = "INSERT INTO LichTruc (MaLichTruc, MaBacSi, NgayTruc, PhanCong, TrangThai) VALUES (@MaLichTruc, @MaBacSi, @NgayTruc, @PhanCong, @TrangThai)";
                        SqlCommand insertedcmd = new SqlCommand(query, conn);
                        insertedcmd.Parameters.AddWithValue("@MaLichTruc", newPhanCong.MaLichTruc);
                        insertedcmd.Parameters.AddWithValue("@MaBacSi", newPhanCong.MaBacSi);
                        insertedcmd.Parameters.AddWithValue("@NgayTruc", newPhanCong.NgayTruc);
                        insertedcmd.Parameters.AddWithValue("@PhanCong", newPhanCong.PhanCong);
                        insertedcmd.Parameters.AddWithValue("@TrangThai", newPhanCong.TrangThai);

                        conn.Open();
                        insertedcmd.ExecuteNonQuery();
                        MessageBox.Show("Thêm dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                DSPhanCong.Clear();
                LoadDSPhanCong();
                FilteredDS = new ObservableCollection<PhanCongModel>(DSPhanCong);
            });
        }
    }
}
