   M S B A M L   `   `   `  ����I  EQuanLyBenhVien, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:QuanLyBenhVien.ViewModelQuanLyBenhVien.ViewModel  5   	   9!clr-namespace:QuanLyBenhVien.ViewQuanLyBenhVien.View  5   	   M+clr-namespace:QuanLyBenhVien.CustomControlsQuanLyBenhVien.CustomControls  5   	       QuanLyBenhVien.MainWindow   5   
   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <	viewModel&clr-namespace:QuanLyBenhVien.ViewModel     5   	   3local!clr-namespace:QuanLyBenhVien.View     5   	   FcustomControls+clr-namespace:QuanLyBenhVien.CustomControls     5   	   X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  5	   	     � Title$  
MainWindow��5   	   $	��720��6   $
��1280��6)    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � AllowsTransparency$
 True��5   	   
������  � DataContext 5      ,  &QuanLyBenhVien.ViewModel.MainViewModel  5       � 	Resources 5         CustomExpander �� 
TargetType �� Property �� Value	 �� 
TargetType
 	�Name �� CornerRadius �� Triggers P� Property P� Value ��Name �� 
TargetName �� DataType0  *QuanLyBenhVien.ViewModel.BenhNhanViewModel*   $QuanLyBenhVien.View.ThongTinBenhNhan0  *QuanLyBenhVien.ViewModel.TrangChuViewModel"   QuanLyBenhVien.View.TrangChu0  *QuanLyBenhVien.ViewModel.NhanVienViewModel*   $QuanLyBenhVien.View.ThongTinNhanVien0  *QuanLyBenhVien.ViewModel.LichTrucViewModel"	   QuanLyBenhVien.View.LichTruc3
  -QuanLyBenhVien.ViewModel.LichHenKhamViewModel%   QuanLyBenhVien.View.LichHenKham,  &QuanLyBenhVien.ViewModel.KhoaViewModel*   $QuanLyBenhVien.View.Khoa_ChuyenNganh.  (QuanLyBenhVien.ViewModel.BenhAnViewModel    QuanLyBenhVien.View.BenhAn5  /QuanLyBenhVien.ViewModel.PhieuKhamBenhViewModel'   !QuanLyBenhVien.View.PhieuKhamBenh0  *QuanLyBenhVien.ViewModel.DonThuocViewModel"   QuanLyBenhVien.View.DonThuoc,  &QuanLyBenhVien.ViewModel.BenhViewModel&    QuanLyBenhVien.View.ThongTinBenh-  'QuanLyBenhVien.ViewModel.ThuocViewModel   QuanLyBenhVien.View.Thuoc/  )QuanLyBenhVien.ViewModel.VatDungViewModel!   QuanLyBenhVien.View.VatDung.  (QuanLyBenhVien.ViewModel.HoaDonViewModel    QuanLyBenhVien.View.HoaDon��%k	  &	        (�� Q    5R      *, +)(�� �    5V      *, +)(�� �    5Z      *, +)(�� �    5^      *, +)(�� -    5b      *,
 +)(�� d    5f      *, +)(�� �    5j      *, +)(�� �    5n      *, +)(�� 	    5r      *, +)(�� @    5v      *, +)(�� w    5z      *, +)(�� �    5~      *, +)(�� �    5�      *, +)�� 5   
   " 9�6'   .�5      ��  � ��$	 260��6&   �� 5       � ��$
 Left=�64   �� 5       � �� 5      �� 5      "	 9�6&   .R�5      ]� .+�5      	� $
 HeaderButton��6*   -   5        $	��Top=�5   !   $��40��5    !   $��-5,0,0,5q�5!   !   ���	#FFA7D4EE���#00416a
������ .��5&   "   �� $��Header��63   $
��Left=�5'   +   $��20,0,0,0q�5(   +   ��5)   "   �� 5*   &   "	 	�66   .R�5+   *   �� #��z��61   #��z��5,   -   $��4,0,0,0q�5-   -   $ 0,5,5,0��5.   -   ._�5/   .   �� $��Center=�6?   50   +    51   *   P� 52   .    �@J�IsMouseOver$
 True��6M   . �53   2   ��  � ��$ 0,0,0,5q�6K   �� 54   2    � �� �	#FF16AAD26O   �� 55   2    � �� �#d4ebf86O   �� 56   2    � �� �#0523426P   57   /   P� 59   .    �@d�	IsChecked$
 TrueY�6K   . �5:   2   ��  � ��$ 0,0,0,5q�6K   �� 5;   2    � �� �	#FF16AAD26O   �� 5<   2    � �� �#d4ebf86O   �� 5=   2    � �� �#0523426O   5>   /   5@   '   5B      �� 5C      $ ExpanderContent��6/   $��Bottom=�5D   )   #��z��5E   )   $y�	Collapsed=�5F   )   5G       5H      P� 5I       �@9�
IsExpanded$
 True��6<   . �5J   "   �� $ ExpanderContent��6)    � y�$ Visible=�6\   5K      5M      5O      5P      �� 5R   
   "  6   .R�5S        6'   5T      �� 5V   
   "  6   .R�5W        6   5X      �� 5Z   
   "  6   .R�5[        6'   5\      �� 5^   
   "  6   .R�5_      	  6   5`      �� 5b   
   " 
 6   .R�5c        6"   5d      �� 5f   
   "  6   .R�5g        6'   5h      �� 5j   
   "  6   .R�5k        6   5l      �� 5n   
   "  6   .R�5o        6$   5p      �� 5r   
   "  6   .R�5s        6   5t      �� 5v   
   "  6   .R�5w        6#   5x      �� 5z   
   "  6   .R�5{        6   5|      �� 5~   
   "  6   .R�5        6   5�      �� 5�   
   "  6   .R�5�        6   5�      5�      .��5�      �� $ 10��6   $��1q�5�   	   ��5�   
   �� 5�       �� 
