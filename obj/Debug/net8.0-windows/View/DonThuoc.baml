   M S B A M L   `   `   `  ����I  EQuanLyBenhVien, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:QuanLyBenhVien.ViewQuanLyBenhVien.View  5      M+clr-namespace:QuanLyBenhVien.CustomControlsQuanLyBenhVien.CustomControls  5      "    QuanLyBenhVien.View.DonThuoc   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:QuanLyBenhVien.View     5      FcustomControls+clr-namespace:QuanLyBenhVien.CustomControls     5      .��5
      �   � RowDefinitions  5   
   �� 5      $��60 �6   6*   �� 5      6   .+�5   
   *   $QuanLyBenhVien.CustomControls.Search  -   5          Name$ searchControl��5       $��70,10,70,10q�67   � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      6%   �� 5      6%   .+�5      �� .+�5      �� $ ��Danh sách đơn thuốc��6   $
��Bold"�5      $��Inter)�5      $��Center=�5      $��Center=�5      $��24(�5      $��10q�5      5      &   System.Windows.Controls.DataGrid  5       -   5          Name$ dgvDonThuoc��5          AutoGenerateColumns$ False��60      CanUserAddRows$ False��5!         CanUserDeleteRows$ False��5"      $��	30,5,30,0q�5#      $	��430��5$         ColumnHeaderStyle 5&      �� 5'       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5(      �� 	 �� Property	 � ��
 �� Value

 ��ح�6;   �� 5)      	 � ��$
 Center=�6K   �� 5*      	 � ��$
 Inter)�6;   �� 5+      	 � ��$
 14(�69   �� 5,      	 � ��$
 Medium"�6;   �� 5-      	 � ��$
 10q�68   5.         RowStyle 52      �� 53      )  #System.Windows.Controls.DataGridRow"  6    .�54      �� 	 � ��$

 Left=�6K   �� 55      	 � ��$
 Inter)�6;   �� 56      	 � ��$
 14(�69   �� 57      	 � ��$
 Medium"�6;    �� Triggers 58      P� 59   "    P� Property �@ 
IsSelected P� Value$
 True��6@   . �5:   &   �� 	 � ��
 �#d4ebf86C   �� 5;   &   	 � ��
 �#00416a6C   5<   #   5>         	CellStyle 5B      �� 5C      *  $System.Windows.Controls.DataGridCell"  6    .�5D      �� 	 � ��$

 Left=�6K   �� 5E      	 � ��$
 Inter)�6;   �� 5F      	 � ��$
 14(�69   �� 5G      	 � ��$
 5q�68    5H      P� 5I   "    �@ 
IsSelected$
 True��6@   . �5J   &   �� 	 � ��
 �#d4ebf86C   �� 5K   &   	 � ��
 �#00416a6C   5L   #   5N         Columns 5Q      0  *System.Windows.Controls.DataGridTextColumn  5R      ,  &System.Windows.Controls.DataGridColumn   Header$ Mã đơn thuốc��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$
 1.1* 6b   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 6C   �� *
MaDonThuoc+6b     5S      $ Bệnh nhân��6-   $ * 6_    6@   �� *
MaBenhNhan+6_     5T      $ Bác sĩ��6-   $ * 6Y    6=   �� *	MaBacSi+6Y     5U      $ Ngày lập đơn��6-   $
 1.1* 6}    6C   �� *
NgayLapDon+ �� StringFormat$ 
dd/MM/yyyy��6}   5W      �� 5X       �� Orientation$ 
Horizontal=�6   .+�5Y      �� -   5           childviewButton#��[  5Y      $��	30,15,0,0q�5Z      $	��130��5[      $��Xuất hóa đơn��5\      66   5]      5_      �� 5`      $��1��6   .+�5a      �� $ ��Chi tiết đơn thuốc��6   $
��Bold"�5b      $��Inter)�5c      $��Center=�5d      $��Center=�5e      $��24(�5f      $��10q�5g        5i      -   5        $ dgvCTDonThuoc��5i      $ False��62   $ False��5j      $ False��5k      $��	30,5,30,0q�5l      $	��430��5m       5o      �� 5p      "  6    .�5q      �� 	 � ��

 ��ح�6;   �� 5r      	 � ��$
 Center=�6K   �� 5s      	 � ��$
 Inter)�6;   �� 5t      	 � ��$
 14(�69   �� 5u      	 � ��$
 Medium"�6;   �� 5v      	 � ��$
 10q�68   5w       5{      �� 5|      "  6    .�5}      �� 	 � ��$

 Left=�6K   �� 5~      	 � ��$
 Inter)�6;   �� 5      	 � ��$
 14(�69   �� 5�      	 � ��$
 Medium"�6;    5�      P� 5�   "    �@ 
IsSelected$
 True��6@   . �5�   &   �� 	 � ��
 �#d4ebf86C   �� 5�   &   	 � ��
 �#00416a6C   5�   #   5�       5�      �� 5�      "  6    .�5�      �� 	 � ��$

 Left=�6K   �� 5�      	 � ��$
 Inter)�6;   �� 5�      	 � ��$
 14(�69   �� 5�      	 � ��$
 5q�68    5�      P� 5�   "    �@ 
IsSelected$
 True��6@   . �5�   &   �� 	 � ��
 �#d4ebf86C   �� 5�   &   	 � ��
 �#00416a6C   5�   #   5�       5�        5�      $ Tên thuốc��6-   $ * 6]    6@   �� *
TenThuoc+6]     5�      $ Số lượng��6-   $ * 6[    6?   �� *	SoLuong+6[     5�      $ Giá tiền��6-   $ * 6[    6?   �� *	GiaTien+6[     5�      $ Hướng dẫn��6-   $ * 6c    6@   �� *HuongDanSuDung+6c   5�      �� 5�      $ 
Horizontal=�6   $��RightToLeft=�66   .+�5�      �� #��[  6   $��	30,15,0,0q�5�      $
��Xóa��5�      �� 5�      #��[  6   $��	30,15,0,0q�5�      $��Cập nhật��5�      �� 5�      -   5        #��[  5�      $��	30,15,0,0q�5�      $��Thêm��5�      6,   5�      5�      5�      5�      5�      