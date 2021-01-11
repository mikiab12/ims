using System;
using System.Collections.Generic;
using System.Text;

namespace ims.domain.Extensions
{
    public static class GuidExtensions
    {
        public static Guid ToGuid(this string s)
        {
            return Guid.Parse(s);
        }
    }

    public static class DataReaderExtentsion
    {
        public static int toInt(this Npgsql.NpgsqlDataReader data,int index)
        {
            return int.Parse(data[index].ToString());
        }

        public static DateTime toDate(this Npgsql.NpgsqlDataReader data, int index)
        {
            return DateTime.Parse(data[index].ToString());
        }
    }
}
