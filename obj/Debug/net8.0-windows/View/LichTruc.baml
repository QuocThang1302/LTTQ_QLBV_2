   M S B A M L   `   `   `  ����I  EQuanLyBenhVien, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:QuanLyBenhVien.ViewQuanLyBenhVien.View  5      M+clr-namespace:QuanLyBenhVien.CustomControlsQuanLyBenhVien.CustomControls  5      "    QuanLyBenhVien.View.LichTruc   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:QuanLyBenhVien.View     5      FcustomControls+clr-namespace:QuanLyBenhVien.CustomControls     5      -   5        .��5
      �   � RowDefinitions  5   
   �� 5      $��60 �6   6*   �� 5      6   .+�5   
   *   $QuanLyBenhVien.CustomControls.Search  -   5          Name$ searchControl��5       $��70,10,70,10q�67   � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      6%   �� 5      6%   .+�5      �� .+�5      �� $��Danh sách công việc��6   $
��Bold"�5      $��Inter)�5      $��Center=�5      $��Center=�5      $��24(�5      $��10q�5      5      &   System.Windows.Controls.DataGrid  5       -   5          Name$ dgvCongViec��5          AutoGenerateColumns$ False��60      CanUserAddRows$ False��5!         CanUserDeleteRows$ False��5"         HeadersVisibility$ Column=�5#      
�������   RowBackground
 �����	   AlternatingRowBackground	 �#e8f1ff$��	30,5,30,0q�5'      $	��400��5(      
   ColumnHeaderStyle
 5*      �� 5+       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5,      ��  �� Property � �� �� Value
 ��ح�6;   �� 5-       � ��$ Center=�6K   �� 5.       � ��$ Inter)�6;   �� 5/       � ��$ 14(�69   �� 50       � ��$ Medium"�6;   �� 51       � ��$ 10q�68   52         RowStyle 56      �� 57      )  #System.Windows.Controls.DataGridRow"  6    .�58      ��  � ��$
 Left=�6K   �� 59       � ��$ Inter)�6;   �� 5:       � ��$ 14(�69   �� 5;       � ��$ Medium"�6;    �� Triggers 5<      P� 5=   "    P� Property �@ 
IsSelected P� Value$
 True��6@   . �5>   &   ��  � �� �#d4ebf86C   �� 5?   &    � �� �#00416a6C   5@   #   5B         	CellStyle 5F      �� 5G      *  $System.Windows.Controls.DataGridCell"  6    .�5H      ��  � ��$
 Left=�6K   �� 5I       � ��$ Inter)�6;   �� 5J       � ��$ 14(�69   �� 5K       � ��$ 5q�68    5L      P� 5M   "    �@ 
IsSelected$
 True��6@   . �5N   &   ��  � �� �#d4ebf86C   �� 5O   &    � �� �#00416a6C   5P   #   5R         Columns 5U      0  *System.Windows.Controls.DataGridTextColumn  5V      ,  &System.Windows.Controls.DataGridColumn   Header$ Mã công việc��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6b   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 6C   �� *
MaCongViec+6b     5W      $ Tên công việc��6-   $ * 6d    6D   �� *TenCongViec+6d     5X      $ Mô tả��6-   $ * 6]    6<   �� *MoTaCongViec+6]     5Y      $ Ghi chú��6-   $ * 6Y    6>   �� *GhiChu+6Y   5[      5]      �� 5^      $��1��6   .+�5_      �� $��Danh sách phân công��6   $
��Bold"�5`      $��Inter)�5a      $��Center=�5b      $��Center=�5c      $��24(�5d      $��10q�5e      5f        5g      -   5        $ dgvPhanCong��5g      $ False��60   $ False��5h      $ False��5i      $ Column=�5j      
�������
 �����	 �#e8f1ff$��	30,5,30,0q�5n      $	��400��5o      
 5q      �� 5r      "  6    .�5s      ��  � ��
 ��ح�6;   �� 5t       � ��$ Center=�6K   �� 5u       � ��$ Inter)�6;   �� 5v       � ��$ 14(�69   �� 5w       � ��$ Medium"�6;   �� 5x       � ��$ 10q�68   5y       5}      �� 5~      "  6    .�5      ��  � ��$
 Left=�6K   �� 5�       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ Medium"�6;    5�      P� 5�   "    �@ 
IsSelected$
 True��6@   . �5�   &   ��  � �� �#d4ebf86C   �� 5�   &    � �� �#00416a6C   5�   #   5�       5�      �� 5�      "  6    .�5�      ��  � ��$
 Left=�6K   �� 5�       � ��$ Inter)�6;   �� 5�       � ��$ 14(�69   �� 5�       � ��$ 5q�68    5�      P� 5�   "    �@ 
IsSelected$
 True��6@   . �5�   &   ��  � �� �#d4ebf86C   �� 5�   &    � �� �#00416a6C   5�   #   5�       5�        5�      $ Mã lịch trực��6-   $
 1.2* 6b    6C   �� *
MaLichTruc+6b     5�      $ Bác sĩ��6-   $
 0.8* 6Y    6=   �� *	MaBacSi+6Y     5�      $ Ngày trực��6-   $ * 6x    6@   �� *
NgayTruc+ �� StringFormat$ 
yyyy-MM-dd��6x     5�      $ Phân công��6-   $ * 6]    6@   �� *
PhanCong+6]     5�      $ Trạng thái��6-   $ * 6_    6A   �� *	TrangThai+6_   5�      �� 5�      $��0,10,0,0q�6    �� Orientation$ 
Horizontal=�5�      $��RightToLeft=�66   .+�5�      �� -   5         ��Name$ 
btnCapNhat��5�         childviewButton#��[  5�      $��	30,15,0,0q�5�      $��Cập nhật��5�      60   5�      5�      5�      5�      5�      