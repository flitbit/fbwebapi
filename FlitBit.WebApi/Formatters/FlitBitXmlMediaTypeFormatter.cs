using System;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using FlitBit.Core;

namespace FlitBit.WebApi.Formatters
{
	public class FlitBitXmlMediaTypeFormatter : XmlMediaTypeFormatter
	{
		public override bool CanReadType(Type type)
		{
			var container = FactoryProvider.Factory;
			if (container.CanConstruct(type))
			{
				return true;
			}
		  return base.CanReadType(type);
		}

		public override Task<object> ReadFromStreamAsync(Type type, System.IO.Stream readStream, System.Net.Http.HttpContent content, IFormatterLogger formatterLogger)
		{
			var container = FactoryProvider.Factory;
			if (container.CanConstruct(type))
				type = container.GetImplementationType(type);

			return base.ReadFromStreamAsync(type, readStream, content, formatterLogger);
		}
	}
}
