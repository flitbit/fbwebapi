using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace FlitBit.WebApi
{
  public static class HttpRequestMessageExtensions
  {
    const string MimeTypeApplicationJson = "application/json";

    public static HttpResponseMessage MakeSuccessResponse<TResult>(this HttpRequestMessage request, HttpStatusCode code, TResult result)
    {
      var res = request.CreateResponse(code);
      var content = new StringContent(JsonConvert.SerializeObject(new SuccessResult(code.ToString(), result)));
      content.Headers.ContentType = new MediaTypeHeaderValue(MimeTypeApplicationJson);
      res.Content = content;
      return res;
    }

    public static HttpResponseMessage MakeSuccessResponse(this HttpRequestMessage request, HttpStatusCode code)
    {
      var res = request.CreateResponse(code);
      var content = new StringContent(JsonConvert.SerializeObject(new SuccessResult(code.ToString())));
      content.Headers.ContentType = new MediaTypeHeaderValue(MimeTypeApplicationJson);
      res.Content = content;
      return res;
    }

    public static HttpResponseMessage MakeErrorResponse<TReason>(this HttpRequestMessage request, HttpStatusCode code, TReason reason)
    {
      var res = request.CreateResponse(code);
      var content = new StringContent(JsonConvert.SerializeObject(new SuccessResult(code.ToString(), reason)));
      content.Headers.ContentType = new MediaTypeHeaderValue(MimeTypeApplicationJson);
      res.Content = content;
      return res;
    }

    public static HttpResponseMessage MakeErrorResponse(this HttpRequestMessage request, HttpStatusCode code)
    {
      var res = request.CreateResponse(code);
      var content = new StringContent(JsonConvert.SerializeObject(ErrorResult.FromStatusCode((int)code)));
      content.Headers.ContentType = new MediaTypeHeaderValue(MimeTypeApplicationJson);
      res.Content = content;
      return res;
    }
  }
}