StartPoint$
 0, 0&�6"    �� EndPoint$
 1, 1&�64   .��5�      �  � Color$ #03045E��6    � Offset$ 0Q�6/   � 5�      $ #0077B6��6   $	 0.6Q�6/   � 5�      $ #CAF0F8��6   $ 1Q�6/   5�      ._�5�   
   �  � ColumnDefinitions 5�      �� 5�      $	��280 �6#   �� 5�      $��* �6#   .+�5�      �� $ 	10,0,0,10��6   $��0��5�      ���#00416a._�5�      �� .+�5�      �� $	��140��6   $	��140��6)   
������ $��1q�5�      ���#d4ebf8$��	0,30,0,15q�5�      $ 90��5�      ._�5�      �� $��/Images/hospital_logo.png��6    $
��Fill=�5�       $��0q�5�       $��Center=�5�       $��Center=�5�       $	��180��5�       $	��180��6,   5�      �� 5�      $��Tên bệnh viện��6    $��23(�5�      ���#d4ebf8$��Inter)�5�       �� TextAlignment$ Center=�5�      $��0,5,0,30q�5�      � 5�      $
��Auto��6   .+�5�      �� $��Hidden=�6'   $��Disabled=�5�   #   $	��440��5�   #   .��5�      �� .+�5�   "   	� -   5        $
 btnTrangChu��5�   .     
MenuButton#��[ 5�   .    d� 	IsChecked$
 TrueY�5�   .   ��5�   .   �� *ShowTrangChuViewCommand+.��5�   &   ��  �� Orientation$ 
Horizontal=�61   .+�5�   *   !  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$
 Home=�67     MenuIcon#��[ 6C   �� 5�   *   $��Trang chủ��64     MenuText#��[ 6E   5�   '   5�   #   9� 5�   "   #��[  6+   ��5�   &   �� 5�   *   $ 
Horizontal=�65   .+�5�   .     $ HospitalUser=�6;   #��[ 6O   �� 5�   .   $��Nhân viên��68   #��[ 6I   5�   +   .��5�   &   �� 
������ .+�5�   *   	� -   5        $
 btnTTNhanVien��5�   6     SubMenuButton#��[ 5�   6   ��5�   6   �� *ShowNhanVienViewCommand+.��5�   .   �� $ 
Horizontal=�69   .+�5�   2     $ IdCard=�6?     SubMenuIcon#��[ 6M   �� 5�   2   $��
Thông tin��6<     SubMenuText#��[ 6M   5�   /   5�   +   	� 5�   *   -   5        $
 btnLichTruc��5�   6   #��[ 5�   6   ��5�   6   �� *ShowLichTrucViewCommand+.��5�   .   �� $ 
Horizontal=�69   .+�5�   2     $ CalendarDays=�6?   #��[ 6S   �� 5�   2   $��Lịch trực��6<   #��[ 6M   5�   /   5�   +   	� 5�   *   -   5        $
 btnLichHenKham��5�   6   #��[ 5�   6   ��5�   6   �� *ShowLichHenKhamViewCommand+.��5�   .   �� $ 
Horizontal=�69   .+�5�   2     $ CalendarCheck=�6?   #��[ 6T   �� 5�   2   $��Lịch hẹn khám��6<   #��[ 6Q   5�   /   5�   +   	� 5�   *   -   5        $
 btnKhoa��5�   6   #��[ 5�   6   ��5�   6   �� *ShowKhoaViewCommand+.��5�   .   �� $ 
Horizontal=�69   .+�5�   2     $ Users=�6?   #��[ 6L   �� 5�   2   $
��Khoa��6<   #��[ 6H   5�   /   5�   +   5�   '   5�   #   9� 5�   "   #��[  6+   ��5�   &   �� 5�   *   $ 
Horizontal=�65   .+�5�   .     $ Syringe=�6;   #��[ 6J   �� 5�   .   $��Điều trị��68   #��[ 6H   5�   +   .��5�   &   �� 
������ .+�5�   *   	� -   5        $
 	btnBenhAn��5�   6   #��[ 5�   6   ��5�   6   �� *ShowBenhAnViewCommand+.��5�   .   �� $ 
Horizontal=�69   .+�5�   2     $ FileMedical=�6?   #��[ 6R   �� 5�   2   $��
Bệnh án��6<   #��[ 6K   5   /   5  +   	� 5  *   -   5        $
 btnKhamBenh��5  6   #��[ 5  6   ��5  6   �� *ShowPhieuKhamBenhViewCommand+.��5  .   �� $ 
Horizontal=�69   .+�5  2     $ FilePrescription=�6?   #��[ 6W   �� 5  2   $��Khám bệnh��6<   #��[ 6M   5	  /   5
  +   	� 5  *   -	   5        $
 btnDonThuoc��5  6   #��[ 5  6   ��5  6   �� *ShowDonThuocViewCommand+.��5  .   �� $ 
Horizontal=�69   .+�5  2     $ HouseMedical=�6?   #��[ 6S   �� 5  2   $��Đơn thuốc��6<   #��[ 6M   5  /   5  +   5  '   5  #   	� 5  "   -
   5        $
 btnBenhNhan��5  .   #��[ 5  .   ��5  .   �� *ShowBenhNhanViewCommand+.��5  &   �� $ 
Horizontal=�61   .+�5  *     $ BedPulse=�67   #��[ 6G   �� 5  *   $��Bệnh nhân��64   #��[ 6E   5  '   5  #   9� 5!  "   #��[  6+   ��5"  &   �� 5#  *   $ 
Horizontal=�65   .+�5$  .     $ HospitalUser=�6;   #��[ 6O   �� 5%  .   $��
Thông tin��68   #��[ 6I   5&  +   .��5)  &   �� 
������ .+�5*  *   	� -   5        $
 btnBenh��5*  6   #��[ 5+  6   ��5,  6   �� *ShowBenhViewCommand+.��5.  .   �� $ 
Horizontal=�69   .+�5/  2     $ 
VirusCovid=�6?   #��[ 6Q   �� 50  2   $��Bệnh��6<   #��[ 6H   51  /   52  +   	� 53  *   -   5        $
 btnThuoc��53  6   #��[ 54  6   ��55  6   �� *ShowThuocViewCommand+.��57  .   �� $ 
Horizontal=�69   .+�58  2     $ Capsules=�6?   #��[ 6O   �� 59  2   $��Thuốc��6<   #��[ 6I   5:  /   5;  +   	� 5<  *   -   5        $
 
btnVatDung��5<  6   #��[ 5=  6   ��5>  6   �� *ShowVatDungViewCommand+.��5@  .   �� $ 
Horizontal=�69   .+�5A  2     $ 
Microscope=�6?   #��[ 6Q   �� 5B  2   $��Vật dụng��6<   #��[ 6L   5C  /   5D  +   	� 5E  *   -   5        $
 	btnHoaDon��5E  6   #��[ 5F  6   ��5G  6   �� *ShowHoaDonViewCommand+.��5I  .   �� $ 
Horizontal=�69   .+�5J  2     $ FileInvoiceDollar=�6?   #��[ 6X   �� 5K  2   $��
Hóa đơn��6<   #��[ 6K   5L  /   5M  +   5N  '   5O  #   5P     5Q     5R     5S     5T     �� 5V     $ 	0,10,10,0��6   $��1��5W     ���#d4ebf8._�5Y     �  � RowDefinitions 5Z     �� 5[     $��30 �6(   66   �� 5\     $��40 �6(   66   �� 5]     $��* �6(   65   .+�5`     �� -   5         ��Name$ panelControlBar��5a  !   $��0��5`  !   $ 
Horizontal=�5b  !   $��RightToLeft=�5c  !   
������ $��0,0,5,0q�5e  !   .+�5h     �� -   5         ��Name$ btnClose��5h  !     controlbarButton#��[ 5i  !   
 � Tag$ #f72585��5j  !   .��5l       $ Xmark=�6+     controlbarIcon#��[ 68   5m     �� 5o     -   5        $ btnMaximize��5o  !   #��[ 5p  !   $ #3a0ca3��5q  !   .��5s       $ Square=�6+   #��[ 69   5t     �� 5v     -   5        $ btnMinimize��5v  !   $��30��5w  !   #��[ 5x  !   $ #7209b7��5y  !   .��5{       $ WindowMinimize=�6+   #��[ 6A   5|     5}     � 5     $��1��6    5�     �� 5�     �� 5�     .+�5�     �� $��0��6%   $ 
Horizontal=�5�  %   $��Center=�5�  %   .+�5�       $��30��5�  +   $��30��5�  +      
Foreground �#00416a$��	35,0,10,0q�5�  +    5�  +   �� *Icon+5�  +   �� 5�     ���#00416a$��16(�5�  (   $��Inter)�5�  (   $��Medium"�5�  (   $��Center=�5�  (   ��5�  (   �� *	Caption+5�  (   5�     �� 5�     $��1��6%   $ 
Horizontal=�5�  %   $��Center=�5�  %   $��RightToLeft=�5�  %   $��0,0,10,0q�5�  %   .+�5�     o� 
������ $��Center=�5�  #   $��Center=�5�  #   ��5�  "   �� 5�  &   .R�5�  *   $�   $� Orientation$  Vertical=�6A   5�  '   .@�5�  "   m� $��25��6+   $��Center=�5�  +   $��Center=�5�  +   ! m� Icon! 5�  &     5�  *    �#00416a$��Center=�5�  7   $��Center=�5�  7   $ 	AngleDown=�5�  7    	 userIcon#��[	 6H   .@�5�  &   m� -   5        $��Thông tin cá nhân��5�  /   " m� Command" 5�  /   �� *ShowTTCaNhanViewCommand+! 5�  *     5�  .    �#00416a$��Center=�5�  ;   $
��Left=�5�  ;   $ HospitalUser=�5�  ;   #��[	 6O   5�  '   m� 5�  &   $��Đăng xuất��6/   ! 5�  *     5�  .    �#00416a$��Center=�5�  ;   $
��Left=�5�  ;   $ ArrowRightFromBracket=�5�  ;   #��[	 6X   5�  '   5�  #   5�     �� 5�     $��Phan Ngoc Son��6(   ���#00416a$��Inter)�5�  (   $��16(�5�  (   $��Center=�5�  (   A� 5�     $��35��6&   $��35��62   ���#00416a$	��1.5��5�  &   $��	10,0,10,0q�5�  &   ��5�  "   �� 5�  &   # �� ImageSource$# /Images/hospital_logo.png��61   6[   5�     5�     5�     �� 5�     $��2��6%   $��10q�5�  !   ��5�  !   �� *CurrentChildView+5�     5�     5�     5�     5�     5�     