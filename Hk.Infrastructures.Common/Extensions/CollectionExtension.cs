using System;
using System.Collections;


namespace Hk.Infrastructures.Common.Extensions
{
    public static class CollectionExtension
    {
        public static bool IsNotNull(this ICollection value)
        {
            bool result = value != null && value.Count > 0;
            return result;
        }
    }
}
