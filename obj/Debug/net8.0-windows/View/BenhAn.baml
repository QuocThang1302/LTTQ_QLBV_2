   M S B A M L   `   `   `  ����I  EQuanLyBenhVien, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:QuanLyBenhVien.CustomControlsQuanLyBenhVien.CustomControls  5      9!clr-namespace:QuanLyBenhVien.ViewQuanLyBenhVien.View  5           QuanLyBenhVien.View.BenhAn   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      FcustomControls+clr-namespace:QuanLyBenhVien.CustomControls     5      3local!clr-namespace:QuanLyBenhVien.View     5      -   5        .��5
      �   � RowDefinitions  5   
   �� 5      $��60 �6   �� 5      $
��1.9* �6   �� 5      $��* �6   .+�5   
   *   $QuanLyBenhVien.CustomControls.Search  -   5          Name$ searchControl��5       $��70,10,70,10q�68   �� 5   
   $��1��6   $��
25,10,25,5q�5      $��2q�5      
���   �
������  �� CornerRadius$ 10��5      ._�5      �   5      �� 5      �� 5      $��70 �6$   .+�5       �   5!      �� 5"      $��50 �6(   66   �� 5#      $��80 �6(   66   �� 5$      6*    � ColumnDefinitions 5&      �� 5'      6-   �� 5(      6-   .+�5*      � $��2��6   .+�5+      �� $��Hồ sơ bệnh án��6$   $��Center=�5,   &   $��Center=�5-   &   $��28(�5.   &   $
��Bold"�5/   &   $��Inter)�50   &   51      �� 52      $��1��6!   .+�53      ��  �� Orientation$ 
Horizontal=�6%   $��30, 05, 0, 5q�6>   .+�54      �� $
��Bold"�6(   $��Inter)�6:   $��Mã bệnh án:��6M   $	��160��6b   $��Center=�6n   $��20(�6�   �� 55      -   5        $��txtMaBenhAn��55   &   $	��250��69      TextBoxStyles#��[  6E   57      �� 58      $ 
Horizontal=�6%   $��30, 05, 0, 5q�6>   .+�59      �� $
��Bold"�6(   $��Inter)�6:   $��Bệnh nhân:��6M   $	��160��6`   $��Center=�6l   $��20(�6�   �� 5:      -   5        $��txtBenhNhan��5:   &   $	��250��69   #��[  6E   5;      �� 5<      $ 
Horizontal=�6%   $��30, 05, 0, 5q�6>   5?      �� 5@      $��1��6!   $��1��61   .+�5A      �� $ 
Horizontal=�6%   $��30, 5, 0, 5q�6>   .+�5B      �� $
��Bold"�6(   $��Inter)�6:   $��Mã bệnh nhân:��6M   $	��160��6d   $��Center=�6p   $��20(�6�   �� 5C      -   5        $��txtMaBenhNhan��5C   &   $	��250��6;   #��[  6G   5D      �� 5E      $ 
Horizontal=�6%   $��30, 05, 0, 5q�6>   .+�5F      �� $
��Bold"�6(   $��Inter)�6:   $��Ngày tạo lập:��6M   $	��160��6e   $��Center=�6q   $��20(�6�   �� 5G      -   5        $��txtNgayTaoLap��5G   &   $	��250��6;   #��[  6G    �� 
IsReadOnly$
 True��6�   � 5H      -   5        $��popupCalendar��5H   $   $��False��69   $��Bottom=�6~    � PlacementTarget 6H   ��  �� ElementName$ txtNgayTaoLap��.��5I   "   &   System.Windows.Controls.Calendar  -   5        $��calendar��5I   +   $|�False��6p   5J      5K      5N      �� 5O      $��2��6!   $��2��6.   .+�5P      �� $ 
Horizontal=�6%   $��30, 05, 0, 5q�6>   .+�5Q      �� $
��Bold"�6(   $��Inter)�6:   $��Tình trạng:��6M   $	��160��6b   $��Center=�6n   $��20(�6�   �� 5R      -	   5        $��txtTinhTrang��5R   &   $��40��6:   $	��712��6F   #��[  6R   5S      �� 5T      $ 
Horizontal=�6%   $��30, 05, 0, 5q�6>   .+�5U      �� $
��Bold"�6(   $��Inter)�6:   $��Bệnh:��6M   $	��160��6[   $��Center=�6g   $��20(�6�   �� 5V      -
   5        $��txtBenh��5V   &   $��40��65   $	��712��6A   #��[  6M   5W      �� 5X      $ 
Horizontal=�6%   $��30, 05, 0, 5q�6>   .+�5Y      �� $
��Bold"�6(   $��Inter)�6:   $��Hướng điều trị:��6M   $	��160��6g   $��Center=�6s   $��20(�6�   �� 5Z      -   5        $��txtHuongDieuTri��5Z   &   $��40��6=   $	��712��6I   #��[  6U   5[      5\      5]      �� 5_      $��1��6   $ 
Horizontal=�6*   $��RightToLeft=�6C   .+�5a      �� -   5         ��Name$ btnXoa��5a        childviewButton#��[ 6-   $��52,0,0,0q�5b      $
��Xóa��5c      6!   �� 5e      -   5        $ 
btnCapNhat��5e      #��[ 61   $��52,0,0,0q�5f      $��Cập nhật��5g      6&   �� 5i      -   5        $ btnThem��5i      #��[ 6.   $��52,0,0,0q�5j      $��Thêm��5k      6"   5m      5n      5o      � 5q   
   $��2��6   .+�5r      �� $��25,10,25,10q�6   .+�5s      �� $��Danh sách bệnh án��6   $��18(�5t      $
��Bold"�5u      $��Inter)�5v      $��Center=�5w      &   System.Windows.Controls.DataGrid  5z      -   5        	  Name$	 dgDanhSachBenhAn��5z      
   AutoGenerateColumns$
 False��65      HeadersVisibility$ Column=�5{      
�������   RowBackground
 �����   AlternatingRowBackground �#e8f1ff   CanUserAddRows$ False��5         CanUserDeleteRows$ False��5�      $��0,5,0,0q�5�      $	��170��5�         ColumnHeaderStyle 5�      �� 5�       �� 
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
NgayTaoLap+  5�      $ Tình trạng��6-   $ *	 6C    6M   �� *	TinhTrang+  5�      $ Bệnh��6-   $ *	 6<    6F   �� *Benh+  5�      $ Hướng điều trị��6-   $
 1.5*	 6H    6U   �� *	DieuTri+5�      5�      5�      5�      5�      