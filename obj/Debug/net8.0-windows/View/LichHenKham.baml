   M S B A M L   `   `   `  ����I  EQuanLyBenhVien, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:QuanLyBenhVien.ViewQuanLyBenhVien.View  5      M+clr-namespace:QuanLyBenhVien.CustomControlsQuanLyBenhVien.CustomControls  5      %    QuanLyBenhVien.View.LichHenKham   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:QuanLyBenhVien.View     5      FcustomControls+clr-namespace:QuanLyBenhVien.CustomControls     5      -   5        .��5      �   � RowDefinitions  5   
   �� 5      $��60 �6   �� 5      $
��1.3* �6   �� 5      $��* �6   .+�5   
   *   $QuanLyBenhVien.CustomControls.Search  -   5          Name$ searchControl��5       $��70,10,70,10q�67   �� 5   
   $��1��6   $��
25,10,25,5q�5      $��2q�5      
���   �
������  �� CornerRadius$ 10��5      ._�5      �   5      �� 5      �� 5      �� 5      �� 5       .+�5#      �� $��0��6      TextBlockStyle#��[  5$      $��Thông tin lịch hẹn��5%      $��Center=�5&      $��Center=�5'      $��28(�5(      �� 5*      $��1��6    �� Orientation$ 
Horizontal=�6*   $��	50,0,50,0q�6C   .+�5+      �� $��Mã lịch hẹn:��6    $��Center=�5,       #��[  5-       �� 5/      -   5         ��Name$ tbMaLichHen��5/      $��48.5,0,20,0q�50      $	��220��51        TextBoxStyles#��[ 52      .��6F   53      �� 55      $��Ngày hẹn khám:��6    $��Center=�56       $��35,0,0,0q�57       #��[  58       �� 5:      -   5        $ tbNgayHenKham��5:      $��20,0,0,0q�5;      $��211.5��5<      #��[ 5=       �� 
IsReadOnly$
 True��5>      5?      � 5@      -   5         �Name$ popupCalendarNgayHenKham��5@      $��False��6>   $��Bottom=�6�    � PlacementTarget 6M   ��  �� ElementName$ tbNgayHenKham��.��5A      &   System.Windows.Controls.Calendar  -   5        	  Name$	 calendarNgayHenKham��5A   #   $|�False��6�   5B      5C      �� 5E      $��2��6   $ 
Horizontal=�6*   $��	50,0,50,0q�6C   .+�5F      �� $��Mã bệnh nhân:��6    $��Center=�5G       #��[  5H       �� 5J      -   5        $ tbMaBenhNhan��5J      $��	22,0,20,0q�5K      $	��220��5L      #��[ 5M      .��6F   5N      �� 5P      $��Mã bác sĩ:��6    $��Center=�5Q       $��35,0,0,0q�5R       #��[  5S       �� 5U      -   5        $ 	tbMaBacSi��5U      $��	80,0,20,0q�5V      $��211.5��5W      #��[ 5X      .��6F   5Y      5Z      �� 5\      $��3��6   $ 
Horizontal=�6*   $��RightToLeft=�6C   .+�5]      �� -	   5        
 ��Name$
 btnXoa��5]        childviewButton#��[ 6-   $��52,0,0,0q�5^      $
��Xóa��5_      6+   �� 5a      -
   5        $
 
btnCapNhat��5a      #��[ 61   $��52,0,0,0q�5b      $��Cập nhật��5c      60   �� 5e      -   5        $
 	btnDangKi��5e      #��[ 60   $��52,0,0,0q�5f      $��
Đăng kí��5g      6/   5h      5i      5j      � 5l   
   $��2��6   .+�5m      �� $��25,10,25,10q�6   .+�5n      �� $��Danh sách lịch hẹn��6   $��18(�5o      $
��Bold"�5p      $��Inter)�5q      $��Center=�5r      $��Center=�5s      &   System.Windows.Controls.DataGrid  5u      -   5          Name$ 
dgvLichHen��5u         AutoGenerateColumns$ False��6/      CanUserAddRows$ False��5v         CanUserDeleteRows$ False��5w         HeadersVisibility$ Column=�5x      
�������   RowBackground
 �����   AlternatingRowBackground �#e8f1ff$��0,5,0,0q�5|      $	��190��5}         ColumnHeaderStyle 5      �� 5�       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5�      ��  �� Property � �� �� Value
 ��ح�6;   �� 5�       � ��$ Center=�6K   �� 5�       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ Medium"�6;   �� 5�       � ��$ 10q�68   5�         RowStyle 5�      �� 5�      )  #System.Windows.Controls.DataGridRow"  6    .�5�      ��  � ��$
 Left=�6K   �� 5�       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ Medium"�6;    �� Triggers 5�      P� 5�   "    P� Property �@ 
IsSelected P� Value$
 True��6@   . �5�   &   ��  � �� �#d4ebf86C   �� 5�   &    � �� �#00416a6C   5�   #   5�         	CellStyle 5�      �� 5�      *  $System.Windows.Controls.DataGridCell"  6    .�5�      ��  � ��$
 Left=�6K   �� 5�       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ 5q�68    5�      P� 5�   "    �@ 
IsSelected$
 True��6@   . �5�   &   ��  � �� �#d4ebf86C   �� 5�   &    � �� �#00416a6C   5�   #   5�         Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$ Mã lịch hẹn��6-      Width5	  /System.Windows.Controls.DataGridLengthConverter$ *	 6`   1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding 6B   �� *	MaLichHen+6`     5�      $ Mã bệnh nhân��6-   $ *	 6b    6C   �� *
MaBenhNhan+6b     5�      $ Mã bác sĩ��6-   $ *	 6\    6@   �� *	MaBacSi+6\     5�      $ Ngày hẹn khám��6-   $ *	 6    6D   �� *NgayHenKham+ �� StringFormat$ 
yyyy-MM-dd��6   5�      5�      5�      5�      5�      