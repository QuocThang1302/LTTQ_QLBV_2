   M S B A M L   `   `   `  ����I  EQuanLyBenhVien, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:QuanLyBenhVien.ViewQuanLyBenhVien.View  5   	   C&clr-namespace:QuanLyBenhVien.ViewModelQuanLyBenhVien.ViewModel  5   	   "    QuanLyBenhVien.View.PhanCong   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:QuanLyBenhVien.View     5   	   <	viewModel&clr-namespace:QuanLyBenhVien.ViewModel     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5	   	    � WindowStyle$
 None=�5
   	   
������  � AllowsTransparency$
 True��5   	   $	��900��5   	   $	��820��6    � DataContext 5      0   *QuanLyBenhVien.ViewModel.PhanCongViewModel  5   
   .��5      � .+�5   
   �� $��0��6   ���#00416a$��10q�5       �� CornerRadius$ 10��5      ���#d4ebf8 �� Padding$ 0q�5      $��0q�5      ._�5      �  � RowDefinitions 5      �� 5      $	��100 �6$   �� 5      $��2* �6$   �� 5       $��20 �6$   .+�5$      �� $��PHÂN CÔNG��6   $��40(�5%      $
��Bold"�5&      $��Inter)�5'      ���#00416a$��Center=�5)      $��	0,50,0,10q�5*      $��0��5+      � 5.      $��1��6   $��	0,10,0,10q�6$    5/      �� 50      $��* �6(   �� 51      $��* �6(   .+�54      � $��0��6    � ColumnDefinitions 55      �� 56      $
��0.4* �6/   �� 57      $
��0.6* �6/    5:      �� 5;      �� 5<      �� 5=      �� 5>      �� 5?      �� 5@      .+�5C      �� $��0��6$   $��0��64      TextBlockStyle#��[  5D   $   $��Mã lịch trực:��5E   $   $��Center=�5F   $   $��24(�5G   $   $��	130,0,0,0q�5H   $   �� 5J      $��0��6$   $��1��64   #��[  5K   $   $��Mã bác sĩ:��5L   $   $��Center=�5M   $   $��24(�5N   $   $��	130,0,0,0q�5O   $   �� 5Q      $��0��6$   $��2��64   #��[  5R   $   $��Ngày trực:��5S   $   $��Center=�5T   $   $��24(�5U   $   $��	130,0,0,0q�5V   $   �� 5X      $��0��6$   $��3��64   #��[  5Y   $   $��Phân công:��5Z   $   $��Center=�5[   $   $��24(�5\   $   $��	130,0,0,0q�5]   $   �� 5_      $��0��6$   $��4��64   #��[  5`   $   $��Trạng thái:��5a   $   $��Center=�5b   $   $��24(�5c   $   $��	130,0,0,0q�5d   $   �� 5f      -   5        $��TxB_MaLichTruc��5f   "   $��1��68   $��0��6H     TextBoxStyles#��[ 5h   "   $��Center=�5i   "   $
��Left=�5j   "   $��	10,0,30,0q�5k   "   $	��340��5l   "   ��5g   "   �� *
MaLichTruc+5l   "   �� 5n      -   5        $��TxB_MaBacSi��5n   "   $��1��65   $��1��6E   #��[ 5p   "   $��Center=�5q   "   $
��Left=�5r   "   $��	10,0,30,0q�5s   "   $	��340��5t   "   ��5o   "   �� *	MaBacSi+5t   "   �� 5v      -   5        $��TxB_NgayTruc��5v   "   $��1��66   $��2��6F   #��[ 5x   "   $��Center=�5y   "   $
��Left=�5z   "   $��	10,0,30,0q�5{   "   $	��340��5|   "   ��5w   "   �� *
NgayTruc+ �� StringFormat$ d��	 �� TargetNullValue$	  ��5|   "   �� 5~      -   5        $��TxB_PhanCong��5~   "   $��1��66   $��3��6F   #��[ 5�   "   $��Center=�5�   "   $
��Left=�5�   "   $��	10,0,30,0q�5�   "   $	��340��5�   "   ��5   "   �� *
PhanCong+5�   "   �� 5�      -   5        $��TxB_TrangThai��5�   "   $��1��67   $��4��6G   #��[ 5�   "   $��Center=�5�   "   $
��Left=�5�   "   $��	10,0,30,0q�5�   "   $	��340��5�   "   ��5�   "   �� *	TrangThai+5�   "   �� 5�      $��1��6%   $��5��65   
 �� Orientation$
 
Horizontal=�5�   %   $��RightToLeft=�5�   %   .+�5�      �� -   5         ��Name$ btnThoat��5�   %     childviewButton#��[ 5�   %   $��	100,0,0,0q�5�   %   $��Thoát��5�   %   5�   %   �� 5�      #��[ 6%   $��52,0,0,0q�5�   %   $��
Đồng ý��5�   %   ��66   �� *DongYCommand+5�      5�      � 5�      $��1��6    5�      �� 5�      $
��0.1* �6,   �� 5�      $
��0.9* �6,   .+�5�      � $��0��6    5�      �� 5�   "   �� 5�   "   .+�5�      �� $��0��6(   #��[  5�   (   $��Danh sách nhân viên��5�   (   $��22(�5�   (   $��	15,0,70,0q�5�   (   $��Right=�5�   (   $��Center=�5�   (   �� 5�      $��1��6(   #��[  5�   (   $!��Danh sách đã phân công��5�   (   $��22(�5�   (   $��	53,0,15,0q�5�   (   $
��Left=�5�   (   $��Center=�5�   (   5�      � 5�      $��1��6    5�      �� 5�   "   $��* �63   �� 5�   "   $��* �63   .+�5�      &   System.Windows.Controls.DataGrid  $��0��6'      AutoGenerateColumns$ False��5�   '      CanUserAddRows$ False��5�   '      HeadersVisibility$ Column=�5�   '   
�������   RowBackground
 �����   AlternatingRowBackground �#e8f1ff   CanUserDeleteRows$ False��5�   '   $��	30,5,10,0q�5�   '   $
��Auto��5�   '   ��5�   7   �� *
DSNhanVien+   ColumnHeaderStyle 5�   "   �� 5�   &    �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6,   .�5�   *   ��  �� Property � �� �� Value
 ��ح�6G   �� 5�   *    � ��$ Center=�6W   �� 5�   *    � ��$ Inter)�6G   �� 5�   *    � ��$ 14(�6E   �� 5�   *    � ��$ Medium"�6G   �� 5�   *    � ��$ 10q�6D   5�   '      RowStyle 5�   "   �� 5�   &   )  #System.Windows.Controls.DataGridRow"  6,   .�5�   *   ��  � ��$
 Left=�6W   �� 5�   *    � ��$ Inter)�6G   �� 5�   *    � ��$ 14(�6E   �� 5�   *    � ��$ Medium"�6G    �� Triggers 5�   *   P� 5�   .    P� Property �@ 
IsSelected P� Value$
 True��6L   . �5�   2   ��  � �� �#d4ebf86O   �� 5�   2    � �� �#00416a6O   5�   /   5�   '      	CellStyle 5�   "   �� 5�   &   *  $System.Windows.Controls.DataGridCell"  6,   .�5�   *   ��  � ��$
 Left=�6W   �� 5�   *    � ��$ Inter)�6G   �� 5�   *    � ��$ 14(�6E   �� 5�   *    � ��$ 5q�6D    5�   *   P� 5�   .    �@ 
IsSelected$
 True��6L   . �5�   2   ��  � �� �#d4ebf86O   �� 5�   2    � �� �#00416a6O   5�   /   5�   '      Columns 5�   "   0  *System.Windows.Controls.DataGridTextColumn  5�   &   ,  &System.Windows.Controls.DataGridColumn   Header$ Mã nhân viên��69      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6O   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 6Y   �� *
MaNhanVien+  5�   &   $ 	Họ tên��69   $ * 6I    6S   �� *HoTen+  5�   &   $ Chuyên ngành��69   $
 1.1* 6O    6\   �� *ChuyenNganh+5�        5�      -   5          Name$ 
DSPhanCong��5�   7   $��1��6'   $ False��5�   '   $ False��5�   '   $ False��5   '   $ Column=�5  '   
�������
 ����� �#e8f1ff$��	10,5,30,0q�5  '   $
��Auto��5  '   ��5  '   �� *
DSPhanCong+ 5	  "   �� 5
  &   "  6,   .�5  *   ��  � ��
 ��ح�6G   �� 5  *    � ��$ Center=�6W   �� 5  *    � ��$ Inter)�6G   �� 5  *    � ��$ 14(�6E   �� 5  *    � ��$ Medium"�6G   �� 5  *    � ��$ 10q�6D   5  '    5  "   �� 5  &   "  6,   .�5  *   ��  � ��$
 Left=�6W   �� 5  *    � ��$ Inter)�6G   �� 5  *    � ��$ 14(�6E   �� 5  *    � ��$ Medium"�6G    5  *   P� 5  .    �@ 
IsSelected$
 True��6L   . �5  2   ��  � �� �#d4ebf86O   �� 5  2    � �� �#00416a6O   5  /   5!  '    5%  "   �� 5&  &   "  6,   .�5'  *   ��  � ��$
 Left=�6W   �� 5(  *    � ��$ Inter)�6G   �� 5)  *    � ��$ 14(�6E   �� 5*  *    � ��$ 5q�6D    5+  *   P� 5,  .    �@ 
IsSelected$
 True��6L   . �5-  2   ��  � �� �#d4ebf86O   �� 5.  2    � �� �#00416a6O   5/  /   51  '    54  "     55  &   $ 
Mã lịch��69   $ * 6J    6T   �� *
MaLichTruc+  56  &   $ Mã bác sĩ��69   $
 1.1* 6L    6Y   �� *	MaBacSi+  57  &   $ Ngày trực��69   $
 1.1* 6L    6Y   �� *
NgayTruc+  58  &   $ Phân công��69   $
 1.1* 6L    6Y   �� *
PhanCong+5:     5;     5<     5=     5>     5?     5@     5A     