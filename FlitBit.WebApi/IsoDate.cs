using System;
using System.Globalization;

namespace FlitBit.WebApi
{
  public static class IsoDate
  {
    static readonly string[] DateFormats =
    {
      // most-common javascript formats
      "yyyy-MM-ddTHH:mm:ss.fffZ",
      "yyyy-MM-ddTHH:mm:ss.fffzzz",
      // Basic formats
      "yyyyMMddTHHmmsszzz",
      "yyyyMMddTHHmmsszz",
      "yyyyMMddTHHmmssZ",
      // Extended formats
      "yyyy-MM-ddTHH:mm:sszzz",
      "yyyy-MM-ddTHH:mm:sszz",
      "yyyy-MM-ddTHH:mm:ssZ",
      // All of the above with reduced accuracy
      "yyyyMMddTHHmmzzz",
      "yyyyMMddTHHmmzz",
      "yyyyMMddTHHmmZ",
      "yyyy-MM-ddTHH:mmzzz",
      "yyyy-MM-ddTHH:mmzz",
      "yyyy-MM-ddTHH:mmZ",
      // Accuracy reduced to hours
      "yyyyMMddTHHzzz",
      "yyyyMMddTHHzz",
      "yyyyMMddTHHZ",
      "yyyy-MM-ddTHHzzz",
      "yyyy-MM-ddTHHzz",
      "yyyy-MM-ddTHHZ",
      // Plaino dates
      "yyyyMMddzzz",
      "yyyyMMddzz",
      "yyyyMMddZ",
      "yyyy-MM-ddzzz",
      "yyyy-MM-ddzz",
      "yyyy-MM-ddZ",
      // No timezone indicator
      "yyyyMMdd",
      "yyyy-MM-dd"
    };

    public static bool TryParse(string str, out DateTimeOffset timestamp)
    {
      // from http://stackoverflow.com/questions/3556144/how-to-create-a-net-datetime-from-iso-8601-format
      // modified for DateTimeOffset and Try...
      return DateTimeOffset.TryParseExact(str, DateFormats,
        CultureInfo.InvariantCulture, DateTimeStyles.None, out timestamp);
    }
  }
}