﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Convergys.Assist.Sched.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Password=a55!stm3;Persist Security Info=True;User ID=StreamAssist;Initial Catalog" +
            "=RTA2;Data Source=SQLEDC23")]
        public string RTAConnection {
            get {
                return ((string)(this["RTAConnection"]));
            }
            set {
                this["RTAConnection"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\_logs\\")]
        public string LogFolderPath {
            get {
                return ((string)(this["LogFolderPath"]));
            }
            set {
                this["LogFolderPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ProdStreamAs" +
            "sist_UpgradeTest;Data Source=.\\SQLEXPRESS_CY")]
        public string SAConnection {
            get {
                return ((string)(this["SAConnection"]));
            }
            set {
                this["SAConnection"] = value;
            }
        }
    }
}
