   M S B A M L   `   `   `  ����I  EQuanLyBenhVien, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:QuanLyBenhVien.ViewQuanLyBenhVien.View  5      M+clr-namespace:QuanLyBenhVien.CustomControlsQuanLyBenhVien.CustomControls  5      *    $QuanLyBenhVien.View.Khoa_ChuyenNganh   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:QuanLyBenhVien.View     5      FcustomControls+clr-namespace:QuanLyBenhVien.CustomControls     5      -   5        .��5
      �   � RowDefinitions  5   
   �� 5      $��80 �6   6*   �� 5      $��0.82* �6   6-   �� 5      $��0.18* �6   6-   .+�5   
   *   $QuanLyBenhVien.CustomControls.Search  -   5          Name$ searchControl��5       $��70,10,70,30q�67   � 5   
   $��1��6     5      �� 5      $
��0.1* �6    �� 5      $
��0.9* �6    .+�5      � $��0��6    � ColumnDefinitions 5      �� 5      �� 5      .+�5      �� $��0��6      TextBlockStyle#��[  5       $��Danh sách khoa��5!      $��26(�5"      $��
15,0,130,0q�5#      $��Right=�5$      $��Center=�5%      �� 5'      $��1��6   #��[  5(      $��Danh sách Chuyên ngành��5)      $��26(�5*      $��	75,0,15,0q�5+      $
��Left=�5,      $��Center=�5-      5.      � 50      -   5        $��1��50       51      �� 52      �� 53      .+�56      &   System.Windows.Controls.DataGrid  -   5          Name$ dgvKhoa��56      $��0��6,      AutoGenerateColumns$ False��57         CanUserAddRows$ False��58         CanUserDeleteRows$ False��59         HeadersVisibility$ Column=�5:      
�������   RowBackground
 �����	   AlternatingRowBackground	 �#e8f1ff$��	40,5,10,0q�5>      $
��Auto��5?      
   ColumnHeaderStyle
 5A      �� 5B       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5C      ��  �� Property � �� �� Value
 ��ح�6;   �� 5D       � ��$ Center=�6K   �� 5E       � ��$ Inter)�6;   �� 5F       � ��$ 14(�69   �� 5G       � ��$ Medium"�6;   �� 5H       � ��$ 10q�68   5I         RowStyle 5M      �� 5N      )  #System.Windows.Controls.DataGridRow"  6    .�5O      ��  � ��$
 Left=�6K   �� 5P       � ��$ Inter)�6;   �� 5Q       � ��$ 14(�69   �� 5R       � ��$ Medium"�6;    �� Triggers 5S      P� 5T   "    P� Property �@ 
IsSelected P� Value$
 True��6@   . �5U   &   ��  � �� �#d4ebf86C   �� 5V   &    � �� �#00416a6C   5W   #   5Y         	CellStyle 5]      �� 5^      *  $System.Windows.Controls.DataGridCell"  6    .�5_      ��  � ��$
 Left=�6K   �� 5`       � ��$ Inter)�6;   �� 5a       � ��$ 14(�69   �� 5b       � ��$ 5q�68    5c      P� 5d   "    �@ 
IsSelected$
 True��6@   . �5e   &   ��  � �� �#d4ebf86C   �� 5f   &    � �� �#00416a6C   5g   #   5i         Columns 5l      0  *System.Windows.Controls.DataGridTextColumn  5m      ,  &System.Windows.Controls.DataGridColumn   Header$ Mã khoa��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6Y   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 6>   �� *MaKhoa+6Y     5n      $ 	Tên khoa��6   $ * 6C    6'   �� *	TenKhoa+6C     5o      $ Trưởng khoa��6   $
 1.1* 6I    6*   �� *
TruongKhoa+6I   5q        5s      -   5        $ dgvChuyenNganh��5s      $��1��63   $ False��5t      $ False��5u      $ False��5v      $ Column=�5w      
�������
 �����	 �#e8f1ff$��	10,5,40,0q�5{      $
��Auto��5|      
 5~      �� 5      "  6    .�5�      ��  � ��
 ��ح�6;   �� 5�       � ��$ Center=�6K   �� 5�       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ Medium"�6;   �� 5�       � ��$ 10q�68   5�       5�      �� 5�      "  6    .�5�      ��  � ��$
 Left=�6K   �� 5�       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ Medium"�6;    5�      P� 5�   "    �@ 
IsSelected$
 True��6@   . �5�   &   ��  � �� �#d4ebf86C   �� 5�   &    � �� �#00416a6C   5�   #   5�       5�      �� 5�      "  6    .�5�      ��  � ��$
 Left=�6K   �� 5�       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ 5q�68    5�      P� 5�   "    �@ 
IsSelected$
 True��6@   . �5�   &   ��  � �� �#d4ebf86C   �� 5�   &    � �� �#00416a6C   5�   #   5�       5�        5�      $ Mã chuyên ngành��6-   $
 0.7* 6h    6F   �� *MaChuyenNganh+6h     5�      $ Tên chuyên ngành��6-   $ * 6j    6G   �� *TenChuyenNganh+6j     5�      $
 Khoa��6-   $
 0.5* 6T    6;   �� *Khoa+6T   5�      5�      5�      �� 5�   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��RightToLeft=�6;   .+�5�      �� -   5         ��Name$ btnThem2��5�        childviewButton#��[ 6'   $��52,0,0,0q�5�      $��Cập nhật��5�      6(   �� 5�      -   5        $ btnThem1��5�      #��[ 6'   $��	360,0,0,0q�5�      $��Cập nhật��5�      6(   5�      5�      5�      