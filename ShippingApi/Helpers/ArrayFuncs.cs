using System;

namespace ShippingApi
{
    class ArrayFuncs
    {
        internal static bool IsNullOrEmpty(Array pData)
        {
            return pData == null || pData.Length == 0;
        }
    }
}