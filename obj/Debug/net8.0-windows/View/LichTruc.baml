   M S B A M L   `   `   `  ����I  EQuanLyBenhVien, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:QuanLyBenhVien.ViewQuanLyBenhVien.View  5      M+clr-namespace:QuanLyBenhVien.CustomControlsQuanLyBenhVien.CustomControls  5      "    QuanLyBenhVien.View.LichTruc   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:QuanLyBenhVien.View     5      FcustomControls+clr-namespace:QuanLyBenhVien.CustomControls     5      .��5
      �   � RowDefinitions  5   
   �� 5      $��60 �6   6*   �� 5      6   .+�5   
   *   $QuanLyBenhVien.CustomControls.Search  -   5          Name$ searchControl��5       $��70,10,70,10q�67   � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      6%   �� 5      6%   .+�5      �� .+�5      �� $��Danh sách công việc��6   $
��Bold"�5      $��Inter)�5      $��Center=�5      $��Center=�5      $��24(�5      $��10q�5      5      &   System.Windows.Controls.DataGrid  5       -   5          Name$ dgvCongViec��5          AutoGenerateColumns$ False��60      CanUserAddRows$ False��5!         CanUserDeleteRows$ False��5"      $��	30,5,30,0q�5#      $	��400��5$         ColumnHeaderStyle 5&      �� 5'       �� 
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
 �#00416a6C   5L   #   5N         Columns 5Q      0  *System.Windows.Controls.DataGridTextColumn  5R      ,  &System.Windows.Controls.DataGridColumn   Header$ Mã công việc��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6b   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 6C   �� *
MaCongViec+6b     5S      $ Tên công việc��6-   $ * 6d    6D   �� *TenCongViec+6d     5T      $ Mô tả��6-   $ * 6]    6<   �� *MoTaCongViec+6]     5U      $ Ghi chú��6-   $ * 6Y    6>   �� *GhiChu+6Y   5W      5Y      �� 5Z      $��1��6   .+�5[      �� $��Danh sách phân công��6   $
��Bold"�5\      $��Inter)�5]      $��Center=�5^      $��Center=�5_      $��24(�5`      $��10q�5a      5b        5c      -   5        $ dgvPhanCong��5c      $ False��60   $ False��5d      $ False��5e      $��	30,5,30,0q�5f      $	��400��5g       5i      �� 5j      "  6    .�5k      �� 	 � ��

 ��ح�6;   �� 5l      	 � ��$
 Center=�6K   �� 5m      	 � ��$
 Inter)�6;   �� 5n      	 � ��$
 14(�69   �� 5o      	 � ��$
 Medium"�6;   �� 5p      	 � ��$
 10q�68   5q       5u      �� 5v      "  6    .�5w      �� 	 � ��$

 Left=�6K   �� 5x      	 � ��$
 Inter)�6;   �� 5y      	 � ��$
 14(�69   �� 5z      	 � ��$
 Medium"�6;    5{      P� 5|   "    �@ 
IsSelected$
 True��6@   . �5}   &   �� 	 � ��
 �#d4ebf86C   �� 5~   &   	 � ��
 �#00416a6C   5   #   5�       5�      �� 5�      "  6    .�5�      �� 	 � ��$

 Left=�6K   �� 5�      	 � ��$
 Inter)�6;   �� 5�      	 � ��$
 14(�69   �� 5�      	 � ��$
 5q�68    5�      P� 5�   "    �@ 
IsSelected$
 True��6@   . �5�   &   �� 	 � ��
 �#d4ebf86C   �� 5�   &   	 � ��
 �#00416a6C   5�   #   5�       5�        5�      $ Mã lịch trực��6-   $
 1.2* 6b    6C   �� *
MaLichTruc+6b     5�      $ Bác sĩ��6-   $
 0.8* 6Y    6=   �� *	MaBacSi+6Y     5�      $ Ngày trực��6-   $ * 6]    6@   �� *
NgayTruc+6]     5�      $ Phân công��6-   $ * 6]    6@   �� *
PhanCong+6]     5�      $ Trạng thái��6-   $ * 6_    6A   �� *	TrangThai+6_   5�      �� 5�      $��0,10,0,0q�6    �� Orientation$ 
Horizontal=�5�      $��RightToLeft=�66   .+�5�      ��    childviewButton#��[  6   $��	30,15,0,0q�5�      $
��Xóa��5�      �� 5�      #��[  6   $��	30,15,0,0q�5�      $��Cập nhật��5�      �� 5�      -   5         ��Name$ btnThem��5�      #��[  5�      $��	30,15,0,0q�5�      $��Thêm��5�      5�      5�      5�      5�      5�      5�      