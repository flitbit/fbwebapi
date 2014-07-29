using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;

namespace FlitBit.WebApi
{
  public class WebApiActionResult : IHttpActionResult
  {
    public WebApiActionResult(HttpRequestMessage request, ErrorResult err)
      : this(request, HttpStatusCode.InternalServerError, err)
    {}

    public WebApiActionResult(HttpRequestMessage request, HttpStatusCode statusCode, ErrorResult err)
    {
      this.Request = request;
      this.StatusCode = statusCode;
      this.ErrorResult = err;
    }

    public WebApiActionResult(HttpRequestMessage request)
      : this(request, HttpStatusCode.OK, new SuccessResult())
    {}


    public WebApiActionResult(HttpRequestMessage request, HttpStatusCode statusCode, SuccessResult successResult)
    {
      this.Request = request;
      this.StatusCode = statusCode;
      this.SuccessResult = successResult;
    }

    public HttpRequestMessage Request { get; private set; }

    public ErrorResult ErrorResult { get; private set; }

    public SuccessResult SuccessResult { get; private set; }

    public HttpStatusCode StatusCode { get; private set; }

    public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
    {
      var res = this.Request.CreateResponse(this.StatusCode);
      if (this.ErrorResult != null)
      {
        PerformWriteWebflowErrorBody(res);
      }
      else
      {
        PerformWriteWebflowSuccessBody(res);
      }
      return Task.FromResult(res);
    }

    protected virtual void PerformWriteWebflowErrorBody(HttpResponseMessage response)
    {
      var content = new StringContent(JsonConvert.SerializeObject(this.ErrorResult));
      content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
      response.Content = content;
    }

    protected virtual void PerformWriteWebflowSuccessBody(HttpResponseMessage response)
    {
      var content = new StringContent(JsonConvert.SerializeObject(this.SuccessResult));
      content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
      response.Content = content;
    }
  }
}