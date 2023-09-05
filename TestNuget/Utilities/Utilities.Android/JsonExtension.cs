using Org.Json;

namespace Utilities.Droid
{
    public static class JsonExtenstion
    {
        public static string GetStringExtra(this JSONObject response, string key)
        {
            return response.Has(key) && !response.IsNull(key) ? response.GetString(key) : null;
        }
    }
}
