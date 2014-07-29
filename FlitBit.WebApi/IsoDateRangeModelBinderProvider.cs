using System;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using FlitBit.WebApi.Models;

namespace FlitBit.WebApi
{
  public class IsoDateRangeModelBinderProvider : ModelBinderProvider
  {
    readonly IsoDateRangeModelBinder _binder = new IsoDateRangeModelBinder();
    /// <summary>
    /// Finds a binder for the given type.
    /// </summary>
    /// <returns>
    /// A binder, which can attempt to bind this type. Or null if the binder knows statically that it will never be able to bind the type.
    /// </returns>
    /// <param name="configuration">A configuration object.</param><param name="modelType">The type of the model to bind against.</param>
    public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
    {
      if (modelType == typeof(DateRange))
      {
        return this._binder;
      }
      return null;
    }
  }
}