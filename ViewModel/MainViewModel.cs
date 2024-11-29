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

namespace QuanLyBenhVien.ViewModel
{
    internal class MainViewModel : ViewModelBase
    {
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
            ShowBenhNhanViewCommand = new ViewModelCommand(ExecuteShowBenhNhanViewCommand);

            //Default view
            ExecuteShowBenhNhanViewCommand(null);

            LoadCurrentUserData();
        }

        private void ExecuteShowBenhNhanViewCommand(object obj)
        {
            CurrentChildView = new BenhNhanViewModel();
            Caption = "Bệnh nhân";
            Icon = IconChar.BedPulse;
        }

        // commands
        public ICommand ShowBenhNhanViewCommand { get; }

        private void LoadCurrentUserData()
        {
            //var user = _userRepository.GetByID("abcxyz");
            var user = _userRepository.GetByID(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount.UserID = user.Id;
                CurrentUserAccount.DisplayName = $"{user.LastName} {user.FirstName}";
                CurrentUserAccount.ProfilePicture = null;
            }
            else
            {
                CurrentUserAccount.DisplayName = "Đăng nhập không hợp lệ!!!";
                //Application.Current.Shutdown();
            }
        }
    }
}
