using System;
using System.Net;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace FlitBit.WebApi
{
	[Serializable]
	[DataContract]
	public sealed class ErrorResult
	{
	  public static readonly string DefaultErrorMessage = "unexpected error";

	  public static ErrorResult FromStatusCode(HttpStatusCode httpStatusCode)
	  {
	    return FromStatusCode((int)httpStatusCode);
	  }

	  public static ErrorResult FromStatusCode(int httpStatusCode)
	  {
	    switch (httpStatusCode)
	    {

	      case 400:
	        return new ErrorResult
	        {
	          Error = "BadRequest",
	          Reason = "The request could not be understood due to malformed syntax."
	        };
	      case 401:
	        return new ErrorResult
	        {
	          Error = "Unauthorized",
	          Reason = "The request requires user authentication."
	        };
	      case 402:
	        return new ErrorResult
	        {
	          Error = "PaymentRequired"
	        };
	      case 403:
	        return new ErrorResult
	        {
	          Error = "Forbidden"
	        };
	      case 404:
	        return new ErrorResult
	        {
	          Error = "NotFound",
	          Reason = "Unable to find a resource matching the requested URI."
	        };
	      case 405:
	        return new ErrorResult
	        {
	          Error = "MethodNotAllowed",
	          Reason = "The resource does not allow that method."
	        };
	      case 406:
	        return new ErrorResult
	        {
	          Error = "NotAcceptable",
	          Reason =
	            "The resource can only generate responses that are not acceptable according to the accept header provided."
	        };
	      case 408:
	        return new ErrorResult
	        {
	          Error = "RequestTimeout",
	          Reason = "Client endpoint taking too long to produce a request."
	        };
	      case 409:
	        return new ErrorResult
	        {
	          Error = "Conflict",
	          Reason = "The request conflicts with the current state of the resource."
	        };
	      case 410:
	        return new ErrorResult
	        {
	          Error = "Gone",
	          Reason = "The requested resource is no longer available on the server and no forwarding address is known."
	        };
	      case 411:
	        return new ErrorResult
	        {
	          Error = "LengthRequired",
	          Reason = "The request cannot be accepted without a content-length."
	        };
	      case 412:
	        return new ErrorResult
	        {
	          Error = "PreconditionFailed",
	          Reason = "A given precondition evaluated to false."
	        };
	      case 413:
	        return new ErrorResult
	        {
	          Error = "EntityTooLarge",
	          Reason = "The requested entity is too large."
	        };
	      case 415:
	        return new ErrorResult
	        {
	          Error = "UnsupportedMediaType",
	          Reason = "The resource does not support the media type provided."
	        };
	      case 417:
	        return new ErrorResult
	        {
	          Error = "ExpectationFailed",
	          Reason = "A given expectation cannot be met by the server."
	        };
	      case 422:
	        return new ErrorResult
	        {
	          Error = "UnprocessableEntity",
	          Reason = "The given entity does not conform to the server's requirements for the resource."
	        };
	      case 428:
	        return new ErrorResult
	        {
	          Error = "PreconditionRequired",
	          Reason = "The server requires that the request be conditional."
	        };
	      case 429:
	        return new ErrorResult
	        {
	          Error = "TooManyRequests",
	          Reason = "The client has sent too many requests in rapid succession and is being rate-limited."
	        };
	      case 500:
	        return new ErrorResult
	        {
	          Error = "InternalServerError",
	          Reason = "The server encountered an unexpected condition which prevented it from fulfilling the request."
	        };
	      case 501:
	        return new ErrorResult
	        {
	          Error = "NotImplemented",
	          Reason = "The server does not support the functionality required to fulfill the request."
	        };
	      case 502:
	        return new ErrorResult
	        {
	          Error = "BadGateway",
	          Reason =
	            "The server recieved an invalid response from the upstream server while attempting to fulfill the request."
	        };
	      case 503:
	        return new ErrorResult
	        {
	          Error = "ServiceUnavailable",
	          Reason = "The server is currently unable to handle the request."
	        };
	      case 504:
	        return new ErrorResult
	        {
	          Error = "GatewayTimeout",
	          Reason = "The server failed to receive a timely response from the upstream server."
	        };
	      case 505:
	        return new ErrorResult
	        {
	          Error = "HttpVersionNotSupported",
	          Reason = "The server does not support the version of HTTP specifie."
	        };
	    }
	    throw new ArgumentOutOfRangeException("httpStatusCode", "We don't know that code.");
	  }

	  [DataMember(EmitDefaultValue = true, Name = "error")]
    [JsonProperty("error", NullValueHandling = NullValueHandling.Include)]
    public string Error { get; set; }

		[DataMember(EmitDefaultValue = false, Name = "reason")]
    [JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
		public object Reason { get; set; }

		public ErrorResult()
			: this(DefaultErrorMessage, null) { }

		public ErrorResult(string message)
			: this(message, null) { }

		public ErrorResult(string message, object reason)
		{
			this.Error = message;
			this.Reason = reason;
		}

    public static ErrorResult WithReason(string reason)
    {
      return new ErrorResult { Reason = reason };
    }

    public static ErrorResult WithReason(string err, object reason)
    {
      return new ErrorResult(err, reason);
    }
  }
}
