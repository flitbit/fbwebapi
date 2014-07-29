using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace FlitBit.WebApi
{
	[Serializable]
	[DataContract]
	public sealed class SuccessResult
	{
	  public static readonly string DefaultSuccessMessage = "Ok";

    [DataMember(EmitDefaultValue = true, Name = "success")]
    [JsonProperty("success", NullValueHandling = NullValueHandling.Include)]
    public string Success { get; set; }

    [DataMember(EmitDefaultValue = false, Name = "result")]
    [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
    public object Result { get; set; }

		public SuccessResult()
			: this(DefaultSuccessMessage, null) { }
    
		public SuccessResult(string message)
			: this(message, null) { }

		public SuccessResult(string message, object result)
		{
			this.Success = message;
			this.Result = result;
		}
	}
}
