using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;

namespace FlitBit.WebApi
{
  public class WebApiActionResult<TResult> : IHttpActionResult
  {
    readonly Task<TResult> _producer;
    readonly Func<TResult, HttpResponseMessage> _handleSuccess;
    readonly Func<Exception, HttpResponseMessage> _handleError;

    public WebApiActionResult(HttpRequestMessage request, string messageOnSuccess, Task<TResult> result)
      : this(request, messageOnSuccess, result, null, null)
    {}

    public WebApiActionResult(HttpRequestMessage request, Task<TResult> result)
      : this(request, null, result, null, null)
    {}

    public WebApiActionResult(HttpRequestMessage request, Task<TResult> result,
      Func<TResult, HttpResponseMessage> handleSuccess)
      : this(request, null, result, handleSuccess, null)
    {}

    public WebApiActionResult(HttpRequestMessage request, string messageOnSuccess, Task<TResult> result,
      Func<TResult, HttpResponseMessage> handleSuccess)
      : this(request, messageOnSuccess, result, handleSuccess, null)
    {}

    public WebApiActionResult(HttpRequestMessage request, Task<TResult> result,
      Func<TResult, HttpResponseMessage> handleSuccess,
      Func<Exception, HttpResponseMessage> handleError)
      : this(request, null, result, handleSuccess, handleError)
    {}

    public WebApiActionResult(HttpRequestMessage request, Task<TResult> result,
      Func<Exception, HttpResponseMessage> handleError)
      : this(request, null, result, null, handleError)
    {}

    public WebApiActionResult(HttpRequestMessage request, string messageOnSuccess, Task<TResult> result,
      Func<TResult, HttpResponseMessage> handleSuccess,
      Func<Exception, HttpResponseMessage> handleError)
    {
      Contract.Requires<ArgumentNullException>(request != null);
      Contract.Requires<ArgumentNullException>(result != null);
      Request = request;
      MessageOnSuccess = messageOnSuccess ?? SuccessResult.DefaultSuccessMessage;
      _handleSuccess = handleSuccess ?? HandleSuccessfulResult;
      _handleError = handleError ?? HandleException;
      _producer = result;
    }

    public string MessageOnSuccess { get; private set; }

    public HttpResponseMessage HandleSuccessfulResult(TResult result)
    {
      return Request.MakeSuccessResponse(HttpStatusCode.OK, result);
    }

    HttpResponseMessage HandleException(Exception ex)
    {
      var aggregate = ex as AggregateException;
      if (aggregate != null)
      {
        var flat = aggregate.Flatten();
        var inner = flat.InnerExceptions.FirstOrDefault(e => e is HttpResponseException) as HttpResponseException;
        return HandleException(inner);
      }

      var http = ex as HttpResponseException;
      if (http != null)
      {
        var err = http.Response;
        return Request.CreateResponse(err.StatusCode, EnvelopeResult.FromStatusCode((int)err.StatusCode));
      }
      
      var messages = new StringBuilder(400);
      messages.Append(ex.GetType().FullName).Append(" - ").Append(ex.Message);

      return Request.CreateResponse(HttpStatusCode.InternalServerError, EnvelopeResult.ErrorFrom(messages.ToString()));
    }

    public HttpRequestMessage Request { get; private set; }

    public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
    {
      return _producer.ContinueTransform(inner => this._handleSuccess(inner.Result), _handleError);
    }
  }

}
