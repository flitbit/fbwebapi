using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace FlitBit.WebApi
{
  public class BetterBoolModelBinder : IModelBinder
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
      bool value;
      var modelType = bindingContext.ModelType;
      if (modelType == typeof(bool))
      {
        if (!BindDateTimeOffset(bindingContext, out value))
        {
          return false;
        }
        bindingContext.Model = value;
        return true;
      }
      if (modelType == typeof(bool?))
      {
        if (!BindDateTimeOffset(bindingContext, out value))
        {
          return false;
        }
        bindingContext.Model = (bool?)value;
        return true;
      }
      return false;
    }

    bool BindDateTimeOffset(ModelBindingContext bindingContext, out bool value)
    {
      var val = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
      if (val == null)
      {
        value = default(bool);
        return false;
      }
      var sdate = val.RawValue as string;
      return TryParseBooleanEquivelants(sdate, out value);
    }

    public static bool TryParseBooleanEquivelants(string str, out bool value)
    {
      if (bool.TryParse(str, out value))
      {
        return true;
      }
      if (str.Length == 1)
      {
        if (str[0] == 't'
            || str[0] == 'T'
            || str[0] == 'y'
            || str[0] == 'Y'
            || str[0] == '1')
        {
          value = true;
          return true;
        }
        if (str[0] == 'f'
            || str[0] == 'F'
            || str[0] == 'n'
            || str[0] == 'N'
            || str[0] == '0')
        {
          value = false;
          return true;
        }
      }
      int num;
      if (int.TryParse(str, out num))
      {
        value = (num != 0);
        return true;
      }
      value = default(bool);
      return false;
    }
  }
}