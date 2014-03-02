using System;
using System.Configuration;

namespace Piranha
{
	/// <summary>
	/// The main configuration section for Piranha CMS
	/// </summary>
	internal class ConfigFile : ConfigurationSection
	{
		#region Members
		private const string SETTINGS = "settings" ;
		private const string PROVIDERS = "providers" ;
		#endregion

		/// <summary>
		/// Gets/sets the settings.
		/// </summary>
		[ConfigurationProperty(SETTINGS, IsRequired=false)]
		public SettingsElement Settings {
			get { return (SettingsElement)this[SETTINGS] ; }
			set { this[SETTINGS] = value ; }
		}

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ConfigFile() {
			Settings = new SettingsElement() ;
		}
	}

	/// <summary>
	/// The settings element of the configuration section.
	/// </summary>
	internal class SettingsElement : ConfigurationElement
	{
		#region Members
		private const string DISABLE_METHOD_BINDING = "disableMethodBinding" ;
		private const string DISABLE_MODELSTATE_BINDING = "disableModelStateBinding" ;
		private const string DISABLE_MANAGER = "disableManager" ;
		private const string MANAGER_NAMESPACES = "managerNamespaces" ;
		private const string PASSIVE_MODE = "passiveMode" ;
		private const string PREFIXLESS_PERMALINKS = "prefixlessPermalinks" ;
		#endregion

		/// <summary>
		/// Gets/sets if method binding for Web Pages should be disabled or not.
		/// </summary>
		[ConfigurationProperty(DISABLE_METHOD_BINDING, IsRequired=false)]
		public BooleanElement DisableMethodBinding {
			get { return (BooleanElement)this[DISABLE_METHOD_BINDING] ; }
			set { this[DISABLE_METHOD_BINDING] = value ; }
		}

		/// <summary>
		/// Gets/sets if modelstate binding for Web Pages should be disabled or not.
		/// </summary>
		[ConfigurationProperty(DISABLE_MODELSTATE_BINDING, IsRequired=false)]
		public BooleanElement DisableModelStateBinding {
			get { return (BooleanElement)this[DISABLE_MODELSTATE_BINDING] ; }
			set { this[DISABLE_MODELSTATE_BINDING] = value ; }
		}

		/// <summary>
		/// Gets/sets if the manager interface should be disabled or not.
		/// </summary>
		[ConfigurationProperty(DISABLE_MANAGER, IsRequired=false)]
		public BooleanElement DisableManager {
			get { return (BooleanElement)this[DISABLE_MANAGER] ; }
			set { this[DISABLE_MANAGER] = value ; }
		}

		/// <summary>
		/// Gets/sets the additional manager namespaces.
		/// </summary>
		[ConfigurationProperty(MANAGER_NAMESPACES, IsRequired=false)]
		public StringElement ManagerNamespaces {
			get { return (StringElement)this[MANAGER_NAMESPACES] ; }
			set { this [MANAGER_NAMESPACES] = value ; }
		}

		/// <summary>
		/// Gets/sets if the application is running in passive mode.
		/// </summary>
		[ConfigurationProperty(PASSIVE_MODE, IsRequired=false)]
		public BooleanElement PassiveMode {
			get { return (BooleanElement)this[PASSIVE_MODE] ; }
			set { this[PASSIVE_MODE] = value ; }
		}

		/// <summary>
		/// Gets/sets if the generated permalinks should be prefixless.
		/// </summary>
		[ConfigurationProperty(PREFIXLESS_PERMALINKS, IsRequired=false)]
		public BooleanElement PrefixlessPermalinks {
			get { return (BooleanElement)this[PREFIXLESS_PERMALINKS] ; }
			set { this[PREFIXLESS_PERMALINKS] = value ; }
		}

		/// <summary>
		/// Default constructor.
		/// </summary>
		public SettingsElement() {
			DisableMethodBinding = new BooleanElement() ;
			DisableModelStateBinding = new BooleanElement() ;
			DisableManager = new BooleanElement() ;
			ManagerNamespaces = new StringElement() ;
		}
	}

	/// <summary>
	/// A configuration element with a boolean value.
	/// </summary>
	internal class BooleanElement : ConfigurationElement
	{
		/// <summary>
		/// Gets/sets the element value.
		/// </summary>
		[ConfigurationProperty("value", IsRequired=true)]
		public bool Value { 
			get { return (bool)this["value"] ; }
			set { this["value"] = value ; }
		}
	}

	/// <summary>
	/// A configuration element with a string value.
	/// </summary>
	internal class StringElement : ConfigurationElement
	{
		/// <summary>
		/// Gets/sets the element value.
		/// </summary>
		[ConfigurationProperty("value", IsRequired=true)]
		public string Value {
			get { return (string)this["value"] ; }
			set { this["value"] = value ; }
		}
	}
}