   M S B A M L   `   `   `  ����I  EQuanLyBenhVien, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:QuanLyBenhVien.ViewQuanLyBenhVien.View  5      M+clr-namespace:QuanLyBenhVien.CustomControlsQuanLyBenhVien.CustomControls  5          QuanLyBenhVien.View.Thuoc   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:QuanLyBenhVien.View     5      FcustomControls+clr-namespace:QuanLyBenhVien.CustomControls     5      X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  5      -   5        .��5      �   � RowDefinitions  5   
   �� 5      $��60 �6   �� 5      $
��1.4* �6   �� 5      $��* �6   .+�5   
   *   $QuanLyBenhVien.CustomControls.Search  -   5          Name$ searchControl��5       $��70,10,70,10q�67   �� 5   
   $��1��6   $��
25,10,25,5q�5      $��2q�5      
���   �
������  �� CornerRadius$ 10��5      ._�5      �   5      �� 5      �� 5      �� 5       �� 5!      �� 5"      $
��1.1* �6$   .+�5%      �� $��0��6      TextBlockStyle#��[  5&      $��Thông tin thuốc��5'      $��Center=�5(      $��Center=�5)      $��28(�5*      �� 5,      $��1��6    �� Orientation$ 
Horizontal=�6*   $��	50,0,50,0q�6C   .+�5-      �� $��Mã thuốc:��6    $��Center=�5.       #��[  5/       �� 51      -   5         ��Name$ 	tbMaThuoc��51      $��	35,0,20,0q�52      $	��220��53        TextBoxStyles#��[ 54      .��6F   55      �� 57      $��Tên thuốc:��6    $��Center=�58       $��35,0,0,0q�59       #��[  5:       �� 5<      -   5        $ tbThuoc��5<      $��20,0,0,0q�5=      $	��300��5>      #��[ 5?      .��6F   5@      5A      �� 5C      $��2��6   $ 
Horizontal=�6*   $��	50,0,50,0q�6C   .+�5D      �� $��Công dụng:��6    $��Center=�5E       #��[  5F       �� 5H      -   5        $ 
tbCongDung��5H      $��21,0,0,0q�5I      $	��692��5J      #��[ 5K      .��6F   5L      5M      �� 5O      $��3��6   $ 
Horizontal=�6*   $��	50,0,50,0q�6C   .+�5P      �� $��Số lượng:��6    $��Center=�5Q       #��[  5R       �� 5T      -   5        $ 	tbSoLuong��5T      $��	40,0,20,0q�5U      $	��155��5V      #��[ 5W      .��6F   5X      �� 5Z      $��Giá tiền:��6    $��Center=�5[       $��10,0,0,0q�5\       #��[  5]       �� 5_      -   5        $ 	tbGiaTien��5_      $��20,0,0,0q�5`      $	��158��5a      #��[ 5b      .��6F   5c      �� 5e      $
��HSD:��6    $��Center=�5f       $��30,0,0,0q�5g       #��[  5h       �� 5j      -   5        $ tbHSD��5j      $��20,0,0,0q�5k      $	��155��5l      #��[ 5m       �� 
IsReadOnly$
 True��5n      5o      � 5p      -	   5         �Name$ popupCalendarHSD��5p      $��False��66   $��Bottom=�6s    � PlacementTarget 6E   ��  �� ElementName$ tbHSD��.��5q      &   System.Windows.Controls.Calendar  -
   5        	  Name$	 calendarHSD��5q   #   $|�False��6p   5r      5s      �� 5u      $��4��6   $ 
Horizontal=�6*   $��RightToLeft=�6C   .+�5v      �� -   5        
 ��Name$
 btnXoa��5v        childviewButton#��[ 6-   $��52,0,0,0q�5w      $
��Xóa��5x      6'   �� 5z      -   5        $
 
btnCapNhat��5z      #��[ 61   $��52,0,0,0q�5{      $��Cập nhật��5|      6,   �� 5~      -   5        $
 btnThem��5~      #��[ 6.   $��52,0,0,0q�5      $��Thêm��5�      6(   5�      5�      5�      � 5�   
   $��2��6   .+�5�      �� $��25,10,25,10q�6   .+�5�      �� $��Danh sách thuốc��6   $��18(�5�      $
��Bold"�5�      $��Inter)�5�      $��Center=�5�      $��Center=�5�      &   System.Windows.Controls.DataGrid  5�      -   5          Name$ dgDanhSachThuoc��5�         AutoGenerateColumns$ False��64      CanUserAddRows$ False��5�         CanUserDeleteRows$ False��5�         HeadersVisibility$ Column=�5�      
�������   RowBackground
 �����   AlternatingRowBackground �#e8f1ff$��0,5,0,0q�5�      $	��180��5�         ColumnHeaderStyle 5�      �� 5�       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5�      ��  �� Property � �� �� Value
 ��ح�6;   �� 5�       � ��$ Center=�6K   �� 5�       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ Medium"�6;   �� 5�       � ��$ 10q�68   5�         RowStyle 5�      �� 5�      )  #System.Windows.Controls.DataGridRow"  6    .�5�      ��  � ��$
 Left=�6K   �� 5�       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ Medium"�6;    �� Triggers 5�      P� 5�   "    P� Property �@ 
IsSelected P� Value$
 True��6@   . �5�   &   ��  � �� �#d4ebf86C   �� 5�   &    � �� �#00416a6C   5�   #   5�         	CellStyle 5�      �� 5�      *  $System.Windows.Controls.DataGridCell"  6    .�5�      ��  � ��$
 Left=�6K   �� 5�       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ 5q�68    5�      P� 5�   "    �@ 
IsSelected$
 True��6@   . �5�   &   ��  � �� �#d4ebf86C   �� 5�   &    � �� �#00416a6C   5�   #   5�         Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$ Mã thuốc��6-      Width5	  /System.Windows.Controls.DataGridLengthConverter$
 0.5*	 6?   1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding 6L   �� *	MaThuoc+  5�      $ Tên thuốc��6-   $
 0.6*	 6@    6M   �� *
TenThuoc+  5�      $ Công dụng��6-   $
 1.5*	 6@    6M   �� *
CongDung+  5�      $ Số lượng��6-   $
 0.6*	 6?    6L   �� *	SoLuong+  5�      $ Giá tiền��6-   $
 0.8*	 6?    6M   �� *	GiaTien+  5�      $ Hạn sử dụng��6-   $
 0.8*	 6B    6O   �� *	HanSuDung+ �� StringFormat$ 
yyyy-MM-dd��5�      5�      5�      5�      5�      