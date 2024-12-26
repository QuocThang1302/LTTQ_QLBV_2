using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyBenhVien.ViewModel
{
    class NhanVienViewModel : ViewModelBase
    {
        private string _maNhanVien;
        private string _ho;
        private string _chuyenNganh;
        private DateTime? _ngaySinh;
        private string _email;
        private string _chucVu;
        private string _ten;
        private string _gioiTinh;
        private string _cccd;
        private string _soDienThoai;
        private string _diaChi;
        private string _matKhau;
        // Thuộc tính mới để điều chỉnh UI
        private Visibility _btnMatKhauVisibility = Visibility.Hidden; // Mặc định ẩn
        public Visibility BtnMatKhauVisibility
        {
            get => _btnMatKhauVisibility;
            set
            {
                _btnMatKhauVisibility = value;
                OnPropertyChanged();
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //Thêm mới xong
        public void Reset()
        {
            MaNhanVien = string.Empty;
            Ho = string.Empty;
            Ten = string.Empty;
            ChuyenNganh = string.Empty;
            Email = string.Empty;
            ChucVu = string.Empty;
            GioiTinh = string.Empty;
            CCCD = string.Empty;
            SoDienThoai = string.Empty;
            DiaChi = string.Empty;
            NgaySinh = null;
        }
        public string MaNhanVien
        {
            get => _maNhanVien;
            set
            {
                if (_maNhanVien != value)
                {
                    _maNhanVien = value;
                    OnPropertyChanged(nameof(MaNhanVien));
                }
            }
        }

        public string Ho
        {
            get => _ho;
            set
            {
                if (_ho != value)
                {
                    _ho = value;
                    OnPropertyChanged(nameof(Ho));
                }
            }
        }

        public string ChuyenNganh
        {
            get => _chuyenNganh;
            set
            {
                if (_chuyenNganh != value)
                {
                    _chuyenNganh = value;
                    OnPropertyChanged(nameof(ChuyenNganh));
                }
            }
        }

        public DateTime? NgaySinh
        {
            get => _ngaySinh;
            set
            {
                if (_ngaySinh != value)
                {
                    _ngaySinh = value;
                    OnPropertyChanged(nameof(NgaySinh));
                }
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string ChucVu
        {
            get => _chucVu;
            set
            {
                if (_chucVu != value)
                {
                    _chucVu = value;
                    OnPropertyChanged(nameof(ChucVu));
                }
            }
        }

        public string Ten
        {
            get => _ten;
            set
            {
                if (_ten != value)
                {
                    _ten = value;
                    OnPropertyChanged(nameof(Ten));
                }
            }
        }

        public string GioiTinh
        {
            get => _gioiTinh;
            set
            {
                if (_gioiTinh != value)
                {
                    _gioiTinh = value;
                    OnPropertyChanged(nameof(GioiTinh));
                }
            }
        }

        public string CCCD
        {
            get => _cccd;
            set
            {
                if (_cccd != value)
                {
                    _cccd = value;
                    OnPropertyChanged(nameof(CCCD));
                }
            }
        }

        public string SoDienThoai
        {
            get => _soDienThoai;
            set
            {
                if (_soDienThoai != value)
                {
                    _soDienThoai = value;
                    OnPropertyChanged(nameof(SoDienThoai));
                }
            }
        }
        public string MatKhau
        {
            get => _matKhau;
            set
            {
                if (_matKhau != value)
                {
                    _matKhau = value;
                    OnPropertyChanged(nameof(MatKhau));
                }
            }
        }
        public string DiaChi
        {
            get => _diaChi;
            set
            {
                if (_diaChi != value)
                {
                    _diaChi = value;
                    OnPropertyChanged(nameof(DiaChi));
                }
            }
        }
    }
}
