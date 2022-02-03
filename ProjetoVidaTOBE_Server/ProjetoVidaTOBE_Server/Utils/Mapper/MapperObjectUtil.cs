using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVidaTOBE_Server.Utils.Mapper
{
    public static class MapperObjectUtil
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
                throw new Exception("Erro ao converter datarow em um objeto");
            }
        }

        public static T FromDataReader<T>(IDataReader reader, string nomeCampo) {
            try
            {
                if (reader[nomeCampo] == null || reader[nomeCampo] == DBNull.Value)
                {
                    return default(T);
                }
                return (T)Convert.ChangeType(reader[nomeCampo], typeof(T), CultureInfo.CurrentCulture);
            }
            catch (Exception)
            {
                throw new Exception("Erro ao converter reader em um objeto");
            }
        }
    }
}
