using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using FlitBit.IoC;
using FlitBit.IoC.Meta;

namespace FlitBit.WebApi
{
	[ContainerRegister(typeof(IHttpControllerActivator), RegistrationBehaviors.Default)]
	public class FlitBitIoCHttpControllerActivator : IHttpControllerActivator
	{
		public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
		{
		  using (var container = IoC.Create.SharedOrNewContainer())
		  {
		    if (container.CanConstruct(controllerType))
		    {
		      return container.NewUntyped(LifespanTracking.External, controllerType) as IHttpController;
		    }
		  }

		  return default(IHttpController);
		}
	}

}
