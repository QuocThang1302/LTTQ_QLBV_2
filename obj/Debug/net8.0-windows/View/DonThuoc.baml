   M S B A M L   `   `   `  ����I  EQuanLyBenhVien, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:QuanLyBenhVien.ViewQuanLyBenhVien.View  5      M+clr-namespace:QuanLyBenhVien.CustomControlsQuanLyBenhVien.CustomControls  5      "    QuanLyBenhVien.View.DonThuoc   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:QuanLyBenhVien.View     5      FcustomControls+clr-namespace:QuanLyBenhVien.CustomControls     5      -   5        .��5
      �   � RowDefinitions  5   
   �� 5      $��60 �6   6*   �� 5      6   .+�5   
   *   $QuanLyBenhVien.CustomControls.Search  -   5          Name$ searchControl��5       $��70,10,70,10q�67   � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      6%   �� 5      6%   .+�5      �� .+�5      �� $ ��Danh sách đơn thuốc��6   $
��Bold"�5      $��Inter)�5      $��Center=�5      $��Center=�5      $��24(�5      $��10q�5      5      &   System.Windows.Controls.DataGrid  5       -   5          Name$ dgvDonThuoc��5          AutoGenerateColumns$ False��60      CanUserAddRows$ False��5!         CanUserDeleteRows$ False��5"         HeadersVisibility$ Column=�5#      
�������   RowBackground
 �����	   AlternatingRowBackground	 �#e8f1ff$��	30,5,30,0q�5'      $	��430��5(      
   ColumnHeaderStyle
 5*      �� 5+       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5,      ��  �� Property � �� �� Value
 ��ح�6<   �� 5-       � ��$ Center=�6K   �� 5.       � ��$ Inter)�6;   �� 5/       � ��$ 14(�69   �� 50       � ��$ Medium"�6;   �� 51       � ��$ 10q�68   52         RowStyle 56      �� 57      )  #System.Windows.Controls.DataGridRow"  6    .�58      ��  � ��$
 Left=�6K   �� 59       � ��$ Inter)�6;   �� 5:       � ��$ 14(�69   �� 5;       � ��$ Medium"�6;    �� Triggers 5<      P� 5=   "    P� Property �@ 
IsSelected P� Value$
 True��6@   . �5>   &   ��  � �� �#d4ebf86C   �� 5?   &    � �� �#00416a6C   5@   #   5B         	CellStyle 5F      �� 5G      *  $System.Windows.Controls.DataGridCell"  6    .�5H      ��  � ��$
 Left=�6K   �� 5I       � ��$ Inter)�6;   �� 5J       � ��$ 14(�69   �� 5K       � ��$ 5q�68    5L      P� 5M   "    �@ 
IsSelected$
 True��6@   . �5N   &   ��  � �� �#d4ebf86C   �� 5O   &    � �� �#00416a6C   5P   #   5R         Columns 5U      0  *System.Windows.Controls.DataGridTextColumn  5V      ,  &System.Windows.Controls.DataGridColumn   Header$ Mã đơn thuốc��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$
 1.1* 6b   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 6C   �� *
MaDonThuoc+6b     5W      $ Bệnh nhân��6-   $ * 6_    6@   �� *
MaBenhNhan+6_     5X      $ Bác sĩ��6-   $ * 6Y    6=   �� *	MaBacSi+6Y     5Y      $ Ngày lập đơn��6-   $
 1.1* 6}    6C   �� *
NgayLapDon+ �� StringFormat$ 
yyyy-MM-dd��6}     5Z      $ Hóa đơn��6-   $ * 6\    6?   �� *
MaHoaDon+6\   5\      �� 5]       �� Orientation$ 
Horizontal=�6   .+�5^      �� -   5         ��Name$ btnXuatHoaDon��5^         childviewButton#��[  5_      $��	30,15,0,0q�5`      $	��130��5a      $��Xuất hóa đơn��5b      66   5c      5e      �� 5f      $��1��6   .+�5g      �� $ ��Chi tiết đơn thuốc��6   $
��Bold"�5h      $��Inter)�5i      $��Center=�5j      $��Center=�5k      $��24(�5l      $��10q�5m        5o      -   5        $ dgvCTDonThuoc��5o      $ False��62   $ False��5p      $ False��5q      $ Column=�5r      
�������
 �����	 �#e8f1ff$��	30,5,30,0q�5v      $	��430��5w      
 5y      �� 5z      "  6    .�5{      ��  � ��
 ��ح�6;   �� 5|       � ��$ Center=�6K   �� 5}       � ��$ Inter)�6;   �� 5~       � ��$ 14(�69   �� 5       � ��$ Medium"�6;   �� 5�       � ��$ 10q�68   5�       5�      �� 5�      "  6    .�5�      ��  � ��$
 Left=�6K   �� 5�       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ Medium"�6;    5�      P� 5�   "    �@ 
IsSelected$
 True��6@   . �5�   &   ��  � �� �#d4ebf86C   �� 5�   &    � �� �#00416a6C   5�   #   5�       5�      �� 5�      "  6    .�5�      ��  � ��$
 Left=�6K   �� 5�       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ 5q�68    5�      P� 5�   "    �@ 
IsSelected$
 True��6@   . �5�   &   ��  � �� �#d4ebf86C   �� 5�   &    � �� �#00416a6C   5�   #   5�       5�        5�      $ Tên thuốc��6-   $ * 6]    6@   �� *
TenThuoc+6]     5�      $ Số lượng��6-   $ * 6[    6?   �� *	SoLuong+6[     5�      $ Giá tiền��6-   $ * 6[    6?   �� *	GiaTien+6[     5�      $ Hướng dẫn��6-   $ * 6c    6@   �� *HuongDanSuDung+6c   5�      �� 5�      $ 
Horizontal=�6   $��RightToLeft=�66   .+�5�      �� -   5        #��[  5�      $��	30,15,0,0q�5�      $
��Xóa��5�      6+   �� 5�      -   5        #��[  5�      $��	30,15,0,0q�5�      $��Cập nhật��5�      60   5�      5�      5�      5�      5�      