   M S B A M L   `   `   `  ����I  EQuanLyBenhVien, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:QuanLyBenhVien.ViewModelQuanLyBenhVien.ViewModel  5   	   9!clr-namespace:QuanLyBenhVien.ViewQuanLyBenhVien.View  5   	   M+clr-namespace:QuanLyBenhVien.CustomControlsQuanLyBenhVien.CustomControls  5   	       QuanLyBenhVien.MainWindow   5   
   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <	viewModel&clr-namespace:QuanLyBenhVien.ViewModel     5   	   3local!clr-namespace:QuanLyBenhVien.View     5   	   FcustomControls+clr-namespace:QuanLyBenhVien.CustomControls     5   	   X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  5	   	     � Title$  
MainWindow��5   	   $	��720��6   $
��1280��6)    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � AllowsTransparency$
 True��5   	   
������  � DataContext 5      ,  &QuanLyBenhVien.ViewModel.MainViewModel  5       � 	Resources 5       �� DataType0  *QuanLyBenhVien.ViewModel.BenhNhanViewModel*   $QuanLyBenhVien.View.ThongTinBenhNhan0  *QuanLyBenhVien.ViewModel.TrangChuViewModel"   QuanLyBenhVien.View.TrangChu0  *QuanLyBenhVien.ViewModel.NhanVienViewModel*   $QuanLyBenhVien.View.ThongTinNhanVien0  *QuanLyBenhVien.ViewModel.LichTrucViewModel"	   QuanLyBenhVien.View.LichTruc3
  -QuanLyBenhVien.ViewModel.LichHenKhamViewModel%   QuanLyBenhVien.View.LichHenKham,  &QuanLyBenhVien.ViewModel.KhoaViewModel*   $QuanLyBenhVien.View.Khoa_ChuyenNganh.  (QuanLyBenhVien.ViewModel.BenhAnViewModel    QuanLyBenhVien.View.BenhAn5  /QuanLyBenhVien.ViewModel.PhieuKhamBenhViewModel'   !QuanLyBenhVien.View.PhieuKhamBenh0  *QuanLyBenhVien.ViewModel.DonThuocViewModel"   QuanLyBenhVien.View.DonThuoc,  &QuanLyBenhVien.ViewModel.BenhViewModel&    QuanLyBenhVien.View.ThongTinBenh-  'QuanLyBenhVien.ViewModel.ThuocViewModel   QuanLyBenhVien.View.Thuoc/  )QuanLyBenhVien.ViewModel.VatDungViewModel!   QuanLyBenhVien.View.VatDung.  (QuanLyBenhVien.ViewModel.HoaDonViewModel    QuanLyBenhVien.View.HoaDon��%  (��       5      *, +)(�� 7     5      *, +)(�� n     5      *, +)(�� �     5"      *, +)(�� �     5&      *,
 +)(��     5*      *, +)(�� J    5.      *, +)(�� �    52      *, +)(�� �    56      *, +)(�� �    5:      *, +)(�� &    5>      *, +)(�� ]    5B      *, +)(�� �    5F      *, +)�� 5   
   "  6   .R�5        6'   5      �� 5   
   "  6   .R�5        6   5      �� 5   
   "  6   .R�5        6'   5       �� 5"   
   "  6   .R�5#      	  6   5$      �� 5&   
   " 
 6   .R�5'        6"   5(      �� 5*   
   "  6   .R�5+        6'   5,      �� 5.   
   "  6   .R�5/        6   50      �� 52   
   "  6   .R�53        6$   54      �� 56   
   "  6   .R�57        6   58      �� 5:   
   "  6   .R�5;        6#   5<      �� 5>   
   "  6   .R�5?        6   5@      �� 5B   
   "  6   .R�5C        6   5D      �� 5F   
   "  6   .R�5G        6   5H      5I      .��5K      ��  �� CornerRadius$ 10��6   $��1q�5L   	   ��5M   
   �� 5N       �� 
StartPoint$
 0, 0&�6"   	 �� EndPoint$
	 1, 1&�64   .��5O      � 
 � Color$
 #03045E��6    � Offset$ 0Q�6/   � 5P      $
 #0077B6��6   $	 0.6Q�6/   � 5Q      $
 #CAF0F8��6   $ 1Q�6/   5R      ._�5U   
   �  � ColumnDefinitions 5V      �� 5W      $	��280 �6#   �� 5X      $��* �6#   .+�5[      �� $ 	10,0,0,10��6   $��0��5\      ���#00416a._�5^      �� .+�5_      �� $	��140��6   $	��140��6)   
������ $��1q�5a      ���#d4ebf8$��	0,30,0,15q�5c      $ 90��5d      ._�5e      �� $��/Images/hospital_logo.png��6    $
��Fill=�5f       $��0q�5g       $��Center=�5h       $��Center=�5i       $	��180��5j       $	��180��6,   5k      �� 5m      $��Spring Aroma Hopital��6    $
��Bold"�5n      $��23(�5o      ���#d4ebf8$��Calibri)�5q       �� TextAlignment$ Center=�5r      $��0,5,0,30q�5s      � 5u      $
��Auto��6   .+�5v      �� $��Hidden=�6'   $��Disabled=�5w   '   $	��440��5x   '   .��5y      �� .+�5z   "   	� -   5         	�Name$ btnTrangChu��5z   .      
MenuButton#��[  5{   .    d� 	IsChecked$
 TrueY�5}   .   ��5|   .   �� *ShowTrangChuViewCommand+.��5   &   ��  �� Orientation$ 
Horizontal=�61   .+�5�   *   !  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$
 Home=�67     MenuIcon#��[ 6C   �� 5�   *   $��Trang chủ��64     MenuText#��[ 6E   5�   '   5�   #   9� 5�   "   -   5         9�Name$ 
exNhanVien��5�   +     CustomExpander#��[ 5�   +   ��5�   &   ]� 5�   *   .+�5�   .   	� -   5        $ btnNhanVien��5�   :   #��[  5�   :   .��5�   2   �� $ 
Horizontal=�6=   .+�5�   6     $ HospitalUser=�6C   #��[ 6W   �� 5�   6   $��Nhân viên��6@   #��[ 6Q   5�   3   5�   /   5�   +   .��5�   &   �� 
������ .+�5�   *   	� -   5        $ btnTTNhanVien��5�   6     SubMenuButton#��[ 5�   6   ��5�   6   �� *ShowNhanVienViewCommand+.��5�   .   �� $ 
Horizontal=�69   .+�5�   2     $ IdCard=�6?     SubMenuIcon#��[ 6M   �� 5�   2   $��
Thông tin��6<     SubMenuText#��[ 6M   5�   /   5�   +   	� 5�   *   -   5        $ btnLichTruc��5�   6   #��[ 5�   6   ��5�   6   �� *ShowLichTrucViewCommand+.��5�   .   �� $ 
Horizontal=�69   .+�5�   2     $ CalendarDays=�6?   #��[ 6S   �� 5�   2   $��Lịch trực��6<   #��[ 6M   5�   /   5�   +   	� 5�   *   -   5        $ btnLichHenKham��5�   6   #��[ 5�   6   ��5�   6   �� *ShowLichHenKhamViewCommand+.��5�   .   �� $ 
Horizontal=�69   .+�5�   2     $ CalendarCheck=�6?   #��[ 6T   �� 5�   2   $��Lịch hẹn khám��6<   #��[ 6Q   5�   /   5�   +   	� 5�   *   -   5        $ btnKhoa��5�   6   #��[ 5�   6   ��5�   6   �� *ShowKhoaViewCommand+.��5�   .   �� $ 
Horizontal=�69   .+�5�   2     $ Users=�6?   #��[ 6L   �� 5�   2   $
��Khoa��6<   #��[ 6H   5�   /   5�   +   5�   '   5�   #   9� 5�   "   -   5        $ 	exDieuTri��5�   +   #��[ 5�   +   ��5�   &   ]� 5�   *   .+�5�   .   	� -	   5        $ 
btnDieuTri��5�   :   #��[  5�   :   .��5�   2   �� $ 
Horizontal=�6=   .+�5�   6     $ Syringe=�6C   #��[ 6R   �� 5�   6   $��Điều trị��6@   #��[ 6P   5�   3   5�   /   5�   +   .��5�   &   �� 
������ .+�5�   *   	� -
   5        $ 	btnBenhAn��5�   6   #��[ 5�   6   ��5�   6   �� *ShowBenhAnViewCommand+.��5�   .   �� $ 
Horizontal=�69   .+�5�   2     $ FileMedical=�6?   #��[ 6R   �� 5�   2   $��
Bệnh án��6<   #��[ 6K   5�   /   5�   +   	� 5�   *   -   5        $ btnKhamBenh��5�   6   #��[ 5�   6   ��5�   6   �� *ShowPhieuKhamBenhViewCommand+.��5�   .   �� $ 
Horizontal=�69   .+�5�   2     $ FilePrescription=�6?   #��[ 6W   �� 5�   2   $��Khám bệnh��6<   #��[ 6M   5�   /   5�   +   	� 5�   *   -   5        $ btnDonThuoc��5�   6   #��[ 5�   6   ��5�   6   �� *ShowDonThuocViewCommand+.��5�   .   �� $ 
Horizontal=�69   .+�5�   2     $ HouseMedical=�6?   #��[ 6S   �� 5�   2   $��Đơn thuốc��6<   #��[ 6M   5�   /   5�   +   5�   '   5�   #   	� 5�   "   -   5        $ btnBenhNhan��5�   .   #��[  5�   .   ��5�   .   �� *ShowBenhNhanViewCommand+.��5�   &   �� $ 
Horizontal=�61   .+�5�   *     $ BedPulse=�67   #��[ 6G   �� 5�   *   $��Bệnh nhân��64   #��[ 6E   5�   '   5�   #   9� 5�   "   -   5        $ 
exThongTin��5�   +   #��[ 5�   +   ��5�   &   ]� 5�   *   .+�5�   .   	� -   5        $ btnThongTin��5�   :   #��[  5�   :   .��5�   2   �� $ 
Horizontal=�6=   .+�5�   6     $ HospitalUser=�6C   #��[ 6W   �� 5�   6   $��
Thông tin��6@   #��[ 6Q   5�   3   5�   /   5   +   .��5  &   �� 
������ .+�5  *   	� -   5        $ btnBenh��5  6   #��[ 5  6   ��5  6   �� *ShowBenhViewCommand+.��5  .   �� $ 
Horizontal=�69   .+�5	  2     $ 
VirusCovid=�6?   #��[ 6Q   �� 5
  2   $��Bệnh��6<   #��[ 6H   5  /   5  +   	� 5  *   -   5        $ btnThuoc��5  6   #��[ 5  6   ��5  6   �� *ShowThuocViewCommand+.��5  .   �� $ 
Horizontal=�69   .+�5  2     $ Capsules=�6?   #��[ 6O   �� 5  2   $��Thuốc��6<   #��[ 6I   5  /   5  +   	� 5  *   -   5        $ 
btnVatDung��5  6   #��[ 5  6   ��5  6   �� *ShowVatDungViewCommand+.��5  .   �� $ 
Horizontal=�69   .+�5  2     $ 
Microscope=�6?   #��[ 6Q   �� 5  2   $��Vật dụng��6<   #��[ 6L   5  /   5  +   	� 5  *   -   5        $ 	btnHoaDon��5  6   #��[ 5   6   ��5!  6   �� *ShowHoaDonViewCommand+.��5#  .   �� $ 
Horizontal=�69   .+�5$  2     $ FileInvoiceDollar=�6?   #��[ 6X   �� 5%  2   $��
Hóa đơn��6<   #��[ 6K   5&  /   5'  +   5(  '   5)  #   5*     5+     5,     5-     5.     �� 50     $ 	0,10,10,0��6   $��1��51     ���#d4ebf8._�53     �  � RowDefinitions 54     �� 55     $��30 �6(   66   �� 56     $��40 �6(   66   �� 57     $��* �6(   65   .+�5:     �� -   5         ��Name$ panelControlBar��5;  !   $��0��5:  !   $ 
Horizontal=�5<  !   $��RightToLeft=�5=  !   
������ $��0,0,5,0q�5?  !   .+�5B     �� -   5         ��Name$ btnClose��5B  !     controlbarButton#��[ 5C  !   
 � Tag$ #f72585��5D  !   .��5F       $ Xmark=�6+     controlbarIcon#��[ 68   5G     �� 5P     -   5        $ btnMinimize��5P  !   $��30��5Q  !   #��[ 5R  !   $ #7209b7��5S  !   .��5U       $ WindowMinimize=�6+   #��[ 6A   5V     5W     � 5Y     $��1��6    5Z     �� 5[     �� 5\     .+�5_     �� $��0��6%   $ 
Horizontal=�5`  %   $��Center=�5a  %   .+�5b       $��30��5c  +   $��30��5d  +      
Foreground �#00416a$��	35,0,10,0q�5f  +    5b  +   �� *Icon+5f  +   �� 5h     ���#00416a$��16(�5j  (   $��Inter)�5k  (   $��Medium"�5l  (   $��Center=�5m  (   ��5h  (   �� *	Caption+5m  (   5n     �� 5p     $��1��6%   $ 
Horizontal=�5q  %   $��Center=�5r  %   $��RightToLeft=�5s  %   $��0,0,10,0q�5t  %   .+�5u     o� 
������ $��Center=�5v  #   $��Center=�5w  #   ��5x  "   �� 5y  &   .R�5z  *   $�  $� Orientation$ Vertical=�6A   5{  '   .@�5~  "   m� $
��Auto��6+    	 MenuItemStyle#��[	 5  +   $��Center=�5�  +   $��Center=�5�  +    m� Icon 5�  &     5�  *   $��Center=�5�  7   $��Center=�5�  7   $ 	AngleDown=�5�  7    
 userIcon#��[
 6H    5�  7   ��  �� Path$ 
