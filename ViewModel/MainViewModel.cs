using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using FontAwesome.Sharp;
using System.Windows.Input;
using QuanLyBenhVien.Model;
using QuanLyBenhVien.Repositories;
using QuanLyBenhVien.View;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace QuanLyBenhVien.ViewModel
{
    internal class MainViewModel : ViewModelBase
    {

        private string _userID;
        private NhanVienViewModel _currentEmployee;

        public string? UserID
        {
            get
            {
                // Truy xuất UserID từ Application.Current.Properties
                return Application.Current.Properties.Contains("UserID") ? Application.Current.Properties["UserID"] as string : null;
            }
            set
            {
                if (value != UserID)
                {
                    // Cập nhật UserID vào Application.Current.Properties
                    Application.Current.Properties["UserID"] = value;
                    OnPropertyChanged(nameof(UserID));
                }
            }
        }
        // Display name này để lưu trữ tên người dùng
        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                if (_displayName != value)
                {
                    _displayName = value;
                    OnPropertyChanged(nameof(DisplayName));
                }
            }
        }
        //fields
        private UserAccountModel _currentUserAccount;
        private IUserRepository _userRepository;

        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        internal UserAccountModel CurrentUserAccount { get => _currentUserAccount; set { _currentUserAccount = value; OnPropertyChanged(nameof(CurrentUserAccount)); } }

        public ViewModelBase CurrentChildView
        {
            get => _currentChildView;
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public string Caption
        {
            get => _caption;
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        public MainViewModel()
        {
            _userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            //Initialize commands
            ShowTrangChuViewCommand = new ViewModelCommand(ExecuteShowTrangChuViewCommand);
            ShowBenhNhanViewCommand = new ViewModelCommand(ExecuteShowBenhNhanViewCommand);
            ShowNhanVienViewCommand = new ViewModelCommand(ExecuteShowNhanVienViewCommand);
            ShowLichTrucViewCommand = new ViewModelCommand(ExecuteShowLichTrucViewCommand);
            ShowLichHenKhamViewCommand = new ViewModelCommand(ExecuteShowLichHenKhamViewCommand);
            ShowKhoaViewCommand = new ViewModelCommand(ExecuteShowKhoaViewCommand);
            ShowBenhAnViewCommand = new ViewModelCommand(ExecuteShowBenhAnViewCommand);
            ShowPhieuKhamBenhViewCommand = new ViewModelCommand(ExecuteShowPhieuKhamBenhViewCommand);
            ShowDonThuocViewCommand = new ViewModelCommand(ExecuteShowDonThuocViewCommand);
            ShowBenhViewCommand = new ViewModelCommand(ExecuteShowBenhViewCommand);
            ShowThuocViewCommand = new ViewModelCommand(ExecuteShowThuocViewCommand);
            ShowVatDungViewCommand = new ViewModelCommand(ExecuteShowVatDungViewCommand);
            ShowHoaDonViewCommand = new ViewModelCommand(ExecuteShowHoaDonViewCommand);
            ShowTTCaNhanViewCommand = new ViewModelCommand(ExecuteShowTTCaNhanViewCommand);
            LogoutCommand = new ViewModelCommand(ExecuteLogOutCommand);
            //Default view
            ExecuteShowTrangChuViewCommand(null);

            LoadCurrentUserData();
            DisplayName = CurrentUserAccount.DisplayName;
        }

        private void ExecuteLogOutCommand(object obj)
        {
            // Đăng xuất: điều hướng trở lại màn hình đăng nhập
            Application.Current.Dispatcher.Invoke(() =>
            {
                var loginWindow = new LoginView();
                loginWindow.Show();

                Application.Current.MainWindow = loginWindow;

                Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Close();

                loginWindow.IsVisibleChanged += (s, ev) =>
                {
                    if (loginWindow.IsVisible == false && loginWindow.IsLoaded)
                    {
                        Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            var mainWindow = new MainWindow();
                            mainWindow.Show();

                            // Đảm bảo không gọi Close() ngay lập tức
                            loginWindow.Close();
                        });
                    }
                };
            });
        }
        

        private void ExecuteShowTTCaNhanViewCommand(object obj)
        {
            var nhanVienData = _userRepository.GetNhanVienByID(UserID);
            if (nhanVienData != null)
            {
                CurrentChildView = new NhanVienViewModel
                {
                    BtnMatKhauVisibility = Visibility.Visible,
                    // Cập nhật thuộc tính UI thử thôi 
                    MaNhanVien = nhanVienData.MaNhanVien,
                    Ho = nhanVienData.Ho,
                    Ten = nhanVienData.Ten,
                    ChuyenNganh = nhanVienData.ChuyenNganh,
                    Email = nhanVienData.Email,
                    ChucVu = nhanVienData.ChucVu,
                    GioiTinh = nhanVienData.GioiTinh,
                    CCCD = nhanVienData.CCCD,
                    SoDienThoai = nhanVienData.SoDienThoai,
                    DiaChi = nhanVienData.DiaChi,
                    NgaySinh = nhanVienData.NgaySinh
                    //MatKhau = nhanVienData.MatKhau
                    
                   
                    
                };
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin nhân viên!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            Caption = "Nhân viên";
            Icon = IconChar.IdCard;
        }

        private void ExecuteShowHoaDonViewCommand(object obj)
        {
            CurrentChildView = new HoaDonViewModel();
            Caption = "Hóa đơn";
            Icon = IconChar.FileInvoiceDollar;
        }

        private void ExecuteShowVatDungViewCommand(object obj)
        {
            CurrentChildView = new VatDungViewModel();
            Caption = "Vật dụng";
            Icon = IconChar.Microscope;
        }

        private void ExecuteShowThuocViewCommand(object obj)
        {
            CurrentChildView = new ThuocViewModel();
            Caption = "Thuốc";
            Icon = IconChar.Capsules;
        }

        private void ExecuteShowBenhViewCommand(object obj)
        {
            CurrentChildView = new BenhViewModel();
            Caption = "Bệnh";
            Icon = IconChar.VirusCovid;
        }

        private void ExecuteShowDonThuocViewCommand(object obj)
        {
            CurrentChildView = new DonThuocViewModel();
            Caption = "Đơn thuốc";
            Icon = IconChar.HouseMedical;
        }

        private void ExecuteShowPhieuKhamBenhViewCommand(object obj)
        {
            CurrentChildView = new PhieuKhamBenhViewModel();
            Caption = "Phiếu khám bệnh";
            Icon = IconChar.FilePrescription;
        }

        private void ExecuteShowBenhAnViewCommand(object obj)
        {
            CurrentChildView = new BenhAnViewModel();
            Caption = "Bệnh án";
            Icon = IconChar.FileMedical;
        }

        private void ExecuteShowKhoaViewCommand(object obj)
        {
            CurrentChildView = new KhoaViewModel();
            Caption = "Khoa";
            Icon = IconChar.Users;
        }

        private void ExecuteShowLichHenKhamViewCommand(object obj)
        {
            CurrentChildView = new LichHenKhamViewModel();
            Caption = "Lịch hẹn khám";
            Icon = IconChar.CalendarCheck;
        }

        private void ExecuteShowLichTrucViewCommand(object obj)
        {
            CurrentChildView = new LichTrucViewModel();
            Caption = "Lịch trực";
            Icon = IconChar.CalendarDays;
        }

        private void ExecuteShowNhanVienViewCommand(object obj)
        {
            CurrentChildView = new NhanVienViewModel();
            Caption = "Nhân viên";
            Icon = IconChar.IdCard;
        }

        private void ExecuteShowTrangChuViewCommand(object obj)
        {
            CurrentChildView = new TrangChuViewModel();
            Caption = "Trang chủ";
            Icon = IconChar.Home;
        }

        private void ExecuteShowBenhNhanViewCommand(object obj)
        {
            CurrentChildView = new BenhNhanViewModel();
            Caption = "Bệnh nhân";
            Icon = IconChar.BedPulse;
        }

        // commands
        public ICommand ShowBenhNhanViewCommand { get; }
        public ICommand ShowTrangChuViewCommand { get; }
        public ICommand ShowNhanVienViewCommand { get; }
        public ICommand ShowLichTrucViewCommand { get; }
        public ICommand ShowLichHenKhamViewCommand { get; }
        public ICommand ShowKhoaViewCommand { get; }
        public ICommand ShowBenhAnViewCommand { get; }
        public ICommand ShowPhieuKhamBenhViewCommand { get; }
        public ICommand ShowDonThuocViewCommand { get; }
        public ICommand ShowBenhViewCommand { get; }
        public ICommand ShowThuocViewCommand { get; }
        public ICommand ShowVatDungViewCommand { get; }
        public ICommand ShowHoaDonViewCommand { get; }
        public ICommand ShowTTCaNhanViewCommand { get; }
        public ICommand LogoutCommand { get; }
        private void LoadCurrentUserData()
        {
            if (Thread.CurrentPrincipal?.Identity?.Name != null)
            {
                var user = _userRepository?.GetByID(Thread.CurrentPrincipal.Identity.Name);
                if (user != null)
                {
                    CurrentUserAccount.UserID = user.Id;
                    CurrentUserAccount.DisplayName = $"{user.LastName} {user.FirstName}";
                    CurrentUserAccount.ProfilePicture = null;
                }
                else
                {
                    CurrentUserAccount.DisplayName = "Đăng nhập không hợp lệ!!!";
                }
            }
            else
            {
                CurrentUserAccount.DisplayName = "Đăng nhập không hợp lệ!!!";
            }
        }
    }
}
