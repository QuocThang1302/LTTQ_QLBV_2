   M S B A M L   `   `   `  ����I  EQuanLyBenhVien, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:QuanLyBenhVien.CustomControlsQuanLyBenhVien.CustomControls  5      9!clr-namespace:QuanLyBenhVien.ViewQuanLyBenhVien.View  5           QuanLyBenhVien.View.BenhAn   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      FcustomControls+clr-namespace:QuanLyBenhVien.CustomControls     5      3local!clr-namespace:QuanLyBenhVien.View     5      -   5        .��5      �   � RowDefinitions  5   
   �� 5      $��60 �6   �� 5      $
��1.9* �6   �� 5      $��* �6   .+�5   
   *   $QuanLyBenhVien.CustomControls.Search  -   5          Name$ searchControl��5       $��70,10,70,10q�68   �� 5   
   $��1��6   $��
25,10,25,5q�5      $��2q�5      
���   �
������  �� CornerRadius$ 10��5      ._�5      �   5      �� 5      �� 5      $��70 �6$   .+�5"      �   5#      �� 5$      $��50 �6(   66   �� 5%      $��80 �6(   66   �� 5&      6*    � ColumnDefinitions 5(      �� 5)      6-   �� 5*      6-   .+�5,      � $��2��6   .+�5-      �� $��Hồ sơ bệnh án��6$   $��Center=�5.   &   $��Center=�5/   &   $��28(�50   &   $
��Bold"�51   &   $��Inter)�52   &   53      �� 54      $��1��6!   .+�55      ��  �� Orientation$ 
Horizontal=�6%   $��30, 05, 0, 5q�6>   .+�56      �� $
��Bold"�6(   $��Inter)�6:   $��Mã bệnh án:��6M   $	��160��6b   $��Center=�6n   $��20(�6�   �� 57      -   5        $��txtMaBenhAn��57   &   $	��250��69      TextBoxStyles#��[  6E   59      �� 5:      $ 
Horizontal=�6%   $��30, 05, 0, 5q�6>   .+�5;      �� $
��Bold"�6(   $��Inter)�6:   $��Bệnh nhân:��6M   $	��160��6`   $��Center=�6l   $��20(�6�   �� 5<      -   5        $��txtBenhNhan��5<   &   $	��250��69   #��[  6E   5=      �� 5>      $ 
Horizontal=�6%   $��30, 05, 0, 5q�6>   5A      �� 5B      $��1��6!   $��1��61   .+�5C      �� $ 
Horizontal=�6%   $��30, 5, 0, 5q�6>   .+�5D      �� $
��Bold"�6(   $��Inter)�6:   $��Mã bệnh nhân:��6M   $	��160��6d   $��Center=�6p   $��20(�6�   �� 5E      -   5        $��txtMaBenhNhan��5E   &   $	��250��6;   #��[  6G   5F      �� 5G      $ 
Horizontal=�6%   $��30, 05, 0, 5q�6>   .+�5H      �� $
��Bold"�6(   $��Inter)�6:   $��Ngày tạo lập:��6M   $	��160��6e   $��Center=�6q   $��20(�6�   �� 5I      -   5        $��txtNgayTaoLap��5I   &   $	��250��6;   #��[  6G    �� 
IsReadOnly$
 True��6�   � 5J      -   5        $��popupCalendar��5J   $   $��False��69   $��Bottom=�6~    � PlacementTarget 6H   ��  �� ElementName$ txtNgayTaoLap��.��5K   "   &   System.Windows.Controls.Calendar  -   5        $��calendar��5K   +   $|�False��6p   5L      5M      5P      �� 5Q      $��2��6!   $��2��6.   .+�5R      �� $ 
Horizontal=�6%   $��30, 05, 0, 5q�6>   .+�5S      �� $
��Bold"�6(   $��Inter)�6:   $��Tình trạng:��6M   $	��160��6b   $��Center=�6n   $��20(�6�   �� 5T      -	   5        $��txtTinhTrang��5T   &   $��40��6:   $	��712��6F   #��[  6R   5U      �� 5V      $ 
Horizontal=�6%   $��30, 05, 0, 5q�6>   .+�5W      �� $
��Bold"�6(   $��Inter)�6:   $��Bệnh:��6M   $	��160��6[   $��Center=�6g   $��20(�6�   �� 5X      -
   5        $��txtBenh��5X   &   $��40��65   $	��712��6A   #��[  6M   5Y      �� 5Z      $ 
Horizontal=�6%   $��30, 05, 0, 5q�6>   .+�5[      �� $
��Bold"�6(   $��Inter)�6:   $��Hướng điều trị:��6M   $	��160��6g   $��Center=�6s   $��20(�6�   �� 5\      -   5        $��txtHuongDieuTri��5\   &   $��40��6=   $	��712��6I   #��[  6U   5]      5^      5_      �� 5a      $��1��6   $ 
Horizontal=�6*   $��RightToLeft=�6C   .+�5c      �� -   5         ��Name$ btnXoa��5c        childviewButton#��[ 6-   $��52,0,0,0q�5d      $
��Xóa��5e      6!   �� 5g      -   5        $ 
btnCapNhat��5g      #��[ 61   $��52,0,0,0q�5h      $��Cập nhật��5i      6&   �� 5k      -   5        $ btnThem��5k      #��[ 6.   $��52,0,0,0q�5l      $��Thêm��5m      6"   5o      5p      5q      � 5s   
   $��2��6   .+�5t      �� $��25,10,25,10q�6   .+�5u      �� $��Danh sách bệnh án��6   $��18(�5v      $
��Bold"�5w      $��Inter)�5x      $��Center=�5y      &   System.Windows.Controls.DataGrid  5|      -   5        	  Name$	 dgDanhSachBenhAn��5|      
   AutoGenerateColumns$
 False��65      HeadersVisibility$ Column=�5}      
�������   RowBackground
 �����   AlternatingRowBackground �#e8f1ff   CanUserAddRows$ False��5�         CanUserDeleteRows$ False��5�      $��0,5,0,0q�5�      $	��170��5�         ColumnHeaderStyle 5�      �� 5�       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5�      ��  �� Property � �� �� Value
 ��ح�6;   �� 5�       � ��$ Center=�6K   �� 5�       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ Medium"�6;   �� 5�       � ��$ 10q�68   5�         RowStyle 5�      �� 5�      )  #System.Windows.Controls.DataGridRow"  6    .�5�      ��  � ��$
 Left=�6K   �� 5�       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ Medium"�6;    �� Triggers 5�      P� 5�   "    P� Property �@ 
IsSelected P� Value$
 True��6@   . �5�   &   ��  � �� �#d4ebf86C   �� 5�   &    � �� �#00416a6C   5�   #   5�         	CellStyle 5�      �� 5�      *  $System.Windows.Controls.DataGridCell"  6    .�5�      ��  � ��$
 Left=�6K   �� 5�       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ 5q�68    5�      P� 5�   "    �@ 
IsSelected$
 True��6@   . �5�   &   ��  � �� �#d4ebf86C   �� 5�   &    � �� �#00416a6C   5�   #   5�         Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$ Mã bệnh án��6-      Width5	  /System.Windows.Controls.DataGridLengthConverter$ *	 6D   1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding 6N   �� *
MaBenhAn+  5�      $ Tên bệnh nhân��6-   $ *	 6E    6O   �� *Ten+  5�      $ Mã bệnh nhân��6-   $ *	 6E    6O   �� *
MaBenhNhan+  5�      $ Ngày tạo lập��6-   $ *	 6F    6P   �� *
NgayTaoLap+ �� StringFormat$ 
yyyy-MM-dd��  5�      $ Tình trạng��6-   $ *	 6C    6M   �� *	TinhTrang+  5�      $ Bệnh��6-   $ *	 6<    6F   �� *Benh+  5�      $ Hướng điều trị��6-   $
 1.5*	 6H    6U   �� *	DieuTri+5�      5�      5�      5�      5�      