Foreground� �� RelativeSource ��  �� AncestorType MenuItem5�  H   ��5�  &   �� 5�  *   $��5,0,0,0q�5�  4   $��Inter)�5�  4   $��16(�5�  4   $��Center=�5�  4   ��5�  4   �� *DisplayName+��5�  4   �� $ 
Foreground� ��  MenuItem5�  4   .@�5�  &   m� -   5          SubMenuItemStyle#��[ 5�  /    m� IsCheckable$ False��6i    m� Command 5�  /   �� *ShowTTCaNhanViewCommand+ 5�  Y   Z� ��5�  *   	� 5�  .   -   5        $ btnTTCaNhan��5�  :     RadioButtonStyle#��[ 5�  :   ��5�  :   �� *ShowTTCaNhanViewCommand+.��5�  2   �� $ 
Horizontal=�6=   .+�5�  6     $ 	UserLarge=�6C   $��Center=�5�  C   $��	10,0,15,0q�5�  C   $
��Left=�5�  C   $��20��5�  C   $��20��6N   #��[
 5�  C    5�  C   �� $ 
Foreground� ��  MenuItem5�  C   �� 5�  6   $��Thông tin cá nhân��6@   $��16(�5�  @   $��Center=�5�  @   $
��Bold"�5�  @   $��Inter)�5�  @   ��5�  @   �� $ 
Foreground� ��  MenuItem5�  @   5�  3   5�  /   5�  '   m� 5�  &   #��[ 6/   $ False��6i    6Y   Z�  5�  /   �� *LogoutCommand+��5�  *   	� 5�  .   -   5        $ btnDangXuat��5�  :   #��[ 5�  :   ��5�  :   �� *LogoutCommand+.��5�  2   �� $ 
Horizontal=�6=   .+�5�  6     $��Center=�6C   $ RightFromBracket=�5�  C   $��	10,0,15,0q�5�  C   $
��Left=�5�  C   $��20��5�  C   $��20��6N   #��[
 5�  C    5�  C   �� $ 
Foreground� ��  MenuItem5�  C   �� 5�  6   $��Đăng xuất��6@   $��16(�5�  @   $��Center=�5�  @   $
��Bold"�5�  @   $��Inter)�5�  @   ��5�  @   �� $ 
Foreground� ��  MenuItem5�  @   5�  3   5�  /   5�  '   5�  #   5�     A� 5�     $��35��6&   $��35��62   ���#00416a$	��1.5��5�  &   $��	10,0,10,0q�5�  &   ��5�  "   �� 5�  &    �� ImageSource$ /Images/hospital_logo.png��61   6[   5�     5�     5�     �� 5�     $��2��6%   $��10q�5�  !   ��5�  !   �� *CurrentChildView+5�     5�     5�     5�     5�     5�     