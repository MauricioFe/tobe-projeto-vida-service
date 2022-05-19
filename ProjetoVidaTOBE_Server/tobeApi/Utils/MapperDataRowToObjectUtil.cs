using System;
using System.Data;
using System.Globalization;

namespace tobeApi.Utils
{
    public static class MapperDataRowToObjectUtil
    {
        public static T CreateItemFromRow<T>(this DataRow row, string nomeCampo)
        {
            try
            {
                if (!row.Table.Columns.Contains(nomeCampo)) return default(T);

                if ((row[nomeCampo] == null) || (row[nomeCampo] == DBNull.Value))
                    return default(T);
                if (row[nomeCampo].GetType() == typeof(TimeSpan))
                {
                    return (T)Convert.ChangeType(row[nomeCampo].ToString(), typeof(string), CultureInfo.CurrentCulture);
                }
                return (T)Convert.ChangeType(row[nomeCampo], typeof(T), CultureInfo.CurrentCulture);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
