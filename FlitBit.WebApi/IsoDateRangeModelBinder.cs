using System;
using System.Globalization;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using FlitBit.WebApi.Models;

namespace FlitBit.WebApi
{
  public class IsoDateRangeModelBinder : IModelBinder
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
      var modelType = bindingContext.ModelType;
      DateRange range;
      if (modelType == typeof(DateRange) && 
        TryParseIsoDateRange(bindingContext, out range))
      {
        bindingContext.Model = range;
        return true;
      }
      return false;
    }

    bool TryParseIsoDateRange(ModelBindingContext bindingContext, out DateRange range)
    {
      // Expect: iso-date-string ',' iso-date-string
      var val = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
      if (val != null)
      {
        var str = val.RawValue as string;
        if (str != null)
        {
          var p = str.Split(',');
          DateTimeOffset start, end;
          if (p.Length == 2
              && IsoDate.TryParse(p[0], out start)
              && IsoDate.TryParse(p[1], out end)
              && end >= start)
          {
            range = new DateRange
            {
              Start = start,
              End = end
            };
            return true;
          }
          bindingContext.ModelState.AddModelError(bindingContext.ModelName,
          "URL parameter contains a malformed or unexpected value.");
          bindingContext.ModelState.AddModelError(bindingContext.ModelName,
          "Expected: iso-8601-datetime-string ',' iso-8601-datetime-string.");
          bindingContext.ModelState.AddModelError(bindingContext.ModelName,
          String.Format("Example: '{0}=2014-01-01Z,2014-02-01Z'.", bindingContext.ModelName));
          range = default(DateRange);
          return true;
        }
      }
      range = default(DateRange);
      return false;
    }

    
  }
}