   M S B A M L   `   `   `  ����I  EQuanLyBenhVien, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:QuanLyBenhVien.ViewQuanLyBenhVien.View  5      M+clr-namespace:QuanLyBenhVien.CustomControlsQuanLyBenhVien.CustomControls  5      %    QuanLyBenhVien.View.LichHenKham   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:QuanLyBenhVien.View     5      FcustomControls+clr-namespace:QuanLyBenhVien.CustomControls     5      -   5        .��5
      �   � RowDefinitions  5   
   �� 5      $��60 �6   �� 5      $
��1.3* �6   �� 5      $��* �6   .+�5   
   *   $QuanLyBenhVien.CustomControls.Search  -   5          Name$ searchControl��5       $��70,10,70,10q�67   �� 5   
   $��1��6   $��
25,10,25,5q�5      $��2q�5      
���   �
������  �� CornerRadius$ 10��5      ._�5      �   5      �� 5      �� 5      �� 5      �� 5      .+�5!      �� $��0��6      TextBlockStyle#��[  5"      $��Thông tin lịch hẹn��5#      $��Center=�5$      $��Center=�5%      $��28(�5&      �� 5(      $��1��6    �� Orientation$ 
Horizontal=�6*   $��	50,0,50,0q�6C   .+�5)      �� $��Mã lịch hẹn:��6    $��Center=�5*       #��[  5+       �� 5-      -   5         ��Name$ tbMaLichHen��5-      $��48.5,0,20,0q�5.      $	��220��5/        TextBoxStyles#��[ 50      .��6F   51      �� 53      $��Ngày hẹn khám:��6    $��Center=�54       $��35,0,0,0q�55       #��[  56       �� 58      -   5        $ tbNgayHenKham��58      $��20,0,0,0q�59      $��211.5��5:      #��[ 5;      .��5<       5=      5>      �� 5@      $��2��6   $ 
Horizontal=�6*   $��	50,0,50,0q�6C   .+�5A      �� $��Mã bệnh nhân:��6    $��Center=�5B       #��[  5C       �� 5E      -   5        $ tbMaBenhNhan��5E      $��	22,0,20,0q�5F      $	��220��5G      #��[ 5H      .��6F   5I      �� 5K      $��Mã bác sĩ:��6    $��Center=�5L       $��35,0,0,0q�5M       #��[  5N       �� 5P      -   5        $ 	tbMaBacSi��5P      $��	80,0,20,0q�5Q      $��211.5��5R      #��[ 5S      .��6F   5T      5U      �� 5W      $��3��6   $ 
Horizontal=�6*   $��RightToLeft=�6C   .+�5X      �� -   5         ��Name$ btnXoa��5X        childviewButton#��[ 6-   $��52,0,0,0q�5Y      $
��Xóa��5Z      6+   �� 5\      -   5        $ 
btnCapNhat��5\      #��[ 61   $��52,0,0,0q�5]      $��Cập nhật��5^      60   �� 5`      -	   5        $ 	btnDangKi��5`      #��[ 60   $��52,0,0,0q�5a      $��
Đăng kí��5b      6/   5c      5d      5e      � 5g   
   $��2��6   .+�5h      �� $��25,10,25,10q�6   .+�5i      �� $��Danh sách lịch hẹn��6   $��18(�5j      $
��Bold"�5k      $��Inter)�5l      $��Center=�5m      $��Center=�5n      &   System.Windows.Controls.DataGrid  5p      -
   5          Name$ 
dgvLichHen��5p         AutoGenerateColumns$ False��6/      CanUserAddRows$ False��5q      	   CanUserDeleteRows$	 False��5r      
   HeadersVisibility$
 Column=�5s      
�������   RowBackground
 �����   AlternatingRowBackground �#e8f1ff$��0,5,0,0q�5w      $	��190��5x         ColumnHeaderStyle 5z      �� 5{       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5|      ��  �� Property � �� �� Value
 ��ح�6;   �� 5}       � ��$ Center=�6K   �� 5~       � ��$ Inter)�6;   �� 5       � ��$ 14(�69   �� 5�       � ��$ Medium"�6;   �� 5�       � ��$ 10q�68   5�         RowStyle 5�      �� 5�      )  #System.Windows.Controls.DataGridRow"  6    .�5�      ��  � ��$
 Left=�6K   �� 5�       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ Medium"�6;    �� Triggers 5�      P� 5�   "    P� Property �@ 
IsSelected P� Value$
 True��6@   . �5�   &   ��  � �� �#d4ebf86C   �� 5�   &    � �� �#00416a6C   5�   #   5�         	CellStyle 5�      �� 5�      *  $System.Windows.Controls.DataGridCell"  6    .�5�      ��  � ��$
 Left=�6K   �� 5�       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ 5q�68    5�      P� 5�   "    �@ 
IsSelected$
 True��6@   . �5�   &   ��  � �� �#d4ebf86C   �� 5�   &    � �� �#00416a6C   5�   #   5�         Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$ Mã lịch hẹn��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6`   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 6B   �� *	MaLichHen+6`     5�      $ Mã bệnh nhân��6-   $ * 6b    6C   �� *
MaBenhNhan+6b     5�      $ Mã bác sĩ��6-   $ * 6\    6@   �� *	MaBacSi+6\     5�      $ Ngày hẹn khám��6-   $ * 6d    6D   �� *NgayHenKham+6d   5�      5�      5�      5�      5�      