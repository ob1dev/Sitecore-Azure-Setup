using System;
using System.Configuration;
using System.Web.Configuration;
using Microsoft.WindowsAzure;
using Sitecore.Diagnostics;

namespace Sitecore.Azure.Startup
{
  /// <summary>
  /// Provides a way to overwrite configuration settings in Sitecore application for the Azure platform.
  /// </summary>
  public class ConfigRegistration
  {
    /// <summary>
    /// The connection string list setting.
    /// </summary>
    private const string ConnectionStringNamesSetting = "ConnectionStringNames";

    /// <summary>
    /// The array of connection string names that must be overwritten.
    /// </summary>
    private static readonly string[] ConnectionStringNames;

    /// <summary>
    /// Initializes the <see cref="ConfigRegistration"/> class.
    /// </summary>
    static ConfigRegistration()
    {
      ConnectionStringNames = CloudConfigurationManager.GetSetting(ConnectionStringNamesSetting).Replace(" ", string.Empty).Split(',');
    }

    /// <summary>
    /// Overwrites all matching connection strings from the ServiceConfiguration.cscfg file.
    /// </summary>
    public static void OverwriteConnectionStrings()
    {
      var configuration = WebConfigurationManager.OpenWebConfiguration("~");

      foreach (var name in ConnectionStringNames)
      {
        OverwriteConnectionString(name, configuration);
      }

      configuration.Save();
    }

    /// <summary>
    /// Overwrites a specified connection string.
    /// </summary>
    /// <param name="name">The name.</param>
    public static void OverwriteConnectionString(string name)
    {
      var configuration = WebConfigurationManager.OpenWebConfiguration("~");
        
      OverwriteConnectionString(name, configuration);

      configuration.Save(ConfigurationSaveMode.Minimal);

      var message = string.Format("The '{0}' connection string has been overwritten in the configuration file.", name);
      Log.Info(message, typeof(ConfigRegistration));
    }

    /// <summary>
    /// Overwrites the connection string.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="configuration">The configuration.</param>
    protected static void OverwriteConnectionString(string name, System.Configuration.Configuration configuration)
    {
      try
      {
        var connectionString = CloudConfigurationManager.GetSetting(name);
        var section = (ConnectionStringsSection) configuration.GetSection("connectionStrings");
        var connectionStringSetting = section.ConnectionStrings[name];

        if (connectionString != null & connectionStringSetting != null)
        {
          connectionStringSetting.ConnectionString = connectionString;
        }

      }
      catch (Exception exception)
      {
        var message = string.Format("Exception occurred during the 'Application_Start' event on overwriting the '{0}' connection strings.", name);
        Log.Error(message, exception, typeof(ConfigRegistration));
      }
    }
  }
}