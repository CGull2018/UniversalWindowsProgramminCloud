﻿#pragma checksum "C:\Users\Gullickson\Documents\Rasmussen Classes\Oct1-Nov6\UWP Cloud\InventoryMobileBank\InventoryMobileBank\Module02Lab.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0216223CF73AA4E8AB5AB4476E4E2D49"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventoryMobileBank
{
    partial class Module02Lab : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Module02Lab.xaml line 22
                {
                    global::Windows.UI.Xaml.Controls.HyperlinkButton element2 = (global::Windows.UI.Xaml.Controls.HyperlinkButton)(target);
                    ((global::Windows.UI.Xaml.Controls.HyperlinkButton)element2).Click += this.HyperlinkButton_Click_1;
                }
                break;
            case 3: // Module02Lab.xaml line 23
                {
                    this.btnGetAPI = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnGetAPI).Click += this.btnGetAPI_Click;
                }
                break;
            case 4: // Module02Lab.xaml line 25
                {
                    this.txtResult = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

