﻿#pragma checksum "..\..\..\UserControls\SwitchUC.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FA1DD1D915B6C84D669EFFADEF4D4B45"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18408
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
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


namespace UENSimulation.UserControls {
    
    
    /// <summary>
    /// SwitchUC
    /// </summary>
    public partial class SwitchUC : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\UserControls\SwitchUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border border;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\UserControls\SwitchUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label_0;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\UserControls\SwitchUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label_1;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\UserControls\SwitchUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label_2;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/UENSimulation;component/usercontrols/switchuc.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\SwitchUC.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.border = ((System.Windows.Controls.Border)(target));
            return;
            case 2:
            this.label_0 = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.label_1 = ((System.Windows.Controls.Label)(target));
            
            #line 12 "..\..\..\UserControls\SwitchUC.xaml"
            this.label_1.MouseEnter += new System.Windows.Input.MouseEventHandler(this.label_1_MouseEnter);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\UserControls\SwitchUC.xaml"
            this.label_1.MouseLeave += new System.Windows.Input.MouseEventHandler(this.label_1_MouseLeave);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\UserControls\SwitchUC.xaml"
            this.label_1.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.label_1_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.label_2 = ((System.Windows.Controls.Label)(target));
            
            #line 13 "..\..\..\UserControls\SwitchUC.xaml"
            this.label_2.MouseEnter += new System.Windows.Input.MouseEventHandler(this.label_2_MouseEnter);
            
            #line default
            #line hidden
            
            #line 13 "..\..\..\UserControls\SwitchUC.xaml"
            this.label_2.MouseLeave += new System.Windows.Input.MouseEventHandler(this.label_2_MouseLeave);
            
            #line default
            #line hidden
            
            #line 13 "..\..\..\UserControls\SwitchUC.xaml"
            this.label_2.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.label_2_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

