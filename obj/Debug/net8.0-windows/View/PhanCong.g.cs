﻿#pragma checksum "..\..\..\..\View\PhanCong.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2577B12D1635914A3D4BD7A4C465DE4C1F9EF308"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using QuanLyBenhVien.View;
using QuanLyBenhVien.ViewModel;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace QuanLyBenhVien.View {
    
    
    /// <summary>
    /// PhanCong
    /// </summary>
    public partial class PhanCong : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 102 "..\..\..\..\View\PhanCong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxB_MaLichTruc;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\..\..\View\PhanCong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxB_MaBacSi;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\..\..\View\PhanCong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxB_NgayTruc;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\..\..\View\PhanCong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxB_PhanCong;
        
        #line default
        #line hidden
        
        
        #line 134 "..\..\..\..\View\PhanCong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxB_TrangThai;
        
        #line default
        #line hidden
        
        
        #line 148 "..\..\..\..\View\PhanCong.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnThoat;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/QuanLyBenhVien;component/view/phancong.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\PhanCong.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 11 "..\..\..\..\View\PhanCong.xaml"
            ((QuanLyBenhVien.View.PhanCong)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TxB_MaLichTruc = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.TxB_MaBacSi = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.TxB_NgayTruc = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.TxB_PhanCong = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.TxB_TrangThai = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.btnThoat = ((System.Windows.Controls.Button)(target));
            
            #line 149 "..\..\..\..\View\PhanCong.xaml"
            this.btnThoat.Click += new System.Windows.RoutedEventHandler(this.btnThoat_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

