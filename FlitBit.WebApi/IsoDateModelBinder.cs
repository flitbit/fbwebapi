using System;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using Newtonsoft.Json.Schema;

namespace FlitBit.WebApi
{
  public class IsoDateModelBinder : IModelBinder
  {
    /// <summary>
    /// Binds the model to a value by using the specified controller context and binding context.
    /// </summary>
    /// <returns>
    /// The bound value.
    /// </returns>
    /// <param name="actionContext">The action context.</param><param name="bindingContext">The binding context.</param>
    public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
    {
      DateTimeOffset isoDate;
      var modelType = bindingContext.ModelType;
      if (modelType == typeof(DateTime))
      {
        if (!BindDateTimeOffset(bindingContext, out isoDate))
        {
          return false;
        }
        bindingContext.Model = isoDate.ToUniversalTime().DateTime;
        return true;
      }
      if (modelType == typeof(DateTime?))
      {
        if (!BindDateTimeOffset(bindingContext, out isoDate))
        {
          return false;
        }
        bindingContext.Model = (DateTime?)isoDate.ToUniversalTime().DateTime;
        return true;
      }
      if (modelType == typeof(DateTimeOffset))
      {
        if (!BindDateTimeOffset(bindingContext, out isoDate))
        {
          return false;
        }
        bindingContext.Model = isoDate;
        return true;
      }
      if (modelType == typeof(DateTimeOffset?))
      {
        if (!BindDateTimeOffset(bindingContext, out isoDate))
        {
          return false;
        }
        bindingContext.Model = (DateTimeOffset?)isoDate;
        return true;
      }
      return false;
    }

    bool BindDateTimeOffset(ModelBindingContext bindingContext, out DateTimeOffset isoDate)
    {
      var val = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
      if (val != null)
      {
        var sdate = val.RawValue as string;
        if (IsoDate.TryParse(sdate, out isoDate))
        {
          return true;
        }
        bindingContext.ModelState.AddModelError(bindingContext.ModelName,
          "URL parameter contains a malformed or unexpected value.");
        bindingContext.ModelState.AddModelError(bindingContext.ModelName,
          "Expected: iso-8601-datetime-string.");
        bindingContext.ModelState.AddModelError(bindingContext.ModelName,
        String.Format("Example: '{0}=2014-01-01T07:30:00Z'.", bindingContext.ModelName));
        isoDate = default(DateTime);
        return true;
      }
      isoDate = default(DateTimeOffset);
      return false;
    }
  }
}