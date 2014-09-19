﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace cloudscribe.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class SetupResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SetupResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("cloudscribe.Resources.SetupResources", typeof(SetupResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to In order to use MySQL under Medium Trust, The MySQL Connector for .NET must be installed in the Global Assembly Cache on the server. If you see a SecurityException in the error message then the Connector is not installed.
        ///You can download the MySql Connector for .NET from here: http://dev.mysql.com/downloads/connector/net/5.0.html.
        /// </summary>
        public static string MediumTrustMySQLMessage {
            get {
                return ResourceManager.GetString("MediumTrustMySQLMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to In order to use PostgreSQL under Medium Trust, the npgsql Data Access library must be installedin the Global Assembly Cache on the server. If you see a SecurityException below then the npgsql component is not installed.
        ///You can get more information or download npgsql from here: http://npgsql.projects.postgresql.org/.
        /// </summary>
        public static string MediumTrustnpgsqlMessage {
            get {
                return ResourceManager.GetString("MediumTrustnpgsqlMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No scripts found in folder.
        /// </summary>
        public static string NoScriptsFilesFoundMessage {
            get {
                return ResourceManager.GetString("NoScriptsFilesFoundMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Running script {0} - {1}.
        /// </summary>
        public static string RunningScriptMessage {
            get {
                return ResourceManager.GetString("RunningScriptMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Setup is disabled but running setup anyway because current user is an administrator..
        /// </summary>
        public static string RunningSetupForAdminUser {
            get {
                return ResourceManager.GetString("RunningSetupForAdminUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to not found. Cannot run scripts..
        /// </summary>
        public static string ScriptFolderNotFoundAddendum {
            get {
                return ResourceManager.GetString("ScriptFolderNotFoundAddendum", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Setup already in progress..
        /// </summary>
        public static string SetupAlreadyInProgress {
            get {
                return ResourceManager.GetString("SetupAlreadyInProgress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Setup is disabled. To continue, please set DisableSetup to false in Web.config.
        /// </summary>
        public static string SetupDisabledMessage {
            get {
                return ResourceManager.GetString("SetupDisabledMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Note: This page shows some information that is helpful during setup and upgrades but for security it would be best not to show any information when the system is up to date. You can disable setup and suppress all information on this page by setting DisableSetup=true in Web.config. When you need to upgrade, you can set this back to false. If you are logged in as Administrator setup will run and you will be able to see this page even if it is disabled in Web.config so you can easily leave it disabled and just [rest of string was truncated]&quot;;.
        /// </summary>
        public static string SetupEnabledMessage {
            get {
                return ResourceManager.GetString("SetupEnabledMessage", resourceCulture);
            }
        }
    }
}
