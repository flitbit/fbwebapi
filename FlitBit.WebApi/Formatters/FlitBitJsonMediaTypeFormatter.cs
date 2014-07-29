using FlitBit.Core;
using FlitBit.Represent.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace FlitBit.WebApi.Formatters
{
	public class FlitBitJsonMediaTypeFormatter : JsonMediaTypeFormatter
	{
		public FlitBitJsonMediaTypeFormatter()
		{
		  var resolver = this.SerializerSettings.ContractResolver as DefaultContractResolver;
		  if (resolver != null)
				resolver.IgnoreSerializableAttribute = true;
		}

	  public override bool CanReadType(Type type)
		{
			var container = FactoryProvider.Factory;
			if (container.CanConstruct(type))
			{
				return true;
			}
		  return base.CanReadType(type);
		}

	  /// <summary>
	  /// Called during serialization and deserialization to get the <see cref="T:Newtonsoft.Json.JsonSerializer"/>.
	  /// </summary>
	  /// <returns>
	  /// The JsonSerializer used during serialization and deserialization.
	  /// </returns>
	  public override JsonSerializer CreateJsonSerializer()
	  {
	    var settings = CreateDefaultSerializerSettings();
	    settings.Converters = new[]
	    {
	      new FactorySupportedJsonConverter()
	    };
	    return JsonSerializer.Create(settings);
	  }

	  public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, System.Net.Http.HttpContent content, IFormatterLogger formatterLogger)
		{
			var container = FactoryProvider.Factory;
			if ((type.IsAbstract || type.IsInterface) && container.CanConstruct(type))
				type = container.GetImplementationType(type);
      
			return base.ReadFromStreamAsync(type, readStream, content, formatterLogger);
		}
	}
  
}
