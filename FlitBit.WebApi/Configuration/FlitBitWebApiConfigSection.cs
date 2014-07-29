using System.Configuration;

namespace FlitBit.WebApi.Configuration
{
  /// <summary>
  ///   Configuration section for webapi.
  /// </summary>
  public sealed class FlitBitWebApiConfigSection : ConfigurationSection
  {
    internal const string SectionName = "flitbit.webapi";
    const string PropertyNameEnableEnvelope = "enableEnvelope";

    
    /// <summary>
    ///   Indicates whether api result bodies should be returned in standard envelopes.
    /// </summary>
    [ConfigurationProperty(PropertyNameEnableEnvelope, DefaultValue = true)]
    public bool EnableEnvelope { get { return (bool)this[PropertyNameEnableEnvelope]; } set { this[PropertyNameEnableEnvelope] = value; } }
    
    internal static FlitBitWebApiConfigSection Instance
    {
      get
      {
        var config = ConfigurationManager.GetSection(SectionName)
          as FlitBitWebApiConfigSection;
        return config ?? new FlitBitWebApiConfigSection();
      }
    }
  }
}
