using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlitBit.WebApi
{
  public static class ApiControllerExtensions
  {
    public static HttpResponseMessage CreateConventionalError(this ApiController controller, HttpStatusCode code,
      object reason)
    {
      return controller.Request.CreateResponse(code, EnvelopeResult.FromStatusCode(code, reason));
    }

    public static HttpResponseMessage CreateConventionalSuccess(this ApiController controller, HttpStatusCode code,
      object result)
    {
      return controller.Request.CreateResponse(code, EnvelopeResult.SuccessFrom(code, result));
    }
  }
}
