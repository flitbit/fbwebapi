#region COPYRIGHT© 2013-2014 Phillip Clark. All rights reserved.
// For licensing information see License.txt (MIT style licensing).
#endregion

using System.Net.Http.Formatting;
using FlitBit.IoC;
using FlitBit.WebApi.Configuration;
using FlitBit.WebApi.Formatters;
using FlitBit.Wireup;
using FlitBit.Wireup.Meta;

[assembly: Wireup(typeof(FlitBit.WebApi.AssemblyWireup))]

namespace FlitBit.WebApi
{
  /// <summary>
  ///   Wires up this assembly.
  /// </summary>
  public sealed class AssemblyWireup : IWireupCommand
  {

    /// <summary>
    ///   Wires up this assembly.
    /// </summary>
    /// <param name="coordinator"></param>
    public void Execute(IWireupCoordinator coordinator)
    {
      // Register custom media type formatter to ensure emitted
      // types like DTOs can be configured on the web api methods.
      var config = FlitBitWebApiConfigSection.Instance;
      if (config.EnableEnvelope)
      {
        Container.Root
                 .ForType<JsonMediaTypeFormatter>()
                 .Register<WebApiJsonMediaTypeFormatter>()
                 .End()
                 .ForType<XmlMediaTypeFormatter>()
                 .Register<WebApiXmlMediaTypeFormatter>()
                 .End();
      }
      else
      {
        Container.Root
                 .ForType<JsonMediaTypeFormatter>()
                 .Register<FlitBitJsonMediaTypeFormatter>()
                 .End()
                 .ForType<XmlMediaTypeFormatter>()
                 .Register<FlitBitXmlMediaTypeFormatter>()
                 .End();
      }
    }
  }
}