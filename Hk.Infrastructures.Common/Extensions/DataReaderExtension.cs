using System;
using System.Data;
namespace Hk.Infrastructures.Common.Extensions
{
    public static class DataReaderExtension
    {
        public static decimal ToSafeDecimal(this IDataReader value, string columnName)
        {
            decimal result = 0;
            if (columnName.IsNotEmpty() && value != null)
            {
                var index = value.GetOrdinal(columnName);
                if (!value.IsDBNull(index))
                {
                    decimal.TryParse(value[index].ToString(), out result);
                }
            }
            return result;
        }

        public static int ToSafeInt(this IDataReader value, string columnName)
        {
            int result = 0;
            if (columnName.IsNotEmpty() && value != null)
            {

                var index = value.GetOrdinal(columnName);
                if (!value.IsDBNull(index))
                {
                    int.TryParse(value[index].ToString(), out result);
                }

            }
            return result;
        }

        public static long ToSafeLong(this IDataReader value, string columnName)
        {
            long result = 0;
            if (columnName.IsNotEmpty() && value != null)
            {
                var index = value.GetOrdinal(columnName);
                if (!value.IsDBNull(index))
                {
                    long.TryParse(value[index].ToString(), out result);
                }

            }
            return result;
        }

        public static string ToSafeString(this IDataReader value, string columnName)
        {
            string result = string.Empty;
            if (columnName.IsNotEmpty() && value != null)
            {

                var index = value.GetOrdinal(columnName);
                if (!value.IsDBNull(index))
                {
                    result = value[index].ToString();
                }

            }
            return result;
        }

        public static DateTime ToSafeDateTime(this IDataReader value, string columnName)
        {
            DateTime result = DateTime.MinValue;
            if (columnName.IsNotEmpty() && value != null)
            {

                var index = value.GetOrdinal(columnName);
                if (!value.IsDBNull(index))
                {
                    DateTime.TryParse(value[index].ToString(), out result);
                }

            }
            return result;
        }

        public static float ToSafeFloat(this IDataReader value, string columnName)
        {
            float result = 0;
            if (columnName.IsNotEmpty() && value != null)
            {

                var index = value.GetOrdinal(columnName);
                if (!value.IsDBNull(index))
                {
                    float.TryParse(value[index].ToString(), out result);
                }

            }
            return result;
        }

        public static bool ToSafeBoolean(this IDataReader value, string columnName)
        {
            bool result = false;
            if (columnName.IsNotEmpty() && value != null)
            {

                var index = value.GetOrdinal(columnName);
                if (!value.IsDBNull(index))
                {
                    try
                    {
                        result = Convert.ToBoolean(value[index]);
                        //bool.TryParse(value[index].ToString(), out result);
                    }
                    catch { }
                }

            }
            return result;
        }

    }
}
