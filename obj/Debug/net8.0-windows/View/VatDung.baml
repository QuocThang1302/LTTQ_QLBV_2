   M S B A M L   `   `   `  ����I  EQuanLyBenhVien, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:QuanLyBenhVien.ViewQuanLyBenhVien.View  5      M+clr-namespace:QuanLyBenhVien.CustomControlsQuanLyBenhVien.CustomControls  5      !    QuanLyBenhVien.View.VatDung   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:QuanLyBenhVien.View     5      FcustomControls+clr-namespace:QuanLyBenhVien.CustomControls     5      X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  5      -   5        .��5      �   � RowDefinitions  5   
   �� 5      $��60 �6   �� 5      $
��1.4* �6   �� 5      $��* �6   .+�5   
   *   $QuanLyBenhVien.CustomControls.Search  -   5          Name$ searchControl��5       $��70,10,70,10q�67   �� 5   
   $��1��6   $��
25,10,25,5q�5      $��2q�5      
���   �
������  �� CornerRadius$ 10��5      ._�5      �   5      �� 5      �� 5      �� 5      �� 5      �� 5       $
��1.1* �6$   .+�5#      �� $��0��6   $��Thông tin vật dụng��5$      $��Center=�5%      $��Center=�5&      $��28(�5'      $
��Bold"�5(      $��Inter)�5)      �� 5+      $��1��6    �� Orientation$ 
Horizontal=�6*   $��	40,0,40,0q�6C   .+�5,      �� $��Mã vật dụng:��6    $��Center=�5-          TextBlockStyle#��[  5.       �� 50      -   5         ��Name$ tbMaVatDung��50      $��	20,0,20,0q�51      $	��250��52        TextBoxStyles#��[ 53      .��6F   54      �� 56      $��Tên vật dụng:��6    $��Center=�57       $��30,0,0,0q�58       #��[  59       �� 5;      -   5        $ 	tbVatDung��5;      $��20,0,0,0q�5<      $	��250��5=      #��[ 5>      .��6F   5?      5@      �� 5B      $��2��6   $ 
Horizontal=�6*   $��	40,0,40,0q�6C   .+�5C      �� $��	Mô tả:��6    $��Center=�5D       #��[  5E       �� 5G      -   5        $ tbMoTa��5G      $��	84,0,20,0q�5H      $	��250��5I      #��[ 5J      .��6F   5K      �� 5M      $��Giá tiền:��6    $��Center=�5N       $��30,0,0,0q�5O       #��[  5P       �� 5R      -   5        $ 	tbGiaTien��5R      $��70,0,0,0q�5S      $	��250��5T      #��[ 5U      .��6F   5V      5W      �� 5Y      $��3��6   $ 
Horizontal=�6*   $��	40,0,40,0q�6C   .+�5Z      �� $��Số lượng:��6    $��Center=�5[       #��[  5\       �� 5^      -   5        $ 	tbSoLuong��5^      $��	54,0,20,0q�5_      $	��250��5`      #��[ 5a      .��6F   5b      �� 5d      $��Người quản lý:��6    $��Center=�5e       $��30,0,0,0q�5f       #��[  5g       �� 5i      -   5        $ tbQuanLy��5i      $��9,0,0,0q�5j      $	��250��5k      #��[ 5l      .��6F   5m      5n      �� 5p      $��4��6   $ 
Horizontal=�6*   $��RightToLeft=�6C   .+�5q      �� -	   5         ��Name$ btnXoa��5q        childviewButton#��[ 6-   $��52,0,0,0q�5r      $
��Xóa��5s      6+   �� 5u      -
   5        $ 
btnCapNhat��5u      #��[ 61   $��52,0,0,0q�5v      $��Cập nhật��5w      60   �� 5y      -   5        $ btnThem��5y      #��[ 6.   $��52,0,0,0q�5z      $��Thêm��5{      6,   5|      5}      5~      � 5�   
   $��2��6   .+�5�      �� $��25,10,25,10q�6   .+�5�      �� $��Danh sách vật dụng��6   $��18(�5�      $
��Bold"�5�      $��Inter)�5�      $��Center=�5�      &   System.Windows.Controls.DataGrid  5�      -   5          Name$ dgDanhSachVatDung��5�         AutoGenerateColumns$ False��66      CanUserAddRows$ False��5�      	   CanUserDeleteRows$	 False��5�      
   HeadersVisibility$
 Column=�5�      
�������   RowBackground
 �����   AlternatingRowBackground �#e8f1ff$��0,5,0,0q�5�      $	��180��5�         ColumnHeaderStyle 5�      �� 5�       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5�      ��  �� Property � �� �� Value
 ��ح�6;   �� 5�       � ��$ Center=�6K   �� 5�       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ Medium"�6;   �� 5�       � ��$ 10q�68   5�         RowStyle 5�      �� 5�      )  #System.Windows.Controls.DataGridRow"  6    .�5�      ��  � ��$
 Left=�6K   �� 5�       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ Medium"�6;    �� Triggers 5�      P� 5�   "    P� Property �@ 
IsSelected P� Value$
 True��6@   . �5�   &   ��  � �� �#d4ebf86C   �� 5�   &    � �� �#00416a6C   5�   #   5�         	CellStyle 5�      �� 5�      *  $System.Windows.Controls.DataGridCell"  6    .�5�      ��  � ��$
 Left=�6K   �� 5�       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ 5q�68    5�      P� 5�   "    �@ 
IsSelected$
 True��6@   . �5�   &   ��  � �� �#d4ebf86C   �� 5�   &    � �� �#00416a6C   5�   #   5�         Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$ Mã vật dụng��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.7* 6B   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 6O   �� *	MaVatDung+  5�      $ Tên vật dụng��6-   $ * 6C    6M   �� *
TenVatDung+  5�      $ Mô tả��6-   $
 1.5* 6<    6I   �� *MoTa+  5�      $ Số lượng��6-   $
 0.6* 6?    6L   �� *	SoLuong+  5�      $ Giá tiền��6-   $
 0.6* 6?    6L   �� *Gia+  5�      $ Mã quản lý��6-   $
 0.6* 6A    6N   �� *
MaQuanLy+5�      5�      5�      5�      5�      