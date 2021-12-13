using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVidaTOBE_Server.Utils.Mapper
{
    public static class MapperDataRowObjectUtil
    {
        public static T CreateItemFromRow<T>(this DataRow row, string nomeCampo)
        {
            try
            {
                if (!row.Table.Columns.Contains(nomeCampo)) return default(T);

                if ((row[nomeCampo] == null) || (row[nomeCampo] == DBNull.Value))
                    return default(T);

                return (T)Convert.ChangeType(row[nomeCampo], typeof(T), CultureInfo.CurrentCulture);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
