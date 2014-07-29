using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using FlitBit.Core.Log;

namespace FlitBit.WebApi.Formatters
{
	public class WebApiJsonMediaTypeFormatter : FlitBitJsonMediaTypeFormatter
	{
    static readonly ILogSink LogSink = typeof(WebApiJsonMediaTypeFormatter).GetLogSink();


		public override Task WriteToStreamAsync(Type type, object value, System.IO.Stream writeStream, HttpContent content, System.Net.TransportContext transportContext)
		{
		  if (!typeof(EnvelopeResult).IsAssignableFrom(type))
		  {
		    if (typeof(HttpError).IsAssignableFrom(type))
		    {
          var err = (HttpError)value;
		      if (err.ExceptionType == null)
		      {
		        var current = HttpContext.Current;
		        if (current != null)
		        {
		          value = err.ModelState != null 
                ? EnvelopeResult.FromStatusCode(current.Response.StatusCode, err.ModelState) 
                : EnvelopeResult.FromStatusCode(current.Response.StatusCode, err.Message);
		        }
		      }
		      else
		      {
#if DEBUG
		        var reason = new
		        {
		          Message = err.ExceptionMessage,
		          err.ExceptionType,
		          err.StackTrace
		        };
#else
		      var reason = new
		      {
		        Message = err.ExceptionMessage,
		        err.ExceptionType
		      };
#endif
		        LogSink.Error("HttpError bubbled to media type formatter.", err.ExceptionMessage, err.ExceptionType,
		          err.StackTrace);
		        value = EnvelopeResult.ErrorFrom(reason);
		      }
		    }
		    else if (!typeof(HttpResponseMessage).IsAssignableFrom(type))
		    {
		      value = EnvelopeResult.SuccessFrom(value);
		    }
		  }

		  return base.WriteToStreamAsync(value.GetType(), value, writeStream, content, transportContext);
		}
	}
}
