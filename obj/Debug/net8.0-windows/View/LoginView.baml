   M S B A M L   `   `   `  ����I  EQuanLyBenhVien, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:QuanLyBenhVien.ViewQuanLyBenhVien.View  5   	   C&clr-namespace:QuanLyBenhVien.ViewModelQuanLyBenhVien.ViewModel  5   	   M+clr-namespace:QuanLyBenhVien.CustomControlsQuanLyBenhVien.CustomControls  6B   #    QuanLyBenhVien.View.LoginView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:QuanLyBenhVien.View     5   	   <	viewModel&clr-namespace:QuanLyBenhVien.ViewModel     5   	   Fcustomcontrols+clr-namespace:QuanLyBenhVien.CustomControls     6B   -   5          � Title$  	LoginView��5	   	   $	��550��6   $	��800��6(    � WindowStyle$
 None=�5
   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   
������  � AllowsTransparency$
 True��5   	    � DataContext 5      -  'QuanLyBenhVien.ViewModel.LoginViewModel  5   
    � 	Resources 5         BooleanToVisibility��%   &	        �� 5   
   6'   5      y�5      �� 5   
    �� Path$ IsViewVisible�6    �� Mode$ TwoWay=�6'   	 �� 	Converter#	 [  65   .��5      �� 
 �� CornerRadius$
 12��6   ��5   
   �� 5       �� ImageSource$ /Images/hospital.jpg��6    j� Stretch$
 Fill=�5      ._�5"   
   �� $
 10��6   $��3q�5#      ��5$      �� 5%       �� 
StartPoint$
 0, 0&�6&    �� EndPoint$
 1, 1&�68   .��5&      �  � Color$ #03045E��6#    � Offset$ 0Q�63   � 5'      $ #0077B6��6#   $	 0.6Q�63   � 5(      $ #CAF0F8��6#   $ 1Q�63   5)      ��5,      �� 5-       �� Opacity$	 0.7Q�6&   .��5.      � $ #12c2e9��6#   $ 0Q�63   � 5/      $ #bbadff��6#   $	 0.8Q�63   50      ._�53      �  � RowDefinitions 54      �� 55      $��30 �6$   �� 56      .+�59      �  � ColumnDefinitions 5:      �� 5;      �� 5<      $��30 �6+   �� 5=      $��30 �6+   �� 5>      $��5 �6+   .+�5A      �� ���#FFFFFF$��12(�5B       $��
Montserrat)�5C       $��0��5D       $��Center=�5E       $��10, 0, 0, 0q�5F       �� 5G      .�6   ĐĂNG NHẬP6*   5H      �� 5J      -   5         ��Name$ btnMinimize��5J      $��1��5K      $��0q�5M      
�������$��
Montserrat)�5O      $��20(�5P       � Cursor$
 Hand��5Q      .��5R      �� .�6   -6"   ��5T      �� 5U       �� 
TargetType" ��6$   .�5V   "   ��  �� Property � �� �� Value �#48CAE46?    �� Triggers 5W   "   P� 5X   &    P� Property �@J�IsMouseOver P� Value$
 True��6E   . �5Y   *   ��  � �� �#0077B66G   5Z   '   5\      ��5_      �� 5`       �� 
TargetType" ��6.   .R�5a   "   �� $��18��6)   $��18��64   $
 9��5b   )   #��z��5c   )   ._�5d   &   �� $��Center=�67   $��Center=�5e   7   5f   #   5g      5i      �� 5k      -   5        $ btnClose��5k      $��2��5l      $��0q�5n      
�������$��
Montserrat)�5p      $��16(�5q      $
 Hand��5r      .��5s      �� .�6   X6"   ��5u      �� 5v      " ��6$   .�5w   "   ��  � �� �#FFAFCC6?    5x   "   P� 5y   &    �@J�IsMouseOver$
 True��6E   . �5z   *   ��  � �� �#fb6f926G   5{   '   5}      ��5�      �� 5�      " ��6.   .R�5�   "   �� $��18��6)   $��18��64   $
 9��5�   )   #��z��5�   )   ._�5�   &   �� $��Center=�67   $��Center=�5�   7   5�   #   5�      5�      5�      �� 5�      $	��280��6   $��2��5�       �� Orientation$ Vertical=�5�      $��0, 35, 0, 60q�5�      ��5�      �� 5�       �� Color$ White��6*   $	 0.7Q�68   .+�5�      �� $��/Images/hospital_logo.png��6   $	��120��5�      $	��120��6(   $
��Fill=�65   $��0,5,0,5q�5�      �� 5�      $��Spring Aroma Hopital��6    $
��Bold"�5�       $��23(�5�       ���#00416a$��Calibri)�5�        �� TextAlignment$ Center=�5�       $��0q�5�       �� 5�      $��   Mã nhân viên��6    ���#00416a$��16(�5�       $��Medium"�5�       $��Inter)�5�       $��0, 25, 0, 0q�5�       �� 5�      -   5          ��Name$  txtID��5�      $��15(�5�      $��Medium"�5�      $��
Montserrat)�5�      
���   �! �� 
CaretBrush
! �����
�������$��	0,0,0,1.5q�5�      $��28��5�      $	��250��5�      $��Bottom=�5�      $��0,5,0,0q�5�      $��35,0,0,0q�5�      ��5�      �� *UserID+" �� UpdateSourceTrigger$" PropertyChanged=�.��5�   1   ��5�      �� 5�      $ /Images/user.png��6)   $ Uniform=�5�   )   # j� 
AlignmentX$
# Left=�5�   )   .��5�   .   5�      �� 5�      $��   Mật Khẩu��6    ���#00416a$��16(�5�       $��Medium"�5�       $��Inter)�5�       $��0, 15, 0, 0q�5�       7   1QuanLyBenhVien.CustomControls.BindablePasswordBox  5�      $��28��5�   9   $	��250��5�   9   $��0,5,0,0q�5�   9   $   Password$ 5�   9   �� *
Password+$ TwoWay=�$" PropertyChanged=�5�      �� 5�      
���  ��$��12(�5�       $��Medium"�5�       $��
Montserrat)�5�       $��
0, 8, 0, 0q�5�       $
��Wrap=�5�       ��5�       �� *ErrorMessage+5�       �� 5�      -   5        $ btnLogin��5�      $��2��5�      $��0q�5�      
�������$��
Montserrat)�5�      $��16(�5�      $
 Hand��5�      $��0,20,0,0q�5�      ��5�      �� *LoginCommand+.��5�      �� .�6   ĐĂNG NHẬP6*   ��5�      �� 5�      " ��6$   .�5�   "   ��  � �� �#48CAE46?    5�   "   P� 5�   &    �@J�IsMouseOver$
 True��6E   . �5�   *   ��  � �� �#0077B66G   5�   '   5�      ��5�      �� 5�      " ��6.   .R�5�   "   �� $	��150��6)   $��40��65   $
 20��5�   )   #��z��5�   )   ._�5�   &   �� $��Center=�67   $��Center=�5�   7   5�   #   5�      5�      5�      5�      5�      5�      5�      