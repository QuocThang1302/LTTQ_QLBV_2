   M S B A M L   `   `   `  ����I  EQuanLyBenhVien, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:QuanLyBenhVien.ViewQuanLyBenhVien.View  5      M+clr-namespace:QuanLyBenhVien.CustomControlsQuanLyBenhVien.CustomControls  5      &     QuanLyBenhVien.View.ThongTinBenh   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:QuanLyBenhVien.View     5      FcustomControls+clr-namespace:QuanLyBenhVien.CustomControls     5      -   5        .��5
      �   � RowDefinitions  5   
   �� 5      $��60 �6   �� 5      $
��1.5* �6   �� 5      $��* �6   .+�5   
   *   $QuanLyBenhVien.CustomControls.Search  -   5          Name$ searchControl��5       $��70,10,70,10q�67   �� 5   
   $��1��6   $��
25,10,25,5q�5      $��2q�5      
���   �
������  �� CornerRadius$ 10��5      ._�5      �   5      �� 5      �� 5      �� 5      �� 5      �� 5      $
��1.1* �6$   .+�5"      �� $��0��6      TextBlockStyle#��[  5#      $��Thông tin bệnh��5$      $��Center=�5%      $��Center=�5&      $��28(�5'      �� 5)      $��1��6    �� Orientation$ 
Horizontal=�6*   $��	50,0,50,0q�6C   .+�5*      �� $��Mã bệnh:��6    $��Center=�5+       #��[  5,       �� 5.      -   5         ��Name$ tbMaBenh��5.      $��	45,0,20,0q�5/      $	��220��50        TextBoxStyles#��[ 51      .��6F   52      �� 54      $��Tên bệnh:��6    $��Center=�55       $��35,0,0,0q�56       #��[  57       �� 59      -   5        $ tbBenh��59      $��20,0,0,0q�5:      $	��303��5;      #��[ 5<      .��6F   5=      5>      �� 5@      $��2��6   $ 
Horizontal=�6*   $��	50,0,50,0q�6C   .+�5A      �� $��	Mô tả:��6    $��Center=�5B       #��[  5C       �� 5E      -   5        $ tbMoTa��5E      $��72,0,0,0q�5F      $	��690��5G      #��[ 5H      .��6F   5I      5J      �� 5L      $��3��6   $ 
Horizontal=�6*   $��	50,0,50,0q�6C   .+�5M      �� $��Triệu chứng:��6    $��Center=�5N       #��[  5O       �� 5Q      -   5        $ tbTrieuChung��5Q      $��	15,0,20,0q�5R      $	��690��5S      #��[ 5T      .��6F   5U      5V      �� 5X      $��4��6   $ 
Horizontal=�6*   $��RightToLeft=�6C   .+�5Y      �� -   5         ��Name$ btnXoa��5Y        childviewButton#��[ 6-   $��52,0,0,0q�5Z      $
��Xóa��5[      6+   �� 5]      -   5        $ 
btnCapNhat��5]      #��[ 61   $��52,0,0,0q�5^      $��Cập nhật��5_      60   �� 5a      -	   5        $ btnThem��5a      #��[ 6.   $��52,0,0,0q�5b      $��Thêm��5c      6,   5d      5e      5f      � 5h   
   $��2��6   .+�5i      �� $��25,10,25,10q�6   .+�5j      �� $��Danh sách bệnh��6   $��18(�5k      $
��Bold"�5l      $��Inter)�5m      $��Center=�5n      $��Center=�5o      &   System.Windows.Controls.DataGrid  5q      -
   5          Name$ dgDanhSachBenh��5q         AutoGenerateColumns$ False��63      CanUserAddRows$ False��5r      	   CanUserDeleteRows$	 False��5s      
   HeadersVisibility$
 Column=�5t      
�������   RowBackground
 �����   AlternatingRowBackground �#e8f1ff$��0,5,0,0q�5x      $	��172��5y         ColumnHeaderStyle 5{      �� 5|       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5}      ��  �� Property � �� �� Value
 ��ح�6;   �� 5~       � ��$ Center=�6K   �� 5       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ Medium"�6;   �� 5�       � ��$ 10q�68   5�         RowStyle 5�      �� 5�      )  #System.Windows.Controls.DataGridRow"  6    .�5�      ��  � ��$
 Left=�6K   �� 5�       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ Medium"�6;    �� Triggers 5�      P� 5�   "    P� Property �@ 
IsSelected P� Value$
 True��6@   . �5�   &   ��  � �� �#d4ebf86C   �� 5�   &    � �� �#00416a6C   5�   #   5�         	CellStyle 5�      �� 5�      *  $System.Windows.Controls.DataGridCell"  6    .�5�      ��  � ��$
 Left=�6K   �� 5�       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ 5q�68    5�      P� 5�   "    �@ 
IsSelected$
 True��6@   . �5�   &   ��  � �� �#d4ebf86C   �� 5�   &    � �� �#00416a6C   5�   #   5�         Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Mã bệnh��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.5* 6>   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 6K   �� *MaBenh+  5�      $ Tên bệnh��6-   $
 0.5* 6?    6L   �� *	TenBenh+  5�      $ Mô tả��6-   $
 1.3* 6<    6I   �� *MoTa+  5�      $ Triệu chứng��6-   $ * 6B    6L   �� *
TrieuChung+5�      5�      5�      5�      5�      