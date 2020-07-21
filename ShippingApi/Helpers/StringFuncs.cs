using System;

namespace ShippingApi
{
    class StringFuncs
    {
        internal static bool CaseInsensitiveCompare(string itemId, string p_AND_A_ITEMID, bool v)
        {
            return itemId.Equals(p_AND_A_ITEMID, StringComparison.OrdinalIgnoreCase);
        }

        internal static bool IsNullOrEmpty(string configuration, bool v)
        {
            return string.IsNullOrEmpty(configuration);
        }

        internal static void TryParse(string maxConfiguration, out int max, int v)
        {
            if (!Int32.TryParse(maxConfiguration, out max))
                max = v;
        }
    }
}