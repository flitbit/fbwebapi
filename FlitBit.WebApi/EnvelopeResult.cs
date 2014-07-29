using System;
using System.Net;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace FlitBit.WebApi
{
	[Serializable]
	[DataContract]
	public sealed class EnvelopeResult
	{
	  public static readonly string DefaultSuccessMessage = "Ok";
	  public static readonly string DefaultErrorMessage = "Unexpected";

    [DataMember(EmitDefaultValue = true, Name = "success")]
    [JsonProperty("success", NullValueHandling = NullValueHandling.Ignore)]
    public string Success { get; private set; }

    [DataMember(EmitDefaultValue = false, Name = "result")]
    [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
    public object Result { get; private set; }

    [DataMember(EmitDefaultValue = true, Name = "error")]
    [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
    public string Error { get; private set; }

	  [DataMember(EmitDefaultValue = false, Name = "reason"),
	   JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
	  public object Reason { get; private set; }

	  public static EnvelopeResult SuccessFrom(object result)
    {
      return new EnvelopeResult(true, DefaultSuccessMessage, result);
    }

    public static EnvelopeResult SuccessFrom(string message, object result)
    {
      return new EnvelopeResult(true, message, result);
    }
    public static EnvelopeResult SuccessFrom(HttpStatusCode code, object result)
    {
      return new EnvelopeResult(true, Enum.GetName(typeof(HttpStatusCode), code), result);
    }

    public static EnvelopeResult ErrorFrom(object reason)
    {
      return new EnvelopeResult(false, DefaultErrorMessage, reason);
    }
    public static EnvelopeResult ErrorFrom(string message, object reason)
    {
      return new EnvelopeResult(false, message, reason);
    }

	  public static EnvelopeResult FromStatusCode(HttpStatusCode httpStatusCode)
	  {
	    return FromStatusCode((int)httpStatusCode, null);
	  }

    public static EnvelopeResult FromStatusCode(HttpStatusCode httpStatusCode, object reason)
    {
      return FromStatusCode((int)httpStatusCode, reason);
    }

	  public static EnvelopeResult FromStatusCode(int httpStatusCode)
	  {
      return FromStatusCode(httpStatusCode, null);
	  }

    public static EnvelopeResult FromStatusCode(int httpStatusCode, object reason)
    {
      switch (httpStatusCode)
      {
        case 300:
          return new EnvelopeResult(false,
            "MultipleChoices", reason ??
            "More than one option is available; please disambiguate."
          );
        case 301:
          return new EnvelopeResult(false,
            "Moved Permanently", reason ??
            "The resource has been permanently moved."
          );
        case 400:
          return new EnvelopeResult(false,
            "BadRequest", reason ??
            "The request could not be understood due to malformed syntax."
          );
        case 401:
          return new EnvelopeResult(false,
            "Unauthorized", reason ??
            "The request requires user authentication."
          );
        case 402:
          return new EnvelopeResult(false,
            "PaymentRequired", reason ?? null
          );
        case 403:
          return new EnvelopeResult(false,
            "Forbidden", reason ?? null
          );
        case 404:
          return new EnvelopeResult(false,
            "NotFound", reason ??
            "Unable to find a resource matching the requested URI."
          );
        case 405:
          return new EnvelopeResult(false,
            "MethodNotAllowed", reason ??
            "The resource does not allow that method."
          );
        case 406:
          return new EnvelopeResult(false,
            "NotAcceptable", reason ??
            "The resource can only generate responses that are not acceptable according to the accept header provided."
          );
        case 408:
          return new EnvelopeResult(false,
            "RequestTimeout", reason ??
            "Client endpoint taking too long to produce a request."
          );
        case 409:
          return new EnvelopeResult(false,
            "Conflict", reason ??
            "The request conflicts with the current state of the resource."
          );
        case 410:
          return new EnvelopeResult(false,
            "Gone", reason ??
            "The requested resource is no longer available on the server and no forwarding address is known."
          );
        case 411:
          return new EnvelopeResult(false,
            "LengthRequired", reason ??
            "The request cannot be accepted without a content-length."
          );
        case 412:
          return new EnvelopeResult(false,
            "PreconditionFailed", reason ??
            "A given precondition evaluated to false."
          );
        case 413:
          return new EnvelopeResult(false,
            "EntityTooLarge", reason ??
            "The requested entity is too large."
          );
        case 415:
          return new EnvelopeResult(false,
            "UnsupportedMediaType", reason ??
            "The resource does not support the media type provided."
          );
        case 417:
          return new EnvelopeResult(false,
            "ExpectationFailed", reason ??
            "A given expectation cannot be met by the server."
          );
        case 422:
          return new EnvelopeResult(false,
            "UnprocessableEntity", reason ??
            "The given entity does not conform to the server's requirements for the resource."
          );
        case 428:
          return new EnvelopeResult(false,
            "PreconditionRequired", reason ??
            "The server requires that the request be conditional."
          );
        case 429:
          return new EnvelopeResult(false,
            "TooManyRequests", reason ??
            "The client has sent too many requests in rapid succession and is being rate-limited."
          );
        case 500:
          return new EnvelopeResult(false,
            "InternalServerError", reason ??
            "The server encountered an unexpected condition which prevented it from fulfilling the request."
          );
        case 501:
          return new EnvelopeResult(false,
            "NotImplemented", reason ??
            "The server does not support the functionality required to fulfill the request."
          );
        case 502:
          return new EnvelopeResult(false,
            "BadGateway", reason ??
            "The server recieved an invalid response from the upstream server while attempting to fulfill the request."
          );
        case 503:
          return new EnvelopeResult(false,
            "ServiceUnavailable", reason ??
            "The server is currently unable to handle the request."
          );
        case 504:
          return new EnvelopeResult(false,
            "GatewayTimeout", reason ??
            "The server failed to receive a timely response from the upstream server."
          );
        case 505:
          return new EnvelopeResult(false,
            "HttpVersionNotSupported", reason ??
            "The server does not support the version of HTTP specifie."
          );
      }
      return new EnvelopeResult(false, Convert.ToString(httpStatusCode), reason);
    }


	  private EnvelopeResult(bool success, string msg, object data)
	  {
	    if (success)
	    {
	      Success = msg;
	      Result = data;
	    }
	    else
	    {
	      Error = msg;
	      Reason = data;
	    }
	  }
	}
}
