using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace tobeApi.Data
{
    public interface IDataAccess
    {
        MySqlConnection Connection { get; }
        DataTable GetTable(string sql, params MySqlParameter[] parameters);
        DataRow GetRow(string sql, params MySqlParameter[] parameters);
        long GetCount(string sql, params MySqlParameter[] parameters);
        void ExecuteCommand(string sql, params MySqlParameter[] parameters);
    }
}
