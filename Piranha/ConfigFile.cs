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
		/// Gets/sets the providers.
		/// </summary>
		[ConfigurationProperty(PROVIDERS, IsRequired=false)]
		public ProviderElement Providers {
			get { return (ProviderElement)this[PROVIDERS] ; }
			set { this[PROVIDERS] = value ; }
		}

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ConfigFile() {
			Settings = new SettingsElement() ;
			Providers = new ProviderElement() ;
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
		/// Gets/if the application is running in passive mode.
		/// </summary>
		[ConfigurationProperty(PASSIVE_MODE, IsRequired=false)]
		public BooleanElement PassiveMode {
			get { return (BooleanElement)this[PASSIVE_MODE] ; }
			set { this[PASSIVE_MODE] = value ; }
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
	/// The provider element of the configuration section.
	/// </summary>
	internal class ProviderElement : ConfigurationElement
	{
		#region Members
        private const string MEDIA_PROVIDER = "mediaProvider";
        private const string CACHE_PROVIDER = "cacheProvider";
        private const string LOG_PROVIDER = "logProvider";
		#endregion

		/// <summary>
		/// Gets/sets the current configured media provider.
		/// </summary>
		[ConfigurationProperty(MEDIA_PROVIDER, IsRequired=false)]
		public StringElement MediaProvider {
			get { return (StringElement)this[MEDIA_PROVIDER] ; }
			set { this[MEDIA_PROVIDER] = value ; }
		}

        /// <summary>
        /// Gets/sets the current configured cache provider.
        /// </summary>
        [ConfigurationProperty(CACHE_PROVIDER, IsRequired = false)]
        public StringElement CacheProvider
        {
            get { return (StringElement)this[CACHE_PROVIDER]; }
            set { this[CACHE_PROVIDER] = value; }
        }

        /// <summary>
        /// Gets/sets the current configured log provider.
        /// </summary>
        [ConfigurationProperty(LOG_PROVIDER, IsRequired = false)]
        public StringElement LogProvider
        {
            get { return (StringElement)this[LOG_PROVIDER]; }
            set { this[LOG_PROVIDER] = value; }
        }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ProviderElement() {
            MediaProvider = new StringElement();
            CacheProvider = new StringElement();
            LogProvider = new StringElement();
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