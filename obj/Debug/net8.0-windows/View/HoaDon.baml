   M S B A M L   `   `   `  ����I  EQuanLyBenhVien, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:QuanLyBenhVien.ViewQuanLyBenhVien.View  5      M+clr-namespace:QuanLyBenhVien.CustomControlsQuanLyBenhVien.CustomControls  5           QuanLyBenhVien.View.HoaDon   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:QuanLyBenhVien.View     5      FcustomControls+clr-namespace:QuanLyBenhVien.CustomControls     5      X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  5      -   5        .��5      �   � RowDefinitions  5   
   �� 5      $��80 �6   6*   �� 5      $��0.82* �6   6-   �� 5      $��0.18* �6   6-   .+�5   
   *   $QuanLyBenhVien.CustomControls.Search  -   5          Name$ searchControl��5       $��70,15,70,15q�67   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 	dgvHoaDon��5      $��1��6&      AutoGenerateColumns$ False��5         CanUserAddRows$ False��5         CanUserDeleteRows$ False��5         HeadersVisibility$ Column=�5      
�������   RowBackground
 �����   AlternatingRowBackground �#e8f1ff$��	15,5,15,0q�5      $
��Auto��5      	   ColumnHeaderStyle	 5      �� 5       
 �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"
  6   .�5!      ��  �� Property � �� �� Value
 ��ح�63   �� 5"       � ��$ Center=�6C   �� 5#       � ��$ Inter)�63   �� 5$       � ��$ 14(�61   �� 5%       � ��$ Medium"�63   �� 5&       � ��$ 10q�60   5'         RowStyle 5+      �� 5,      )  #System.Windows.Controls.DataGridRow"
  6   .�5-      ��  � ��$
 Left=�6C   �� 5.       � ��$ Inter)�63   �� 5/       � ��$ 14(�61   �� 50       � ��$ Medium"�63    �� Triggers 51      P� 52       P� Property �@ 
IsSelected P� Value$
 True��68   . �53      ��  � �� �#d4ebf86;   �� 54       � �� �#00416a6;   55      57         	CellStyle 5;      �� 5<      *  $System.Windows.Controls.DataGridCell"
  6   .�5=      ��  � ��$
 Left=�6C   �� 5>       � ��$ Inter)�63   �� 5?       � ��$ 14(�61   �� 5@       � ��$ 5q�60    5A      P� 5B       �@ 
IsSelected$
 True��68   . �5C      ��  � �� �#d4ebf86;   �� 5D       � �� �#00416a6;   5E      5G         Columns 5J      0  *System.Windows.Controls.DataGridTextColumn  5K      ,  &System.Windows.Controls.DataGridColumn   Header$ Mã hóa đơn��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6V   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 69   �� *
MaHoaDon+6V     5L      $ Tên hóa đơn��6%   $ * 6X    6:   �� *	TenHoaDon+6X     5M      $ Mã bệnh nhân��6%   $
 1.1* 6Z    6;   �� *
MaBenhNhan+6Z     5N      $ Mã nhân viên��6%   $ * 6Z    6;   �� *
MaNhanVien+6Z     5O      $ Ngày lập��6%   $ * 6t    67   �� *NgayLapHoaDon+ �� StringFormat$ 
yyyy-MM-dd��6t     5P      $ Thành tiền��6%   $ * 6U    69   �� *	GiaTien+6U     5Q      $ Trạng thái��6%   $ * 6W    69   �� *	TrangThai+6W   5S      �� 5U   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��RightToLeft=�6;   .+�5V      �� -   5         ��Name$ btn_Them��5V         childviewButton#��[  5W      $��52,0,0,0q�5X      $
��Xóa��5Y      6#   �� 5]      -   5        $ btn_Xoa��5]      #��[  5^      $��52,0,0,0q�5_      $��Cập nhật��5`      6(   5a      5b      5c      