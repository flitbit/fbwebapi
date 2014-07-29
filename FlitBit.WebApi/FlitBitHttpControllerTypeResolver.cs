using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using FlitBit.IoC.Meta;

namespace FlitBit.WebApi
{
	[ContainerRegister(typeof(IHttpControllerTypeResolver), RegistrationBehaviors.Default)]
	public class FlitBitHttpControllerTypeResolver : DefaultHttpControllerTypeResolver
	{
		public FlitBitHttpControllerTypeResolver()
			: base(t => t != null && t.IsVisible && typeof(IHttpController).IsAssignableFrom(t)) { }
	}
}
