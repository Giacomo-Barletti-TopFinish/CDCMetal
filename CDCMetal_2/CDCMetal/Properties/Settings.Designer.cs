﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CDCMetal.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\\\\dc02\\CONDIVISA\\Metal Plus\\Qualità - BAP e COLLAUDI GUCCI\\COLLAUDI")]
        public string PathCollaudo {
            get {
                return ((string)(this["PathCollaudo"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\\\\dc02\\CONDIVISA\\Metal Plus\\Qualità - BAP e COLLAUDI GUCCI\\COLLAUDI\\SCHEDE TECNIC" +
            "HE")]
        public string PathSchedeTecnic {
            get {
                return ((string)(this["PathSchedeTecnic"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("KONICA MINOLTA CM-A145")]
        public string StrumentoColore {
            get {
                return ((string)(this["StrumentoColore"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("XRF Bowman serie P (S/N P1718131)")]
        public string StrumentoSpessore {
            get {
                return ((string)(this["StrumentoSpessore"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\\\\dc02\\Galvanica Condivisa\\Analisi Piombo_Cadmio\\Certificati")]
        public string PathAnalisiPiombo {
            get {
                return ((string)(this["PathAnalisiPiombo"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\\\\dc02\\GalvanicaFiles\\Laboratorio e Backup\\LABORATORIO\\Referti Lab\\Referti {0}")]
        public string PathRefertiLaboratorio {
            get {
                return ((string)(this["PathRefertiLaboratorio"]));
            }
        }
    }
}
